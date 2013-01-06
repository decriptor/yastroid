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
	[Activity (Label = "ServerGroupListActivity")]
	public class ServerGroupListActivity : ListActivity
	{
		SQLiteDatabase _database;
		YastroidDatabase _dbhelper;
		ICursor _cursor;

		ProgressDialog groupListProgress;
		ServerGroupCursorAdapter _groupCursorAdapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.server_group_list);
			_dbhelper = new YastroidDatabase(this);
			_cursor = _dbhelper.ReadableDatabase.RawQuery("SELECT * FROM "+YastroidDatabase.GROUP_TABLE_NAME, null);
			StartManagingCursor(_cursor);
			ListView.SetOnCreateContextMenuListener(this);

			_dbhelper = new YastroidDatabase(this);

			_groupCursorAdapter = new ServerGroupCursorAdapter(this, _cursor);
			ListView.Adapter = ListAdapter = _groupCursorAdapter;

			groupListProgress = ProgressDialog.Show(this, "Please wait...",
					"Retrieving groups...", true);
			getGroups();
		}

		protected override void OnDestroy ()
		{
			StopManagingCursor(_cursor);
			base.OnDestroy ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			StartManagingCursor(_cursor);
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			_cursor.MoveToPosition(position);
			var intent = new Intent(this, typeof(ServerListActivity));
			ServerGroupItem g = _serverList[position];
			intent.PutExtra("GROUP_ID", g.Id);
			intent.PutExtra("GROUP_NAME", g.Name);
			StartActivity(intent);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = MenuInflater;
			inflater.Inflate(Resource.Menu.grouplistmenu, menu);
			return true;
		}

		public bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.add_group:
				Intent intent = new Intent(this, typeof(GroupAddActivity));
				StartActivity(intent);
				return true;
			}
			return false;
		}

		public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu (menu, v, menuInfo);
				menu.Add(0, 0, 0, "Delete");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.MenuInfo;
			
			switch (item.ItemId) {
			case 0: {
				ServerGroupItem g = _serverList[info.Position];
				deleteGroup(g.Id);
				return true;
			}
			default:
				return base.OnContextItemSelected(item);
			}
		}

		void deleteGroup(long id)
		{
			if(id == YastroidDatabase.GROUP_DEFAULT_ALL) {
				Toast.MakeText(this, "Can't delete the default group", ToastLength.Short).Show();
			}
			else {
				_database = _dbhelper.WritableDatabase;
				_database.Delete(YastroidDatabase.GROUP_TABLE_NAME, "_id=" + id, null);
				_database.Close();
				getGroups();
			}
		}

		void getGroups()
		{
			_database = _dbhelper.WritableDatabase;
			try {
				ICursor sc = _database.Query(YastroidDatabase.GROUP_TABLE_NAME, new string[] {
					"_id",YastroidDatabase.GROUP_NAME, YastroidDatabase.GROUP_DESCRIPTION, YastroidDatabase.GROUP_ICON },
						null, null, null, null, null);

				sc.MoveToFirst();
				ServerGroupItem g;
				_serverList = new List<ServerGroupItem>();
				if (!sc.IsAfterLast) {
					do {
						g = new ServerGroupItem(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
								.GetInt(3));
						_serverList.Add(g);
					} while (sc.MoveToNext());
				}
				sc.Close();
				_database.Close();
				Log.Info("ARRAY", "" + _serverList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			RunOnUiThread(() =>
			{
				_groupCursorAdapter.Clear();
				if (_serverList != null && _serverList.Count > 0) {
					_groupCursorAdapter.NotifyDataSetChanged();
					for (int i = 0; i < _serverList.Count; i++)
						_groupCursorAdapter.Add(_serverList[i]);
				} else {
					// Add button 'Add new group'
				}
				groupListProgress.Dismiss();
				_groupCursorAdapter.NotifyDataSetChanged();

			});
		}
	}
}
