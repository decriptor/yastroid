package com.novell.android.yastroid;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.content.*;
import android.database.sqlite.SQLiteDatabase;

public class Yastroid extends Activity {
	
	private SharedPreferences settings;
	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        // Initialize preferences
        settings = getPreferences(MODE_PRIVATE);
        String user = settings.getString("username", null);
        String pass = settings.getString("password", null);
        
        if (user == null || pass == null) {
        	//Toast.makeText(Yastroid.this, "Credentials are either not set or invalid", Toast.LENGTH_SHORT).show();
        }
        
        // Force database upgrade if needed
        dbhelper = new YastroidOpenHelper(this);
        database = dbhelper.getWritableDatabase();
        database.close();
        
        // Preliminary Group work. The ability to add/delete groups needs
        // to be finish.  Adding servers to groups is still completely missing.
        Intent intent = new Intent(Yastroid.this, GroupListActivity.class);
        //Intent intent = new Intent(Yastroid.this, ServerListActivity.class);
        startActivity(intent);
    }
        
    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
    	MenuInflater inflater = getMenuInflater();
    	inflater.inflate(R.menu.menu, menu);
    	return true;
    }
    
    public boolean onOptionsItemSelected(MenuItem item)
    {
    	switch (item.getItemId()) {
    	case R.id.preferences:
    		return true;
    	}
    	return false;
    }
    
}