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
import android.widget.AdapterView.OnItemClickListener;

public class ServerActivity extends ListActivity {

	private ProgressDialog moduleListProgress;
	private ArrayList<String> moduleList = new ArrayList<String>();

	/*
	 * (non-Javadoc)
	 * 
	 * @see android.app.Activity#onCreate(android.os.Bundle)
	 */
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.server);
		setListAdapter(new ArrayAdapter<String>(this, R.layout.module_list_row,
				moduleList));

		// TODO: Can we use lv.addHeaderView to make a nicer header than what we
		// do in server.xml?
		ListView lv = getListView();
		lv.setTextFilterEnabled(true);

		lv.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				Bundle b;
				
				b = getIntent().getExtras();
				//b.putInt("SERVER_ID", b.getInt("SERVER_ID"));
				//b.putString("SERVER_NAME", b.getString("SERVER_NAME"));
				//b.putString("SERVER_SCHEME", b.getString("SERVER_SCHEME"));
				//b.putString("SERVER_HOSTNAME", b.getString("SERVER_HOSTNAME"));
				//b.putInt("SERVER_PORT", b.getInt("SERVER_PORT"));
				//b.putString("SERVER_USER", b.getString("SERVER_USER"));
				//b.putString("SERVER_PASS", b.getString("SERVER_PASS"));
				if (position == 1) {
					Intent intent = new Intent(ServerActivity.this, SystemStatusActivity.class);
					intent.putExtras(b);
					startActivity(intent);
				}
			}
		});

		new Thread(new Runnable() {
			@Override
			public void run() {

				// TODO: Add icons for each module, which change dynamically
				// (for example, green icon when system is healthy, red
				// otherwise)
				Bundle b = getIntent().getExtras();
				Server yastServer =
					new Server (b.getInt("SERVER_ID"),
							b.getString("SERVER_NAME"),
							b.getString("SERVER_SCHEME"),
							b.getString("SERVER_HOSTNAME"),
							b.getInt("SERVER_PORT"),
							b.getString("SERVER_USER"),
							b.getString("SERVER_PASS"));
				
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
					if (healthSummary == Health.Error)
						healthStr = "Cannot read system status";
					else if (healthSummary == Health.Unhealthy)
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
