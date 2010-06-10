package com.novell.android.yastroid;

import android.graphics.drawable.Drawable;

public class SystemStatus {
	private Drawable icon;
	private String name;
	
	public SystemStatus() {
		this(null, null);
	}
	
	public SystemStatus(String name, Drawable icon) {
		this.icon = icon;
		this.name = name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public void setIcon(Drawable icon) {
		this.icon = icon;
	}
	
	public String getName() {
		return this.name;
	}
	
	public Drawable getIcon() {
		return this.icon;
	}

}
