using Android.Util;
using Android.Database.Sqlite;
using Android.Widget;
using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System;

namespace YaSTroid
{
	public class GroupListAdapter : ArrayAdapter<Group>
	{

		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;

		List<Group> groups;
		Context context;

		public GroupListAdapter(Context context, int textViewResourceId, List<Group> groups) : base(context, textViewResourceId, groups)
		{
			this.groups = groups;
			this.context = context;
			dbhelper = new YastroidOpenHelper(context );
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View groupView = convertView;
			if (groupView == null) {
				LayoutInflater vi = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
				groupView = vi.Inflate(Resource.Layout.grouplistrow, null);
			}

			Group g = groups.get(position);
			ImageView icon = groupView.FindViewById<ImageView>(Resource.Id.module_icon);

			if (icon != null) {
				icon.SetImageResource(g.getIcon());
			}
			if (g != null) {
				TextView tt = groupView.FindViewById<TextView>(Resource.Id.group_name);
				TextView bt = groupView.FindViewById<TextView>(Resource.Id.group_description);
				if (tt != null) {
					tt.Text = g.getName() + " (" + getServerCount(g.getId()) + ")";
				}
				if (bt != null) {
					bt.Text = g.getDescription();
				}
			}
			return groupView;
		}
		
		private int getServerCount(int id)
		{
			database = dbhelper.getWritableDatabase();
			Cursor sc;
			int count = 0;
			try {
				if (id == GROUP_DEFAULT_ALL) {
					sc = database.query(SERVERS_TABLE_NAME, new string[] {
							"_id", },
							null, null, null, null, null);
				} else {
					sc = database.query(SERVERS_TABLE_NAME, new string[] {
						"_id", },
						"_id=" + id, null, null, null, null);
				}

				count = sc.getCount();
				sc.close();
				database.Close();
			} catch (Exception e) {
				Log.Error("getServerCount", e.Message);
			}
			return count;
		}
	}
}
