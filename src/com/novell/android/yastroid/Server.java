package com.novell.android.yastroid;

public class Server {
	
	private String name;
	private String ipAddress;
	private String uptime;
	
	public String getServerName() {
		return name;
	}
	
	public void setServerName(String serverName) {
		this.name = serverName;
	}
	
	public String getIpAddress() {
		return ipAddress;
	}
	
	public void setIpAddress(String ipAdress) {
		this.ipAddress = ipAdress;
	}
	
	public String getUptime() {
		return uptime;
	}
	
	public void setUptime(String uptime) {
		this.uptime = uptime;
	}
}
