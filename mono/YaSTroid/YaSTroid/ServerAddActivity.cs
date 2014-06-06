using System;
using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Util;
using Android.Widget;
using Java.Net;

namespace YaSTroid
{
	[Activity (Label = "ServerActivity")]
	public class ServerAddActivity : Activity
	{
		private SQLiteDatabase database;
		private Button addButton;
		private int groupId;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.serveradd);

			YastroidOpenHelper helper = new YastroidOpenHelper(this);
			database = helper.WritableDatabase;

			Intent groupIntent = Intent;
			Bundle b = groupIntent.Extras;
			groupId = b.GetInt("GROUP_ID");

			this.addButton = FindViewById<Button>(Resource.Id.button_add_server);
			this.addButton.Click += (sender, e) => {
				if (addServer ()) {
					Finish ();
				}
			};				
		}
		

		private bool addServer()
		{
			bool result = false;
			try
			{	
				URI uri = new URI(FindViewById<EditText>(Resource.Id.edit_server_host).Text);
				string name = FindViewById<EditText>(Resource.Id.edit_server_name).Text;
				string scheme = null;
				string host = uri.Host;
				string port = FindViewById<EditText>(Resource.Id.edit_server_port).Text;
				string user = FindViewById<EditText>(Resource.Id.edit_server_user).Text;
				string pass = FindViewById<EditText>(Resource.Id.edit_server_pass).Text;
				
				if (uri.Scheme == null) {
					scheme = "http";
					host = uri.SchemeSpecificPart;
				} else {
					scheme = uri.Scheme;
				}
				
				if (name.Length == 0 || host.Length == 0 || port.Length == 0 || user.Length == 0 || pass.Length == 0) {
					Toast.MakeText(this, "One or more fields are empty", ToastLength.Short).Show();
					return false;
				}

				ContentValues values = new ContentValues();
				values.Put(YastroidOpenHelper.SERVERS_NAME, name);
				values.Put(YastroidOpenHelper.SERVERS_SCHEME, scheme);
				values.Put(YastroidOpenHelper.SERVERS_HOST, host);
				values.Put(YastroidOpenHelper.SERVERS_PORT, port);
				values.Put(YastroidOpenHelper.SERVERS_USER, user);
				values.Put(YastroidOpenHelper.SERVERS_PASS, pass);
				values.Put(YastroidOpenHelper.SERVERS_GROUP, groupId);
				
				database.Insert(YastroidOpenHelper.SERVERS_TABLE_NAME, "null", values);
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
