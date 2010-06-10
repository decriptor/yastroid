package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.util.ArrayList;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ListView; //import android.widget.Toast;

import com.novell.android.yastroid.Server;

public class ServerListActivity extends ListActivity {

	private SQLiteDatabase database;
	private ArrayList<Server> serverList = null;

	private ProgressDialog serverListProgress = null;
	private ServerListAdapter serverAdapter;
	private Runnable serverView;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.serverlist);

		YastroidOpenHelper helper = new YastroidOpenHelper(this);
		database = helper.getReadableDatabase();

		serverList = new ArrayList<Server>();
		this.serverAdapter = new ServerListAdapter(this,
				R.layout.serverlistrow, serverList);
		setListAdapter(this.serverAdapter);

		serverView = new Runnable() {
			@Override
			public void run() {
				getServers();
			}
		};
		Thread thread = new Thread(null, serverView, "ServerListBackground");
		thread.start();
		serverListProgress = ProgressDialog.show(this, "Please wait...",
				"Retrieving data...", true);
	}

	@Override
	protected void onResume() {
		super.onResume();
		// TODO: force reload
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		Intent intent = new Intent(ServerListActivity.this, ServerActivity.class);
		Server s = serverList.get(position);
		intent.putExtra("SERVER_NAME", s.getName());
		intent.putExtra("SERVER_SCHEME", s.getScheme());
		intent.putExtra("SERVER_HOSTNAME", s.getHostname());
		intent.putExtra("SERVER_PORT", s.getPort());
		intent.putExtra("SERVER_USER", s.getUser());
		intent.putExtra("SERVER_PASS", s.getPass());
		startActivity(intent);
	}

	private void getServers() {
		try {
			Cursor sc = database.query(SERVER_TABLE_NAME, new String[] {
					"name", "scheme", "hostname", "port", "user", "pass" },
					null, null, null, null, null);

			sc.moveToFirst();
			Server s;
			serverList = new ArrayList<Server>();
			if (!sc.isAfterLast()) {
				do {
					s = new Server(sc.getString(0), sc.getString(1), sc
							.getString(2), sc.getInt(3), sc.getString(4), sc
							.getString(5));
					serverList.add(s);
				} while (sc.moveToNext());
			}
			sc.close();
			database.close();
			Thread.sleep(2000);
			Log.i("ARRAY", "" + serverList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		runOnUiThread(returnRes);
	}

	private Runnable returnRes = new Runnable() {

		@Override
		public void run() {
			if (serverList != null && serverList.size() > 0) {
				serverAdapter.notifyDataSetChanged();
				for (int i = 0; i < serverList.size(); i++)
					serverAdapter.add(serverList.get(i));
			}
			serverListProgress.dismiss();
			serverAdapter.notifyDataSetChanged();
		}
	};

}
