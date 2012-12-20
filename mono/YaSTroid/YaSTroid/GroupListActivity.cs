using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Database.Sqlite;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using System;

namespace YaSTroid
{
	[Activity (Label = "GroupListActivity")]
	public class GroupListActivity : ListActivity
	{
		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;
		List<Group> groupList = null;

		ProgressDialog groupListProgress = null;
		GroupListAdapter groupAdapter;
		Runnable groupView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.grouplist);
			ListView.SetOnCreateContextMenuListener(this);

			dbhelper = new YastroidOpenHelper(this);

			groupList = new List<Group>();
			groupAdapter = new GroupListAdapter(this,Resource.Layout.grouplistrow, groupList);
			ListAdapter = groupAdapter;

//			groupView = new Runnable() {
//				@Override
//				public void run() {
//					getGroups();
//				}
//			};
			
			Thread thread = new Thread(null, groupView, "ServerListBackground");
			thread.start();
			groupListProgress = ProgressDialog.Show(this, "Please wait...",
					"Retrieving groups...", true);
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
			Group g = groupList[position];
			intent.PutExtra("GROUP_ID", g.getId());
			intent.PutExtra("GROUP_NAME", g.getName());
			StartActivity(intent);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			return base.OnCreateOptionsMenu (menu);
			MenuInflater inflater = getMenuInflater();
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

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
				menu.Add(0, 0, 0, "Delete");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.MenuInfo;
			
			switch (item.ItemId) {
			case 0: {
				Group g = groupList[info.Position];
				deleteGroup(g.getId());
				return true;
			}
			default:
				return base.OnContextItemSelected(item);
			}
		}

		private void deleteGroup(long id) {
			if(id == GROUP_DEFAULT_ALL) {
				Toast.MakeText(this, "Can't delete the default group", ToastLength.Short).Show();
			}
			else {
				database = dbhelper.getWritableDatabase();
				database.Delete(GROUP_TABLE_NAME, "_id=" + id, null);
				database.Close();
				getGroups();
			}
		}

		private void getGroups() {
			database = dbhelper.getWritableDatabase();
			try {
				Cursor sc = database.query(GROUP_TABLE_NAME, new string[] {
						"_id",GROUP_NAME, GROUP_DESCRIPTION, GROUP_ICON },
						null, null, null, null, null);

				sc.moveToFirst();
				Group g;
				groupList = new ArrayList<Group>();
				if (!sc.isAfterLast()) {
					do {
						g = new Group(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
								.GetInt(3));
						groupList.add(g);
					} while (sc.moveToNext());
				}
				sc.close();
				database.Close();
				Log.Info("ARRAY", "" + groupList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}
			RunOnUiThread(returnRes);
		}

//		private Runnable returnRes = new Runnable() {
//
//			@Override
//			public void run() {
//				groupAdapter.clear();
//				if (groupList != null && groupList.size() > 0) {
//					groupAdapter.notifyDataSetChanged();
//					for (int i = 0; i < groupList.size(); i++)
//						groupAdapter.add(groupList.get(i));
//				} else {
//					// Add button 'Add new group'
//				}
//				groupListProgress.dismiss();
//				groupAdapter.notifyDataSetChanged();
//			}
//		};

	}
}
