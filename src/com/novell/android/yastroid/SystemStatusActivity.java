package com.novell.android.yastroid;

import java.util.ArrayList;
import java.util.Collection;
import com.novell.webyast.status.Log;
import com.novell.webyast.status.StatusModule;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import com.novell.webyast.status.Health;

public class SystemStatusActivity extends ListActivity {
	private StatusListAdapter statusListAdapter;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.system_status);
		statusListAdapter = new StatusListAdapter(this, R.layout.system_status_list_item, new ArrayList<SystemStatus>());
		setListAdapter(statusListAdapter);
		buildList();
		statusListAdapter.notifyDataSetChanged();
	}
	
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		Intent statusIntent = null;
		SystemStatus systemStatus = null;
		
		super.onListItemClick(l, v, position, id);
		systemStatus = (SystemStatus)getListView().getItemAtPosition(position);
        statusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
		Bundle b = getIntent().getExtras();
		// XXX: Temporal, should be replace by something better. Singleton?	
		statusIntent.putExtra("SERVER_NAME", b.getString("SERVER_NAME"));
		statusIntent.putExtra("SERVER_SCHEME", b.getString("SERVER_SCHEME"));
		statusIntent.putExtra("SERVER_HOSTNAME", b.getString("SERVER_HOSTNAME"));
		statusIntent.putExtra("SERVER_PORT", b.getInt("SERVER_PORT"));
		statusIntent.putExtra("SERVER_USER", b.getString("SERVER_USER"));
		statusIntent.putExtra("SERVER_PASS", b.getString("SERVER_PASS"));
		// XXX
		switch (systemStatus.getSystemType()) {
		case SystemStatus.NETWORK_STATUS:
	        statusIntent.putExtra("RESOURCE_TYPE", getString(R.string.network_status_text));
	        break;
		case SystemStatus.MEMORY_STATUS:
	        statusIntent.putExtra("RESOURCE_TYPE", getString(R.string.memory_status_text));
	        break;
		case SystemStatus.DISK_STATUS:
	        statusIntent.putExtra("RESOURCE_TYPE", getString(R.string.disk_status_text));
	        break;
		case SystemStatus.CPU_STATUS:
	        statusIntent.putExtra("RESOURCE_TYPE", getString(R.string.cpu_status_text));
	        break;
		//case SystemStatus.SYSTEM_MSGS_STATUS:
	        //statusIntent = new Intent(SystemStatusActivity.this, SystemMessagesActivity.class);
	        //break;
		}
		if (statusIntent != null) {
			startActivity(statusIntent);
		}
	}
	
	protected void buildList() {
		Intent statusIntent;
		SystemStatus status;
		Server server;
		StatusModule statusModule;
		Collection<Health> healthCollection;
		int statusID;
		Bundle b;
		Health health;
		
		statusIntent = getIntent();
		b = statusIntent.getExtras();
		server = new Server(b);
		statusModule = server.getStatusModule();
		healthCollection = statusModule.getFullHealth();
		health = new Health(0, 0, "Network", "");
		if(healthCollection.contains(health))
			statusID = SystemStatus.STATUS_RED;
		else
			statusID = SystemStatus.STATUS_GREEN;
		status = new SystemStatus(this.getApplication(), SystemStatus.NETWORK_STATUS, statusID);
		statusListAdapter.add(status);
		health.setHeadline("Memory");
		if(healthCollection.contains(health))
			statusID = SystemStatus.STATUS_RED;
		else
			statusID = SystemStatus.STATUS_GREEN;
		status = new SystemStatus(this.getApplication(), SystemStatus.MEMORY_STATUS, statusID);
		statusListAdapter.add(status);
		health.setHeadline("Disk");
		if(healthCollection.contains(health))
			statusID = SystemStatus.STATUS_RED;
		else
			statusID = SystemStatus.STATUS_GREEN;
		status = new SystemStatus(this.getApplication(), SystemStatus.DISK_STATUS, statusID);
		statusListAdapter.add(status);
		health.setHeadline("CPU");
		if(healthCollection.contains(health))
			statusID = SystemStatus.STATUS_RED;
		else
			statusID = SystemStatus.STATUS_GREEN;
		status = new SystemStatus(this.getApplication(), SystemStatus.CPU_STATUS, statusID);
		statusListAdapter.add(status);

		// TODO: Use a ProgressDialog
		//Bundle b = getIntent().getExtras(); // XXX: Temporal, should be replace by something better. Singleton?	
		Server yastServer =
			new Server (b.getInt("SERVER_ID"),
					b.getString("SERVER_NAME"),
					b.getString("SERVER_SCHEME"),
					b.getString("SERVER_HOSTNAME"),
					b.getInt("SERVER_PORT"),
					b.getString("SERVER_USER"),
					b.getString("SERVER_PASS"));

		try {
			Collection<Log> logs = yastServer.getStatusModule ().getLogs ();
			if (logs == null) {
				status = new SystemStatus(getApplication(), "Cannot get logs from server");
				statusListAdapter.add(status);
			} else {
				for (Log l : logs) {
					status = new SystemStatus (getApplication(), l.getDescription());
					statusListAdapter.add(status);
				}
			}
		} catch (Exception e) {
			// Unable to get logs
			status = new SystemStatus(getApplication(), "Cannot read messages");
			statusListAdapter.add(status);
			System.out.println(e.getMessage());
		}
	}
}
