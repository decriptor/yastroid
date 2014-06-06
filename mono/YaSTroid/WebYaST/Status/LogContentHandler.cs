
//import java.util.ArrayList;
//import java.util.Collection;
//
//import org.xml.sax.Attributes;
//import org.xml.sax.SAXException;
//import org.xml.sax.helpers.DefaultHandler;
using System;
using Android.App;
using Org.Xml.Sax.Helpers;
using System.Collections.Generic;
using Org.Xml.Sax;

namespace YaSTroid.WebYaST.Status
{
	public class LogContentHandler : DefaultHandler
	{
		private List<Log> logs;

		private bool inLog;
		private bool inId;
		private bool inPath;
		private bool inDescription;
		private bool inContent;
		private bool inValue;
		private bool inPosition;
		
		private string id;
		private string path;
		private string description;
		private int contentPosition;
		private string contentValue;
		
		public List<Log> getLogs ()
		{
			return logs;
		}

		public void startElement (string uri, string localName, string qName, IAttributes atts)// throws SAXException
		{
			base.StartElement (uri, localName, qName, atts);
			
			if (localName.CompareTo ("log") == 0)
				inLog = true;
				if (logs == null)
					logs = new List<Log> ();
			else if (localName.CompareTo ("id") == 0 && inLog)
				inId = true;
			else if (localName.CompareTo ("path") == 0 && inLog)
				inPath = true;
			else if (localName.CompareTo ("description") == 0 && inLog)
				inDescription = true;
			else if (localName.CompareTo ("content") == 0 && inLog)
				inContent = true;
			else if (localName.CompareTo ("value") == 0 && inContent)
				inValue = true;
			else if (localName.CompareTo ("position") == 0 && inContent)
				inPosition = true;
		}

		public void endElement (string uri, string localName, string qName)// throws SAXException
		{
			base.EndElement (uri, localName, qName);
			
			if (localName.CompareTo ("log") == 0) {
				inLog = false;
				logs.Add (new Log (id, path, description, contentPosition, contentValue));
				id = path = description = contentValue = null;
				contentPosition = 0;
			} else if (localName.CompareTo ("id") == 0 && inLog)
				inId = false;
			else if (localName.CompareTo ("path") == 0 && inLog)
				inPath = false;
			else if (localName.CompareTo ("description") == 0 && inLog)
				inDescription = false;
			else if (localName.CompareTo ("content") == 0 && inLog)
				inContent = false;
			else if (localName.CompareTo ("value") == 0 && inContent)
				inValue = false;
			else if (localName.CompareTo ("position") == 0 && inContent)
				inPosition = false;
		}
		
		public void characters (char[] ch, int start, int length)// throws SAXException
		{
			base.Characters (ch, start, length);
			String text = new string (ch, start, length);

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
					else if (inPosition) {
						int result;
						Int32.TryParse (text, out result);
						contentPosition = result;
					}
				}
			}

		}
	}
}
