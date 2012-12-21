using System.Collections.Generic;

namespace WebYaST.Status
{
	public class MetricContentHandler
	{
		List<Metric> metrics;
		
		bool inMetric;
		
		bool inId;
		bool inIdentifier;
		bool inHost;
		bool inPlugin;
		bool inPluginInstance;
		bool inType;
		bool inTypeInstance;
		bool inLimits;
		bool inLimit;
		bool inMetricColumn;
		bool inMax;
		bool inMin;
		bool inValueRoot;
		bool inValue;
		
		string id;
		string identifier;
		string host;
		string plugin;
		string pluginInstance;
		string type;
		string typeInstance;	
		List<Limit> limits;
		List<Value> values;
		List<float> floatValues;
		
		string metricColumn;
		int max;
		int min;
		
		int interval;
		string column;
		int start;
		
		public List<Metric> getMetrics ()
		{
			return metrics;
		}
		
//		public void startElement (string uri, string localName, string qName, Attributes atts)
//		{
//			base.startElement (uri, localName, qName, atts);
//			
//			if (localName.compareTo ("metric") == 0) {
//				inMetric = true;
//				 if (metrics == null)
//					 metrics = new ArrayList<Metric> ();
//				values = new ArrayList<Value> ();
//			} else if (localName.compareTo ("id") == 0 && inMetric)
//				inId = true;
//			else if (localName.compareTo ("identifier") == 0 && inMetric)
//				inIdentifier = true;
//			else if (localName.compareTo ("host") == 0 && inMetric)
//				inHost = true;
//			else if (localName.compareTo ("plugin") == 0 && inMetric)
//				inPlugin = true;
//			else if (localName.compareTo ("plugin_instance") == 0 && inMetric)
//				inPluginInstance = true;
//			else if (localName.compareTo ("type") == 0 && inMetric)
//				inType = true;
//			else if (localName.compareTo ("type_instance") == 0 && inMetric)
//				inTypeInstance = true;
//			else if (localName.compareTo ("limits") == 0 && inMetric) {
//				inLimits = true;
//				limits = new ArrayList<Limit> ();
//			} else if (localName.compareTo ("limit") == 0 && inLimits)
//				inLimit = true;
//			else if (localName.compareTo ("metric_column") == 0 && inLimit)
//				inMetricColumn = true;
//			else if (localName.compareTo ("max") == 0 && inLimit)
//				inMax = true;
//			else if (localName.compareTo ("min") == 0 && inLimit)
//				inMin = true;
//			else if (localName.compareTo ("value") == 0 && inMetric) {
//				if (!inValueRoot) {
//					inValueRoot = true;
//					interval = Integer.parseInt (atts.getValue ("interval"));
//					column = atts.getValue ("column");
//					start = Integer.parseInt (atts.getValue ("start"));
//					floatValues = new ArrayList<Float> (); 
//				} else // inner <value>
//					inValue = true;
//			} 
//		}
		
		public void endElement (string uri, string localName, string qName)
		{
//			base.endElement (uri, localName, qName);
//			
//			if (localName.compareTo ("metric") == 0) {
//				inMetric = false;
//				metrics.add (new Metric (id, identifier, host, plugin,
//						pluginInstance, type, typeInstance, limits, values));
//				id = identifier = host = plugin = null; 
//				pluginInstance = type = typeInstance = null;
//				limits = null;
//			} else if (localName.compareTo ("id") == 0 && inMetric)
//				inId = false;
//			else if (localName.compareTo ("identifier") == 0 && inMetric)
//				inIdentifier = false;
//			else if (localName.compareTo ("host") == 0 && inMetric)
//				inHost = false;
//			else if (localName.compareTo ("plugin") == 0 && inMetric)
//				inPlugin = false;
//			else if (localName.compareTo ("plugin_instance") == 0 && inMetric)
//				inPluginInstance = false;
//			else if (localName.compareTo ("type") == 0 && inMetric)
//				inType = false;
//			else if (localName.compareTo ("type_instance") == 0 && inMetric)
//				inTypeInstance = false;
//			else if (localName.compareTo ("limits") == 0 && inMetric)
//				inLimits = false;
//			else if (localName.compareTo ("limit") == 0 && inLimits) {
//				inLimit = false;
//				limits.add (new Limit (metricColumn, max, min));
//				metricColumn = null;
//				max = min = 0;
//			} else if (localName.compareTo ("metric_column") == 0 && inLimit)
//				inMetricColumn = false;
//			else if (localName.compareTo ("max") == 0 && inLimit)
//				inMax = false;
//			else if (localName.compareTo ("min") == 0 && inLimit)
//				inMin = false;
//			else if (localName.compareTo ("value") == 0 && inMetric) {
//				if (inValue)
//					inValue = false;
//				else {
//					values.add (new Value (interval, column, start, floatValues));
//					floatValues = null;
//					column = null;
//					interval = start = 0;
//					inValueRoot = false;
//				}
//			}
		}
		
		public void characters (char[] ch, int start, int length)
		{
//			base.characters (ch, start, length);
//			string text = new string (ch, start, length);
//			
//			if (inMetric) {
//				if (inId)
//					id = text;
//				else if (inIdentifier)
//					identifier = text;
//				else if (inHost)
//					host = text;
//				else if (inPlugin)
//					plugin = text;
//				else if (inPluginInstance)
//					pluginInstance = text;
//				else if (inType)
//					type = text;
//				else if (inTypeInstance)
//					typeInstance = text;
//				else if (inLimits) {
//					if (inLimit) {
//						if (inMetricColumn)
//							metricColumn = text;
//						else if (inMax)
//							max = Integer.parseInt (text);
//						else if (inMin)
//							min = Integer.parseInt (text);
//					}
//				} else if (inValueRoot) {
//					if (inValue)
//						floatValues.Add (Float.parseFloat (text.replace("\"", "")));
//				} 
//			}
		}
	}
}
