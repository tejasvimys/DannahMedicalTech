package com.androidhive.musicplayer;

import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.security.NoSuchAlgorithmException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Random;
import java.util.concurrent.TimeUnit;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.AssetManager;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.Environment;
import android.os.Handler;
import android.os.StrictMode;
import android.os.SystemClock;
import android.telephony.TelephonyManager;
import android.text.InputType;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.os.Build;
import com.androidhive.musicplayer.Utilities;

import org.json.JSONArray;

public class AndroidBuildingMusicPlayerActivity extends Activity implements OnCompletionListener, SeekBar.OnSeekBarChangeListener {

	private ImageButton btnPlay;
	private ImageButton btnForward;
	private ImageButton btnBackward;
	private ImageButton btnNext;
	private ImageButton btnPrevious;
	private ImageButton btnPlaylist;
	private ImageButton btnRepeat;
	private ImageButton btnShuffle;
	private SeekBar songProgressBar;
	private TextView songTitleLabel;
	private TextView songCurrentDurationLabel;
	private TextView songTotalDurationLabel;
	// Media Player
	private  MediaPlayer mp;
	// Handler to update UI timer, progress bar etc,.
	private Handler mHandler = new Handler();;
	private SongsManager songManager;
	private Utilities utils;
	private int seekForwardTime = 5000; // 5000 milliseconds
	private int seekBackwardTime = 5000; // 5000 milliseconds
	private int currentSongIndex = 0; 
	private boolean isShuffle = false;
	private boolean isRepeat = false;
	private String m_Text = "";
	private String patientID = "";
	private ArrayList<HashMap<String, String>> songsList = new ArrayList<HashMap<String, String>>();
    databasehandler db = new databasehandler(this);
	private String macID=null;
	private String serialNo=null;
	public static String PACKAGE_NAME;
	DatabaseLogger dbLogger = new DatabaseLogger(this);



	/**
     * alert message
     */

