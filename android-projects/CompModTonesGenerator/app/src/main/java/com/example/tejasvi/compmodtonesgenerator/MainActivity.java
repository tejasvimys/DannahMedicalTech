package com.example.tejasvi.compmodtonesgenerator;

import android.annotation.TargetApi;
import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Color;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.CountDownTimer;
import android.os.Environment;
import android.os.Handler;
import android.os.StrictMode;
import android.os.SystemClock;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.InputType;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Chronometer;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.ToggleButton;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.XML;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.security.NoSuchAlgorithmException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import java.util.List;
import java.util.Random;
import java.util.concurrent.ThreadLocalRandom;
import java.util.concurrent.TimeUnit;



public class MainActivity extends AppCompatActivity  {

    //Global Declarations
    //private static int BaseFrequency = 10000;
   // private int BaseFrequency = 4000;
    private  double Freq1 = 0;
    private  double Freq2 = 0;
    private  double Freq3 = 0;
    private  double Freq4 = 0;

    Thread thread= null;
    public boolean loopBreak = false;

    protected void generateFrequencies(int BaseFrequency)
    {
        try
        {
            Freq1 = Math.floor(BaseFrequency * 0.773 - 44.5);
            Freq2= Math.floor(BaseFrequency * 0.903 - 21.5);
            Freq3= Math.floor(BaseFrequency * 1.09 + 52);
            Freq4= Math.floor(BaseFrequency * 1.395 + 26.5);

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private String m_Text = "";
    private String patientID = "";
    databasehandler db = new databasehandler(this);
    DatabaseLogger dbLogger = new DatabaseLogger(this);
    private String macID=null;
    private String serialNo=null;
    public static String PACKAGE_NAME;
    private SQLiteDatabase mySQLiteDatabase;

    /**
     * alert message
     */

    public void showAlert(String dialogMessage){
        AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this).create();
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

    public String getValuefromXMLFile(){

        String login="";
        InputStream inputstream;

        try
        {
            String secStore = System.getenv("SECONDARY_STORAGE");
            String mPath=  secStore+"/tinnitustrio/cmt/Freq.xml";

            String primStore = System.getenv("EXTERNAL_STORAGE");
            String newPath=  secStore+"/tinnitustrio/cmt/Freq.xml";

            File homePath = new File(mPath);

            if(!homePath.isDirectory())
            {
                homePath= new File(newPath);
            }

            FileInputStream fileInputStream= new FileInputStream(homePath);

            XmlPullParserFactory  XmlFactoryObject = XmlPullParserFactory.newInstance();

            XmlPullParser myparser = XmlFactoryObject.newPullParser();

            myparser.setFeature(XmlPullParser.FEATURE_PROCESS_NAMESPACES, false);

            myparser.setInput(fileInputStream, null);

            parseXMLAndStoreIt(myparser);
        }

        catch (Exception ex)
        {

        }

        return login;
    }
    public volatile boolean parsingComplete = true;

    public int valuefrmxmlfile =0;
    public void parseXMLAndStoreIt(XmlPullParser myParser) {
        int event;
        String text = null;
        String a = null, b = null, c = null, d = null;
        try {
            event = myParser.getEventType();
            while (event != XmlPullParser.END_DOCUMENT) {
                String name = myParser.getName();
                switch (event) {
                    case XmlPullParser.START_TAG:
                        break;
                    case XmlPullParser.TEXT:
                        text = myParser.getText();
                        break;

                    case XmlPullParser.END_TAG:
                        if (name.equals("Value")) {
                            valuefrmxmlfile = Integer.parseInt(text);
                        }
                        break;
                }
                event = myParser.next();
            }
            parsingComplete = false;
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void DeActivateSystem()
    {
        try{

           String syncDB = db.ReturnPatientId();
            GetService serviceCall = new GetService();
            boolean returnServiceCall = serviceCall.DeactivateStatus(syncDB);

            if(returnServiceCall==false){

                File dir = new File(Environment.getExternalStorageDirectory()
                        + File.separator + "TinnitusTrio");

                if (dir.isDirectory())
                {
                    String[] children = dir.list();
                    for (int i = 0; i < children.length; i++)
                    {
                        new File(dir, children[i]).delete();
                    }

                    dir.delete();
                }

            }
        }

        catch (Exception ex)
        {

        }
    }



    //Export the log file to XML File for Sync

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

    public boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }

    @Override
    public void onStop()
    {
        super.onStop();
    }
    @Override
    public void onDestroy() {

        super.onDestroy();
       if( isNetworkAvailable()){
           ConvertSQLtoXML();
           DeActivateSystem();
      }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //if (android.os.Build.VERSION.SDK_INT > 9) {
       //     StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        //    StrictMode.setThreadPolicy(policy);
      //  }

        // Spinner element
        Spinner spinner = (Spinner) findViewById(R.id.spinner);

        // Spinner Drop down elements
        List<String> categories = new ArrayList<String>();
        categories.add("No Limit");
        categories.add("1");
        categories.add("3");
        categories.add("5");
        categories.add("10");
        categories.add("15");
        categories.add("20");
        categories.add("25");
        categories.add("30");
        categories.add("40");
        categories.add("50");
        categories.add("60");

        // Creating adapter for spinner
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, categories);

        // Drop down layout style - list view with radio button
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        // attaching data adapter to spinner
        spinner.setAdapter(dataAdapter);



        // generateFrequencies(getFreqVal);

        //code for security implementation
        //for internet to work in main activity
        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

        if( isNetworkAvailable()){
            //DeActivateSystem();
        }

        //to get the packagename
        PACKAGE_NAME = getApplicationContext().getPackageName();
        //check if local database exists or not
        boolean isDatabaseExists = db.checkFolderExists();


        boolean isTablesandDatabaseExists = db.checkDataBase();

        if (isDatabaseExists==false || isTablesandDatabaseExists==false){

		/*
	* Input dialog to enter the patient id
	* */
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("Enter Unique ID");
            builder.setCancelable(false);

            // Set up the input
            final EditText input = new EditText(this);
            // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
            input.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
            builder.setView(input);

            // Set up the buttons
            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    m_Text = input.getText().toString();
                    patientID = input.getText().toString();
                    //getting the data to insert to database
                    Log.d("Insert: ", "Inserting ..");

                    macID = getMACAddress();

                    if (!macID.isEmpty()) {

                        m_Text = m_Text + macID;
                    }

                    String hashedText = null;

                    try {
                        hashedText = Utilities.SHA1(m_Text);
                    } catch (NoSuchAlgorithmException e) {
                        e.printStackTrace();
                    } catch (UnsupportedEncodingException e) {
                        e.printStackTrace();
                    }

                    boolean isDatabaseExists = db.checkDataBase();

                    if (isDatabaseExists == false) {

                        GetService serviceCall = new GetService();

                        boolean returnServiceCall = serviceCall.InsertUserDetails(patientID, macID, serialNo, hashedText);

                        if (returnServiceCall == true) {
                            db.addPatient(new sqlitesync(hashedText, "AAEE123456", patientID, macID));
                        } else {

                            AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this).create();
                            alertDialog.setTitle("Alert");
                            alertDialog.setMessage("Invalid User");
                            alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                                    new DialogInterface.OnClickListener() {
                                        public void onClick(DialogInterface dialog, int which) {
                                            dialog.dismiss();
                                            finish();
                                            System.exit(0);
                                            Intent intent = new Intent(Intent.ACTION_DELETE);
                                            intent.setData(Uri.parse("package:" + PACKAGE_NAME));
                                            startActivity(intent);
                                        }
                                    });
                            alertDialog.show();
                        }
                    }

                }
            });
            builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.cancel();
                    finish();
                    System.exit(0);
                }
            });

            builder.show();

			/*
	* Input dialog to enter the patient id ends here
	* */
        }

        else{
            //db.deleteTable();
            int rowCnt = db.getProfilesCount();
            //showAlert(String.valueOf(rowCnt));

            if(rowCnt==0){
	/*
	* Input dialog to enter the patient id
	* */
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.setTitle("Enter Unique ID");
                builder.setCancelable(false);

                // Set up the input
                final EditText input = new EditText(this);
                // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
                input.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
                builder.setView(input);

                // Set up the buttons
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        m_Text = input.getText().toString();
                        patientID = input.getText().toString();


                        //getting the data to insert to database
                        Log.d("Insert: ", "Inserting ..");

                        macID = getMACAddress();

                        if (!macID.isEmpty()) {

                            m_Text = m_Text + macID;
                        }

                        // if(!serialNo.isEmpty()){
                        //   m_Text=m_Text+macID;
                        //}

                        String hashedText = null;

                        try {
                            hashedText = Utilities.SHA1(m_Text);
                        } catch (NoSuchAlgorithmException e) {
                            e.printStackTrace();
                        } catch (UnsupportedEncodingException e) {
                            e.printStackTrace();
                        }

                        boolean isDatabaseExists = db.checkDataBase();

                        if (isDatabaseExists == false) {

                            GetService serviceCall = new GetService();

                            boolean returnServiceCall = serviceCall.InsertUserDetails(patientID, macID, serialNo, hashedText);

                            if (returnServiceCall == true) {
                                db.addPatient(new sqlitesync(hashedText, serialNo, patientID, macID));
                            } else {
                                AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this).create();
                                alertDialog.setTitle("Alert");
                                alertDialog.setMessage("Invalid User");
                                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                                        new DialogInterface.OnClickListener() {
                                            public void onClick(DialogInterface dialog, int which) {
                                                dialog.dismiss();
                                                Intent intent = new Intent(Intent.ACTION_DELETE);
                                                intent.setData(Uri.parse("package:" + PACKAGE_NAME));
                                                startActivity(intent);
                                                finish();
                                                System.exit(0);
                                            }
                                        });
                                alertDialog.show();
                            }
                        }

                    }
                });
                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                        finish();
                        System.exit(0);
                    }
                });

                builder.show();
            }

            else{
                List<sqlitesync> contacts = db.getPatientDetails();

                String getPatientid=null;
                String hashValue = null;

                for (sqlitesync cn : contacts) {
                    //String log = "MAcId: "+cn.getmacID()+" ,Serial No: " + cn.getSerialno() + " ,Patient id: " + cn.getPatientID() + ",Hash Value id: " + cn.gethashValue();
                    // Writing Contacts to log
                    //Log.d("Name: ", log);

                    getPatientid = cn.getPatientID();
                    hashValue = cn.gethashValue();
                    macID= getMACAddress();
                    String hashedText=null;

                    m_Text = getPatientid+macID;

                    try {
                        hashedText = Utilities.SHA1(m_Text);
                    } catch (NoSuchAlgorithmException e) {
                        e.printStackTrace();
                    } catch (UnsupportedEncodingException e) {
                        e.printStackTrace();
                    }

                    if(!hashValue.equals(hashedText)) {
                        AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this).create();
                        alertDialog.setTitle("Alert");
                        alertDialog.setMessage("Invalid User");
                        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                                new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int which) {
                                        dialog.dismiss();
                                        Intent intent = new Intent(Intent.ACTION_DELETE);
                                        intent.setData(Uri.parse("package:" + PACKAGE_NAME));
                                        startActivity(intent);
                                        finish();
                                        System.exit(0);
                                    }
                                });
                        alertDialog.show();
                    }

                }
            }

        }

