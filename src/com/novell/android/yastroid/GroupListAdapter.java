package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import java.util.ArrayList;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class GroupListAdapter extends ArrayAdapter<Group> {

	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;

	private ArrayList<Group> groups;
	private Context context;

	public GroupListAdapter(Context context, int textViewResourceId,
			ArrayList<Group> groups) {
		super(context, textViewResourceId, groups);
		this.groups = groups;
		this.context = context;
		dbhelper = new YastroidOpenHelper(context );
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View groupView = convertView;
		if (groupView == null) {
			LayoutInflater vi = (LayoutInflater) context
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			groupView = vi.inflate(R.layout.grouplistrow, null);
		}

		Group g = groups.get(position);
		ImageView icon = (ImageView) groupView.findViewById(R.id.module_icon);
		if (icon != null) {
			icon.setImageResource(g.getIcon());
		}
		if (g != null) {
			TextView tt = (TextView) groupView.findViewById(R.id.group_name);
			TextView bt = (TextView) groupView.findViewById(R.id.group_description);
			if (tt != null) {
				tt.setText(g.getName() + " (" + getServerCount(g.getId()) + ")");
			}
			if (bt != null) {
				bt.setText(g.getDescription());
			}
		}
		return groupView;
	}
	
	private int getServerCount(int id) {
		database = dbhelper.getWritableDatabase();
		int count = 0;
		try {
			Cursor sc = database.query(SERVERS_TABLE_NAME, new String[] {
					"_id", },
					"_id=" + id, null, null, null, null);

			count = sc.getCount();
			sc.close();
			database.close();
		} catch (Exception e) {
			Log.e("getServerCount", e.getMessage());
		}
		return count;
	}
}
