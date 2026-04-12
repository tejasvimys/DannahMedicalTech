package com.example.tejasvi.compmodtonesgenerator;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.database.sqlite.SQLiteOpenHelper;
import android.os.Environment;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by tejasvi on 1/2/16.
 */
public class DatabaseLogger extends SQLiteOpenHelper {

    private static final int DATABASE_VERSION = 1;

    private static final String DATABASE_NAME = "securityManager";

    private static final String TABLE_LOGGER = "cmtLogger";
    private static final String TABLE_SECURITY = "security";

    private static final String KEY_PATIENTID = "patientId";
    private static final String KEY_LOGDATE = "LogDate";
    private static final String KEY_LOGTIME = "LogTime";
    private static final String KEY_DURATION = "Duration";
    private static final String KEY_ISACTIVE   ="isactive";
    private static final String KEY_ISSYNC ="isSync";
    private static String PatientID;

    public DatabaseLogger(Context context) {
        super(context, Environment.getExternalStorageDirectory()
                + File.separator + "TinnitusTrio"
                + File.separator + DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_CONTACTS_TABLE = "CREATE TABLE " + TABLE_LOGGER + "("
                + KEY_PATIENTID + " TEXT," + KEY_LOGDATE + " TEXT," + KEY_LOGTIME + " TEXT,"
                + KEY_DURATION + " TEXT," + KEY_ISACTIVE+" TEXT," + KEY_ISSYNC + " TEXT"+")";
        db.execSQL(CREATE_CONTACTS_TABLE);
    }

    // Upgrading database
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_LOGGER);

        // Create tables again
        onCreate(db);
    }

    // Adding new entry
    public void addPatient(sqlitesync patient) {
        getPatientDetails();
        CheckDatabaseExists();
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        //values.put(KEY_PATIENTID, patient.getPatientID());
        values.put(KEY_PATIENTID,  PatientID);
        values.put(KEY_LOGDATE, patient.get_logDate());
        values.put(KEY_LOGTIME, patient.get_logTime());
        values.put(KEY_DURATION, patient.get_Duration());
        values.put(KEY_ISACTIVE, "1");
        values.put(KEY_ISSYNC,"1");

        db.insert(TABLE_LOGGER, null, values);
        db.close();
    }

    /**
     * Check if the database exist and can be read.
     *
     * @return true if it exists and can be read, false if it doesn't
     */
    public boolean checkDataBase() {
        SQLiteDatabase checkDB = null;
        try {
            checkDB = SQLiteDatabase.openDatabase(Environment.getExternalStorageDirectory()
                            + File.separator + "TinnitusTrio"
                            + File.separator + DATABASE_NAME, null,
                    SQLiteDatabase.OPEN_READONLY);
            checkDB.close();
        } catch (SQLiteException e) {
            // database doesn't exist yet.
        }
        return checkDB != null;
    }

    public boolean checkFolderExists() {

        final String checkDB = Environment.getExternalStorageDirectory()
                + File.separator + "TinnitusTrio";
        //+ File.separator;

        File f = new File(checkDB);

        if (f.isDirectory() && f.exists()) {
            return true;
        } else {
            return false;
        }
    }

    // Getting All Contacts
    public List<sqlitesync> getPatientDetails() {

        List<sqlitesync> contactList = new ArrayList<sqlitesync>();
        // Select All Query
        String selectQuery = "SELECT * FROM " + TABLE_SECURITY;

        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {
            do {
                sqlitesync contact = new sqlitesync();
                contact.setPatientID(cursor.getString(0));
                PatientID=cursor.getString(0);
                // Adding contact to list
                contactList.add(contact);
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
    }

    public void CheckDatabaseExists()
    {
        try {
            String selectQuery = "SELECT DISTINCT tbl_name from sqlite_master where tbl_name ='"+ TABLE_LOGGER+"'";

            SQLiteDatabase db = this.getWritableDatabase();
            Cursor cursor = db.rawQuery(selectQuery, null);


            if (cursor.getCount() == 0) {
                onCreate(db);
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<sqlitesync> getLogRecords() {

        List<sqlitesync> contactList = new ArrayList<sqlitesync>();
        // Select All Query
try{
    String selectQuery = "SELECT  * FROM " + TABLE_LOGGER;

    SQLiteDatabase db = this.getWritableDatabase();
    Cursor cursor = db.rawQuery(selectQuery, null);

    if(cursor==null)
    {

    }

    // looping through all rows and adding to list
    // if (cursor.moveToFirst()) {
    // do {
    //    sqlitesync contact = new sqlitesync();
    //    contact.setPatientID(cursor.getString(0));
    // Adding contact to list
    //   contactList.add(contact);
    //  } while (cursor.moveToNext());
    //  }

    // return contact list
}

catch (Exception ex){

    throw ex;
}


        return contactList;
    }

    public void deleteTable(){
        SQLiteDatabase db = this.getReadableDatabase();
        db.execSQL("delete from "+ TABLE_LOGGER);
    }

}

