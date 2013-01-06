using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Database.Sqlite;
using Android.Util;
using System;

namespace YaSTroid
{
	[Activity (Label = "GroupAddActivity")]
	public class GroupAddActivity : Activity
	{
		SQLiteDatabase database;
		Button addButton;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.groupadd);

			YastroidDatabase helper = new YastroidDatabase(this);
			database = helper.WritableDatabase;
			
			addButton = FindViewById<Button>(Resource.Id.button_add_group);
			addButton.Click += (sender, e) => {
				if(addGroup())
					Finish();
			};
		}

		bool addGroup()
		{
			bool result = false;
			try {
				
				string name = FindViewById<EditText>(Resource.Id.edit_group_name).Text;
				string description = FindViewById<EditText>(Resource.Id.edit_group_description).Text;
				string icon = FindViewById<EditText>(Resource.Id.edit_group_icon).Text;
				
				
				if (name.Length == 0 || description.Length == 0/* || icon.length() == 0*/)
				{
					Toast.MakeText(this, "One or more fields are empty", ToastLength.Short).Show();
					return false;
				}

				ContentValues values = new ContentValues();
				values.Put(YastroidDatabase.GROUP_NAME, name);
				values.Put(YastroidDatabase.GROUP_DESCRIPTION, description);
				values.Put(YastroidDatabase.GROUP_ICON, icon);
				
				database.Insert(YastroidDatabase.GROUP_TABLE_NAME, "null", values);
				database.Close();
				Log.Info("addGroup", name + " group has been added.");
				result = true;
			} catch (Exception e) {
				Log.Error("addGroup", e.Message);
			}
			Toast.MakeText(this, "Group Added", ToastLength.Short).Show();
			return result;
		}
	}
}
