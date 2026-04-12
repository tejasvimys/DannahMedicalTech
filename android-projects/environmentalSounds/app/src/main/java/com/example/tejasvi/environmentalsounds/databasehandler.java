package com.example.tejasvi.environmentalsounds;

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
 * Created by tejasvi on 26/12/15.
 */
public class databasehandler extends SQLiteOpenHelper {

    private static final int DATABASE_VERSION = 1;

    private static final String DATABASE_NAME = "securityManager";

    private static final String TABLE_SECURITY = "security";

    private static final String KEY_PATIENTID = "patientID";
    private static final String KEY_MACID = "macid";
    private static final String KEY_SERIALNO = "serialno";
    private static final String KEY_ISACTIVE   ="isactive";
    private static final String HASH_VALUE ="uniqueID";

    public databasehandler(Context context) {
        super(context, Environment.getExternalStorageDirectory()
                + File.separator + "TinnitusTrio"
                + File.separator + DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_CONTACTS_TABLE = "CREATE TABLE " + TABLE_SECURITY + "("
                + KEY_PATIENTID + " TEXT," + KEY_MACID + " TEXT,"
                + KEY_SERIALNO + " TEXT," + KEY_ISACTIVE+" TEXT," + HASH_VALUE + " TEXT"+")";
        db.execSQL(CREATE_CONTACTS_TABLE);
    }

    // Upgrading database
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_SECURITY);

        // Create tables again
        onCreate(db);
    }


    // Adding new entry
    public void addPatient(sqlitesync patient) {
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_PATIENTID, patient.getPatientID());
        values.put(KEY_MACID, patient.getmacID());
        values.put(KEY_SERIALNO, patient.getSerialno());
        values.put(KEY_ISACTIVE, "1");
        values.put(HASH_VALUE,patient.gethashValue());

        db.insert(TABLE_SECURITY, null, values);
        db.close();
    }

    // Getting single patientid
    //public sqlitesync getActiveStatus(String isActive) {}

    // Getting All Contacts
    public List<sqlitesync> getPatientDetails() {

        List<sqlitesync> contactList = new ArrayList<sqlitesync>();
        // Select All Query
        String selectQuery = "SELECT  * FROM " + TABLE_SECURITY;

        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {
            do {
                sqlitesync contact = new sqlitesync();
                contact.setmacID((cursor.getString(1)));
                contact.setSerialNo(cursor.getString(2));
                contact.setPatientID(cursor.getString(0));
                contact.sethashValue(cursor.getString(4));
                // Adding contact to list
                contactList.add(contact);
            } while (cursor.moveToNext());
        }

        // return contact list
        return contactList;
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

    public int getProfilesCount() {

        try {
            String countQuery = "SELECT  * FROM " + TABLE_SECURITY;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(countQuery, null);
            int cnt = cursor.getCount();
            cursor.close();
            return cnt;
        }

        catch (Exception ex)
        {
            return 0;
        }

    }

    public void deleteTable(){
        SQLiteDatabase db = this.getReadableDatabase();
        db.execSQL("delete from "+ TABLE_SECURITY);
    }

    public String ReturnPatientId(){
        String selectQuery = "SELECT  * FROM " + TABLE_SECURITY;

        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        String patientid="";
        if (cursor.moveToFirst()) {
            do {

                patientid= cursor.getString(0);
            }while (cursor.moveToNext());
        }

        return patientid;
    }



    // Updating single patient
   // public int updatePatient(sqlitesync contact) {}

    // Deleting single contact
    //public void deleteContact(Contact contact) {}

}
