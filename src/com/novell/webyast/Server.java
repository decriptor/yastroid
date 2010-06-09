package com.novell.webyast;

import com.novell.webyast.status.StatusModule;
import com.novell.webyast.update.UpdateModule;

public class Server {
	
	private String hostname;
	private String user;
	private String pass;
	private int port;
	private String scheme;
	
	public Server(String scheme, String hostname, int port, String user, String pass) {
		this.hostname = hostname;
		this.user = user;
		this.pass = pass;
		this.port = port;
		this.scheme = scheme;
	}
	
	public String getHostname() {
		return hostname;
	}
	
	public int getPort() {
		return port;
	}
	
	public String getScheme() {
		return scheme;
	}
	
	public String getUser() {
		return user;
	}
	
	public String getPass() {
		return pass;
	}

	// FIXME: JUnit this
	public StatusModule getStatusModule ()
	{
		return new StatusModule (this);
	}
}
