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

namespace YaSTroid.Servers
{
	[Activity (Label = "ServerListActivity")]
	public class ServerListFragment : ListFragment
	{
		SQLiteDatabase _database;
		YastroidDatabase _dbhelper;
		List<Server> _serverList;

		ServerListAdapter _serverAdapter;

		int _groupId = 0;
		string _groupName;

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			SetEmptyText ("No servers found");
			SetHasOptionsMenu (true);

			_serverAdapter = new ServerListAdapter ();
			ListAdapter = _serverAdapter;

			SetListShown (true);
		}

		public override void OnCreateOptionsMenu (IMenu menu, MenuInflater inflater)
		{
			var item = menu.Add ("Add Server");
		}

//		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//		{
//			base.OnCreateView (inflater, container, savedInstanceState);
//			inflater.Inflate(Resource.Layout.serverlist);
//
//			Intent groupIntent = Intent;
//			Bundle b = groupIntent.Extras;
//			_groupId = b.GetInt("GROUP_ID");
//			_groupName = b.GetString("GROUP_NAME");
//
//			ListView.SetOnCreateContextMenuListener(this);
//
//			_dbhelper = new YastroidDatabase(this);
//
//			_serverList = new List<Server>();
//			_serverAdapter = new ServerListAdapter(this, Resource.Layout.serverlistrow, _serverList);
//			ListAdapter = _serverAdapter;
//
//			getServers();
//		}

		public override void OnResume()
		{
			base.OnResume();
			getServers();
		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			Intent intent = new Intent(Activity, typeof(ServerActivity));
			Server s = _serverList[position];
			intent.PutExtra("SERVER_ID", s.getId());
			intent.PutExtra("SERVER_NAME", s.getName());
			intent.PutExtra("SERVER_SCHEME", s.Scheme);
			intent.PutExtra("SERVER_HOSTNAME", s.Hostname);
			intent.PutExtra("SERVER_PORT", s.Port);
			intent.PutExtra("SERVER_USER", s.User);
			intent.PutExtra("SERVER_PASS", s.Password);
			StartActivity(intent);
		}


//		public bool onOptionsItemSelected(IMenuItem item)
//		{
//			switch (item.ItemId) {
//			case Resource.Id.add_server:
//				Intent intent = new Intent(this, typeof(ServerAddActivity));
//				intent.PutExtra("GROUP_ID", groupId);
//				StartActivity(intent);
//				return true;
//			}
//			return false;
//		}

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
				Server s = _serverList[info.Position];
				Intent intent = new Intent(Activity,
						typeof(ServerEditActivity));
				intent.PutExtra("SERVER_ID", s.getId());
				StartActivity(intent);
				return true;
			}
			case 1: {
				Server s = _serverList[info.Position];
				deleteServer(s.getId());
				return true;
			}
			default:
				return base.OnContextItemSelected(item);
			}
		}

		private void deleteServer(long id)
		{
			_database = _dbhelper.WritableDatabase;
			_database.Delete(YastroidDatabase.SERVERS_TABLE_NAME, "_id=" + id, null);
			_database.Close();
			getServers();
		}

		private void getServers()
		{
			_database = _dbhelper.ReadableDatabase;
			ICursor sc;
			try {
				if (_groupId == YastroidDatabase.GROUP_DEFAULT_ALL) {
					sc = _database.Query(YastroidDatabase.SERVERS_TABLE_NAME, new string[] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
							null, null, null, null, null);
				} else {
					sc = _database.Query(YastroidDatabase.SERVERS_TABLE_NAME, new string[] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
							YastroidDatabase.SERVERS_GROUP + "=" + _groupId, null, null, null, null);
				}

				sc.MoveToFirst();
				Server s;
				_serverList = new List<Server>();
				if (!sc.IsAfterLast)
				{
					do {
						s = new Server(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
								.GetString(3), sc.GetInt(4), sc.GetString(5), sc
								.GetString(6), sc.GetInt(7));
						_serverList.Add(s);
					} while (sc.MoveToNext());
				}
				sc.Close();
				_database.Close();
				Log.Info("ARRAY", "" + _serverList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			Activity.RunOnUiThread(() =>
			{
				_serverAdapter.Clear();
				if (_serverList != null && _serverList.Count > 0)
					{
					_serverAdapter.NotifyDataSetChanged();
					for (int i = 0; i < _serverList.Count; i++)
						_serverAdapter.Add(_serverList[i]);
				} else {
					// Add button 'Add new Server'
				}
				_serverAdapter.NotifyDataSetChanged();
			});
		}
	}
}
