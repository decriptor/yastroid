using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	[Activity (Label = "GroupListActivity")]
	public class GroupListActivity : ListActivity
	{
		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;
		List<Group> groupList = null;

		GroupListAdapter groupAdapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.grouplist);
			ListView.SetOnCreateContextMenuListener(this);

			dbhelper = new YastroidOpenHelper(this);

			groupList = new List<Group>();
			ListAdapter = groupAdapter = new GroupListAdapter(this, Resource.Layout.grouplistrow, groupList);
		}

		protected override void OnResume()
		{
			base.OnResume();
			getGroups();
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);
			Intent intent = new Intent(this, typeof(ServerListActivity));
			Group g = groupList[position];
			intent.PutExtra("GROUP_ID", g.getId());
			intent.PutExtra("GROUP_NAME", g.getName());
			StartActivity(intent);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.grouplistmenu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Resource.Id.add_group:
				Intent intent = new Intent(this, typeof(GroupAddActivity));
				StartActivity(intent);
				return true;
			}
			return false;
		}

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
			menu.Add(0, 0, 0, "Delete");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			
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

		void deleteGroup(long id)
		{
			if(id == YastroidOpenHelper.GROUP_DEFAULT_ALL) {
				Toast.MakeText(this, "Can't delete the default group", ToastLength.Short).Show();
			}
			else {
				database = dbhelper.WritableDatabase;
				database.Delete(YastroidOpenHelper.GROUP_TABLE_NAME, "_id=" + id, null);
				database.Close();
				getGroups();
			}
		}

		void getGroups()
		{
			database = dbhelper.WritableDatabase;
			try {
				ICursor sc = database.Query(YastroidOpenHelper.GROUP_TABLE_NAME, new [] {
					"_id",YastroidOpenHelper.GROUP_NAME, YastroidOpenHelper.GROUP_DESCRIPTION, YastroidOpenHelper.GROUP_ICON },
						null, null, null, null, null);

				sc.MoveToFirst();
				Group g;
				groupList.Clear ();
				if (!sc.IsAfterLast) {
					do {
						g = new Group(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
							.GetInt(3));
						groupList.Add(g);
					} while (sc.MoveToNext());
				}
				sc.Close();
				database.Close();
				Log.Info("[Group Count]", "" + groupList.Count);
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}

			UpdateGroupAdapter ();
		}

		void UpdateGroupAdapter()
		{
			groupAdapter.Clear();
			if (groupList != null && groupList.Count > 0) {
				groupAdapter.NotifyDataSetChanged();
				for (int i = 0; i < groupList.Count; i++)
					groupAdapter.Add(groupList[i]);
			} else {
				// Add button 'Add new group'
			}
			groupAdapter.NotifyDataSetChanged();
		}
	}
}
