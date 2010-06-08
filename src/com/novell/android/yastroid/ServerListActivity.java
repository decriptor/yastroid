package com.novell.android.yastroid;

import java.util.ArrayList;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ListView;

import com.novell.android.yastroid.Server;

public class ServerListActivity extends ListActivity {
	private ProgressDialog serverListProgress = null;
	private ArrayList<Server> serversList = null;
	private ServerListAdapter serverAdapter;
	private Runnable serverView;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
		setContentView(R.layout.serverslist);
    
		
		serversList = new ArrayList<Server>();
		this.serverAdapter = new ServerListAdapter(this, R.layout.serverslistrow, serversList);
		setListAdapter(this.serverAdapter);
		
		serverView = new Runnable() {
			@Override
			public void run() {
				getServers();
			}
		};
		Thread thread = new Thread(null, serverView, "ServerListBackground");
		thread.start();
		serverListProgress = ProgressDialog.show(this, "Please wait...", "Retrieving data...", true);
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
        Intent intent = new Intent(ServerListActivity.this, SystemStatusActivity.class);
        startActivity(intent);
	}

	@Override
	protected void onResume() {
		super.onResume();
		// TODO: force reload
	}
    
	private void getServers() {
		try {
			serversList = new ArrayList<Server>();
			Server s1 = new Server();
			s1.setServerName("webyast1");
			s1.setIpAddress("1.2.3.4");
			Server s2 = new Server();
			s2.setServerName("webyast2");
			s2.setIpAddress("4.3.2.1");
			serversList.add(s1);
			serversList.add(s2);
				Thread.sleep(2000);
			Log.i("ARRAY", ""+ serversList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		runOnUiThread(returnRes);
	}
	
	private Runnable returnRes = new Runnable() {
		
		@Override
		public void run() {
			if(serversList != null && serversList.size() > 0) {
				serverAdapter.notifyDataSetChanged();
				for(int i = 0; i < serversList.size(); i++)
					serverAdapter.add(serversList.get(i));
			}
			serverListProgress.dismiss();
			serverAdapter.notifyDataSetChanged();
		}
	};
    
}
