package com.novell.android.yastroid;

public class Module {
	
	private String name;
	private String display;
	private int icon;

	public Module( String name, String display, int icon ) {
		this.name = name;
		this.display = display;
		this.icon = icon;
	}
	
	public String getName() {
		return name;
	}
	
	public String getDisplay() {
		return display;
	}

	public int getIcon() {
		return icon;
	}

}
