using System;
using System.Collections.Generic;

using Android.App;
using Android.Database;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YaSTroid.Groups
{
	public class ServerGroupListAdapter : BaseAdapter
	{
		Activity _activity;
		List<ServerGroupItem> _groups;

		public ServerGroupListAdapter(Activity activity, List<ServerGroupItem> groups) : base()
		{
			_activity = activity;
			_groups = groups;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var group = _groups [position];

			var view = (convertView ??
				_activity.LayoutInflater.Inflate (
						Resource.Layout.server_group_item,
						parent,
						false)) as LinearLayout;

			var icon = view.FindViewById<ImageView> (Resource.Id.module_icon);
			var name = view.FindViewById<TextView> (Resource.Id.module_name);
			var description = view.FindViewById<TextView> (Resource.Id.group_description);

//			icon.SetImageResource (group.Icon);
			name.Text = group.Name + " (" + getServerCount (group.Id) + ")";
			description.Text = group.Description;
			return view;
		}

		int getServerCount(int id)
		{
			var _database = new YastroidDatabase(_activity).ReadableDatabase;
			ICursor sc;
			int count = 0;
			try {
				if (id == YastroidDatabase.GROUP_DEFAULT_ALL) {
					sc = _database.Query(YastroidDatabase.SERVERS_TABLE_NAME, new string[] {
							"_id", },
							null, null, null, null, null);
				} else {
					sc = _database.Query(YastroidDatabase.SERVERS_TABLE_NAME, new string[] {
						"_id", },
						"_id=" + id, null, null, null, null);
				}

				count = sc.Count;
				sc.Close();
				_database.Close();
			} catch (Exception e) {
				Log.Error("getServerCount", e.Message);
			}
			return count;
		}

		#region implemented abstract members of BaseAdapter
		public override int Count
		{ 
			get { return _groups.Count; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return position;
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		#endregion
	}
}
