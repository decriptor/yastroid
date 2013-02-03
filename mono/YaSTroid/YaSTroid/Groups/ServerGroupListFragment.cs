using Android.App;
using Android.Content;
using Android.OS;
using Android.Database;
using Android.Widget;
using System;
using Android.Util;
using Android.Views;

namespace YaSTroid.Groups
{
	[Activity (Label = "ServerGroupListActivity")]
	public class ServerGroupListFragment : ListFragment
	{
		YastroidDatabase _dbhelper;

		ServerGroupAdapter _groupCursorAdapter;

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			SetEmptyText("No server groups found");
			SetHasOptionsMenu(true);

			_groupCursorAdapter = new ServerGroupAdapter(Activity, );
			ListAdapter = _groupCursorAdapter;

			SetListShown(true);
		}

		public override void OnCreateOptionsMenu (IMenu menu, MenuInflater inflater)
		{
			var item = menu.Add("Add Group");
		}

//		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//		{
//			base.OnCreateView (inflater, container, savedInstanceState);
//			View v = inflater.Inflate(Resource.Layout.server_group_list, container);
//
//			_dbhelper = new YastroidDatabase(Activity);
//			_cursor = _dbhelper.FetchAllServerGroups();
//			ListView.SetOnCreateContextMenuListener(this);
//
//			_dbhelper = new YastroidDatabase(Activity);
//
//			_groupCursorAdapter = new ServerGroupAdapter(Activity, _cursor);
//			ListView.Adapter = ListAdapter = _groupCursorAdapter;
//
//			return v;
//		}
		             
		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
//			base.OnListItemClick (l, v, position, id);
//
//			if(_cursor.MoveToPosition(position))
//			{
				// FIXME notify activity that the server list fragment needs to update its
				// list of servers for that group
				Log.Info("ServerGroupListFragment", "Item clicked: " + id);
//				intent.PutExtra("GROUP_ID", _cursor.GetInt(0));
//				intent.PutExtra("GROUP_NAME", _cursor.GetString(1));
//				StartActivity(intent);

//			}
		}


//		public bool OnOptionsItemSelected(IMenuItem item)
//		{
//			switch (item.ItemId) {
//			case Resource.Id.add_group:
//				AddGroup();
//				return true;
//			}
//			return base.OnOptionsItemSelected(item);
//		}

//		public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
//		{
//			base.OnCreateContextMenu (menu, v, menuInfo);
//				menu.Add(0, 0, 0, "Delete");
//		}

//		public override bool OnContextItemSelected(IMenuItem item)
//		{
//			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.MenuInfo;
//			
//			switch (item.ItemId)
//			{
//				case 0:
//					ServerGroupItem g = _serverList[info.Position];
//					DeleteGroup(g.Id);
//					return true;
//				default:
//					return base.OnContextItemSelected(item);
//			}
//		}

		void AddGroup()
		{
			var intent = new Intent(Activity, typeof(GroupAddActivity));
			StartActivity(intent);
		}

//		void DeleteGroup(long id)
//		{
//			if(id == YastroidDatabase.GROUP_DEFAULT_ALL) {
//				Toast.MakeText(this, "Can't delete the default group", ToastLength.Short).Show();
//			}
//			else {
//				var database = _dbhelper.WritableDatabase;
//				database.Delete(YastroidDatabase.GROUP_TABLE_NAME, "_id=" + id, null);
//				database.Close();
//				GetGroups();
//			}
//		}

//		void GetGroups()
//		{
//			var database = _dbhelper.WritableDatabase;
//			try {
//				ICursor sc = database.Query(YastroidDatabase.GROUP_TABLE_NAME, new string[] {
//					"_id",YastroidDatabase.GROUP_NAME, YastroidDatabase.GROUP_DESCRIPTION, YastroidDatabase.GROUP_ICON },
//						null, null, null, null, null);
//
//				sc.MoveToFirst();
//				ServerGroupItem g;
//				_serverList = new List<ServerGroupItem>();
//				if (!sc.IsAfterLast) {
//					do {
//						g = new ServerGroupItem(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
//								.GetInt(3));
//						_serverList.Add(g);
//					} while (sc.MoveToNext());
//				}
//				sc.Close();
//				database.Close();
//				Log.Info("ARRAY", "" + _serverList.Count);
//			} catch (Exception e) {
//				Log.Error("BACKGROUND_PROC", e.Message);
//			}
//
//			RunOnUiThread(() =>
//			{
//				_groupCursorAdapter.Clear();
//				if (_serverList != null && _serverList.Count > 0) {
//					_groupCursorAdapter.NotifyDataSetChanged();
//					for (int i = 0; i < _serverList.Count; i++)
//						_groupCursorAdapter.Add(_serverList[i]);
//				} else {
//					// Add button 'Add new group'
//				}
//				groupListProgress.Dismiss();
//				_groupCursorAdapter.NotifyDataSetChanged();
//
//			});
//		}
	}
}
