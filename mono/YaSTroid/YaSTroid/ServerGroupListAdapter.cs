using Android.Util;
using Android.Database.Sqlite;
using Android.Widget;
using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System;
using Android.Database;
using Android.App;

namespace YaSTroid
{
	public class ServerGroupListAdapter : ArrayAdapter<ServerGroupItem>
	{
		SQLiteDatabase _database;
		YastroidOpenHelper _dbhelper;

		List<ServerGroupItem> _serverGroups;
		Context _context;

		public ServerGroupListAdapter(Context context, int textViewResourceId, List<ServerGroupItem> groups) : base(context, textViewResourceId, groups)
		{
			_serverGroups = groups;
			_context = context;
			_dbhelper = new YastroidOpenHelper(context );
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View groupView = convertView ?? (_context as Activity).LayoutInflater.Inflate(
				Resource.Layout.server_group_item, parent, false);

			var serverItem = _serverGroups[position];
			var icon = groupView.FindViewById<ImageView>(Resource.Id.module_icon);

			if (icon != null) {
				icon.SetImageResource(serverItem.Icon);
			}
			if (serverItem != null) {
				TextView groupName = groupView.FindViewById<TextView>(Resource.Id.group_name);
				TextView groupDescription = groupView.FindViewById<TextView>(Resource.Id.group_description);
				if (groupName != null) {
					groupName.Text = serverItem.Name + " (" + getServerCount(serverItem.Id) + ")";
				}
				if (groupDescription != null) {
					groupDescription.Text = serverItem.Description;

				}
			}
			return groupView;
		}
		
		private int getServerCount(int id)
		{
			_database = _dbhelper.WritableDatabase;
			ICursor sc;
			int count = 0;
			try {
				if (id == YastroidOpenHelper.GROUP_DEFAULT_ALL) {
					sc = _database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new string[] {
							"_id", },
							null, null, null, null, null);
				} else {
					sc = _database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new string[] {
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
	}
}
