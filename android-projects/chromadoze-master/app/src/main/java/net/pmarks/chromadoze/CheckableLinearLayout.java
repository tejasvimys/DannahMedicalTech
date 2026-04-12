// Copyright (C) 2013  Paul Marks  http://www.pmarks.net/
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
import android.util.AttributeSet;
import android.util.Log;
import android.widget.Checkable;
import android.widget.LinearLayout;

public class CheckableLinearLayout extends LinearLayout implements Checkable {

    private Checkable mChild;

    public CheckableLinearLayout(Context context, AttributeSet attrs) {
        super(context, attrs);
        Log.i("main", "Inside Constructor of CheakableLinearLayout");

    }

    @Override
    protected void onFinishInflate() {
        super.onFinishInflate();
        Log.i("main", "Inside onFinishInflate of CheakableLinearLayout");
        for (int i = 0; i < getChildCount(); i++) {
            try {
                mChild = (Checkable) getChildAt(i);
                return;
            } catch (ClassCastException e) {
            }
        }
    }

    @Override
    public boolean isChecked() {
        Log.i("main", "Inside isCheked of CheakableLinearLayout");
        return mChild.isChecked();
    }

    @Override
    public void setChecked(boolean checked)
    {
        Log.i("main", "Inside setCheked of CheakableLinearLayout");
        mChild.setChecked(checked);
    }

    @Override
    public void toggle() {
        Log.i("main", "Inside toggel of CheakableLinearLayout");
        mChild.toggle();
    }

    @Override
    public void setEnabled(boolean enabled) {
        super.setEnabled(enabled);
        Log.i("main", "Inside setEnabled of CheakableLinearLayout");
        for (int i = 0; i < getChildCount(); i++) {
            getChildAt(i).setEnabled(enabled);
        }
    }
}
