package com.novell.android.yastroid;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;
import android.widget.ImageView;

public class SystemStatusActivity extends Activity {
	private TextView networkStatusText;
	private TextView memoryStatusText;
	private TextView diskStatusText;
	private TextView cpuStatusText;
	private TextView systemMsgsStatusText;
	private ImageView networkStatusIcon;
	private ImageView memoryStatusIcon;
	private ImageView diskStatusIcon;
	private ImageView cpuStatusIcon;
	
	/* (non-Javadoc)
	 * @see android.app.Activity#onCreate(android.os.Bundle)
	 */
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.system_status);
		setUpViews();
		showStatus();
	}
	
	private void showStatus() {
		networkStatusText.setText(R.string.network_status_text);
		memoryStatusText.setText(R.string.memory_status_text);
		diskStatusText.setText(R.string.disk_status_text);
		cpuStatusText.setText(R.string.cpu_status_text);
		systemMsgsStatusText.setText(R.string.system_msgs_status_text);
		//networkStatusIcon.setImageDrawable(networkStatusIcon.getDrawable());
		//networkStatusIcon.setVisibility(View.VISIBLE);
		//memoryStatusIcon.setImageDrawable(memoryStatusIcon.getDrawable());
		//diskStatusIcon.setImageDrawable(diskStatusIcon.getDrawable());
		//cpuStatusIcon.setImageDrawable(cpuStatusIcon.getDrawable());
	}
	
	private void setUpViews() {
		networkStatusText = (TextView)findViewById(R.id.network_status_text);
		memoryStatusText = (TextView)findViewById(R.id.memory_status_text);
		diskStatusText = (TextView)findViewById(R.id.disk_status_text);
		cpuStatusText = (TextView)findViewById(R.id.cpu_status_text);
		systemMsgsStatusText = (TextView)findViewById(R.id.system_msgs_status_text);
		//networkStatusIcon =(ImageView)findViewById(R.id.network_status_icon);
		//memoryStatusIcon =(ImageView)findViewById(R.id.memory_status_icon);
		//diskStatusIcon =(ImageView)findViewById(R.id.disk_status_icon);
		//cpuStatusIcon =(ImageView)findViewById(R.id.cpu_status_icon);
	}
}
