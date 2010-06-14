package com.novell.android.yastroid;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class GroupListAdapter extends ArrayAdapter<Group> {

	private ArrayList<Group> groups;
	private Context context;

	public GroupListAdapter(Context context, int textViewResourceId,
			ArrayList<Group> groups) {
		super(context, textViewResourceId, groups);
		this.groups = groups;
		this.context = context;
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
				tt.setText(g.getName());
			}
			if (bt != null) {
				bt.setText(g.getDescription());
			}
		}
		return groupView;
	}
}
