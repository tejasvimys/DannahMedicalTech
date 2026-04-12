package com.androidhive.musicplayer;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Environment;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.File;

/**
 * Created by tejasvi on 7/2/16.
 */
public class DatabaseExporter {

    private static final String DATABASE_NAME = "securityManager";

    private static final String TABLE_SECURITY = "cmmLogger";

    private static final String DB_PATH= Environment.getExternalStorageDirectory()
            + File.separator + "TinnitusTrio"
            + File.separator;

    public JSONArray getResults()
    {
        String myPath = DB_PATH + DATABASE_NAME;// Set path to your database

        String myTable = TABLE_SECURITY;//Set name of your table

        SQLiteDatabase myDataBase = SQLiteDatabase.openDatabase(myPath, null, SQLiteDatabase.OPEN_READONLY);

        String searchQuery = "SELECT  * FROM " + myTable;
        Cursor cursor = myDataBase.rawQuery(searchQuery, null );

        JSONArray resultSet     = new JSONArray();

        cursor.moveToFirst();
        while (cursor.isAfterLast() == false) {

            int totalColumn = cursor.getColumnCount();
            JSONObject rowObject = new JSONObject();

            for( int i=0 ;  i< totalColumn ; i++ )
            {
                if( cursor.getColumnName(i) != null )
                {
                    try
                    {
                        if( cursor.getString(i) != null )
                        {
                            Log.d("TAG_NAME", cursor.getString(i));
                            rowObject.put(cursor.getColumnName(i) ,  cursor.getString(i) );
                        }
                        else
                        {
                            rowObject.put( cursor.getColumnName(i) ,  "" );
                        }
                    }
                    catch( Exception e )
                    {
                        Log.d("TAG_NAME", e.getMessage());
                    }
                }
            }
            resultSet.put(rowObject);
            cursor.moveToNext();
        }
        cursor.close();
        Log.d("TAG_NAME", resultSet.toString());
        return resultSet;
    }


}