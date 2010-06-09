package com.novell.webyast.status;

// FIXME: JUnit this
public class Log {
	private String id;
	private String path;
	private String description;
	private int contentPosition;
	private String contentValue;

	public Log (String id, String path, String description, 
			int contentPosition, String contentValue)
	{
		this.id = id;
		this.path = path;
		this.description = description;
		this.contentPosition = contentPosition;
		this.contentValue = contentValue;
	}

	public String getId ()
	{
		return id;
	}

	public String getPath ()
	{
		return path;
	}

	public String getDescription ()
	{
		return description;
	}

	public int getContentPosition ()
	{
		return contentPosition;
	}

	public String getContentValue ()
	{
		return contentValue;
	}
}
