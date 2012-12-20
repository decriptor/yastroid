using System.Collections.Generic;

namespace WebYaST.Status
{
	public class Log
	{
		string id;
		string path;
		string description;
		int contentPosition;
		string contentValue;

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
		
		public static List<Log> FromXmlData (string xmlData)
		{
			LogContentHandler contentHandler = new LogContentHandler ();
			//Xml.parse (xmlData, contentHandler);
			return contentHandler.getLogs ();
		}
	}
}
