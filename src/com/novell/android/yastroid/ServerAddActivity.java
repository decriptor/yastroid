package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.net.URI;
import java.util.ArrayList;

import android.app.Activity;
import android.content.ContentValues;
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
	private ArrayList<Server> serverList = null;
	private Button addButton;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.serveradd);

		YastroidOpenHelper helper = new YastroidOpenHelper(this);
		database = helper.getWritableDatabase();
		
		this.addButton = (Button)this.findViewById(R.id.button_add_server);
		this.addButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				addServer();
				finish();
			}
		});
	}

	private void addServer() {
		try {
			URI uri = new URI(((EditText)findViewById(R.id.edit_server_host)).getText().toString());
			
			ContentValues values = new ContentValues();
			values.put(SERVERS_NAME, ((EditText)findViewById(R.id.edit_server_name)).getText().toString());
			values.put(SERVERS_SCHEME, uri.getScheme());
			values.put(SERVERS_HOST, uri.getHost());
			values.put(SERVERS_PORT, ((EditText)findViewById(R.id.edit_server_port)).getText().toString());
			values.put(SERVERS_USER, ((EditText)findViewById(R.id.edit_server_user)).getText().toString());
			values.put(SERVERS_PASS, ((EditText)findViewById(R.id.edit_server_pass)).getText().toString());
			
			database.insert(SERVERS_TABLE_NAME, "null", values);
			database.close();
			Log.i("ARRAY", "" + serverList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		Toast.makeText(ServerAddActivity.this, "Server Added", Toast.LENGTH_SHORT).show();
	}
}
