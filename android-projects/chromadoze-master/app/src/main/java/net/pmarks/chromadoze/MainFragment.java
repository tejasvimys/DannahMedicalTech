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

import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Timer;
import java.util.TimerTask;

public class MainFragment extends Fragment implements NoiseService.PercentListener {
    private EqualizerView mEqualizer;
    private TextView mStateText;
    private ProgressBar mPercentBar;
    private UIState mUiState;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        Log.i("main", "Inside onCreateview of MainFragment");
        View v = inflater.inflate(R.layout.main_fragment, container, false);

        mEqualizer = (EqualizerView) v.findViewById(R.id.EqualizerView);
        Log.i("main", "before textview of MainFragment");
        mStateText = (TextView) v.findViewById(R.id.StateText);
        Log.i("main", "before progressbar of MainFragment");

        mPercentBar = (ProgressBar) v.findViewById(R.id.PercentBar);
        Log.i("main", "before return of MainFragment");
        return v;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        Log.i("main", "Inside onActivityCreated of MainFragment");
        super.onActivityCreated(savedInstanceState);
        mUiState = ((ChromaDoze) getActivity()).getUIState();
        mEqualizer.setUiState(mUiState);
    }

    @Override
    public void onResume() {
        Log.i("main", "Inside onResume of MainFragment");
        super.onResume();
        // Start receiving progress events.
        NoiseService.addPercentListener(this);
        mUiState.addLockListener(mEqualizer);

        ((ChromaDoze) getActivity()).setFragmentId(FragmentIndex.ID_CHROMA_DOZE);
        Log.i("main", "End of onResume of MainFragment");

    }

    @Override
    public void onPause() {
        Log.i("main", "Inside onPause of MainFragment");
        super.onPause();
        // Stop receiving progress events.
        NoiseService.removePercentListener(this);
        mUiState.removeLockListener(mEqualizer);
    }

    @Override
    public void onNoiseServicePercentChange(int percent) {
        Log.i("main", "Inside onNoiseServicePercentChange of MainFragment");
        int vis;
        if (percent < 0) {
            vis = View.INVISIBLE;
        } else if (percent < 100) {
            mPercentBar.setProgress(percent);
            vis = View.VISIBLE;
        } else {
            vis = View.INVISIBLE;
        }
        mPercentBar.setVisibility(vis);
        mStateText.setVisibility(vis);
    }
    
}
