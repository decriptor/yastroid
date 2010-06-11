package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

public class LogContentHandler extends DefaultHandler {
	
	private Collection<Log> logs;

	private boolean inLog;
	private boolean inId;
	private boolean inPath;
	private boolean inDescription;
	private boolean inContent;
	private boolean inValue;
	private boolean inPosition;
	
	private String id;
	private String path;
	private String description;
	private int contentPosition;
	private String contentValue;
	
	public Collection<Log> getLogs ()
	{
		return logs;
	}

	public void startElement (String uri, String localName, String qName, Attributes atts) throws SAXException
	{
		super.startElement (uri, localName, qName, atts);
		
		if (localName.compareTo ("log") == 0)
			inLog = true;
			if (logs == null)
				logs = new ArrayList<Log> ();
		else if (localName.compareTo ("id") == 0 && inLog)
			inId = true;
		else if (localName.compareTo ("path") == 0 && inLog)
			inPath = true;
		else if (localName.compareTo ("description") == 0 && inLog)
			inDescription = true;
		else if (localName.compareTo ("content") == 0 && inLog)
			inContent = true;
		else if (localName.compareTo ("value") == 0 && inContent)
			inValue = true;
		else if (localName.compareTo ("position") == 0 && inContent)
			inPosition = true;
	}

	public void endElement (String uri, String localName, String qName) throws SAXException
	{
		super.endElement (uri, localName, qName);
		
		if (localName.compareTo ("log") == 0) {
			inLog = false;
			logs.add (new Log (id, path, description, contentPosition, contentValue));
			id = path = description = contentValue = null;
			contentPosition = 0;
		} else if (localName.compareTo ("id") == 0 && inLog)
			inId = false;
		else if (localName.compareTo ("path") == 0 && inLog)
			inPath = false;
		else if (localName.compareTo ("description") == 0 && inLog)
			inDescription = false;
		else if (localName.compareTo ("content") == 0 && inLog)
			inContent = false;
		else if (localName.compareTo ("value") == 0 && inContent)
			inValue = false;
		else if (localName.compareTo ("position") == 0 && inContent)
			inPosition = false;
	}
	
	public void characters (char[] ch, int start, int length) throws SAXException
	{
		super.characters (ch, start, length);
		String text = new String (ch, start, length);

		if (inLog) {
			if (inId)
				id = text;
			else if (inPath)
				path = text;
			else if (inDescription)
				description = text;
			else if (inContent) {
				if (inValue)
					contentValue = text;
				else if (inPosition)
					contentPosition = Integer.parseInt (text);
			}
		}

	}
}
