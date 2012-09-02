package com.novell.android.yastroid;

public class Group {
	
	private int id;
	private String name;
	private String description;
	private int icon;

	public Group(int id ,String name, String description, int icon) {
		this.id = id;
		this.name = name;
		this.description = description;
		this.icon = icon;
	}
	
	public String getName() {
		return name;
	}
	
	public String getDescription() {
		return description;
	}

	public int getId() {
		return id;
	}
	
	public int getIcon() {
		return icon;
	}

}
