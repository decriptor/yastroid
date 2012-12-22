using Android.Util;
using Android.Database.Sqlite;
using Android.Content;

namespace YaSTroid
{
	public class YastroidOpenHelper : SQLiteOpenHelper
	{

		public const string TAG = "YaSTroidDatabase";

		public const string DATABASE_NAME = "yastroid";
		public const int DATABASE_VERSION = 4;
		public const string SERVERS_TABLE_NAME = "servers";
		public const string SERVERS_ID = "_id";
		public const string SERVERS_NAME = "name";
		public const string SERVERS_SCHEME = "scheme";
		public const string SERVERS_HOST = "hostname";
		public const string SERVERS_PORT = "port";
		public const string SERVERS_USER = "user";
		public const string SERVERS_PASS = "pass";
		public const string SERVERS_GROUP = "grp";
		public const string GROUP_TABLE_NAME = "groups";
		public const string GROUP_NAME = "name";
		public const string GROUP_DESCRIPTION = "description";
		public const string GROUP_ICON = "icon";
		public const int GROUP_DEFAULT_ALL = 1;
		

		const string CREATE_SERVER_TABLE = "CREATE TABLE "
				+ SERVERS_TABLE_NAME + " ("
				+ SERVERS_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, "
				+ SERVERS_NAME + " TEXT, "
				+ SERVERS_SCHEME + " TEXT, "
				+ SERVERS_HOST + " TEXT, "
				+ SERVERS_PORT + " INTEGER, "
				+ SERVERS_USER + " TEXT, "
				+ SERVERS_PASS + " TEXT, "
				+ SERVERS_GROUP + " INTEGER);";
		const string CREATE_GROUP_TABLE = "CREATE TABLE "
				+ GROUP_TABLE_NAME + " ("
				+ "_id INTEGER PRIMARY KEY AUTOINCREMENT, "
				+ GROUP_NAME + " TEXT, "
				+ GROUP_DESCRIPTION + " TEXT, "
				+ GROUP_ICON + " INTEGER);";
		
		const string DEFAULT_GROUP = "INSERT INTO "
				+ GROUP_TABLE_NAME
				+ "(name,description,icon) VALUES ('All', 'All servers', '0');";

	//	private static final string ADD_SERVER = "INSERT INTO "
	//			+ SERVERS_TABLE_NAME
	//			+ "(name,scheme,hostname,port,user,pass,grp) VALUES ('webyast1', 'http', '137.65.132.194', '4984','root','sandy','" + GROUP_DEFAULT_ALL +"');";

		public YastroidOpenHelper(Context context) : base(context, DATABASE_NAME, null, DATABASE_VERSION)
		{
		}

		public override void OnCreate(SQLiteDatabase db)
		{
			db.ExecSQL(CREATE_SERVER_TABLE);
			db.ExecSQL(CREATE_GROUP_TABLE);
			db.ExecSQL(DEFAULT_GROUP);
			// Demo data
			//db.execSQL(ADD_SERVER);
		}

		public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			Log.Warn(TAG, "Upgrading database from version " + oldVersion + "to "
					+ newVersion);
			// Any changes to the database structure should occur here.
			// This is called if the DATABASE_VERSION installed is older
			// than the new version.
			// ie. db.execSQL("alter table " + TASKS_TABLE + " add column " +
			// TASK_ADDRESS + " text");
			//db.execSQL("ALTER TABLE servers ADD COLUMN grp INTEGER;");
			//db.execSQL(CREATE_GROUP_TABLE);
			//db.execSQL(DEFAULT_GROUP);
			//db.execSQL("UPDATE " + SERVERS_TABLE_NAME + " SET " + SERVERS_GROUP + "='" + GROUP_DEFAULT_ALL + "';");
		}
	}
}