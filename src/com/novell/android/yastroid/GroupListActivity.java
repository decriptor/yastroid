package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.util.ArrayList;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

public class GroupListActivity extends ListActivity {

	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;
	private ArrayList<Group> groupList = null;

	private ProgressDialog groupListProgress = null;
	private GroupListAdapter groupAdapter;
	private Runnable groupView;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.grouplist);
		getListView().setOnCreateContextMenuListener(this);

		dbhelper = new YastroidOpenHelper(this);

		groupList = new ArrayList<Group>();
		this.groupAdapter = new GroupListAdapter(this,
				R.layout.grouplistrow, groupList);
				setListAdapter(this.groupAdapter);

		groupView = new Runnable() {
			@Override
			public void run() {
				getGroups();
			}
		};
		
		Thread thread = new Thread(null, groupView, "ServerListBackground");
		thread.start();
		groupListProgress = ProgressDialog.show(this, "Please wait...",
				"Retrieving groups...", true);
	}

	@Override
	protected void onResume() {
		super.onResume();
		getGroups();
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		Intent intent = new Intent(GroupListActivity.this,
				ServerListActivity.class);
		Group g = groupList.get(position);
		intent.putExtra("GROUP_ID", g.getId());
		intent.putExtra("GROUP_NAME", g.getName());
		startActivity(intent);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.grouplistmenu, menu);
		return true;
	}

	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.add_group:
			Intent intent = new Intent(GroupListActivity.this, GroupAddActivity.class);
			startActivity(intent);
			return true;
		}
		return false;
	}

	@Override
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		super.onCreateContextMenu(menu, v, menuInfo);
			menu.add(0, 0, 0, "Delete");
	}

	@Override
	public boolean onContextItemSelected(MenuItem item) {
		AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item
				.getMenuInfo();
		
		switch (item.getItemId()) {
		case 0: {
			Group g = groupList.get(info.position);
			deleteGroup(g.getId());
			return true;
		}
		default:
			return super.onContextItemSelected(item);
		}
	}

	private void deleteGroup(long id) {
		if(id == GROUP_DEFAULT_ALL) {
			Toast.makeText(GroupListActivity.this, "Can't delete the default group", Toast.LENGTH_SHORT).show();
		}
		else {
			database = dbhelper.getWritableDatabase();
			database.delete(GROUP_TABLE_NAME, "_id=" + id, null);
			database.close();
			getGroups();
		}
	}

	private void getGroups() {
		database = dbhelper.getWritableDatabase();
		try {
			Cursor sc = database.query(GROUP_TABLE_NAME, new String[] {
					"_id",GROUP_NAME, GROUP_DESCRIPTION, GROUP_ICON },
					null, null, null, null, null);

			sc.moveToFirst();
			Group g;
			groupList = new ArrayList<Group>();
			if (!sc.isAfterLast()) {
				do {
					g = new Group(sc.getInt(0), sc.getString(1), sc.getString(2), sc
							.getInt(3));
					groupList.add(g);
				} while (sc.moveToNext());
			}
			sc.close();
			database.close();
			Log.i("ARRAY", "" + groupList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		runOnUiThread(returnRes);
	}

	private Runnable returnRes = new Runnable() {

		@Override
		public void run() {
			groupAdapter.clear();
			if (groupList != null && groupList.size() > 0) {
				groupAdapter.notifyDataSetChanged();
				for (int i = 0; i < groupList.size(); i++)
					groupAdapter.add(groupList.get(i));
			} else {
				// Add button 'Add new group'
			}
			groupListProgress.dismiss();
			groupAdapter.notifyDataSetChanged();
		}
	};

}
