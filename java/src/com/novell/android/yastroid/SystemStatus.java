package com.novell.android.yastroid;

import android.app.Application;
import android.graphics.drawable.Drawable;

public class SystemStatus {
	public static final int LOG_STATUS = 0;
	public static final int NETWORK_STATUS = 1;
	public static final int MEMORY_STATUS = 2;
	public static final int DISK_STATUS = 3;
	public static final int CPU_STATUS = 4;
	public static final int STATUS_GREEN = 100;
	public static final int STATUS_RED = 101;
	private Application app;
	private int systemType;
	private int status;
	private Drawable icon;
	private String name;
	
	public SystemStatus(Application app, int systemType, int status) {
		this.app = app;
		setSystemType(systemType);
		setStatus(status);
	}
	
	public SystemStatus(Application app, int systemType) {
		this(app, systemType, 0);
	}
	
	public SystemStatus(Application app, String name) {
		this.app = app;
		this.name = name;
	}
	
	public void setSystemType(int type) {
		this.systemType = type;
		switch (type) {
		case NETWORK_STATUS:
			name = app.getResources().getString(R.string.network_status_text);
			break;
		case MEMORY_STATUS:
			name = app.getResources().getString(R.string.memory_status_text);
			break;
		case DISK_STATUS:
			name = app.getResources().getString(R.string.disk_status_text);
			break;
		case CPU_STATUS:
			name = app.getResources().getString(R.string.cpu_status_text);
			break;
		default:
			name = "";
			break;
		}
	}
	
	public void setStatus(int status) {
		this.status = status;
		switch (status) {
		case STATUS_GREEN:
			icon = app.getResources().getDrawable(R.drawable.status_green);
			break;
		case 0:
			icon = null;
			break;
		default:
			icon = app.getResources().getDrawable(R.drawable.status_red);
			break;
		}
	}
	
	public int getSystemType() {
		return this.systemType;
	}
	
	public int getStatus() {
		return this.status;
	}
	
	public String getName() {
		return this.name;
	}
	
	public Drawable getIcon() {
		return this.icon;
	}
}
