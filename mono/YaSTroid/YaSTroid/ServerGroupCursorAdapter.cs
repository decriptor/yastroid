using System;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	public class ServerGroupCursorAdapter : CursorAdapter
	{
		Activity _context;

		public ServerGroupCursorAdapter(Activity context, ICursor cursor) : base(context, cursor)
		{
			_context = context;
		}

		#region implemented abstract members of CursorAdapter
		public override void BindView (View view, Context context, ICursor cursor)
		{
			var icon = view.FindViewById<ImageView>(Resource.Id.module_icon);
			var name = view.FindViewById<TextView>(Resource.Id.group_name);
			var description = view.FindViewById<TextView>(Resource.Id.group_description);

			var groupIcon = cursor.GetInt(YastroidDatabase.GROUP_ICON);
			string groupName = cursor.GetString(YastroidDatabase.GROUP_NAME);
			string groupCountString = "(" + cursor.GetInt("_id") + ")";
			string groupDescription = cursor.GetString (YastroidDatabase.GROUP_DESCRIPTION);

			icon.SetImageResource(groupIcon);

			name.Text = cursor.GetString(groupName+groupCountString);
			description.Text = cursor.GetString(groupDescription);
		}
		
		public override View NewView (Context context, ICursor cursor, ViewGroup parent)
		{
			return _context.LayoutInflater.Inflate(Resource.Layout.server_group_item, parent, false);
		}
		#endregion

		int getServerCount(int id)
		{
			var _database = new YastroidDatabase(this).ReadableDatabase;
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
	}
}
