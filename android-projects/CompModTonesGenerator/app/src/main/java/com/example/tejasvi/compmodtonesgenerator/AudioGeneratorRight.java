package com.example.tejasvi.compmodtonesgenerator;

import android.media.AudioFormat;
import android.media.AudioManager;
import android.media.AudioTrack;

/**
 * Created by tejasvi on 13/12/15.
 */
public class AudioGeneratorRight {

    private int sampleRate;
    private AudioTrack audioTrack;

    public AudioGeneratorRight(int sampleRate) {
        this.sampleRate = sampleRate;
    }

    public double[] getSineWave(int samples,int sampleRate,double frequencyOfTone){
        double[] sample = new double[samples];
       for (int i = 0; i < samples; i++) {
           sample[i] = Math.sin(2 * Math.PI * i / (sampleRate/frequencyOfTone));
        }

        return sample;
    }

    public byte[] get16BitPcm(double[] samples) {
        byte[] generatedSound = new byte[2 * samples.length];
        int idx = 0;
        int i = 0 ;

        int ramp = samples.length / 20 ;                                    // Amplitude ramp as a percent of sample count

        //int index = 0;

        //for (double sample : samples) {
        // scale to maximum amplitude
        //  short maxSample = (short) (((sample) * Short.MAX_VALUE));
        // in 16 bit wav PCM, first byte is the low order byte

        //generatedSound[index++] = (byte) (maxSample & 0x00ff);
        //generatedSound[index++] = (byte) ((maxSample & 0xff00) >>> 8);
        //}
        for (i = 0; i< ramp; ++i) {                                     // Ramp amplitude up (to avoid clicks)
            double dVal = samples[i];
            // Ramp up to maximum
            final short val = (short) ((dVal * 32767 * i/ramp));
            // in 16 bit wav PCM, first byte is the low order byte
            generatedSound[idx++] = (byte) (val & 0x00ff);
            generatedSound[idx++] = (byte) ((val & 0xff00) >>> 8);
        }


        for (i = i; i< samples.length - ramp; ++i) {                        // Max amplitude for most of the samples
            double dVal = samples[i];
            // scale to maximum amplitude
            final short val = (short) ((dVal * 32767));
            // in 16 bit wav PCM, first byte is the low order byte
            generatedSound[idx++] = (byte) (val & 0x00ff);
            generatedSound[idx++] = (byte) ((val & 0xff00) >>> 8);
        }

        for (i = i; i< samples.length; ++i) {                               // Ramp amplitude down
            double dVal = samples[i];
            // Ramp down to zero
            final short val = (short) ((dVal * 32767 * (samples.length-i)/ramp ));
            // in 16 bit wav PCM, first byte is the low order byte
            generatedSound[idx++] = (byte) (val & 0x00ff);
            generatedSound[idx++] = (byte) ((val & 0xff00) >>> 8);
        }

        return generatedSound;
    }

    public void createPlayer(){
        audioTrack = new AudioTrack(AudioManager.STREAM_MUSIC,
                sampleRate, AudioFormat.CHANNEL_CONFIGURATION_MONO,
                AudioFormat.ENCODING_PCM_16BIT, sampleRate,
                AudioTrack.MODE_STREAM);
        audioTrack.setStereoVolume(0, 100);
        audioTrack.play();
    }

    public void writeSound(double[] samples) {
        byte[] generatedSnd = get16BitPcm(samples);
        audioTrack.write(generatedSnd, 0, generatedSnd.length);
    }

    public void destroyAudioTrack() {
        audioTrack.stop();
        audioTrack.release();
    }
}
