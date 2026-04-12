// Copyright (C) 2011  Paul Marks  http://www.pmarks.net/
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

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.widget.Chronometer;

import junit.framework.Assert;

import java.util.ArrayList;

public class UIState {

    private final Context mContext;

    private boolean mLocked = false;
    private boolean mLockBusy = false;
    private final ArrayList<LockListener> mLockListeners = new ArrayList<>();

    public final TrackedPosition mActivePos = new TrackedPosition();
    public PhononMutable mScratchPhonon;
    public ArrayList<Phonon> mSavedPhonons;



    public UIState(Context context) {
        Log.i("main", "Inside constructor of UIState");
        mContext = context;
    }

    private boolean mDirty = false;
    private boolean mAutoPlay;
    private boolean mIgnoreAudioFocus;
    private boolean mVolumeLimitEnabled;
    private int mVolumeLimit;
    public static final int MAX_VOLUME = 100;

    public void saveState(SharedPreferences.Editor pref) {

        Log.i("main", "Inside savestate of UIState");
        pref.putBoolean("locked", mLocked);
        pref.putBoolean("autoPlay", mAutoPlay);
        pref.putBoolean("ignoreAudioFocus", mIgnoreAudioFocus);
        pref.putInt("volumeLimit", getVolumeLimit());
        pref.putString("phononS", mScratchPhonon.toJSON());
        for (int i = 0; i < mSavedPhonons.size(); i++) {
            pref.putString("phonon" + i, mSavedPhonons.get(i).toJSON());
            mSavedPhonons.get(i);
        }
        pref.putInt("activePhonon", mActivePos.getPos());
    }
//importent
    public void loadState(SharedPreferences pref) {
        Log.i("main", "Inside loadState of UIState");
        mLocked = pref.getBoolean("locked", false);
        setAutoPlay(pref.getBoolean("autoPlay", false), false);

        setIgnoreAudioFocus(pref.getBoolean("ignoreAudioFocus", false));
        setVolumeLimit(pref.getInt("volumeLimit", MAX_VOLUME));
        setVolumeLimitEnabled(mVolumeLimit != MAX_VOLUME);

        // Load the scratch phonon.

        mScratchPhonon = new PhononMutable();
        Log.i("main",pref.getString("phononS", null)+"");
        if (mScratchPhonon.loadFromJSON(pref.getString("phononS", null))) {
        } else if (mScratchPhonon.loadFromLegacyPrefs(pref)) {

        } else {
                   mScratchPhonon.resetToDefault();
        }

        // Load the saved phonons.
        mSavedPhonons = new ArrayList<>();
         for (int i = 0; i < TrackedPosition.NOWHERE; i++) {
            PhononMutable phm = new PhononMutable();
            Log.i("main",i+"");

            if (!phm.loadFromJSON(pref.getString("phonon" + i, null))) {
                break;
            }
            mSavedPhonons.add(phm);
             Log.i("main", mSavedPhonons.add(phm) + "");
        }

        // Load the currently-selected phonon.
        final int active = pref.getInt("activePhonon", -1);
        mActivePos.setPos(-1 <= active && active < mSavedPhonons.size() ?
                active : -1);
    }

    public void addLockListener(LockListener l)
    {
        Log.i("main", "Inside addLockListener of UIState");
        mLockListeners.add(l);
    }

    public void removeLockListener(LockListener l) {
        Log.i("main", "Inside removelockListener of UIState");
        if (!mLockListeners.remove(l)) {
            throw new IllegalStateException();
        }
    }

    private void notifyLockListeners(LockListener.LockEvent e) {
        Log.i("main", "Inside notifyLockListener of UIState");
        for (LockListener l : mLockListeners) {
            l.onLockStateChange(e);
        }
    }

    public void sendToService() {
        Log.i("main", "Inside sendtoservice of UIState");
        Intent intent = new Intent(mContext, NoiseService.class);
        Log.i("main", "Before WriteIntent call of UIState");
        getPhonon().writeIntent(intent);
        Log.i("main", "Before first putExtra of UIState");
        intent.putExtra("volumeLimit", (float) getVolumeLimit() / MAX_VOLUME);
        Log.i("main", "Before Second putExtra of UIState");
        intent.putExtra("ignoreAudioFocus", mIgnoreAudioFocus);
        Log.i("main", "before context StartService of UIState");
       //it will call writeToparcel of spectrumData
        mContext.startService(intent);
        Log.i("main", "At End Of sendTo Service of UIState");
        mDirty = false;

        //implement chrono start here
    }

