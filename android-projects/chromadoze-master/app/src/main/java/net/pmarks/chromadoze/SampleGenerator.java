// Copyright (C) 2010  Paul Marks  http://www.pmarks.net/
//
// This file is part of Chroma Doze.
//
// Chroma Doze is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Chroma Doze is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Chroma Doze.  If not, see <http://www.gnu.org/licenses/>.

package net.pmarks.chromadoze;

import android.os.Process;
import android.util.Log;

import edu.emory.mathcs.jtransforms.dct.FloatDCT_1D;

class SampleGenerator {
    private final NoiseService mNoiseService;
    private final AudioParams mParams;
    private final SampleShuffler mSampleShuffler;
    private final Thread mWorkerThread;

    // Communication variables; must be synchronized.
    private boolean mStopping;
    private SpectrumData mPendingSpectrum;

    // Variables accessed from the thread only.
    private int mLastDctSize = -1;
    private FloatDCT_1D mDct;
    private final XORShiftRandom mRandom = new XORShiftRandom();  // Not thread safe.

    public SampleGenerator(NoiseService noiseService, AudioParams params,
                           SampleShuffler sampleShuffler) {

        Log.i("main", "Inside Constructor of SampleGenarator");
        mNoiseService = noiseService;
        mParams = params;
        mSampleShuffler = sampleShuffler;

        mWorkerThread = new Thread("SampleGeneratorThread") {
            @Override
            public void run() {
                try {
                    threadLoop();
                } catch (StopException e) {
                }
            }
        };
        mWorkerThread.start();
    }

    public void stopThread() {
        synchronized (this) {
            Log.i("main", "Inside stopThread of SampleGenarator");
            mStopping = true;
            notify();
        }
        try {
            mWorkerThread.join();
        } catch (InterruptedException e) {
        }
    }

    public synchronized void updateSpectrum(SpectrumData spectrum) {
        Log.i("main", "Inside updateSpectrum of SampleGenarator");
        mPendingSpectrum = spectrum;
        notify();
    }

    private void threadLoop() throws StopException {
        Log.i("main", "Inside ThreadLoop of SampleGenarator");
        Process.setThreadPriority(Process.THREAD_PRIORITY_BACKGROUND);
        Log.i("main", "After setThreadPriority ThreadLoop of SampleGenarator");
        // Chunk-making progress:
        final SampleGeneratorState state = new SampleGeneratorState();
        Log.i("main", "After SampleGenaratorState ThreadLoop of SampleGenarator");
        SpectrumData spectrum = null;

        while (true) {
            Log.i("main", "Inside while of ThreadLoop of SampleGenarator");
            // This does one of 3 things:
            // - Throw StopException if stopThread() was called.
            // - Check if a new spectrum is waiting.
            // - Block if there's no work to do.
            final boolean doWait = state.done();
            Log.i("main", "after done of ThreadLoop of SampleGenarator");
            final SpectrumData newSpectrum = popPendingSpectrum(doWait);
            Log.i("main", "after poppendingspectrum of ThreadLoop of SampleGenarator");
            if (newSpectrum != null && !newSpectrum.sameSpectrum(spectrum)) {
                Log.i("main", "Inside if of ThreadLoop of SampleGenarator");
                spectrum = newSpectrum;
                state.reset();
                mNoiseService.updatePercentAsync(state.getPercent());
            } else if (doWait) {
                Log.i("main", "Inside else if of ThreadLoop of SampleGenarator");
                // Nothing changed.  Keep waiting.
                continue;
            }

            // Generate the next chunk of sound.
            Log.i("main", "Before doIDCT of ThreadLoop of SampleGenarator");
            float[] dctData = doIDCT(state.getChunkSize(), spectrum);
            if (mSampleShuffler.handleChunk(dctData, state.getStage())) {
                // Not dropped.
                Log.i("main", "Inside if (mSampleShuffler.handleChunk(dctData, state.getStage())) of ThreadLoop of SampleGenarator");
                state.advance();
                mNoiseService.updatePercentAsync(state.getPercent());
            }

            if (state.done()) {
                // No chunks left; save RAM.
                Log.i("main", "Inside if (state.done()) of ThreadLoop of SampleGenarator");
                mDct = null;
                mLastDctSize = -1;
            }
            Log.i("main", "End of ThreadLoop of SampleGenarator");}


    }

    private synchronized SpectrumData popPendingSpectrum(boolean doWait)
            throws StopException {
        Log.i("main", "Inside popPendingSpectrum of SampleGenarator");
        if (doWait && !mStopping && mPendingSpectrum == null) {
            // Wait once.  The retry loop is in the caller.
            try {
                Log.i("main", "Inside try of popPendingSpectrum of SampleGenarator");
                wait();
                Log.i("main", "after wait of try of popPendingSpectrum of SampleGenarator");
            } catch (InterruptedException e) {
            }
        }
        if (mStopping) {
            Log.i("main", "Inside if (mStopping) of popPendingSpectrum of SampleGenarator");
            throw new StopException();
        }
        try {
            Log.i("main", "before returning of popPendingSpectrum of SampleGenarator");
            return mPendingSpectrum;
        } finally {
            mPendingSpectrum = null;
        }
    }

    private float[] doIDCT(int dctSize, SpectrumData spectrum) {
        Log.i("main", "Inside doIDCT of SampleGenarator");
        if (dctSize != mLastDctSize) {
            mDct = new FloatDCT_1D(dctSize);
            mLastDctSize = dctSize;
        }
        float[] dctData = new float[dctSize];

        spectrum.fill(dctData, mParams.SAMPLE_RATE);

        // Multiply by a block of white noise.
        for (int i = 0; i < dctSize; ) {
            long rand = mRandom.nextLong();
            for (int b = 0; b < 8; b++) {
                dctData[i++] *= (byte) rand / 128f;
                rand >>= 8;
            }
        }

        mDct.inverse(dctData, false);
        return dctData;
    }

    private static class StopException extends Exception {
    }
}
