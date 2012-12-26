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
		YastroidOpenHelper _dbhelper;
		List<ServerGroupItem> _serverList;

		ProgressDialog groupListProgress;
		ServerGroupListAdapter groupAdapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.server_group_list);
			ListView.SetOnCreateContextMenuListener(this);

			_dbhelper = new YastroidOpenHelper(this);

			_serverList = new List<ServerGroupItem>();
			groupAdapter = new ServerGroupListAdapter(this, Resource.Layout.server_group_item, _serverList);
			ListAdapter = groupAdapter;

//			groupView = new Runnable() {
//				@Override
//				public void run() {
//					getGroups();
//				}
//			};
			
//			Thread thread = new Thread(null, groupView, "ServerListBackground");
//			thread.start();
			groupListProgress = ProgressDialog.Show(this, "Please wait...",
					"Retrieving groups...", true);
			getGroups();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			getGroups();
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Intent intent = new Intent(this, typeof(ServerListActivity));
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

		public bool onOptionsItemSelected(IMenuItem item) {
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
			if(id == YastroidOpenHelper.GROUP_DEFAULT_ALL) {
				Toast.MakeText(this, "Can't delete the default group", ToastLength.Short).Show();
			}
			else {
				_database = _dbhelper.WritableDatabase;
				_database.Delete(YastroidOpenHelper.GROUP_TABLE_NAME, "_id=" + id, null);
				_database.Close();
				getGroups();
			}
		}

		void getGroups()
		{
			_database = _dbhelper.WritableDatabase;
			try {
				ICursor sc = _database.Query(YastroidOpenHelper.GROUP_TABLE_NAME, new string[] {
					"_id",YastroidOpenHelper.GROUP_NAME, YastroidOpenHelper.GROUP_DESCRIPTION, YastroidOpenHelper.GROUP_ICON },
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
				groupAdapter.Clear();
				if (_serverList != null && _serverList.Count > 0) {
					groupAdapter.NotifyDataSetChanged();
					for (int i = 0; i < _serverList.Count; i++)
						groupAdapter.Add(_serverList[i]);
				} else {
					// Add button 'Add new group'
				}
				groupListProgress.Dismiss();
				groupAdapter.NotifyDataSetChanged();

			});
		}
	}
}
