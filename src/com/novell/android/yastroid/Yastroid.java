package com.novell.android.yastroid;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
//import android.preference.PreferenceManager;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Toast;

public class Yastroid extends Activity {
	SharedPreferences settings;
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
        	Toast.makeText(Yastroid.this, "Credentials are either not set or invalid", Toast.LENGTH_LONG).show();
        }
        
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
    		Preferences p = new Preferences();
    		p.runOnUiThread(null);
    		return true;
    	}
    	return false;
    }
    
}