    public boolean sendIfDirty() {
        Log.i("main", "Inside sendifdirty of UIState");
        if (mDirty || (mActivePos.getPos() == -1 && mScratchPhonon.isDirty())) {
            Log.i("main", "Inside IF OF SENDIFDIRTY of UIState");
            sendToService();
            return true;
        }
        return false;
    }

    public void stopService() {

        Log.i("main", "Inside stopservice of UIState");
        NoiseService.sendStopIntent(mContext);

        //implement chrono stop here
    }

    public void toggleLocked() {
        Log.i("main", "Inside toggleLocked of UIState");
        mLocked = !mLocked;
        if (!mLocked) {
            mLockBusy = false;
        }
        notifyLockListeners(LockListener.LockEvent.TOGGLE);
    }

    public boolean getLocked() {

        Log.i("main", "Inside getLocked of UIState");
        return mLocked;
    }

    public void setLockBusy(boolean busy) {
        Log.i("main", "Inside setLockBusy of UIState");
        Assert.assertTrue(mLocked);
        if (mLockBusy != busy) {
            mLockBusy = busy;
            notifyLockListeners(LockListener.LockEvent.BUSY);
        }
    }

    public boolean getLockBusy() {
        Log.i("main", "Inside getLockBusy of UIState");
        return mLockBusy;
    }

    public Phonon getPhonon() {
        Log.i("main", "Inside getPhonOn of UIState");
        if (mActivePos.getPos() == -1) {
            return mScratchPhonon;
        }
        return mSavedPhonons.get(mActivePos.getPos());
    }

    public PhononMutable getPhononMutable() {
        Log.i("main", "Inside getPhonOnMutable of UIState");
        if (mActivePos.getPos() != -1) {
            mScratchPhonon = mSavedPhonons.get(mActivePos.getPos()).makeMutableCopy();
            mActivePos.setPos(-1);
        }
        return mScratchPhonon;
    }

    // -1 or 0..n
    public void setActivePhonon(int index) {
        Log.i("main", "Inside setActivityPhonOn of UIState");
        if (!(-1 <= index && index < mSavedPhonons.size())) {
            throw new ArrayIndexOutOfBoundsException();
        }
        mActivePos.setPos(index);
        sendToService();
    }

    // This interface is for receiving a callback when the state
    // of the Input Lock has changed.
    public interface LockListener {
        enum LockEvent {TOGGLE, BUSY}

        void onLockStateChange(LockEvent e);
    }

    public void setAutoPlay(boolean enabled, boolean fromUser) {
        Log.i("main", "Inside setAutoPlay of UIState");
        mAutoPlay = enabled;
        if (fromUser) {
            // Demonstrate AutoPlay by acting like the Play/Stop button.
            if (enabled) {
                sendToService();
            } else {
                stopService();
            }
        }
    }

    public boolean getAutoPlay() {

        Log.i("main", "Inside getAutoPlay of UIState");
        return mAutoPlay;
    }

    public void setIgnoreAudioFocus(boolean enabled) {
        Log.i("main", "Inside setIgnoreAudioFocus of UIState");
        if (mIgnoreAudioFocus == enabled) {
            return;
        }
        mIgnoreAudioFocus = enabled;
        mDirty = true;
    }

    public boolean getIgnoreAudioFocus() {
        Log.i("main", "Inside getIgnoreAudioFocus of UIState");
        return mIgnoreAudioFocus;
    }

    public void setVolumeLimitEnabled(boolean enabled) {
        Log.i("main", "Inside setVolumeLimitEnabled of UIState");
        if (mVolumeLimitEnabled == enabled) {
            return;
        }
        mVolumeLimitEnabled = enabled;
        if (mVolumeLimit != MAX_VOLUME) {
            mDirty = true;
        }
    }

    public void setVolumeLimit(int limit) {
        Log.i("main", "Inside setVolumeLimit of UIState");
        if (limit < 0) {
            limit = 0;
        } else if (limit > MAX_VOLUME) {
            limit = MAX_VOLUME;
        }
        if (mVolumeLimit == limit) {
            return;
        }
        mVolumeLimit = limit;
        if (mVolumeLimitEnabled) {
            mDirty = true;
        }
    }

    public boolean getVolumeLimitEnabled() {
        Log.i("main", "Inside getVolumeLimitEnabled of UIState");
        return mVolumeLimitEnabled;
    }

    public int getVolumeLimit()
    {
        Log.i("main", "Inside getVolumeLimit of UIState");
        return mVolumeLimitEnabled ? mVolumeLimit : MAX_VOLUME;
    }
}
