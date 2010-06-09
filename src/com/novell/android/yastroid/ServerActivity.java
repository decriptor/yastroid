package com.novell.android.yastroid;

import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

public class ServerActivity extends ListActivity {

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

		// TODO: Add icons for each module, which change dynamically
		//       (for example, green icon when system is healthy, red otherwise)
		setListAdapter(new ArrayAdapter<String>(this, R.layout.module_list_row,
				MODULES));

		// TODO: Can we use lv.addHeaderView to make a nicer header than what we do in server.xml?
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
	}

	// TODO: Figure out available modules dynamically
	// TODO: Populate module text dynamically
	String[] MODULES = new String[] { "5 Security Updates Available",
			"Your System Is Sick :-(" };
}
