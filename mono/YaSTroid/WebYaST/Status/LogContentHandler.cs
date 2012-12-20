using System.Collections.Generic;

namespace WebYaST.Status
{
	public class LogContentHandler
	{
		List<Log> logs;

		bool inLog;
		bool inId;
		bool inPath;
		bool inDescription;
		bool inContent;
		bool inValue;
		bool inPosition;
		
		string id;
		string path;
		string description;
		int contentPosition;
		string contentValue;
		
		public List<Log> getLogs ()
		{
			return logs;
		}

//		public void startElement (string uri, string localName, string qName, Attributes atts)
//		{
//			base.startElement (uri, localName, qName, atts);
//			
//			if (localName.compareTo ("log") == 0)
//				inLog = true;
//				if (logs == null)
//					logs = new ArrayList<Log> ();
//			else if (localName.compareTo ("id") == 0 && inLog)
//				inId = true;
//			else if (localName.compareTo ("path") == 0 && inLog)
//				inPath = true;
//			else if (localName.compareTo ("description") == 0 && inLog)
//				inDescription = true;
//			else if (localName.compareTo ("content") == 0 && inLog)
//				inContent = true;
//			else if (localName.compareTo ("value") == 0 && inContent)
//				inValue = true;
//			else if (localName.compareTo ("position") == 0 && inContent)
//				inPosition = true;
//		}

		public void endElement (string uri, string localName, string qName)
		{
//			base.endElement (uri, localName, qName);
//			
//			if (localName.compareTo ("log") == 0) {
//				inLog = false;
//				logs.add (new Log (id, path, description, contentPosition, contentValue));
//				id = path = description = contentValue = null;
//				contentPosition = 0;
//			} else if (localName.compareTo ("id") == 0 && inLog)
//				inId = false;
//			else if (localName.compareTo ("path") == 0 && inLog)
//				inPath = false;
//			else if (localName.compareTo ("description") == 0 && inLog)
//				inDescription = false;
//			else if (localName.compareTo ("content") == 0 && inLog)
//				inContent = false;
//			else if (localName.compareTo ("value") == 0 && inContent)
//				inValue = false;
//			else if (localName.compareTo ("position") == 0 && inContent)
//				inPosition = false;
		}
		
		public void characters (char[] ch, int start, int length)
		{
//			base.characters (ch, start, length);
//			string text = new string (ch, start, length);
//
//			if (inLog) {
//				if (inId)
//					id = text;
//				else if (inPath)
//					path = text;
//				else if (inDescription)
//					description = text;
//				else if (inContent) {
//					if (inValue)
//						contentValue = text;
//					else if (inPosition)
//						contentPosition = Integer.parseInt (text);
//				}
		}
	}
}