    public void showAlert(String dialogMessage){
        AlertDialog alertDialog = new AlertDialog.Builder(AndroidBuildingMusicPlayerActivity.this).create();
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


	@Override
	public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
		setContentView(R.layout.player);


		// Spinner element
		Spinner spinner = (Spinner) findViewById(R.id.spinner2);

		// Spinner click listener
		// spinner.setOnItemSelectedListener((AdapterView.OnItemSelectedListener) this);

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


////code for security implementation
		//for internet to work in main activity
		if (android.os.Build.VERSION.SDK_INT > 9) {
			StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
			StrictMode.setThreadPolicy(policy);
		}

		//to get the packagename
		PACKAGE_NAME = getApplicationContext().getPackageName();

		//check if local database exists or not
		boolean isDatabaseExists = db.checkFolderExists();

if (isDatabaseExists==false){

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

					AlertDialog alertDialog = new AlertDialog.Builder(AndroidBuildingMusicPlayerActivity.this).create();
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
						AlertDialog alertDialog = new AlertDialog.Builder(AndroidBuildingMusicPlayerActivity.this).create();
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
				AlertDialog alertDialog = new AlertDialog.Builder(AndroidBuildingMusicPlayerActivity.this).create();
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

        // All player buttons
		btnPlay = (ImageButton) findViewById(R.id.btnPlay);
		btnForward = (ImageButton) findViewById(R.id.btnForward);
		btnBackward = (ImageButton) findViewById(R.id.btnBackward);
		btnNext = (ImageButton) findViewById(R.id.btnNext);
		btnPrevious = (ImageButton) findViewById(R.id.btnPrevious);
		btnPlaylist = (ImageButton) findViewById(R.id.btnPlaylist);
		btnRepeat = (ImageButton) findViewById(R.id.btnRepeat);
		btnShuffle = (ImageButton) findViewById(R.id.btnShuffle);
		songProgressBar = (SeekBar) findViewById(R.id.songProgressBar);
		songTitleLabel = (TextView) findViewById(R.id.songTitle);
		songCurrentDurationLabel = (TextView) findViewById(R.id.songCurrentDurationLabel);
		songTotalDurationLabel = (TextView) findViewById(R.id.songTotalDurationLabel);
		
		// Mediaplayer
		mp = new MediaPlayer();
		songManager = new SongsManager();
		utils = new Utilities();
		
		// Listeners
		songProgressBar.setOnSeekBarChangeListener(this); // Important
		mp.setOnCompletionListener(this); // Important
		
		// Getting all songs list
		songsList = songManager.getPlayList();
		
		// By default play first song
		//playSong(0);
				
		/**
		 * Play button click event
		 * plays a song and changes button to pause image
		 * pauses a song and changes button to play image
		 * */
		btnPlay.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// check for already playing
				if(mp.isPlaying()){
					if(mp!=null){
						mp.pause();
						// Changing button image to play button
						btnPlay.setImageResource(R.drawable.btn_play);
					}
				}else{
					// Resume song
					if(mp!=null){
						mp.start();
						// Changing button image to pause button
						btnPlay.setImageResource(R.drawable.btn_pause);
					}
				}

				//TIMER FUNCTION
				Spinner spinner = (Spinner)findViewById(R.id.spinner2);
				String text = spinner.getSelectedItem().toString();

				if (text.trim() !="No Limit")
				{
					new CountDownTimer(TimeUnit.MINUTES.toMillis(Integer.parseInt(text.trim())), 1000) {

						public void onTick(long millisUntilFinished) {

						}

						public void onFinish() {

							if( isNetworkAvailable()){
								ConvertSQLtoXML();
								DeActivateSystem();
							}

							System.exit(0);
						}
					}.start();
				}
				
			}
		});
		
		/**
		 * Forward button click event
		 * Forwards song specified seconds
		 * */
		btnForward.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// get current song position				
				int currentPosition = mp.getCurrentPosition();
				// check if seekForward time is lesser than song duration
				if(currentPosition + seekForwardTime <= mp.getDuration()){
					// forward song
					mp.seekTo(currentPosition + seekForwardTime);
				}else{
					// forward to end position
					mp.seekTo(mp.getDuration());
				}
			}
		});
		
		/**
		 * Backward button click event
		 * Backward song to specified seconds
		 * */
		btnBackward.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// get current song position				
				int currentPosition = mp.getCurrentPosition();
				// check if seekBackward time is greater than 0 sec
				if(currentPosition - seekBackwardTime >= 0){
					// forward song
					mp.seekTo(currentPosition - seekBackwardTime);
				}else{
					// backward to starting position
					mp.seekTo(0);
				}
				
			}
		});
		
		/**
		 * Next button click event
		 * Plays next song by taking currentSongIndex + 1
		 * */
		btnNext.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// check if next song is there or not
				if(currentSongIndex < (songsList.size() - 1)){
					playSong(currentSongIndex + 1);
					currentSongIndex = currentSongIndex + 1;
				}else{
					// play first song
					playSong(0);
					currentSongIndex = 0;
				}
				
			}
		});
		
		/**
		 * Back button click event
		 * Plays previous song by currentSongIndex - 1
		 * */
		btnPrevious.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				if(currentSongIndex > 0){
					playSong(currentSongIndex - 1);
					currentSongIndex = currentSongIndex - 1;
				}else{
					// play last song
					playSong(songsList.size() - 1);
					currentSongIndex = songsList.size() - 1;
				}
				
			}
		});
		
		/**
		 * Button Click event for Repeat button
		 * Enables repeat flag to true
		 * */
		btnRepeat.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				if(isRepeat){
					isRepeat = false;
					Toast.makeText(getApplicationContext(), "Repeat is OFF", Toast.LENGTH_SHORT).show();
					btnRepeat.setImageResource(R.drawable.btn_repeat);
				}else{
					// make repeat to true
					isRepeat = true;
					Toast.makeText(getApplicationContext(), "Repeat is ON", Toast.LENGTH_SHORT).show();
					// make shuffle to false
					isShuffle = false;
					btnRepeat.setImageResource(R.drawable.btn_repeat_focused);
					btnShuffle.setImageResource(R.drawable.btn_shuffle);
				}	
			}
		});
		
		/**
		 * Button Click event for Shuffle button
		 * Enables shuffle flag to true
		 * */
		btnShuffle.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				if(isShuffle){
					isShuffle = false;
					Toast.makeText(getApplicationContext(), "Shuffle is OFF", Toast.LENGTH_SHORT).show();
					btnShuffle.setImageResource(R.drawable.btn_shuffle);
				}else{
					// make repeat to true
					isShuffle= true;
					Toast.makeText(getApplicationContext(), "Shuffle is ON", Toast.LENGTH_SHORT).show();
					// make shuffle to false
					isRepeat = false;
					btnShuffle.setImageResource(R.drawable.btn_shuffle_focused);
					btnRepeat.setImageResource(R.drawable.btn_repeat);
				}	
			}
		});
		
		/**
		 * Button Click event for Play list click event
		 * Launches list activity which displays list of songs
		 * */
		btnPlaylist.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				Intent i = new Intent(getApplicationContext(), PlayListActivity.class);
				startActivityForResult(i, 100);			
			}
		});
		
	}

	/**
	 * Receiving song index from playlist view
	 * and play the song
	 * */
	@Override
    protected void onActivityResult(int requestCode,
                                     int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode == 100){
         	 currentSongIndex = data.getExtras().getInt("songIndex");
         	 // play selected song
             playSong(currentSongIndex);
        }
 
    }
	
	/**
	 * Function to play a song
	 * @param songIndex - index of song
	 * */
	public void  playSong(int songIndex){
		// Play song
		try {
        	mp.reset();
			mp.setDataSource(songsList.get(songIndex).get("songPath"));
			mp.prepare();
			mp.start();
			// Displaying Song title
			String songTitle = songsList.get(songIndex).get("songTitle");
        	songTitleLabel.setText(songTitle);
			
        	// Changing Button Image to pause image
			btnPlay.setImageResource(R.drawable.btn_pause);
			
			// set Progress bar values
			songProgressBar.setProgress(0);
			songProgressBar.setMax(100);
			
			// Updating progress bar
			updateProgressBar();

			boolean isDatabaseExists = dbLogger.checkDataBase();

			if (isDatabaseExists == true) {

				DateFormat dateFormat = new SimpleDateFormat("hh:mm:ss a");
				Date date = new Date();
				String time=dateFormat.format(date);

				DateFormat outputFormatter = new SimpleDateFormat("MM/dd/yyyy");
				String output = outputFormatter.format(date);

				long totalDuration=mp.getDuration();

				String totDurn = String.valueOf(totalDuration);

				dbLogger.addPatient(new sqlitesync(output, time, songTitle, totDurn, "1", "1"));

				dbLogger.getLogRecords();

			}

		} catch (IllegalArgumentException e) {
			e.printStackTrace();
		} catch (IllegalStateException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Update timer on seekbar
	 * */
	public void updateProgressBar() {
        mHandler.postDelayed(mUpdateTimeTask, 100);        
    }	
	
	/**
	 * Background Runnable thread
	 * */
	private Runnable mUpdateTimeTask = new Runnable() {
		   public void run() {
			   long totalDuration = mp.getDuration();
			   long currentDuration = mp.getCurrentPosition();
			  
			   // Displaying Total Duration time
			   songTotalDurationLabel.setText(""+utils.milliSecondsToTimer(totalDuration));
			   // Displaying time completed playing
			   songCurrentDurationLabel.setText(""+utils.milliSecondsToTimer(currentDuration));
			   
			   // Updating progress bar
			   int progress = (int)(utils.getProgressPercentage(currentDuration, totalDuration));
			   //Log.d("Progress", ""+progress);
			   songProgressBar.setProgress(progress);
			   
			   // Running this thread after 100 milliseconds
		       mHandler.postDelayed(this, 100);
		   }
		};
		
	/**
	 * 
	 * */
	@Override
	public void onProgressChanged(SeekBar seekBar, int progress, boolean fromTouch) {
		
	}

	/**
	 * When user starts moving the progress handler
	 * */
	@Override
	public void onStartTrackingTouch(SeekBar seekBar) {
		// remove message Handler from updating progress bar
		mHandler.removeCallbacks(mUpdateTimeTask);
    }
	
	/**
	 * When user stops moving the progress hanlder
	 * */
	@Override
    public void onStopTrackingTouch(SeekBar seekBar) {
		mHandler.removeCallbacks(mUpdateTimeTask);
		int totalDuration = mp.getDuration();
		int currentPosition = utils.progressToTimer(seekBar.getProgress(), totalDuration);
		
		// forward or backward to certain seconds
		mp.seekTo(currentPosition);
		
		// update timer progress again
		updateProgressBar();
    }

	/**
	 * On Song Playing completed
	 * if repeat is ON play same song again
	 * if shuffle is ON play random song
	 * */
	@Override
	public void onCompletion(MediaPlayer arg0) {
		
		// check for repeat is ON or OFF
		if(isRepeat){
			// repeat is on play same song again
			playSong(currentSongIndex);
		} else if(isShuffle){
			// shuffle is on - play a random song
			Random rand = new Random();
			currentSongIndex = rand.nextInt((songsList.size() - 1) - 0 + 1) + 0;
			playSong(currentSongIndex);
		} else{
			// no repeat or shuffle ON - play next song
			if(currentSongIndex < (songsList.size() - 1)){
				playSong(currentSongIndex + 1);
				currentSongIndex = currentSongIndex + 1;
			}else{
				// play first song
				playSong(0);
				currentSongIndex = 0;
			}
		}
	}
	
	@Override
	 public void onDestroy(){
		mHandler.removeCallbacks(mUpdateTimeTask);
		super.onDestroy();
		if( isNetworkAvailable()){
			//ConvertSQLtoXML();
			//DeActivateSystem();
		}
	    mp.release();
	 }

	public boolean isNetworkAvailable() {
		ConnectivityManager connectivityManager
				= (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
		return activeNetworkInfo != null && activeNetworkInfo.isConnected();
	}
	
}