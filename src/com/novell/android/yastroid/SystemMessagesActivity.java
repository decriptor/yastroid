package com.novell.android.yastroid;

import java.util.ArrayList;

import android.app.ListActivity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.util.Log;

public class SystemMessagesActivity extends ListActivity {

	private ArrayList<SystemMessage> messageList = null;

	private ProgressDialog messageListProgress = null;
	private SystemMessagesAdapter messageAdapter;
	private Runnable messageView;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.system_messages);

		messageList = new ArrayList<SystemMessage>();
		this.messageAdapter = new SystemMessagesAdapter(this,
				R.layout.system_messages_row, messageList);
				setListAdapter(this.messageAdapter);

		messageView = new Runnable() {
			@Override
			public void run() {
				getMessages();
			}
		};
		
		Thread thread = new Thread(null, messageView, "MessageListBackground");
		thread.start();
		messageListProgress = ProgressDialog.show(this, "Please wait...",
				"Retrieving data...", true);
	}

	@Override
	protected void onResume() {
		super.onResume();
		getMessages();
	}

//	@Override
//	protected void onListItemClick(ListView l, View v, int position, long id) {
//		super.onListItemClick(l, v, position, id);
//		Intent intent = new Intent(ServerListActivity.this,
//				ServerActivity.class);
//		Server s = messageList.get(position);
//		intent.putExtra("SERVER_PASS", s.getPass());
//		startActivity(intent);
//	}

	private void getMessages() {
		try {
			SystemMessage m;
			messageList = new ArrayList<SystemMessage>();
			m = new SystemMessage("this is a system log message", "module", "date");
			messageList.add(m);
			Log.i("ARRAY", "" + messageList.size());
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		runOnUiThread(returnRes);
	}

	private Runnable returnRes = new Runnable() {

		@Override
		public void run() {
			messageAdapter.clear();
			if (messageList != null && messageList.size() > 0) {
				messageAdapter.notifyDataSetChanged();
				for (int i = 0; i < messageList.size(); i++)
					messageAdapter.add(messageList.get(i));
			}
			messageListProgress.dismiss();
			messageAdapter.notifyDataSetChanged();
		}
	};
} 