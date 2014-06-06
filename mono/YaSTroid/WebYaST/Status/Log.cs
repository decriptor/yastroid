
//import java.util.Collection;
//
//import org.xml.sax.SAXException;
//
//import android.util.Xml;
using System.Collections.Generic;
using Android.Util;

namespace YaSTroid.WebYaST.Status
{
	// FIXME: JUnit this
	public class Log
	{
		private string id;
		private string path;
		private string description;
		private int contentPosition;
		private string contentValue;

		public Log (string id, string path, string description, 
				int contentPosition, string contentValue)
		{
			this.id = id;
			this.path = path;
			this.description = description;
			this.contentPosition = contentPosition;
			this.contentValue = contentValue;
		}

		public string getId ()
		{
			return id;
		}

		public string getPath ()
		{
			return path;
		}

		public string getDescription ()
		{
			return description;
		}

		public int getContentPosition ()
		{
			return contentPosition;
		}

		public string getContentValue ()
		{
			return contentValue;
		}
		
		public static List<Log> FromXmlData (string xmlData)// throws SAXException
		{
			LogContentHandler contentHandler = new LogContentHandler ();
			Xml.Parse (xmlData, contentHandler);
			return contentHandler.getLogs ();
		}
	}
}
