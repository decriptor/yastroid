package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.net.URI;

import android.app.Activity;
import android.content.ContentValues;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class ServerAddActivity extends Activity {

	private SQLiteDatabase database;
	private Button addButton;
	private int groupId;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.serveradd);

		YastroidOpenHelper helper = new YastroidOpenHelper(this);
		database = helper.getWritableDatabase();

		Intent groupIntent = getIntent();
		Bundle b = groupIntent.getExtras();
		groupId = b.getInt("GROUP_ID");

		this.addButton = (Button)this.findViewById(R.id.button_add_server);
		this.addButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if(addServer()) {
					finish();
				}
			}
		});
	}
	

	private boolean addServer() {
		boolean result = false;
		try {
			
			URI uri = new URI(((EditText)findViewById(R.id.edit_server_host)).getText().toString());
			String name = ((EditText)findViewById(R.id.edit_server_name)).getText().toString();
			String scheme = null;
			String host = uri.getHost();
			String port = ((EditText)findViewById(R.id.edit_server_port)).getText().toString();
			String user = ((EditText)findViewById(R.id.edit_server_user)).getText().toString();
			String pass = ((EditText)findViewById(R.id.edit_server_pass)).getText().toString();
			
			if (uri.getScheme() == null) {
				scheme = "http";
				host = uri.getSchemeSpecificPart();
			} else {
				scheme = uri.getScheme();
			}
			
			if (name.length() == 0 || host.length() == 0 || port.length() == 0 || user.length() == 0 || pass.length() == 0) {
				Toast.makeText(ServerAddActivity.this, "One or more fields are empty", Toast.LENGTH_SHORT).show();
				return false;
			}

			ContentValues values = new ContentValues();
			values.put(SERVERS_NAME, name);
			values.put(SERVERS_SCHEME, scheme);
			values.put(SERVERS_HOST, host);
			values.put(SERVERS_PORT, port);
			values.put(SERVERS_USER, user);
			values.put(SERVERS_PASS, pass);
			values.put(SERVERS_GROUP, groupId);
			
			database.insert(SERVERS_TABLE_NAME, "null", values);
			database.close();
			Log.i("ARRAY", "WebYaST server " + name + " has been added.");
			result = true;
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		Toast.makeText(ServerAddActivity.this, "Server Added", Toast.LENGTH_SHORT).show();
		return result;
	}
}
