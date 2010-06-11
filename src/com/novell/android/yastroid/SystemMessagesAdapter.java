package com.novell.android.yastroid;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class SystemMessagesAdapter extends ArrayAdapter<SystemMessage> {

	private ArrayList<SystemMessage> messages;
	private Context context;

	public SystemMessagesAdapter(Context context, int textViewResourceId,
			ArrayList<SystemMessage> messages) {
		super(context, textViewResourceId, messages);
		this.messages = messages;
		this.context = context;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View messageView = convertView;
		if (messageView == null) {
			LayoutInflater vi = (LayoutInflater) context
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			messageView = vi.inflate(R.layout.system_messages_row, null);
		}

		SystemMessage s = messages.get(position);
		if (s != null) {
			TextView tt = (TextView) messageView.findViewById(R.id.toptext);
			TextView bt = (TextView) messageView.findViewById(R.id.bottomtext);
			if (tt != null) {
				tt.setText(s.getMessage());
			}
			if (bt != null) {
				bt.setText(s.getModule() + ", " + s.getDate());
			}
		}
		return messageView;
	}
}
