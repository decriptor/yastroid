using System;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Util;

namespace YaSTroid
{
	public class ServerHelper
	{
		private SQLiteDatabase database;
		private YastroidOpenHelper dbhelper;
		private Context context;

		ServerHelper(Context context) {
			this.context = context;
			dbhelper = new YastroidOpenHelper(context);

		}

		public Server getServer(int id) {
			database = dbhelper.ReadableDatabase;
			Server s = null;
			try {
				ICursor sc = database.Query(YastroidOpenHelper.SERVERS_TABLE_NAME,
						new string[] { "_id", "name", "scheme", "hostname", "port",
								"user", "pass", "grp" }, "_id="+id, null, null, null, null);

				if(sc.Count == 1) {
					s = new Server(sc.GetInt(0), sc.GetString(1), sc
								.GetString(2), sc.GetString(3), sc.GetInt(4), sc
								.GetString(5), sc.GetString(6), sc.GetInt(7));
					}
				sc.Close();
				database.Close();
			} catch (Exception e) {
				Log.Error("BACKGROUND_PROC", e.Message);
			}
			return s;
		}

	}
}
