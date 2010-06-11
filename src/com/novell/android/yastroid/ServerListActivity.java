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
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView; //import android.widget.Toast;

import com.novell.android.yastroid.Server;

public class ServerListActivity extends ListActivity {

	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;
	private ArrayList<Server> serverList = null;

	private ProgressDialog serverListProgress = null;
	private ServerListAdapter serverAdapter;
	private Runnable serverView;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.serverlist);
		getListView().setOnCreateContextMenuListener(this);

		dbhelper = new YastroidOpenHelper(this);

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
		getServers();
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		Intent intent = new Intent(ServerListActivity.this,
				ServerActivity.class);
		Server s = serverList.get(position);
		intent.putExtra("SERVER_ID", s.getId());
		intent.putExtra("SERVER_NAME", s.getName());
		intent.putExtra("SERVER_SCHEME", s.getScheme());
		intent.putExtra("SERVER_HOSTNAME", s.getHostname());
		intent.putExtra("SERVER_PORT", s.getPort());
		intent.putExtra("SERVER_USER", s.getUser());
		intent.putExtra("SERVER_PASS", s.getPass());
		startActivity(intent);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.serverlistmenu, menu);
		return true;
	}

	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.add_server:
			Intent intent = new Intent(ServerListActivity.this,
					ServerAddActivity.class);
			startActivity(intent);
			return true;
		}
		return false;
	}

	@Override
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		super.onCreateContextMenu(menu, v, menuInfo);
			menu.add(0, 0, 0, "Delete");
	}

	@Override
	public boolean onContextItemSelected(MenuItem item) {
		AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item
				.getMenuInfo();
		
		switch (item.getItemId()) {
		case 0: {
			Server s = serverList.get(info.position);
			deleteServer(s.getId());
			return true;
		}
		default:
			return super.onContextItemSelected(item);
		}
	}

	private void deleteServer(long id) {
		database = dbhelper.getWritableDatabase();
		database.delete(SERVERS_TABLE_NAME, "_id=" + id, null);
		database.close();
		getServers();
	}

	private void getServers() {
		database = dbhelper.getReadableDatabase();
		try {
			Cursor sc = database.query(SERVERS_TABLE_NAME, new String[] {
					"_id","name", "scheme", "hostname", "port", "user", "pass" },
					null, null, null, null, null);

			sc.moveToFirst();
			Server s;
			serverList = new ArrayList<Server>();
			if (!sc.isAfterLast()) {
				do {
					s = new Server(sc.getInt(0), sc.getString(1), sc.getString(2), sc
							.getString(3), sc.getInt(4), sc.getString(5), sc
							.getString(6));
					serverList.add(s);
				} while (sc.moveToNext());
			}
			sc.close();
			database.close();
			Log.i("ARRAY", "" + serverList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		runOnUiThread(returnRes);
	}

	private Runnable returnRes = new Runnable() {

		@Override
		public void run() {
			serverAdapter.clear();
			if (serverList != null && serverList.size() > 0) {
				serverAdapter.notifyDataSetChanged();
				for (int i = 0; i < serverList.size(); i++)
					serverAdapter.add(serverList.get(i));
			} else {
				// Add button 'Add new Server'
			}
			serverListProgress.dismiss();
			serverAdapter.notifyDataSetChanged();
		}
	};

}
