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

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.support.annotation.NonNull;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;

public class EqualizerView extends android.view.View implements UIState.LockListener {
    private String text;
    private Paint textPaint;
    private Paint textPaint2;
private static String temp="hz";
    private static int j;

    private static final int BAND_COUNT = SpectrumData.BAND_COUNT;

    private final Paint mBarColor[] = new Paint[BAND_COUNT];
    private final Paint mBaseColor[] = new Paint[4];
    private final Paint mWhite = new Paint();

    private UIState mUiState;

    private float mWidth;
    private float mHeight;
    private float mBarWidth;
    private float mZeroLineY;

    public EqualizerView(Context context, AttributeSet attrs) {

        super(context, attrs);
        Log.i("main", "Inside Parenthasize Constructor of Equalizerview");

     //   text = j+temp;
        textPaint = new Paint();
        textPaint.setColor(Color.WHITE);
        textPaint.setTextSize(20);


        //textPaint.setStyle(Paint.Style.FILL);
        makeColors();
    }

    public void setUiState(UIState uiState) {

        Log.i("main", "Inside setUiState of Equalizerview");
        mUiState = uiState;
        invalidate();
    }

    private void makeColors() {
        Log.i("main", "Inside makeColor of Equalizerview");
        Bitmap bmp = BitmapFactory.decodeResource(getResources(), R.drawable.spectrum);
        for (int i = 0; i < BAND_COUNT; i++) {
            Paint p = new Paint();
            int x = (bmp.getWidth() - 1) * i / (BAND_COUNT - 1);
            p.setColor(bmp.getPixel(x, 0));
            mBarColor[i] = p;
        }

        int i = 0;
        for (int v : new int[]{100, 75, 55, 50}) {
            Paint p = new Paint();
            p.setColor(Color.rgb(v, v, v));
            mBaseColor[i++] = p;
        }

        mWhite.setColor(Color.WHITE);
    }

    @Override
    protected void onDraw(Canvas canvas) {
        Log.i("main", "Inside onDraw of Equalizerview");
        final Phonon ph = mUiState != null ? mUiState.getPhonon() : null;
        Log.i("main", "before isLoked of onDraw of Equalizerview");
        final boolean isLocked = mUiState != null ? mUiState.getLocked() : false;
        Log.i("main", "before for of onDraw of Equalizerview");

        //declaring value arrays for the values in the array

        String[] freqArray = {"100","125","160","200","230","250","315","400", "450","500","630","720","800","1000","1125","1250","1600","2000","2250","2500","3150","3500","4000","5000","5700","6300","8000","10000", "12500","14000","15000","16000" };

        for (int i = 0; i < BAND_COUNT; i++) {

            j = i;

            Log.i("main", i + "");
            float bar = ph != null ? ph.getBar(i) : .1f;
            float startX = mBarWidth * i;
            Log.i("main", mBarWidth + "");
            float stopX = startX + mBarWidth;
            Log.i("main", startX + "");
            Log.i("main", stopX + "");
            float startY = barToY(bar);
            Log.i("main", startY + "");
            float midY = startY + mBarWidth;
            Log.i("main", midY + "");


            // textPaint.setTextAlign(Paint.Align.CENTER);
            Paint.FontMetrics fm = new Paint.FontMetrics();
            textPaint.getFontMetrics(fm);


            if (bar > 0) {
                Log.i("main", "inside if of onDraw of Equalizerview");
                canvas.drawRect(startX, startY, stopX, midY, mBarColor[i]);
                Log.i("main", mBarColor[i] + "");

            }

            // Lower the brightness and contrast when locked.
            canvas.drawRect(startX, midY, stopX, mHeight,
                    mBaseColor[i % 2 + (isLocked ? 2 : 0)]);
//canvas.rotate(90);
            canvas.save();

      //      int fonthight = 2;
        //    text="hz";
          //     for (char c : text.toCharArray()) {

               canvas.rotate(-90, startX, startY + 90);
               //canvas.drawText(((i + 1) * 54) + "hz", startX - 80, startY + 105, textPaint);
                canvas.drawText((freqArray[i] + "HZ"), startX - 25, startY + 105, textPaint);

            //  startY+=fonthight;
            //}
            canvas.restore();
        }

        canvas.drawLine(0, mZeroLineY, mWidth, mZeroLineY, mWhite);
    }

    private float mLastX;
    private float mLastY;

