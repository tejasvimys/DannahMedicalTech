package net.pmarks.chromadoze;

import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import org.w3c.dom.Text;

import java.sql.SQLException;

/**
 * Created by qualigmapc5 on 1/7/15.
 */
public class MySQLite
{



    public SQLiteDatabase db;
    public static final String column_bar="bar_value";
    public static final String table_bar="bars";
    public static final String DATABASE_NAME="myDb.db";
    public static final int DATABASE_VERSION= 1;


      public boolean onCreate(SQLiteDatabase database) {
          Log.i("main", "inside onCreate------>");

        Log.i("main", "table created------>");
return true;
    }


    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        Log.i("main", "inside onUpgrade------>");
        db.execSQL("DROP TABLE IF EXISTS"+table_bar);
        onCreate(db);
    }

    public void addBars(float bar1)
    {
        Log.i("main", "inside addbars------>");
    }
}



 /**
  *
  *  try {
  SQLiteDatabase database = this.getWritableDatabase();


  database.close();
  Log.e("one record added","------------>"+bar);
  }
  catch(Exception e){
  e.printStackTrace();
  }
  this.onCreate(database);
  }
  *
  *
  *
  *
  * public void SaveInSqlite()
    {

        db=openOrCreateDatabase("myDb", Context.MODE_PRIVATE, null);
        db.execSQL("CREATE TABLE IF NOT EXISTS bars(bar1 float);");
        out=(float)25.05;
        db.execSQL("INSERT INTO bars VALUES('"+out+"');");
        showMessage("Success", "Record added");
    }

    public void showMessage(String title,String message)
    {
        AlertDialog.Builder builder=new AlertDialog.Builder(this);
        builder.setCancelable(true);
        builder.setTitle(title);
        builder.setMessage(message);
        builder.show();
    }*/