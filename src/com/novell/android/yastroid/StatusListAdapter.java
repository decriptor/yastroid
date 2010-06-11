package com.novell.android.yastroid;

import java.util.ArrayList;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class StatusListAdapter extends ArrayAdapter<SystemStatus> {
	private ArrayList<SystemStatus> statii;
	private Context context;
	
	public StatusListAdapter(Context context, int textViewResourceId, ArrayList<SystemStatus> statii) {
		super(context, textViewResourceId, statii);
		this.statii = statii;
		this.context = context;
	}
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View systemStatusView = convertView;
		TextView name;
		ImageView icon;
		SystemStatus systemStatus;
		
		if (systemStatusView == null) {
			LayoutInflater vi = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			systemStatusView = vi.inflate(R.layout.system_status_list_item, null);
		}
		
		systemStatus = statii.get(position);
		if (systemStatus != null) {
			name = (TextView) systemStatusView.findViewById(R.id.system_status_text);
			icon = (ImageView) systemStatusView.findViewById(R.id.system_status_icon);
			if (name != null) {
				name.setText(systemStatus.getName());
			}
			if (icon != null) {
				icon.setImageDrawable(systemStatus.getIcon());
			}
		}
		return systemStatusView;
	}	
	}