    @Override
    public boolean onTouchEvent(@NonNull MotionEvent event) {
        Log.i("main", "Inside onTouchEvent of Equalizerview");
        if (mUiState.getLocked()) {
            Log.i("main", "Inside getLocked() onTouchEvent of Equalizerview");
            switch (event.getAction()) {
                case MotionEvent.ACTION_DOWN:
                    mUiState.setLockBusy(true);
                    Log.i("main", "Inside ACTION_DOWN OF getLocked() onTouchEvent of Equalizerview");
                    return true;
                case MotionEvent.ACTION_UP:
                    mUiState.setLockBusy(false);
                    Log.i("main", "Inside ACTION_UP OF getLocked() onTouchEvent of Equalizerview");
                    return true;
                case MotionEvent.ACTION_MOVE:
                    Log.i("main", "Inside ACTION_MOVE OF getLocked() onTouchEvent of Equalizerview");
                    return true;
            }
            return false;
        }

        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                mLastX = event.getX();
                mLastY = event.getY();
                Log.i("main", "Inside ACTION_DOWN OF onTouchEvent of Equalizerview");
                break;
            case MotionEvent.ACTION_UP:
                Log.i("main", "Inside ACTION_UP OF onTouchEvent of Equalizerview");
            case MotionEvent.ACTION_MOVE:
                Log.i("main", "Inside ACTION_MOVE OF onTouchEvent of Equalizerview");
                break;
            default:
                return false;
        }

        PhononMutable phm = mUiState.getPhononMutable();
        Log.i("main",event.getHistorySize()+ "");
        for (int i = 0; i < event.getHistorySize(); i++) {
            Log.i("main", "Before touchLine OF for of onTouchEvent of Equalizerview");
            touchLine(phm, event.getHistoricalX(i), event.getHistoricalY(i));
        }
        Log.i("main", "Before touchLine of onTouchEvent of Equalizerview");
        Log.i("main", event.getX()+" "+event.getY());
        touchLine(phm, event.getX(), event.getY());

        if (mUiState.sendIfDirty()) {
            Log.i("main", "Inside if OF onTouchEvent of Equalizerview");
            invalidate();
        }
        return true;
    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
        Log.i("main", "Inside onSizeChanged of Equalizerview");
        mWidth = getWidth();
        mHeight = getHeight();
        mBarWidth = mWidth / BAND_COUNT;
        mZeroLineY = mHeight * .9f;
    }

    private float yToBar(float y) {
        Log.i("main", "Inside ytobar of Equalizerview");
        float barHeight = 1f - (y / (mZeroLineY - mBarWidth));
        Log.i("main", barHeight+"");
        if (barHeight < 0) {
            Log.i("main", "Inside 1st if of ytobar of Equalizerview");
            return 0;
        }
        if (barHeight > 1) {
            Log.i("main", "Inside 2nd if of ytobar of Equalizerview");
            return 1;
        }

        return barHeight;
    }

    private float barToY(float barHeight) {

        Log.i("main", "Inside barToY of Equalizerview");
        return (1f - barHeight) * (mZeroLineY - mBarWidth);
    }

    private int getBarIndex(float x) {
        Log.i("main", "Inside getBarIndex of Equalizerview");
        int out = (int) (x / mBarWidth);
        if (out < 0) {
            out = 0;
        }
        if (out > BAND_COUNT - 1) {
            out = BAND_COUNT - 1;
        }
        return out;
    }

    // Starting bar?
    // Ending bar?
    // For each bar it exits:
    //   set Y to exit-Y.
    // For the ending point:
    //   set Y to final-Y.

    // Exits:
    //   Right:
    //     0->3: 0, 1, 2 [endpoint in 3]
    //   Left:
    //     3->0: 3, 2, 1 [endpoint in 0]

    private void touchLine(PhononMutable phm, float stopX, float stopY) {
        Log.i("main", "Inside touchLine of Equalizerview");
        float startX = mLastX;
        float startY = mLastY;
        mLastX = stopX;
        mLastY = stopY;
        int startBand = getBarIndex(startX);
        Log.i("main",  startBand+"");
        int stopBand = getBarIndex(stopX);
        Log.i("main", stopBand+"");
        int direction = stopBand > startBand ? 1 : -1;
        Log.i("main", direction+"");
        for (int i = startBand; i != stopBand; i += direction) {
            // Get the x-coordinate where we exited band i.
            Log.i("main",mBarWidth+ "");
            float exitX = i * mBarWidth;
            if (direction > 0) {
                Log.i("main", "Inside if of Equalizerview");
                exitX += mBarWidth;
                Log.i("main",exitX+ "");
            }

            // Get the Y value at exitX.
            float slope = (stopY - startY) / (stopX - startX);
            Log.i("main",slope+ "");
            float exitY = startY + slope * (exitX - startX);
            Log.i("main",exitY+ "");
            phm.setBar(i, yToBar(exitY));
        }
        Log.i("main", "Outside of for of touchLine of Equalizerview");
        // Set the Y endpoint.
        Log.i("main", stopBand+"");
        phm.setBar(stopBand, yToBar(stopY));
    }

    @Override
    public void onLockStateChange(LockEvent e) {
        // Only spend time redrawing if this is an on/off event.
        Log.i("main", "Inside onLockStateChange of Equalizerview");
        if (e == LockEvent.TOGGLE) {
            invalidate();
        }
    }
}
