package com.novell.android.yastroid;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class ServerListAdapter extends ArrayAdapter<Server> {

	private ArrayList<Server> servers;
	private Context context;

	public ServerListAdapter(Context context, int textViewResourceId,
			ArrayList<Server> servers) {
		super(context, textViewResourceId, servers);
		this.servers = servers;
		this.context = context;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View serverView = convertView;
		if (serverView == null) {
			LayoutInflater vi = (LayoutInflater) context
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			serverView = vi.inflate(R.layout.serverlistrow, null);
		}

		Server s = servers.get(position);
		if (s != null) {
			TextView tt = (TextView) serverView.findViewById(R.id.toptext);
			TextView bt = (TextView) serverView.findViewById(R.id.bottomtext);
			if (tt != null) {
				tt.setText(s.getName());
			}
			if (bt != null) {
				bt.setText(s.getFullUrl());
			}
		}
		return serverView;
	}
}
