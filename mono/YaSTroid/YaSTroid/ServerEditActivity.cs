using System;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Util;
using Android.Widget;
using Java.Net;

namespace YaSTroid
{
	[Activity (Label = "ServerActivity")]
	public class ServerEditActivity : Activity
	{

		private SQLiteDatabase database;
		private YastroidOpenHelper dbhelper;
		private Button saveButton;
		private Server s;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.serveredit);

			dbhelper = new YastroidOpenHelper(this);

			Intent serverIntent = Intent;
			Bundle b = serverIntent.Extras;
			getServer(b.GetInt("SERVER_ID"));
			fillLayout( );

			this.saveButton = FindViewById<Button>(Resource.Id.button_save_server);
			this.saveButton.Click += (sender, e) => {
				if(saveServer())
					Finish();
			};
		}

		private void getServer(long serverId) {
			database = dbhelper.ReadableDatabase;
			ICursor sc;
			try {
				sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME, new string[] {
							"_id", "name", "scheme", "hostname", "port", "user", "pass", "grp" },
					YastroidOpenHelper.SERVERS_ID + "=" + serverId, null, null, null, null);

				sc.MoveToFirst();
				s = new Server(sc.GetInt(0), sc.GetString(1), sc.GetString(2), sc
					.GetString(3), sc.GetInt(4), sc.GetString(5), sc
								.GetString(6), sc.GetInt(7));
				sc.Close();
				database.Close();
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}
		}
		
		private void fillLayout()
		{
			FindViewById<EditText>(Resource.Id.edit_server_name).Text = (s.getName());
			FindViewById<EditText>(Resource.Id.edit_server_host).Text = (s.getScheme() + "://"+ s.getHostname());
			FindViewById<EditText>(Resource.Id.edit_server_port).Text = (s.getPort().ToString ());
			FindViewById<EditText>(Resource.Id.edit_server_user).Text = (s.getUser());
		}

		private bool saveServer()
		{
			database = dbhelper.WritableDatabase;
			bool result = false;
			try {
				
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
				
						if (name.Length == 0 || host.Length == 0 || port.Length == 0 || user.Length == 0) {
							Toast.MakeText(this, "One or more fields are empty", ToastLength.Short).Show();
					return false;
				}

				ContentValues values = new ContentValues();
				values.Put(YastroidOpenHelper.SERVERS_NAME, name);
				values.Put(YastroidOpenHelper.SERVERS_SCHEME, scheme);
				values.Put(YastroidOpenHelper.SERVERS_HOST, host);
				values.Put(YastroidOpenHelper.SERVERS_PORT, port);
				values.Put(YastroidOpenHelper.SERVERS_USER, user);
				if(pass.Length == 0 )
					values.Put(YastroidOpenHelper.SERVERS_PASS, s.getPass());
				else
					values.Put(YastroidOpenHelper.SERVERS_PASS, pass);
					values.Put(YastroidOpenHelper.SERVERS_GROUP, s.getGroupId());
				
				database.Update(YastroidOpenHelper.SERVERS_TABLE_NAME, values, YastroidOpenHelper.SERVERS_ID + "=" + s.getId(), null);
						database.Close();
						Log.Info("ARRAY", "WebYaST server " + name + " has been updated.");
				result = true;
			} catch (Exception e) {
							Log.Error("BACKGROUND_PROC", e.Message);
			}
						Toast.MakeText(this, "Server Updated", ToastLength.Short).Show();
			return result;
		}
	}
}
