using Android.App;
using Android.OS;
using Android.Util;
using System;
using System.Collections.Generic;
using Android.Content;

namespace YaSTroid
{
	[Activity (Label = "SystemMessagesActivity")]
	public class SystemMessagesActivity : ListActivity
	{
		List<SystemMessage> messageList;

		ProgressDialog messageListProgress;
		SystemMessagesAdapter messageAdapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.system_messages);

			messageList = new List<SystemMessage>();
			messageAdapter = new SystemMessagesAdapter(this, Resource.Layout.system_messages_row, messageList);
			ListAdapter = messageAdapter;

			messageListProgress = ProgressDialog.Show(this, "Please wait...", "Retrieving data...", true);
			getMessages();
		}

		protected override void OnResume() {
			base.OnResume();
			getMessages();
		}

//		protected override void OnListItemClick (Android.Widget.ListView l, Android.Views.View v, int position, long id)
//		{
//			base.OnListItemClick (l, v, position, id);
//			var intent = new Intent (this, typeof(ServerActivity));
//			var s = messageList [position];
//			intent.PutExtra ("SERVER_PASS", s.getPass());
//			StartActivity (intent);
//		}

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
