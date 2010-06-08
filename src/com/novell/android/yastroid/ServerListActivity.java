package com.novell.android.yastroid;

import java.util.ArrayList;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;

import com.novell.android.yastroid.Server;

public class ServerListActivity extends ListActivity {
	private ProgressDialog serverListProgress = null;
	private ArrayList<Server> servers = null;
	private ServersAdapter adapter;
	private Runnable viewServers;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
		setContentView(R.layout.serverslist);
		
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		
	}

	@Override
	protected void onResume() {
		super.onResume();
		// TODO: force reload
	}
	
	
    
    
}