//code for security implementation ends here

        //spinner <code></code>
        //getValuefromXMLFile();

        //generateFrequencies(valuefrmxmlfile);


        //BUTTON CLICK EVENTS HERE
        //BUTTON DECLARATIONS
        final ToggleButton btnStopCMT=(ToggleButton)findViewById(R.id.btnStop);
        final Chronometer focus = (Chronometer)findViewById(R.id.chrnMtr);

        final ToggleButton btnplayRightSide = (ToggleButton)findViewById(R.id.rtBtn);
        final ToggleButton btnplayleftSide = (ToggleButton)findViewById(R.id.lftButtn);
        final ToggleButton btnplayBothSide = (ToggleButton)findViewById(R.id.bothBtn);
        final ToggleButton btnsetfrequency = (ToggleButton)findViewById(R.id.btnsetFrequency);

        final TextView txtfreq1 = (TextView)findViewById(R.id.txtfreq1);
        final TextView txtfreq2 = (TextView)findViewById(R.id.txtfreq2);
        final TextView txtfreq3 = (TextView)findViewById(R.id.txtfreq3);
        final TextView txtfreq4 = (TextView)findViewById(R.id.txtfreq4);

        txtfreq1.setVisibility(View.GONE);
        txtfreq2.setVisibility(View.GONE);
        txtfreq3.setVisibility(View.GONE);
        txtfreq4.setVisibility(View.GONE);

        btnplayRightSide.setText(null);
        btnplayRightSide.setTextOn(null);
        btnplayRightSide.setTextOff(null);

        btnplayleftSide.setText(null);
        btnplayleftSide.setTextOn(null);
        btnplayleftSide.setTextOff(null);

        btnplayBothSide.setText(null);
        btnplayBothSide.setTextOn(null);
        btnplayBothSide.setTextOff(null);

        btnStopCMT.setText(null);
        btnStopCMT.setTextOn(null);
        btnStopCMT.setTextOff(null);

        btnsetfrequency.setText(null);
        btnsetfrequency.setTextOn(null);
        btnsetfrequency.setTextOff(null);

        //disabling the stop button on page load.// BUG FIX
        btnStopCMT.setBackgroundResource((R.drawable.btns_ui10_mobile0004));
        btnStopCMT.setEnabled(false);


        final  EditText mEditText = (EditText) findViewById(R.id.txtFreqVal);
        mEditText.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View arg0, MotionEvent arg1) {
                mEditText.setCursorVisible(true);
                btnsetfrequency.setBackgroundResource((R.drawable.setbtns0002));
                return false;
            }
        });


        //SETTING THE FREQUENCY BUTTON CLICK
        btnsetfrequency.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View vw) {
                //getting the value of the textbox for generating the tone
                EditText mEdit = (EditText) findViewById(R.id.txtFreqVal);
                int getFreqVal = 0;

                if (mEdit.getText().toString().trim().matches("")) {

                    getFreqVal = 500;
                } else {

                    getFreqVal = Integer.parseInt(mEdit.getText().toString());

                }

                // alert(mEdit.getText().toString());
                generateFrequencies(getFreqVal);
                txtfreq1.setText(String.valueOf(Freq1));
                txtfreq2.setText(String.valueOf(Freq2));
                txtfreq3.setText(String.valueOf(Freq3));
                txtfreq4.setText(String.valueOf(Freq4));

                btnsetfrequency.setBackgroundResource((R.drawable.setbtns0001));

                txtfreq1.setVisibility(View.VISIBLE);
                txtfreq2.setVisibility(View.VISIBLE);
                txtfreq3.setVisibility(View.VISIBLE);
                txtfreq4.setVisibility(View.VISIBLE);

                RelativeLayout mainLayout;

                mainLayout = (RelativeLayout) findViewById(R.id.mainlayout);

                InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(mainLayout.getWindowToken(), 0);

                mEdit.setCursorVisible(false);
            }
        });

        //PLAY BUTTON CLICK
        btnplayBothSide.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View vw) {
                //resetting the chronometer
                focus.setBase(SystemClock.elapsedRealtime());
                focus.setFormat(null);
                focus.setFormat("%s");

                Spinner spinner = (Spinner)findViewById(R.id.spinner);
                String text = spinner.getSelectedItem().toString();

                if (text.trim() !="No Limit")
                {
                    new CountDownTimer(TimeUnit.MINUTES.toMillis(Integer.parseInt(text.trim())), 1000) {

                        public void onTick(long millisUntilFinished) {

                        }

                        public void onFinish() {


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

                            if( isNetworkAvailable()){
                                ConvertSQLtoXML();
                                DeActivateSystem();
                            }

                            System.exit(0);
                        }
                    }.start();
                }

                btnplayBothSide.setBackgroundResource((R.drawable.btns_ui10_mobile0006));
                btnplayleftSide.setBackgroundResource((R.drawable.btns_ui10_mobile0001));
                btnplayRightSide.setBackgroundResource((R.drawable.btns_ui10_mobile0007));

                btnStopCMT.setBackgroundResource((R.drawable.btns_ui10_mobile0003));
                btnStopCMT.setEnabled(true);

                btnplayBothSide.setEnabled(false);
                btnplayleftSide.setEnabled(false);
                btnplayRightSide.setEnabled(false);

                //setting the loopbreak flag to false
                loopBreak = false;


                //THREAD STARTS TO START THE TONE GENERATION
               thread = new Thread() {
                    @Override
                    public void run() {

                            generateTones();
                    }
                };

                focus.start();
                thread.start();
            }
        });

        //LEFT BUTTON CLICK
        btnplayleftSide.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View vw) {
                //resetting the chronometer
                focus.setBase(SystemClock.elapsedRealtime());
                focus.setFormat(null);
                focus.setFormat("%s");

                Spinner spinner = (Spinner)findViewById(R.id.spinner);
                String text = spinner.getSelectedItem().toString();

                if (text.trim() !="No Limit")
                {
                    new CountDownTimer(TimeUnit.MINUTES.toMillis(Integer.parseInt(text.trim())), 1000) {

                        public void onTick(long millisUntilFinished) {

                        }

                        public void onFinish() {


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

                            if( isNetworkAvailable()){
                                ConvertSQLtoXML();
                                DeActivateSystem();
                            }

                            System.exit(0);
                        }
                    }.start();
                }



                btnplayBothSide.setBackgroundResource((R.drawable.btns_ui10_mobile0005));
                btnplayleftSide.setBackgroundResource((R.drawable.btns_ui10_mobile0002));
                btnplayRightSide.setBackgroundResource((R.drawable.btns_ui10_mobile0007));

                btnStopCMT.setBackgroundResource((R.drawable.btns_ui10_mobile0003));
                btnStopCMT.setEnabled(true);

                btnplayBothSide.setEnabled(false);
                btnplayleftSide.setEnabled(false);
                btnplayRightSide.setEnabled(false);
                //setting the loopbreak flag to false
                loopBreak = false;

                //THREAD STARTS TO START THE TONE GENERATION
                thread = new Thread() {
                    @Override
                    public void run() {

                            generateleftTones();
                    }
                };

                focus.start();

                thread.start();
            }
        });

        //RIGHT BUTTON CLICK
        btnplayRightSide.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View vw) {
                //resetting the chronometer
                focus.setBase(SystemClock.elapsedRealtime());
                focus.setFormat(null);
                focus.setFormat("%s");

                Spinner spinner = (Spinner)findViewById(R.id.spinner);
                String text = spinner.getSelectedItem().toString();

                if (text.trim() !="No Limit")
                {
                    new CountDownTimer(TimeUnit.MINUTES.toMillis(Integer.parseInt(text.trim())), 1000) {

                        public void onTick(long millisUntilFinished) {

                        }

                        //LOGGING FOR CHRONOMETER
                        public void onFinish() {

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

                            if( isNetworkAvailable()){
                                ConvertSQLtoXML();
                                DeActivateSystem();
                            }

                            System.exit(0);
                        }
                    }.start();
                }


                btnplayBothSide.setBackgroundResource((R.drawable.btns_ui10_mobile0005));
                btnplayleftSide.setBackgroundResource((R.drawable.btns_ui10_mobile0001));
                btnplayRightSide.setBackgroundResource((R.drawable.btns_ui10_mobile0008));

                btnStopCMT.setBackgroundResource((R.drawable.btns_ui10_mobile0003));
                btnStopCMT.setEnabled(true);

                btnplayBothSide.setEnabled(false);
                btnplayleftSide.setEnabled(false);
                btnplayRightSide.setEnabled(false);

                //setting the loopbreak flag to false
                loopBreak = false;

                //THREAD STARTS TO START THE TONE GENERATION
                thread = new Thread() {
                    @Override
                    public void run() {

                        generaterightTones();
                    }
                };

                focus.start();

                thread.start();
            }
        });


        //stop button click
        btnStopCMT.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View vw) {
                btnplayBothSide.setBackgroundResource((R.drawable.btns_ui10_mobile0005));
                btnplayleftSide.setBackgroundResource((R.drawable.btns_ui10_mobile0001));
                btnplayRightSide.setBackgroundResource((R.drawable.btns_ui10_mobile0007));

                btnStopCMT.setBackgroundResource((R.drawable.btns_ui10_mobile0004));
                btnStopCMT.setEnabled(false);

                btnplayBothSide.setEnabled(true);
                btnplayleftSide.setEnabled(true);
                btnplayRightSide.setEnabled(true);

                focus.stop();
                thread.interrupt();

                //audio.destroyAudioTrack();
                loopBreak = true;

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
            }
        });


    }

    //test alert
    private void alert(String Text)
    {
        AlertDialog alertDialog = new AlertDialog.Builder(MainActivity.this).create();
        alertDialog.setTitle("Alert");
        alertDialog.setMessage(Text);
        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                    }
                });
        alertDialog.show();
    }


    AudioGenerator audio = new AudioGenerator(44100);

    double[] silence = audio.getSineWave(2000, 44100, 0);
    double[] longsilence = audio.getSineWave(45000, 44100, 0);

    int noteDuration = 7500;
    //int noteDuration = 100000;

    public void generateTones(){

        //getting the value of the textbox for generating the tone
        EditText mEdit   = (EditText)findViewById(R.id.txtFreqVal);
        int getFreqVal=0;

        if(mEdit.getText().toString().trim().matches("")) {

            getFreqVal=500;
        }

        else {

            getFreqVal = Integer.parseInt(mEdit.getText().toString());

        }

       // alert(mEdit.getText().toString());
        generateFrequencies(getFreqVal);

        //generateFrequencies(valuefrmxmlfile);

        double[] doNote = audio.getSineWave(noteDuration, 44100,Freq1);
        double[] faNote = audio.getSineWave(noteDuration, 44100, Freq2);
        double[] laNote = audio.getSineWave(noteDuration, 44100, Freq3);
        double[] la1Note = audio.getSineWave((int) (noteDuration*1), 44100, Freq4);


        audio.createPlayer();

        int[] input = new int[4];
        //input=[1,2,3,4];
        input[0] = 1;
        input[1] = 2;
        input[2] = 3;
        input[3] = 4;

        int j = 0;

        for (;;) {

            j++;

            shuffleArray(input);

            for (int i = 0; i < input.length; i++) {
                switch (input[i]) {
                    case 1:
                        audio.writeSound(doNote);
                        audio.writeSound(silence);
                        break;
                    case 2:
                        audio.writeSound(faNote);
                        audio.writeSound(silence);
                        break;
                    case 3:
                        audio.writeSound(laNote);
                        audio.writeSound(silence);
                        break;
                    case 4:
                        audio.writeSound(la1Note);
                        audio.writeSound(silence);
                        break;
                }
            }

            if (j==3){
                j=0;
                audio.writeSound(longsilence);
                //audio.destroyAudioTrack();
            }

            if (loopBreak==true) {
                audio.destroyAudioTrack();
                break;
            }
        }
    }

    AudioGeneratorLeft audioleft = new AudioGeneratorLeft(44100);

    double[] leftsilence = audioleft.getSineWave(2000, 44100, 0);
    double[] leftlongsilence = audioleft.getSineWave(45000, 44100, 0);


    public void generateleftTones(){

        EditText mEdit   = (EditText)findViewById(R.id.txtFreqVal);
        int getFreqVal=0;

        if(mEdit.getText().toString().trim().matches("")) {

            getFreqVal=500;
        }

        else {

            getFreqVal = Integer.parseInt(mEdit.getText().toString());
        }

            // alert(mEdit.getText().toString());
        generateFrequencies(getFreqVal);
        //generateFrequencies(valuefrmxmlfile);


        double[] doNote = audioleft.getSineWave(noteDuration, 44100,Freq1);
        double[] faNote = audioleft.getSineWave(noteDuration, 44100, Freq2);
        double[] laNote = audioleft.getSineWave(noteDuration, 44100, Freq3);
        double[] la1Note = audioleft.getSineWave((int) (noteDuration*1), 44100, Freq4);


        audioleft.createPlayer();

        int[] input = new int[4];
        //input=[1,2,3,4];
        input[0] = 1;
        input[1] = 2;
        input[2] = 3;
        input[3] = 4;

        int j = 0;

        for (;;) {

            j++;

            shuffleArray(input);

            for (int i = 0; i < input.length; i++) {
                switch (input[i]) {
                    case 1:
                        audioleft.writeSound(doNote);
                        audioleft.writeSound(leftsilence);
                        break;
                    case 2:
                        audioleft.writeSound(faNote);
                        audioleft.writeSound(leftsilence);
                        break;
                    case 3:
                        audioleft.writeSound(laNote);
                        audioleft.writeSound(leftsilence);
                        break;
                    case 4:
                        audioleft.writeSound(la1Note);
                        audioleft.writeSound(leftsilence);
                        break;
                }
            }

            if (j==3){
                j=0;
                audioleft.writeSound(leftlongsilence);
                //audio.destroyAudioTrack();
            }

            if (loopBreak==true) {
                audioleft.destroyAudioTrack();
                break;
            }
        }
    }

    AudioGeneratorRight audioright = new AudioGeneratorRight(44100);

    double[] rightsilence = audioright.getSineWave(2000, 44100, 0);
    double[] rightlongsilence = audioright.getSineWave(45000, 44100, 0);


    public void generaterightTones(){

        //getting the value of the textbox for generating the tone
        EditText mEdit   = (EditText)findViewById(R.id.txtFreqVal);
        int getFreqVal=0;

        if(mEdit.getText().toString().trim().matches("")) {

            getFreqVal=500;
        }

        else {
            getFreqVal = Integer.parseInt(mEdit.getText().toString());
        }


        // alert(mEdit.getText().toString());
        generateFrequencies(getFreqVal);
        //generateFrequencies(valuefrmxmlfile);

        double[] doNote = audioright.getSineWave(noteDuration, 44100,Freq1);
        double[] faNote = audioright.getSineWave(noteDuration, 44100, Freq2);
        double[] laNote = audioright.getSineWave(noteDuration, 44100, Freq3);
        double[] la1Note = audioright.getSineWave((int) (noteDuration*1), 44100, Freq4);


        audioright.createPlayer();

        int[] input = new int[4];
        //input=[1,2,3,4];
        input[0] = 1;
        input[1] = 2;
        input[2] = 3;
        input[3] = 4;

        int j = 0;

        for (;;) {

            j++;

            shuffleArray(input);

            for (int i = 0; i < input.length; i++) {
                switch (input[i]) {
                    case 1:
                        audioright.writeSound(doNote);
                        audioright.writeSound(rightsilence);
                        break;
                    case 2:
                        audioright.writeSound(faNote);
                        audioright.writeSound(rightsilence);
                        break;
                    case 3:
                        audioright.writeSound(laNote);
                        audioright.writeSound(rightsilence);
                        break;
                    case 4:
                        audioright.writeSound(la1Note);
                        audioright.writeSound(rightsilence);
                        break;
                }
            }

            if (j==3){
                j=0;
                audioright.writeSound(rightlongsilence);
                //audio.destroyAudioTrack();
            }

            if (loopBreak==true) {
                audioright.destroyAudioTrack();
                break;
            }
        }
    }


    // Implementing Fisher–Yates shuffle
    @TargetApi(Build.VERSION_CODES.LOLLIPOP)
    static void shuffleArray(int[] ar)
    {
        // If running on Java 6 or older, use `new Random()` on RHS here
        Random rnd = ThreadLocalRandom.current();
        for (int i = ar.length - 1; i > 0; i--)
        {
            int index = rnd.nextInt(i + 1);
            // Simple swap
            int a = ar[index];
            ar[index] = ar[i];
            ar[i] = a;
        }
    }
}
