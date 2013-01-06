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

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.serveradd);

			YastroidDatabase helper = new YastroidDatabase(this);
			database = helper.WritableDatabase;

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
				
				Uri uri = new Uri(FindViewById<EditText>(Resource.Id.edit_server_host).Text);
				string name = FindViewById<EditText>(Resource.Id.edit_server_name).Text;
				string scheme = null;
				string host = uri.Host;
				string port = FindViewById<EditText>(Resource.Id.edit_server_port).Text;
				string user = FindViewById<EditText>(Resource.Id.edit_server_user).Text;
				string pass = FindViewById<EditText>(Resource.Id.edit_server_pass).Text;
				
				if (uri.Scheme == null) {
					scheme = "http";
					host = uri.Port.ToString();
				} else {
					scheme = uri.Scheme;
				}
				
				if (name.Length == 0 || host.Length == 0 || port.Length == 0 || user.Length == 0 || pass.Length == 0) {
					Toast.MakeText(this, "One or more fields are empty", ToastLength.Short).Show();
					return false;
				}

				ContentValues values = new ContentValues();
				values.Put(YastroidDatabase.SERVERS_NAME, name);
				values.Put(YastroidDatabase.SERVERS_SCHEME, scheme);
				values.Put(YastroidDatabase.SERVERS_HOST, host);
				values.Put(YastroidDatabase.SERVERS_PORT, port);
				values.Put(YastroidDatabase.SERVERS_USER, user);
				values.Put(YastroidDatabase.SERVERS_PASS, pass);
				values.Put(YastroidDatabase.SERVERS_GROUP, groupId);
				
				database.Insert(YastroidDatabase.SERVERS_TABLE_NAME, "null", values);
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