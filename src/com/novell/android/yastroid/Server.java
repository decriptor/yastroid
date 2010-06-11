package com.novell.android.yastroid;

public class Server extends com.novell.webyast.Server {
	
	private int id;
	private String name;

	public Server(int id, String name, String scheme, String hostname, int port, String user, String pass) {
		super (scheme, hostname, port, user, pass);
		this.id = id;
		this.name = name;
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

}
