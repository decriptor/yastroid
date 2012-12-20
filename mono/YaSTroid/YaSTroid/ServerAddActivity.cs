using Android.App;
using Android.OS;
using Android.Database.Sqlite;
using Android.Widget;
using Android.Content;
using Android.Util;
using System;

namespace YaSTroid
{
	[Activity (Label = "ServerAddActivity")]
	public class ServerAddActivity : Activity
	{
		SQLiteDatabase database;
		Button addButton;
		int groupId;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.serveradd);

			YastroidOpenHelper helper = new YastroidOpenHelper(this);
			database = helper.getWritableDatabase();

			Intent groupIntent = Intent;
			Bundle b = groupIntent.Extras;
			groupId = b.GetInt("GROUP_ID");

			addButton = FindViewById<Button>(Resource.Id.button_add_server);
			addButton.Click += (sender, e) => {
				if(addServer())
					Finish();
			};
		}

		private bool addServer()
		{
			bool result = false;
			try {
				
				URI uri = new URI(FindViewById<EditText>(Resource.Id.edit_server_host).Text);
				string name = FindViewById<EditText>(Resource.Id.edit_server_name).Text;
				string scheme = null;
				string host = uri.getHost();
				string port = FindViewById<EditText>(Resource.Id.edit_server_port).Text;
				string user = FindViewById<EditText>(Resource.Id.edit_server_user).Text;
				string pass = FindViewById<EditText>(Resource.Id.edit_server_pass).Text;
				
				if (uri.getScheme() == null) {
					scheme = "http";
					host = uri.getSchemeSpecificPart();
				} else {
					scheme = uri.getScheme();
				}
				
				if (name.Length == 0 || host.Length == 0 || port.Length == 0 || user.Length == 0 || pass.Length == 0) {
					Toast.MakeText(this, "One or more fields are empty", ToastLength.Short).Show();
					return false;
				}

				ContentValues values = new ContentValues();
				values.Put(SERVERS_NAME, name);
				values.Put(SERVERS_SCHEME, scheme);
				values.Put(SERVERS_HOST, host);
				values.Put(SERVERS_PORT, port);
				values.Put(SERVERS_USER, user);
				values.Put(SERVERS_PASS, pass);
				values.Put(SERVERS_GROUP, groupId);
				
				database.Insert(SERVERS_TABLE_NAME, "null", values);
				database.Close();
				Log.Info("ARRAY", "WebYaST server " + name + " has been added.");
				result = true;
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}
			Toast.MakeText(this, "Server Added", ToastLength.Short).Show();
			return result;
		}
	}
}