package com.novell.android.yastroid;

import java.util.ArrayList;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.ImageView;
import android.app.ListActivity;
import android.content.Intent;
import android.graphics.drawable.Drawable;

public class SystemStatusActivity extends ListActivity {
	private StatusListAdapter statusListAdapter;
	private TextView networkStatusText;
	private TextView memoryStatusText;
	private TextView diskStatusText;
	private TextView cpuStatusText;
	private TextView systemMsgsText;
	private ImageView networkStatusIcon;
	private ImageView memoryStatusIcon;
	private ImageView diskStatusIcon;
	private ImageView cpuStatusIcon;
	
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
		super.onListItemClick(l, v, position, id);
        Intent intent = new Intent(SystemStatusActivity.this, ServerActivity.class);
        //startActivity(intent);
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
	}*/


	private void showStatus() {
		networkStatusText.setText(R.string.network_status_text);
		memoryStatusText.setText(R.string.memory_status_text);
		diskStatusText.setText(R.string.disk_status_text);
		cpuStatusText.setText(R.string.cpu_status_text);
		systemMsgsText.setText(R.string.system_msgs_status_text);
		networkStatusIcon.setImageResource(R.drawable.status_green);
		memoryStatusIcon.setImageResource(R.drawable.status_green);
		diskStatusIcon.setImageResource(R.drawable.status_red);
		cpuStatusIcon.setImageResource(R.drawable.status_green);
	}
	
	private void setUpViews() {
		networkStatusText = (TextView)findViewById(R.id.network_status_text);
		memoryStatusText = (TextView)findViewById(R.id.memory_status_text);
		diskStatusText = (TextView)findViewById(R.id.disk_status_text);
		cpuStatusText = (TextView)findViewById(R.id.cpu_status_text);
		systemMsgsText = (TextView)findViewById(R.id.system_msgs_text);
		networkStatusIcon =(ImageView)findViewById(R.id.network_status_icon);
		memoryStatusIcon =(ImageView)findViewById(R.id.memory_status_icon);
		diskStatusIcon =(ImageView)findViewById(R.id.disk_status_icon);
		cpuStatusIcon =(ImageView)findViewById(R.id.cpu_status_icon);
	}
}
