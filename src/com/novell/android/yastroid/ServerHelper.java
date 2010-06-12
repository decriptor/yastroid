package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;

public class ServerHelper {

	private SQLiteDatabase database;
	private YastroidOpenHelper dbhelper;
	private Context context;

	private int serverId;

	ServerHelper(Context context) {
		this.context = context;
		dbhelper = new YastroidOpenHelper(context);

	}

	public Server getServer(int id) {
		database = dbhelper.getReadableDatabase();
		Server s = null;
		try {
			Cursor sc = database.query(SERVERS_TABLE_NAME,
					new String[] { "_id", "name", "scheme", "hostname", "port",
							"user", "pass" }, "_id="+id, null, null, null, null);

			if(sc.getCount() == 1) {
				s = new Server(sc.getInt(0), sc.getString(1), sc
							.getString(2), sc.getString(3), sc.getInt(4), sc
							.getString(5), sc.getString(6));
				}
			sc.close();
			database.close();
		} catch (Exception e) {
			Log.e("BACKGROUND_PROC", e.getMessage());
		}
		return s;
	}

}
