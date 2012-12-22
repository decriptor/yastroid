using Android.App;
using Android.OS;
using Android.Util;
using System;
using System.Collections.Generic;

namespace YaSTroid
{
	[Activity (Label = "SystemMessagesActivity")]
	public class SystemMessagesActivity : ListActivity
	{
		List<SystemMessage> messageList = null;

		ProgressDialog messageListProgress = null;
		SystemMessagesAdapter messageAdapter;
		//Runnable messageView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.system_messages);

			messageList = new List<SystemMessage>();
			messageAdapter = new SystemMessagesAdapter(this, Resource.Layout.system_messages_row, messageList);
			ListAdapter = messageAdapter;

//			messageView = new Runnable() {
//				@Override
//				public void run() {
//					getMessages();
//				}
//			};
			
//			Thread thread = new Thread(null, messageView, "MessageListBackground");
//			thread.start();
			getMessages();
			messageListProgress = ProgressDialog.Show(this, "Please wait...",
					"Retrieving data...", true);
		}

		protected override void OnResume() {
			base.OnResume();
			getMessages();
		}

	//	@Override
	//	protected void onListItemClick(ListView l, View v, int position, long id) {
	//		base.onListItemClick(l, v, position, id);
	//		Intent intent = new Intent(ServerListActivity.this,
	//				ServerActivity.class);
	//		Server s = messageList.get(position);
	//		intent.putExtra("SERVER_PASS", s.getPass());
	//		startActivity(intent);
	//	}

		void getMessages()
		{
			try
			{
				SystemMessage m;
				messageList = new List<SystemMessage>();
				m = new SystemMessage("this is a system log message", "module", "date");
				messageList.Add(m);
				Log.Info("ARRAY", "" + messageList.Count);
			}
			catch (Exception e)
			{
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			RunOnUiThread(()=>
			{
				messageAdapter.Clear();
				if (messageList != null && messageList.Count > 0)
				{
					messageAdapter.NotifyDataSetChanged();

					for (int i = 0; i < messageList.Count; i++)
						messageAdapter.Add(messageList[i]);
				}
				messageListProgress.Dismiss();
				messageAdapter.NotifyDataSetChanged();
			});
		}
	}
}
