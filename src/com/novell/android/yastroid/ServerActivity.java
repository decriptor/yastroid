package com.novell.android.yastroid;

import java.util.ArrayList;

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

		// String[] MODULES = new String[] { updateStr,
		// "Your System Is Sick :-(" };
		setListAdapter(new ArrayAdapter<String>(this, R.layout.module_list_row,
				moduleList));

		// TODO: Can we use lv.addHeaderView to make a nicer header than what we
		// do in server.xml?
		ListView lv = getListView();
		lv.setTextFilterEnabled(true);

		lv.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				if (position == 1) {
					Intent intent = new Intent(ServerActivity.this,
							SystemStatusActivity.class);
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
				int availableUpdates = 0;
				try {
					Bundle b = getIntent().getExtras();
					Server yastServer =
						new Server (b.getString("SERVER_NAME"),
								b.getString("SERVER_SCHEME"),
								b.getString("SERVER_HOSTNAME"),
								b.getInt("SERVER_PORT"),
								b.getString("SERVER_USER"),
								b.getString("SERVER_PASS"));
					availableUpdates =
							yastServer.getUpdateModule().getNumberOfAvailableUpdates();
				} catch (Exception e) {
					System.out.println(e.getMessage());
				}

				String updateStr = availableUpdates + " updates available";

				// TODO: Figure out available modules dynamically
				// TODO: Populate health text dynamically
				moduleList.add(updateStr);
				moduleList.add("Your System Is Sick :-(");

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
