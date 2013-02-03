using System;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace YaSTroid.Groups
{
	public class ServerGroupAdapter : BaseAdapter
	{
		Activity _activity;
		List<ServerGroupItem> _groups;

		public ServerGroupAdapter(Activity activity) : base()
		{
			_activity = activity;
		}



//		public override void BindView (View view, Context context, ICursor cursor)
//		{
//			var icon = view.FindViewById<ImageView>(Resource.Id.module_icon);
//			var name = view.FindViewById<TextView>(Resource.Id.group_name);
//			var description = view.FindViewById<TextView>(Resource.Id.group_description);
//
//			var groupIcon = cursor.GetInt(3);
//			var groupName = cursor.GetString(1);
//			var groupCountString = "(" + cursor.GetInt(0) + ")";
//			var groupDescription = cursor.GetString (2);
//
//			//icon.SetImageResource(groupIcon);
//
//			name.Text = groupName+groupCountString;
//			description.Text = groupDescription;
//		}
//		
//		public override View NewView (Context context, ICursor cursor, ViewGroup parent)
//		{
//			return (_context as Activity).LayoutInflater.Inflate(Resource.Layout.server_group_item, parent, false);
//		}

		int getServerCount(int id)
		{
			var _database = new YastroidDatabase(_context).ReadableDatabase;
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
		public override Java.Lang.Object GetItem (int position)
		{
			throw new NotImplementedException ();
		}

		public override long GetItemId (int position)
		{
			throw new NotImplementedException ();
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			throw new NotImplementedException ();
		}

		public override int Count {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}
