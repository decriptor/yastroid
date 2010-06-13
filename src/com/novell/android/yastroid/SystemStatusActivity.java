package com.novell.android.yastroid;

import java.util.ArrayList;
import java.util.Collection;

import org.xml.sax.SAXException;

import com.novell.webyast.status.Graph;
import com.novell.webyast.status.Log;
import com.novell.webyast.status.Metric;
import com.novell.webyast.status.StatusModule;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;

public class SystemStatusActivity extends ListActivity {
	private StatusListAdapter statusListAdapter;
	private Runnable systemStatusView;
	private ProgressDialog systemStatusListProgress = null;
	private StatusModule statusModule;
	private	Collection<Graph> graphs;
	private Collection<Log> logs;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.system_status);
		statusListAdapter = new StatusListAdapter(this, R.layout.system_status_list_item, new ArrayList<SystemStatus>());
		setListAdapter(statusListAdapter);
		systemStatusView = new Runnable() {
			@Override
			public void run() {
				buildList();
			}
		};
		
		Thread thread = new Thread(null, systemStatusView, "SystemStatusListBackground");
		thread.start();
		systemStatusListProgress = ProgressDialog.show(this, "Please wait...",
				"Retrieving data...", true);
	}

	@Override
	protected void onResume() {
		super.onResume();
		//buildList();
	}
	
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		Intent statusIntent = null;
		SystemStatus systemStatus = null;
		
		super.onListItemClick(l, v, position, id);
		systemStatus = (SystemStatus)getListView().getItemAtPosition(position);
        statusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
		statusIntent.putExtras(getIntent().getExtras());
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
	    default:
	    	statusIntent = null;
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
		Server yastServer;
		Bundle b;
		
		statusIntent = getIntent();
		b = statusIntent.getExtras();
		yastServer = new Server(b);
		statusModule = yastServer.getStatusModule();
		try {
			graphs = statusModule.getGraphs();
		} catch(SAXException ex) {
			graphs = null;
		}
		
		try {
			logs = statusModule.getLogs();
		} catch(SAXException ex) {
			// Unable to get logs
			logs = null;
			System.out.println(ex.getMessage());
		}
		runOnUiThread(returnRes);
	}
	
	private void populateAdapter() {
		SystemStatus status;
		int statusID;

		statusListAdapter.clear();
		if(statusModule.isHealthy(Metric.NETWORK, graphs))
			statusID = SystemStatus.STATUS_GREEN;
		else
			statusID = SystemStatus.STATUS_RED;
		status = new SystemStatus(this.getApplication(), SystemStatus.NETWORK_STATUS, statusID);
		statusListAdapter.add(status);
		if(statusModule.isHealthy(Metric.MEMORY, graphs))
			statusID = SystemStatus.STATUS_GREEN;
		else
			statusID = SystemStatus.STATUS_RED;
		status = new SystemStatus(this.getApplication(), SystemStatus.MEMORY_STATUS, statusID);
		statusListAdapter.add(status);
		if(statusModule.isHealthy(Metric.DISK, graphs))
			statusID = SystemStatus.STATUS_GREEN;
		else
			statusID = SystemStatus.STATUS_RED;
		status = new SystemStatus(this.getApplication(), SystemStatus.DISK_STATUS, statusID);
		statusListAdapter.add(status);
		if(statusModule.isHealthy(Metric.CPU, graphs))
			statusID = SystemStatus.STATUS_GREEN;
		else
			statusID = SystemStatus.STATUS_RED;
		status = new SystemStatus(this.getApplication(), SystemStatus.CPU_STATUS, statusID);
		statusListAdapter.add(status);

		// Display Logs
		if (logs == null) {
			status = new SystemStatus(getApplication(), "Cannot get logs from server");
			statusListAdapter.add(status);
		} else {
			for (Log l : logs) {
				status = new SystemStatus (getApplication(), l.getDescription());
				statusListAdapter.add(status);
			}
		}
		statusListAdapter.notifyDataSetChanged();
	}

	private Runnable returnRes = new Runnable() {
		@Override
		public void run() {
			populateAdapter();
			systemStatusListProgress.dismiss();
		}
	};

}
