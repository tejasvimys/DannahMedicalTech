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

import android.annotation.TargetApi;
import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.PorterDuff.Mode;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.os.StrictMode;
import android.os.SystemClock;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.widget.Toolbar;
import android.text.InputType;
import android.util.Log;
import android.util.TypedValue;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Chronometer;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.Toast;

import org.json.JSONArray;

import java.io.File;
import java.io.UnsupportedEncodingException;
import java.security.NoSuchAlgorithmException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;

public class ChromaDoze extends ActionBarActivity implements
        NoiseService.PercentListener, UIState.LockListener, OnItemSelectedListener {

    private static final int MENU_PLAY_STOP = 1;
    private static final int MENU_LOCK = 2;

    private UIState mUiState;
    private int mFragmentId = FragmentIndex.ID_CHROMA_DOZE;
public MySQLite mydb;
    public PhononMutable ph;
    private Drawable mToolbarIcon;
    private Spinner mNavSpinner;
    private boolean mServiceActive;
    public static SQLiteDatabase db;
    private static final int BAND_COUNT = SpectrumData.BAND_COUNT;
    float[] barvalue = new float[BAND_COUNT];


    private String m_Text = "";
    private String patientID = "";
    databasehandler dba = new databasehandler(this);
    private String macID=null;
    private String serialNo=null;
    public static String PACKAGE_NAME;
    DatabaseLogger dbLogger = new DatabaseLogger(this);


    /**
     * alert message
     */

    public void showAlert(String dialogMessage){
        AlertDialog alertDialog = new AlertDialog.Builder(ChromaDoze.this).create();
        alertDialog.setCancelable(false);
        alertDialog.setTitle("Alert");
        alertDialog.setMessage(dialogMessage);
        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                    }
                });
        alertDialog.show();
    }

    /*
    *
    * Get Serial number
    * */
    public void getSerialnumber(){
        String device_id = Build.SERIAL;
        serialNo= device_id;
        //showAlert(device_id);
    }

    public void ConvertSQLtoXML(){
        DatabaseExporter dl = new DatabaseExporter();
        String jsonValue = dl.getResults().toString();

        JSONArray obj = dl.getResults();
        GetService serviceCall = new GetService();

        String returnServiceCall = serviceCall.InsertLogDetails(obj);

        String withoutQuotes_line1 = returnServiceCall.replace("\"", "");

        if (withoutQuotes_line1.equals("true"))
        {
            dbLogger.deleteTable();
        }

    }

    /*
    *
    * Get Mac Address
    * */

    public String getMACAddress(){

        WifiManager manager = (WifiManager) getSystemService(Context.WIFI_SERVICE);
        WifiInfo info = manager.getConnectionInfo();
        String address = info.getMacAddress();
        return address;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {

                if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

////code for security implementation
        //for internet to work in main activity

        //to get the packagename
//        PACKAGE_NAME = getApplicationContext().getPackageName();
//
//        //check if local database exists or not
//        boolean isDatabaseExists = dba.checkFolderExists();
//
//        if (isDatabaseExists==false){
//
//		/*
//	* Input dialog to enter the patient id
//	* */
//            AlertDialog.Builder builder = new AlertDialog.Builder(this);
//            builder.setTitle("Enter Unique ID");
//            builder.setCancelable(false);
//
//            // Set up the input
//            final EditText input = new EditText(this);
//            // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
//            input.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
//            builder.setView(input);
//
//            // Set up the buttons
//            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
//                @Override
//                public void onClick(DialogInterface dialog, int which) {
//                    m_Text = input.getText().toString();
//                    patientID = input.getText().toString();
//
//
//                    //getting the data to insert to database
//                    Log.d("Insert: ", "Inserting ..");
//
//                    macID = getMACAddress();
//
//                    if (!macID.isEmpty()) {
//
//                        m_Text = m_Text + macID;
//                    }
//
//                    // if(!serialNo.isEmpty()){
//                    //   m_Text=m_Text+macID;
//                    //}
//
//                    String hashedText = null;
//
//                    try {
//                        hashedText = Utilities.SHA1(m_Text);
//                    } catch (NoSuchAlgorithmException e) {
//                        e.printStackTrace();
//                    } catch (UnsupportedEncodingException e) {
//                        e.printStackTrace();
//                    }
//
//                    boolean isDatabaseExists = dba.checkDataBase();
//
//                    if (isDatabaseExists == false) {
//
//                        GetService serviceCall = new GetService();
//
//                        boolean returnServiceCall = serviceCall.InsertUserDetails(patientID, macID, serialNo, hashedText);
//
//                        if (returnServiceCall == true) {
//                            dba.addPatient(new sqlitesync(hashedText, serialNo, patientID, macID));
//                        } else {
//
//                            AlertDialog alertDialog = new AlertDialog.Builder(ChromaDoze.this).create();
//                            alertDialog.setTitle("Alert");
//                            alertDialog.setMessage("Invalid User");
//                            alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
//                                    new DialogInterface.OnClickListener() {
//                                        public void onClick(DialogInterface dialog, int which) {
//                                            dialog.dismiss();
//                                            finish();
//                                            System.exit(0);
//                                            Intent intent = new Intent(Intent.ACTION_DELETE);
//                                            intent.setData(Uri.parse("package:" + PACKAGE_NAME));
//                                            startActivity(intent);
//                                        }
//                                    });
//                            alertDialog.show();
//                        }
//                    }
//
//                }
//            });
//            builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
//                @Override
//                public void onClick(DialogInterface dialog, int which) {
//                    dialog.cancel();
//                    finish();
//                    System.exit(0);
//                }
//            });
//
//            builder.show();
//
//			/*
//	* Input dialog to enter the patient id ends here
//	* */
//        }
//
//        else{
//            //db.deleteTable();
//            int rowCnt = dba.getProfilesCount();
//            //showAlert(String.valueOf(rowCnt));
//
//            if(rowCnt==0){
//	/*
//	* Input dialog to enter the patient id
//	* */
//                AlertDialog.Builder builder = new AlertDialog.Builder(this);
//                builder.setTitle("Enter Unique ID");
//                builder.setCancelable(false);
//
//                // Set up the input
//                final EditText input = new EditText(this);
//                // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
//                input.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
//                builder.setView(input);
//
//                // Set up the buttons
//                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
//                    @TargetApi(Build.VERSION_CODES.GINGERBREAD)
//                    @Override
//                    public void onClick(DialogInterface dialog, int which) {
//                        m_Text = input.getText().toString();
//                        patientID = input.getText().toString();
//
//
//                        //getting the data to insert to database
//                        Log.d("Insert: ", "Inserting ..");
//
//                        macID = getMACAddress();
//
//                        if (!macID.isEmpty()) {
//
//                            m_Text = m_Text + macID;
//                        }
//
//                        // if(!serialNo.isEmpty()){
//                        //   m_Text=m_Text+macID;
//                        //}
//
//                        String hashedText = null;
//
//                        try {
//                            hashedText = Utilities.SHA1(m_Text);
//                        } catch (NoSuchAlgorithmException e) {
//                            e.printStackTrace();
//                        } catch (UnsupportedEncodingException e) {
//                            e.printStackTrace();
//                        }
//
//                        boolean isDatabaseExists = dba.checkDataBase();
//
//                        if (isDatabaseExists == false) {
//
//                            GetService serviceCall = new GetService();
//
//                            boolean returnServiceCall = serviceCall.InsertUserDetails(patientID, macID, serialNo, hashedText);
//
//                            if (returnServiceCall == true) {
//                                dba.addPatient(new sqlitesync(hashedText, serialNo, patientID, macID));
//                            } else {
//                                AlertDialog alertDialog = new AlertDialog.Builder(ChromaDoze.this).create();
//                                alertDialog.setTitle("Alert");
//                                alertDialog.setMessage("Invalid User");
//                                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
//                                        new DialogInterface.OnClickListener() {
//                                            public void onClick(DialogInterface dialog, int which) {
//                                                dialog.dismiss();
//                                                Intent intent = new Intent(Intent.ACTION_DELETE);
//                                                intent.setData(Uri.parse("package:" + PACKAGE_NAME));
//                                                startActivity(intent);
//                                                finish();
//                                                System.exit(0);
//                                            }
//                                        });
//                                alertDialog.show();
//                            }
//                        }
//
//                    }
//                });
//                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
//                    @Override
//                    public void onClick(DialogInterface dialog, int which) {
//                        dialog.cancel();
//                        finish();
//                        System.exit(0);
//                    }
//                });
//
//                builder.show();
//            }
//
//            else{
//                List<sqlitesync> contacts = dba.getPatientDetails();
//
//                String getPatientid=null;
//                String hashValue = null;
//
//                for (sqlitesync cn : contacts) {
//                    //String log = "MAcId: "+cn.getmacID()+" ,Serial No: " + cn.getSerialno() + " ,Patient id: " + cn.getPatientID() + ",Hash Value id: " + cn.gethashValue();
//                    // Writing Contacts to log
//                    //Log.d("Name: ", log);
//
//                    getPatientid = cn.getPatientID();
//                    hashValue = cn.gethashValue();
//                    macID= getMACAddress();
//                    String hashedText=null;
//
//                    m_Text = getPatientid+macID;
//
//                    try {
//                        hashedText = Utilities.SHA1(m_Text);
//                    } catch (NoSuchAlgorithmException e) {
//                        e.printStackTrace();
//                    } catch (UnsupportedEncodingException e) {
//                        e.printStackTrace();
//                    }
//
//                    if(!hashValue.equals(hashedText)) {
//                        AlertDialog alertDialog = new AlertDialog.Builder(ChromaDoze.this).create();
//                        alertDialog.setTitle("Alert");
//                        alertDialog.setMessage("Invalid User");
//                        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
//                                new DialogInterface.OnClickListener() {
//                                    public void onClick(DialogInterface dialog, int which) {
//                                        dialog.dismiss();
//                                        Intent intent = new Intent(Intent.ACTION_DELETE);
//                                        intent.setData(Uri.parse("package:" + PACKAGE_NAME));
//                                        startActivity(intent);
//                                        finish();
//                                        System.exit(0);
//                                    }
//                                });
//                        alertDialog.show();
//                    }
//
//                }
//            }
//
//        }

//code for security implementation ends here



        Log.i("main", "Inside onCreate of Chromadoze");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);


        Log.i("main", "Before Object Creation of uistate of Chromadoze");
        mUiState = new UIState(getApplication());
        Log.i("main", "After Object Creation of uistate of Chromadoze");
        SharedPreferences pref = getPreferences(MODE_PRIVATE);
        Log.i("main", "Before loadState() call in chromadoze");
        mUiState.loadState(pref);
        Log.i("main", "before toolbarCreation of Chromadoze");
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        Log.i("main", "After setSupportActionBar of Chromadoze");
        ActionBar actionBar = getSupportActionBar();
        Log.i("main", "After ActionBar 0obj of Chromadoze");
        actionBar.setDisplayHomeAsUpEnabled(true);
        Log.i("main", "after setDisplayHomeAsUpEnabledof Chromadoze");
        actionBar.setTitle("");
        Log.i("main", "after setTitle of Chromadoze");
        mNavSpinner = (Spinner) findViewById(R.id.nav_spinner);
        Log.i("main", "before Array Adapter of Chromadoze");
        Log.i("main", "----------------------------------------");
        Log.i("main", actionBar.getThemedContext()+ " context");
        Log.i("main", R.layout.spinner_title+ " spinnertitle");
        Log.i("main",  FragmentIndex.getStrings(this)+ " fragmentindex");


        ArrayAdapter<String> adapter = new ArrayAdapter<>(
                actionBar.getThemedContext(), R.layout.spinner_title,
                FragmentIndex.getStrings(this));
        Log.i("main", "after Array Adapter obj creation of Chromadoze");
        adapter.setDropDownViewResource(R.layout.spinner_dropdown);

        Log.i("main", "After setDropDownViewResource of Chromadoze");
        mNavSpinner.setAdapter(adapter);
        Log.i("main", "After setAdapter of Chromadoze");
        mNavSpinner.setOnItemSelectedListener(this);
        Log.i("main", "After setOnItemSelectedListener of Chromadoze");
        // Created a scaled-down icon for the Toolbar.
        {
            Log.i("main", "Inside static block of Chromadoze");
            TypedValue tv = new TypedValue();
            getTheme().resolveAttribute(R.attr.actionBarSize, tv, true);
            int height = TypedValue.complexToDimensionPixelSize(tv.data, getResources().getDisplayMetrics());
            mToolbarIcon = getScaledImage(R.drawable.chromadoze_icon, height * 2 / 3);
        }

        // When this Activity is first created, set up the initial fragment.
        // After a save/restore, the framework will drop in the last-used
        // fragment automatically.

        if (savedInstanceState == null) {
            Log.i("main", "Before changeFragment of Chromadoze");
            changeFragment(new MainFragment(), false);
            Log.i("main", "After changeFragment of Chromadoze");
        }

try {
    db = openOrCreateDatabase("ChromaDoze_DB", Context.MODE_PRIVATE, null);
    db.execSQL("CREATE TABLE ChromaDoze_Bars(bar1 varchar(10),bar2 varchar(10),bar3 varchar(10),bar4 varchar(10),bar5 varchar(10),bar6 varchar(10),bar7 varchar(10),bar8 varchar(10),bar9 varchar(10),bar10 varchar(10),bar11 varchar(10),bar12 varchar(10),bar13 varchar(10),bar14 varchar(10),bar15 varchar(10),bar16 varchar(10),bar17 varchar(10),bar18 varchar(10),bar19 varchar(10),bar20 varchar(10),bar21 varchar(10),bar22 varchar(10),bar23 varchar(10),bar24 varchar(10),bar25 varchar(10),bar26 varchar(10),bar27 varchar(10),bar28 varchar(10),bar29 varchar(10),bar30 varchar(10),bar31 varchar(10),bar32 varchar(10));");
    Log.i("main", "table created------>");
    }
catch(Exception e) {
    Log.i("main", "" + e);
                    }
    }




    public void save()
    {
        Log.i("main", "inside save------>");

        barvalue=ph.temp;
for(int i=0;i<BAND_COUNT;i++)
{
    Log.i("main",barvalue[i]+"");
}
/*

        ContentValues values=new ContentValues();
        values.put("bar1",barvalue[0]);values.put("bar2",barvalue[1]);values.put("bar3",barvalue[2]);values.put("bar4",barvalue[3]);
        values.put("bar5",barvalue[4]);values.put("bar6",barvalue[5]);values.put("bar7",barvalue[6]);values.put("bar8",barvalue[7]);
        values.put("bar9",barvalue[8]);values.put("bar10",barvalue[9]); values.put("bar11",barvalue[10]);values.put("bar12",barvalue[11]);
        values.put("bar13",barvalue[12]); values.put("bar14",barvalue[13]); values.put("bar15",barvalue[14]); values.put("bar16",barvalue[15]);
        values.put("bar17",barvalue[16]); values.put("bar18",barvalue[17]); values.put("bar19",barvalue[18]); values.put("bar20",barvalue[19]);
        values.put("bar21",barvalue[20]); values.put("bar22",barvalue[21]); values.put("bar23",barvalue[22]); values.put("bar24",barvalue[23]);
        values.put("bar25",barvalue[24]); values.put("bar26",barvalue[25]); values.put("bar27",barvalue[26]); values.put("bar28",barvalue[27]);
        values.put("bar29",barvalue[28]); values.put("bar30",barvalue[29]); values.put("bar31",barvalue[30]);  values.put("bar32",barvalue[31]);

*/


       db.execSQL("INSERT INTO ChromaDoze_Bars VALUES(" + barvalue[0] + "," + barvalue[1] + "," + barvalue[2] + "," + barvalue[3] + "," + barvalue[4] + "," + barvalue[5] + "," + barvalue[6] + "," + barvalue[7] + "," + barvalue[8] + "," + barvalue[9] + "," + barvalue[10] + "," + barvalue[11] + "," + barvalue[12] + "," + barvalue[13] + "," + barvalue[14] + "," + barvalue[15] + "," + barvalue[16] + "," + barvalue[17] + "," + barvalue[18] + "," + barvalue[19] + "," + barvalue[20] + "," + barvalue[21] + "," + barvalue[22] + "," + barvalue[23] + "," + barvalue[24] + "," + barvalue[25] + "," + barvalue[26] + "," + barvalue[27] + "," + barvalue[28] + "," + barvalue[29] + "," + barvalue[30] + "," + barvalue[31] + ");");
        Log.i("main", "table values inserted------>");


        Log.i("main", "before select------>");

        Cursor cursor=db.rawQuery("SELECT * FROM ChromaDoze_Bars", null);
        Log.i("main", ""+cursor.getCount());
        if(cursor.getCount()==0)
        {
            Log.i("main", "No records found");
            return;
        }
int i=0;

        if (cursor.moveToFirst()) {
            do {
                HashMap<String, String> map = new HashMap<String, String>();
                Iterator iterator = map.keySet().iterator();
                map.put("bars1", cursor.getString(i));
                i++;
               } while (cursor.moveToNext());
        }





 /*
        int i=0;

        while(cur.moveToNext()) {


            Log.i("main", i + ":" + cur.getString(i)+"");

       i++;

        }



     ArrayList<HashMap<String, String>> usersList;
        usersList = new ArrayList<HashMap<String, String>>();
        String selectQuery = "SELECT  * FROM users";
        SQLiteDatabase database = this.getWritableDatabase();
        Cursor cursor = database.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                HashMap<String, String> map = new HashMap<String, String>();
                map.put("userId", cursor.getString(0));
                map.put("userName", cursor.getString(1));
                usersList.add(map);
            } while (cursor.moveToNext());
        }
        database.close();
*/
    }







    @Override
    public void onResume() {
        super.onResume();

        Log.i("main", "Inside onResume of Chromadoze");
             // Start receiving progress events.
        NoiseService.addPercentListener(this);
        Log.i("main", "After addPercentListener of onResume of Chromadoze");
        mUiState.addLockListener(this);
        Log.i("main", "After addLockListenerof onResume of Chromadoze");
        if (mUiState.getAutoPlay()) {
            Log.i("main", "Inside if of onResume of Chromadoze");
            mUiState.sendToService();
            Log.i("main", "End of onResume of Chromadoze");
        }
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.i("main", "Inside onPause of Chromadoze");
        // If the equalizer is silent, stop the service.
        // This makes it harder to leave running accidentally.
        if (mServiceActive && mUiState.getPhonon().isSilent()) {
            Log.i("main", "Before stopService of onPause of Chromadoze");
            mUiState.stopService();
            Log.i("main", "After stopService of onPause of Chromadoze");
        }

        SharedPreferences.Editor pref = getPreferences(MODE_PRIVATE).edit();
        pref.clear();
        mUiState.saveState(pref);
        pref.commit();

        // Stop receiving progress events.
        NoiseService.removePercentListener(this);
        mUiState.removeLockListener(this);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        Log.i("main", "Inside onCreateOptionsMenu of Chromadoze");
        MenuItem mi;

        mi = menu.add(0, MENU_PLAY_STOP, 0, getString(R.string.play_stop));
        MenuItemCompat
                .setShowAsAction(mi, MenuItemCompat.SHOW_AS_ACTION_ALWAYS);

        if (mFragmentId == FragmentIndex.ID_CHROMA_DOZE) {
            mi = menu.add(0, MENU_LOCK, 0, getString(R.string.lock_unlock));
            MenuItemCompat.setShowAsAction(mi,
                    MenuItemCompat.SHOW_AS_ACTION_ALWAYS);
        }

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        Log.i("main", "Inside onPrepareOptionsMenu of Chromadoze");
        menu.findItem(MENU_PLAY_STOP).setIcon(
                mServiceActive ? R.drawable.av_stop : R.drawable.av_play);
        MenuItem mi = menu.findItem(MENU_LOCK);
        if (mi != null) {
            mi.setIcon(getLockIcon());
        }
        Log.i("main", "before returnof onPrepareOptionMenu of Chromadoze");
        return super.onPrepareOptionsMenu(menu);

    }

    @Override
    public void onLockStateChange(LockEvent e) {
        Log.i("main", "Inside onLockStateChanged of Chromadoze");
        // Redraw the lock icon for both event types.
        supportInvalidateOptionsMenu();
    }

    // Get the lock icon which reflects the current action.
    private Drawable getLockIcon() {
        Log.i("main", "Inside getLockIcon of Chromadoze");
        Drawable d = getResources().getDrawable(
                mUiState.getLocked() ? R.drawable.action_unlock
                        : R.drawable.action_lock);
        if (mUiState.getLockBusy()) {
            d.setColorFilter(0xFFFF4444, Mode.SRC_IN);
        } else {
            d.clearColorFilter();
        }
        return d;
    }

    @Override
    public boolean onSupportNavigateUp() {
        // Rewind the back stack.
        Log.i("main", "Inside onSupportNavigateUp of Chromadoze");
        getSupportFragmentManager().popBackStack(null,
                FragmentManager.POP_BACK_STACK_INCLUSIVE);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        final Chronometer focus = (Chronometer)findViewById(R.id.chrnMtr);

        Log.i("main", "Inside onOptionItemSelected of Chromadoze");
        switch (item.getItemId()) {
            case MENU_PLAY_STOP:
                Log.i("main", "Inside MENU_PLAY_STOP onOptionItemSelected of Chromadoze");
                // Force the service into its expected state.
                if (!mServiceActive) {
                    mUiState.sendToService();

                    //implement logging chrono start here
                    focus.setBase(SystemClock.elapsedRealtime());
                    focus.setFormat(null);
                    focus.setFormat("%s");

                } else {
                    mUiState.stopService();

                    //implement logging chrono stop here  and log the entry

                    boolean isDatabaseExists = dbLogger.checkDataBase();

                    if (isDatabaseExists == true) {

                        DateFormat dateFormat = new SimpleDateFormat("hh:mm:ss a");
                        Date date = new Date();
                        String time=dateFormat.format(date);

                        DateFormat outputFormatter = new SimpleDateFormat("MM/dd/yyyy");
                        String output = outputFormatter.format(date);

                        Long elapsedTime = SystemClock.elapsedRealtime() - focus.getBase();

                        dbLogger.addPatient(new sqlitesync(output, time,elapsedTime.toString(), "1","1" ));

                        dbLogger.getLogRecords();

                    }

                    //System.exit(0);
                }
                Log.i("main", "end of onOptionItemSelected of Chromadoze");
                return true;
            case MENU_LOCK:
                Log.i("main", "Inside MENU_LOCK onOptionItemSelected of Chromadoze");
                mUiState.toggleLocked();
                supportInvalidateOptionsMenu();
                return true;
        }
        Log.i("main", "End of  onOptionItemSelected of Chromadoze");
        return false;
    }

    @Override
    public void onNoiseServicePercentChange(int percent) {
        Log.i("main", "Inside onNoiseServicePercentChange of Chromadoze");
        Log.i("main", mServiceActive+"");

        boolean newServiceActive = (percent >= 0);
        Log.i("main", newServiceActive+"");
        if (mServiceActive != newServiceActive) {
            mServiceActive = newServiceActive;

            // Redraw the "Play/Stop" button.
            supportInvalidateOptionsMenu();
        }
    }

    private void changeFragment(Fragment f, boolean allowBack) {
        Log.i("main", "Inside ChangeFragment of Chromadoze");
        FragmentManager fragmentManager = getSupportFragmentManager();

        // Prune the stack, so "back" always leads home.
        Log.i("main",fragmentManager.getBackStackEntryCount()+ "");
        if (fragmentManager.getBackStackEntryCount() > 0) {
            onSupportNavigateUp();
        }
        Log.i("main", "Before fragment Transaction of Chromadoze");
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.replace(R.id.fragment_container, f);
        Log.i("main", "After replace Transaction of Chromadoze");
        if (allowBack) {
            Log.i("main", "inside if of Chromadoze");
            transaction.addToBackStack(null);
            transaction
                    .setTransition(FragmentTransaction.TRANSIT_FRAGMENT_FADE);
        }
        Log.i("main", "Before Transaction commit of Chromadoze");
        transaction.commit();
    }

    // Fragments can read this >= onActivityCreated().
    public UIState getUIState() {
        Log.i("main", "Inside getUiState of Chromadoze");
        return mUiState;
    }

    // Each fragment calls this from onResume to tweak the ActionBar.
    public void setFragmentId(int id) {
        Log.i("main", "Inside setFragmentId of Chromadoze");
        mFragmentId = id;

        final boolean enableUp = id != FragmentIndex.ID_CHROMA_DOZE;
        Log.i("main", "Before getSupportActionBar of Chromadoze");
        ActionBar actionBar = getSupportActionBar();
        Log.i("main", "Before supportinvalidateOptionMenu of Chromadoze");
        supportInvalidateOptionsMenu();

        // Use the default left arrow, or a scaled-down Chroma Doze icon.
        Log.i("main", "Before setHomeAsUpindicator of Chromadoze");
        actionBar.setHomeAsUpIndicator(enableUp ? null : mToolbarIcon);

        // When we're on the main page, make the icon non-clickable.
        ImageButton navUp = findImageButton(findViewById(R.id.toolbar));
        Log.i("main", navUp+"");
        if (navUp != null) {

            Log.i("main", "Inside if of navUp of Chromadoze");
            navUp.setClickable(enableUp);
        }
        Log.i("main", "Before SetSelection of Chromadoze");
        mNavSpinner.setSelection(id);
        Log.i("main", "After SetSelection of Chromadoze");
    }

    // Search a View for the first ImageButton.  We use it to locate the
    // home/up button in a Toolbar.
    private static ImageButton findImageButton(View view) {
        Log.i("main", "Inside findImageButton of Chromadoze");
        if (view instanceof ImageButton) {
            Log.i("main", "Inside if of findImageButton of Chromadoze");
            return (ImageButton) view;
        } else if (view instanceof ViewGroup) {
            ViewGroup vg = (ViewGroup) view;
            for (int i = 0; i < vg.getChildCount(); i++) {
                Log.i("main",  vg.getChildCount()+"");
                Log.i("main",  vg.getChildAt(i)+"");
                ImageButton found = findImageButton(vg.getChildAt(i));
                if (found != null) {
                    return found;
                }
            }
        }
        return null;
    }

    // Handle nav_spinner selection.
    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int position,
                               long id) {
        Log.i("main", "Inside onItemSelected of Chromadoze");
        if (position == mFragmentId) {
            Log.i("main", mFragmentId+"");
            Log.i("main", position+"");
            return;
        }
        switch (position) {
            case FragmentIndex.ID_CHROMA_DOZE:
                Log.i("main", "Case chromadoze of Chromadoze");
                onSupportNavigateUp();
                return;
            case FragmentIndex.ID_OPTIONS:
                Log.i("main", "Case Options of Chromadoze");
                changeFragment(new OptionsFragment(), true);
                return;
            case FragmentIndex.ID_MEMORY:
                Log.i("main", "Case Memory of Chromadoze");
                changeFragment(new MemoryFragment(), true);
                return;
            case FragmentIndex.ID_ABOUT:
                Log.i("main", "Case About of Chromadoze");
                changeFragment(new AboutFragment(), true);
                return;
        }
    }

    @Override
    public void onNothingSelected(AdapterView<?> parent) {
        Log.i("main", "Inside onNothingSelected of Chromadoze");
    }

    private Drawable getScaledImage(int resource, int size) {
        Log.i("main", "Inside getScaledImage of Chromadoze");
        Bitmap b = ((BitmapDrawable) getResources().getDrawable(resource)).getBitmap();
        Bitmap bitmapResized = Bitmap.createScaledBitmap(b, size, size, true);
        return new BitmapDrawable(getResources(), bitmapResized);
    }

    @Override
    public void onDestroy(){
        super.onDestroy();
        if( isNetworkAvailable()){
           // ConvertSQLtoXML();
            //DeActivateSystem();
        }
    }


    public boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }

    public void DeActivateSystem()
    {
        try{

            String syncDB = dba.ReturnPatientId();
            GetService serviceCall = new GetService();
            boolean returnServiceCall = serviceCall.DeactivateStatus(syncDB);

            if(returnServiceCall==false){

                File dir = new File(Environment.getExternalStorageDirectory()
                        + File.separator + "TinnitusTrio");

                if (dir.isDirectory())
                {

                    dir.delete();
                   /* String[] children = dir.list();
                    for (int i = 0; i < children.length; i++)
                    {
                        new File(dir, children[i]).delete();
                    }*/
                }
            }
        }

        catch (Exception ex)
        {

        }
    }
}
