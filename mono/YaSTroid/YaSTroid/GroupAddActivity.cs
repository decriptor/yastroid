using System;
using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Util;
using Android.Widget;


namespace YaSTroid
{
	[Activity (Label = "GroupAddActivity")]
	public class GroupAddActivity : Activity
	{
		private SQLiteDatabase database;
		private Button addButton;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.groupadd);

			YastroidOpenHelper helper = new YastroidOpenHelper(this);
			database = helper.WritableDatabase;
			
			addButton = FindViewById<Button>(Resource.Id.button_add_group);

			addButton.Click += OnClickedHandler;
		}

		void OnClickedHandler (object sender, EventArgs e)
		{
			addGroup ();
			Finish ();
		}

		private bool addGroup()
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
				values.Put(YastroidOpenHelper.GROUP_NAME, name);
				values.Put(YastroidOpenHelper.GROUP_DESCRIPTION, description);
				values.Put(YastroidOpenHelper.GROUP_ICON, icon);
				
				database.Insert(YastroidOpenHelper.GROUP_TABLE_NAME, "null", values);
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
