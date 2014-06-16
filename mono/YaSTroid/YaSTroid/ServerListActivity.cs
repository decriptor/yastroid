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
	[Activity (Label = "ServerActivity")]
	public class ServerListActivity : ListActivity
	{
		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;
		List<Server> serverList;

		ServerListAdapter serverAdapter;

		int groupId = 0;
		string groupName;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Bundle b = Intent.Extras;
			groupId = b.GetInt("GROUP_ID");
			groupName = b.GetString("GROUP_NAME");

			SetContentView(Resource.Layout.serverlist);
			ListView.SetOnCreateContextMenuListener (this);

			dbhelper = new YastroidOpenHelper(this);

			serverList = new List<Server>();
			ListAdapter = serverAdapter = new ServerListAdapter (this, Resource.Layout.serverlistrow, serverList);
		}

		protected override void OnResume()
		{
			base.OnResume();
			getServers();
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);
			Intent intent = new Intent(this, typeof(ServerActivity));
			Server s = serverList[position];
			intent.PutExtra("SERVER_ID", s.getId());
			intent.PutExtra("SERVER_NAME", s.getName());
			intent.PutExtra("SERVER_SCHEME", s.getScheme());
			intent.PutExtra("SERVER_HOSTNAME", s.getHostname());
			intent.PutExtra("SERVER_PORT", s.getPort());
			intent.PutExtra("SERVER_USER", s.getUser());
			intent.PutExtra("SERVER_PASS", s.getPass());
			StartActivity(intent);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.serverlistmenu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.add_server:
				var intent = new Intent (this, typeof(ServerAddActivity));
				intent.PutExtra ("GROUP_ID", groupId);
				StartActivity (intent);
				return true;
			default:
				return base.OnOptionsItemSelected (item);
			}
		}

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
				menu.Add(0, 0, 0, "Edit");
				menu.Add(0, 1, 0, "Delete");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			
			switch (item.ItemId) {
			case 0: {
					Server s = serverList[info.Position];
					Intent intent = new Intent(this, typeof(ServerEditActivity));
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

		void deleteServer(long id)
		{
			serverAdapter.Remove (id);
//			database = dbhelper.WritableDatabase;
//			database.Delete(YastroidOpenHelper.SERVERS_TABLE_NAME, "_id=" + id, null);
//			database.Close();
//			getServers();
		}

		void getServers()
		{
			database = dbhelper.ReadableDatabase;
			ICursor sc;
			try {
				if (groupId == YastroidOpenHelper.GROUP_DEFAULT_ALL) {
					sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new [] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
							null, null, null, null, null);
				} else {
					sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new [] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
						YastroidOpenHelper.SERVERS_GROUP + "=" + groupId, null, null, null, null);
				}

				sc.MoveToFirst();
				Server s;
				serverList.Clear ();
				if (!sc.IsAfterLast) {
					do {
						s = new Server(sc.GetInt(0), sc.GetString(1), sc.GetString(2),
							sc.GetString(3), sc.GetInt(4), sc.GetString(5),
							sc.GetString(6), sc.GetInt(7));
						serverList.Add(s);
					} while (sc.MoveToNext());
				}
				sc.Close();
				database.Close();
				Log.Info("Server Array", "" + serverList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			serverAdapter.Clear();
			if (serverList != null && serverList.Count > 0) {
				for (int i = 0; i < serverList.Count; i++)
					serverAdapter.Add(serverList[i]);
			} else {
				// Add button 'Add new Server'
			}
			serverAdapter.NotifyDataSetChanged();
		}
	}
}
