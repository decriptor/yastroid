package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.net.URI;
import java.util.ArrayList;

import android.app.Activity;
import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class ServerEditActivity extends Activity {

	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;
	private Button saveButton;
	private Server s;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.serveredit);

		dbhelper = new YastroidOpenHelper(this);

		Intent serverIntent = getIntent();
		Bundle b = serverIntent.getExtras();
		getServer(b.getInt("SERVER_ID"));
		fillLayout( );

		this.saveButton = (Button)this.findViewById(R.id.button_save_server);
		this.saveButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if(saveServer()) {
					finish();
				}
			}
		});
	}

	private void getServer(long serverId) {
		database = dbhelper.getReadableDatabase();
		Cursor sc;
		try {
				sc = database.query(SERVERS_TABLE_NAME, new String[] {
						"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
						SERVERS_ID + "=" + serverId, null, null, null, null);

			sc.moveToFirst();
			s = new Server(sc.getInt(0), sc.getString(1), sc.getString(2), sc
							.getString(3), sc.getInt(4), sc.getString(5), sc
							.getString(6), sc.getInt(7));
			sc.close();
			database.close();
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
	}
	
	private void fillLayout() {
		((EditText)findViewById(R.id.edit_server_name)).setText(s.getName());
		((EditText)findViewById(R.id.edit_server_host)).setText(s.getScheme() + "://"+ s.getHostname());
		((EditText)findViewById(R.id.edit_server_port)).setText(Integer.toString(s.getPort()));
		((EditText)findViewById(R.id.edit_server_user)).setText(s.getUser());
	}

	private boolean saveServer() {
		database = dbhelper.getWritableDatabase();
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
			
			if (name.length() == 0 || host.length() == 0 || port.length() == 0 || user.length() == 0) {
				Toast.makeText(ServerEditActivity.this, "One or more fields are empty", Toast.LENGTH_SHORT).show();
				return false;
			}

			ContentValues values = new ContentValues();
			values.put(SERVERS_NAME, name);
			values.put(SERVERS_SCHEME, scheme);
			values.put(SERVERS_HOST, host);
			values.put(SERVERS_PORT, port);
			values.put(SERVERS_USER, user);
			if(pass.length() == 0 )
				values.put(SERVERS_PASS, s.getPass());
			else
				values.put(SERVERS_PASS, pass);
			values.put(SERVERS_GROUP, s.getGroupId());
			
			database.update(SERVERS_TABLE_NAME, values, SERVERS_ID + "=" + s.getId(), null);
			database.close();
			Log.i("ARRAY", "WebYaST server " + name + " has been updated.");
			result = true;
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		Toast.makeText(ServerEditActivity.this, "Server Updated", Toast.LENGTH_SHORT).show();
		return result;
	}
}
