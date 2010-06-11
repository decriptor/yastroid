package com.novell.webyast.status;

import java.util.Collection;

import org.xml.sax.SAXException;

import android.util.Xml;

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
	
	public static Collection<Log> FromXmlData (String xmlData) throws SAXException
	{
		LogContentHandler contentHandler = new LogContentHandler ();
		Xml.parse (xmlData, contentHandler);
		return contentHandler.getLogs ();
	}
}
