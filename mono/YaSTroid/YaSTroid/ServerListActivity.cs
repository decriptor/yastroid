using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	[Activity (Label = "ServerListActivity")]
	public class ServerListActivity : ListActivity
	{
		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;
		List<Server> serverList;

		ProgressDialog serverListProgress;
		ServerListAdapter serverAdapter;
//		Runnable serverView;
		
		int groupId = 0;
		string groupName;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Intent groupIntent = Intent;
			Bundle b = groupIntent.Extras;
			groupId = b.GetInt("GROUP_ID");
			groupName = b.GetString("GROUP_NAME");

			SetContentView(Resource.Layout.serverlist);
			ListView.SetOnCreateContextMenuListener(this);

			dbhelper = new YastroidOpenHelper(this);

			serverList = new List<Server>();
			serverAdapter = new ServerListAdapter(this, Resource.Layout.serverlistrow, serverList);
			ListAdapter = serverAdapter;

	//		serverView = new Runnable() {
	//			@Override
	//			public void run() {
	//				getServers();
	//			}
	//		};
			
//			Thread thread = new Thread(null, serverView, "ServerListBackground");
//			thread.start();
			serverListProgress = ProgressDialog.Show(this, "Please wait...", "Retrieving data...", true);
			getServers();
		}

		protected override void OnResume()
			{
			base.OnResume();
			getServers();
		}

		protected override void OnListItemClick (Android.Widget.ListView l, View v, int position, long id)
		{
			Intent intent = new Intent(this, typeof(ServerActivity));
			Server s = serverList[position];
			intent.PutExtra("SERVER_ID", s.getId());
			intent.PutExtra("SERVER_NAME", s.getName());
			intent.PutExtra("SERVER_SCHEME", s.Scheme);
			intent.PutExtra("SERVER_HOSTNAME", s.Hostname);
			intent.PutExtra("SERVER_PORT", s.Port);
			intent.PutExtra("SERVER_USER", s.User);
			intent.PutExtra("SERVER_PASS", s.Password);
			StartActivity(intent);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = MenuInflater;
			inflater.Inflate(Resource.Menu.serverlistmenu, menu);
			return true;
		}

		public bool onOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.add_server:
				Intent intent = new Intent(this, typeof(ServerAddActivity));
				intent.PutExtra("GROUP_ID", groupId);
				StartActivity(intent);
				return true;
			}
			return false;
		}

		public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
				menu.Add(0, 0, 0, "Edit");
				menu.Add(0, 1, 0, "Delete");
		}

		public override bool OnContextItemSelected (IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.MenuInfo;
			
			switch (item.ItemId) {
			case 0: {
				Server s = serverList[info.Position];
				Intent intent = new Intent(this,
						typeof(ServerEditActivity));
				intent.PutExtra("SERVER_ID", s.getId());
				StartActivity(intent);
				return true;
			}
			case 1: {
				Server s = serverList[info.Position];
				deleteServer(s.getId());
				return true;
			}
			default:
				return base.OnContextItemSelected(item);
			}
		}

		private void deleteServer(long id)
		{
			database = dbhelper.WritableDatabase;
			database.Delete(YastroidOpenHelper.SERVERS_TABLE_NAME, "_id=" + id, null);
			database.Close();
			getServers();
		}

		private void getServers()
		{
			database = dbhelper.ReadableDatabase;
			ICursor sc;
			try {
				if (groupId == YastroidOpenHelper.GROUP_DEFAULT_ALL) {
					sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new string[] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
							null, null, null, null, null);
				} else {
					sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new string[] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
							YastroidOpenHelper.SERVERS_GROUP + "=" + groupId, null, null, null, null);
				}

				sc.MoveToFirst();
				Server s;
				serverList = new List<Server>();
				if (!sc.IsAfterLast)
				{
					do {
						s = new Server(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
								.GetString(3), sc.GetInt(4), sc.GetString(5), sc
								.GetString(6), sc.GetInt(7));
						serverList.Add(s);
					} while (sc.MoveToNext());
				}
				sc.Close();
				database.Close();
				Log.Info("ARRAY", "" + serverList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			RunOnUiThread(() =>
			{
				serverAdapter.Clear();
				if (serverList != null && serverList.Count > 0)
					{
					serverAdapter.NotifyDataSetChanged();
					for (int i = 0; i < serverList.Count; i++)
						serverAdapter.Add(serverList[i]);
				} else {
					// Add button 'Add new Server'
				}
				serverListProgress.Dismiss();
				serverAdapter.NotifyDataSetChanged();
			});
		}

//		private Runnable returnRes = new Runnable() {
//
//			@Override
//			public void run() {
//				serverAdapter.clear();
//				if (serverList != null && serverList.size() > 0) {
//					serverAdapter.notifyDataSetChanged();
//					for (int i = 0; i < serverList.size(); i++)
//						serverAdapter.add(serverList.get(i));
//				} else {
//					// Add button 'Add new Server'
//				}
//				serverListProgress.dismiss();
//				serverAdapter.notifyDataSetChanged();
//			}
//		};

	}
}
