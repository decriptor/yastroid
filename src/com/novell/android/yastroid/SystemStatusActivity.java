package com.novell.android.yastroid;

import java.util.ArrayList;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.app.ListActivity;
import android.content.Intent;

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
		case SystemStatus.SYSTEM_MSGS_STATUS:
	        statusIntent = new Intent(SystemStatusActivity.this, SystemMessagesActivity.class);
	        break;
		}
		if (statusIntent != null) {
			startActivity(statusIntent);
		}
	}
	
	protected void buildList() {
		SystemStatus status;
		
		status = new SystemStatus(this.getApplication(), SystemStatus.NETWORK_STATUS, SystemStatus.STATUS_GREEN);
		statusListAdapter.add(status);
		status = new SystemStatus(this.getApplication(), SystemStatus.MEMORY_STATUS, SystemStatus.STATUS_GREEN);
		statusListAdapter.add(status);
		status = new SystemStatus(this.getApplication(), SystemStatus.DISK_STATUS, SystemStatus.STATUS_RED);
		statusListAdapter.add(status);
		status = new SystemStatus(this.getApplication(), SystemStatus.CPU_STATUS, SystemStatus.STATUS_GREEN);
		statusListAdapter.add(status);
		status = new SystemStatus(this.getApplication(), SystemStatus.SYSTEM_MSGS_STATUS);
		statusListAdapter.add(status);
	}

/*	@Override
	protected void onResume() {
		super.onResume();
		loadStatusListIfNotLoaded();
	}
*/
}
