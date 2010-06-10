package com.novell.android.yastroid;

import java.util.ArrayList;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.app.ListActivity;
import android.content.Intent;
import android.graphics.drawable.Drawable;

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
//		setUpViews();
//		showStatus();
	}
	
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		Intent networkStatusIntent, memoryStatusIntent, diskStatusIntent,
			cpuStatusIntent, systemMsgsStatusIntent;
		
		super.onListItemClick(l, v, position, id);
		//SystemStatus status = (SystemStatus)getListView().getItemAtPosition(position);
        networkStatusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
        //memoryStatusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
        //diskStatusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
        //cpuStatusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
        //systemMsgsStatusIntent = new Intent(SystemStatusActivity.this, DisplayResourceActivity.class);
        startActivity(networkStatusIntent);
	}
	
	protected void buildList() {
		SystemStatus status;
		Drawable greenIcon;
		Drawable redIcon;
		
		greenIcon = this.getResources().getDrawable(R.drawable.status_green);
		redIcon = this.getResources().getDrawable(R.drawable.status_red);
		status = new SystemStatus(getString(R.string.network_status_text), greenIcon);
		statusListAdapter.add(status);
		status = new SystemStatus(getString(R.string.memory_status_text), greenIcon);
		statusListAdapter.add(status);
		status = new SystemStatus(getString(R.string.disk_status_text), redIcon);
		statusListAdapter.add(status);
		status = new SystemStatus(getString(R.string.cpu_status_text), greenIcon);
		statusListAdapter.add(status);
		status = new SystemStatus(getString(R.string.system_msgs_status_text), null);
		statusListAdapter.add(status);
	}

/*	@Override
	protected void onResume() {
		super.onResume();
		loadStatusListIfNotLoaded();
	}
*/
}
