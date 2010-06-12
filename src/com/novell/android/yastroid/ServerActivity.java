package com.novell.android.yastroid;

import java.util.ArrayList;

import com.novell.webyast.status.Health;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.AdapterView.OnItemClickListener;

public class ServerActivity extends ListActivity {

	private ProgressDialog moduleListProgress;
	private Server yastServer;
	private ArrayList<String> moduleList = new ArrayList<String>();

	/*
	 * (non-Javadoc)
	 * 
	 * @see android.app.Activity#onCreate(android.os.Bundle)
	 */
	@Override
	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		setContentView(R.layout.server);
		yastServer = new Server(getIntent().getExtras());

		View headerView = getLayoutInflater().inflate(
		        R.layout.server_header, null);
		
		TextView serverName = (TextView)headerView.findViewById(R.id.server_name);
		TextView serverHost = (TextView)headerView.findViewById(R.id.server_address);
		//TextView serverUptime = (TextView)headerView.findViewById(R.id.server_uptime);
		serverName.setText(yastServer.getName());
		serverHost.setText(yastServer.getHostname());
		//serverUptime.setText("Uptime: " + "8 days, 8 Hours");

		ListView lv = getListView();
		lv.setTextFilterEnabled(true);
		lv.addHeaderView(headerView);

		setListAdapter(new ArrayAdapter<String>(this, R.layout.module_list_row,
				moduleList));

		lv.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				if (position == 1) {
					Intent intent = new Intent(ServerActivity.this, SystemStatusActivity.class);
					intent.putExtras(getIntent().getExtras());
				}
			}
		});

		new Thread(new Runnable() {
			@Override
			public void run() {

				// TODO: Add icons for each module, which change dynamically
				// (for example, green icon when system is healthy, red
				// otherwise)
				
				int availableUpdates = 0;
				try {
					availableUpdates =
							yastServer.getUpdateModule().getNumberOfAvailableUpdates();
				} catch (Exception e) {
					System.out.println(e.getMessage());
				}

				String updateStr = availableUpdates + " updates available";

				// TODO: Figure out available modules dynamically
				// TODO: Populate health text dynamically
				moduleList.add(updateStr);

				String healthStr = "System is healthy";
				try {
					// TODO: Clicking this message should show you full details of the status
					int healthSummary = yastServer.getStatusModule ().getHealthSummary ();
					if (healthSummary == Health.ERROR)
						healthStr = "Cannot read system status";
					else if (healthSummary == Health.UNHEALTHY)
						healthStr = "System is not healthy";
				}  catch (Exception e) {
					System.out.println(e.getMessage());
				}
				moduleList.add (healthStr);

				runOnUiThread(new Runnable() {
					@Override
					public void run() {
						moduleListProgress.dismiss();
						((ArrayAdapter<String>) getListAdapter()).notifyDataSetChanged();
					}
				});
			}
		}).start();

		moduleListProgress = ProgressDialog.show(this, "Please wait...",
				"Contacting Server...", true);
	}
}
