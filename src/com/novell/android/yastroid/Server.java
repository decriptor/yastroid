package com.novell.android.yastroid;

public class Server extends com.novell.webyast.Server {
	
	private String name;

	public Server(String name, String scheme, String hostname, int port, String user, String pass) {
		super (scheme, hostname, port, user, pass);
		this.name = name;
	}
	
	public String getName() {
		return name;
	}
	
	public String getFullUrl() {
		return getScheme() + "://" + getHostname() + ":" + getPort();
	}

}
