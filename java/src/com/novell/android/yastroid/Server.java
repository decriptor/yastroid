package com.novell.android.yastroid;

import android.os.Bundle;

public class Server extends com.novell.webyast.Server {
	
	private int id;
	private String name;
	private int groupid;

	public Server(int id, String name, String scheme, String hostname, int port, String user, String pass, int groupid) {
		super (scheme, hostname, port, user, pass);
		this.id = id;
		this.name = name;
		this.groupid = groupid;
	}
	
	public Server(Bundle b) {
		this(b.getInt("SERVER_ID"),
			b.getString("SERVER_NAME"),
			b.getString("SERVER_SCHEME"),
			b.getString("SERVER_HOSTNAME"),
			b.getInt("SERVER_PORT"),
			b.getString("SERVER_USER"),
			b.getString("SERVER_PASS"),
			b.getInt("SERVER_GID"));
	}
	
	public String getName() {
		return name;
	}
	
	public String getFullUrl() {
		return getScheme() + "://" + getHostname() + ":" + getPort();
	}

	public int getId() {
		return id;
	}
	
	public int getGroupId() {
		return groupid;
	}
}
