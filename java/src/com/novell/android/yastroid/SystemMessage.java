package com.novell.android.yastroid;

public class SystemMessage {
	
	private String message;
	private String module;
	private String date;

	public SystemMessage( String message, String module, String date) {
		this.message = message;
		this.module = module;
		this.date = date;
	}
	
	public String getMessage() {
		return message;
	}

	public String getModule() {
		return module;
	}

	public String getDate() {
		return date;
	}

}
