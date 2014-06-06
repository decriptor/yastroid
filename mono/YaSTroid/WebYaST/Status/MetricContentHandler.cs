using System.Collections.Generic;
using Org.Xml.Sax;
using Org.Xml.Sax.Helpers;
using System;

namespace YaSTroid.WebYaST.Status
{
	public class MetricContentHandler : DefaultHandler
	{
		private List<Metric> metrics;
		
		private bool inMetric;
		
		private bool inId;
		private bool inIdentifier;
		private bool inHost;
		private bool inPlugin;
		private bool inPluginInstance;
		private bool inType;
		private bool inTypeInstance;
		private bool inLimits;
		private bool inLimit;
		private bool inMetricColumn;
		private bool inMax;
		private bool inMin;
		private bool inValueRoot;
		private bool inValue;
		
		private string id;
		private string identifier;
		private string host;
		private string plugin;
		private string pluginInstance;
		private string type;
		private string typeInstance;	
		private List<Limit> limits;
		private List<Value> values;
		private List<float> floatValues;
		
		private string metricColumn;
		private int max;
		private int min;

		private int interval;
		private string column;
		private int start;
		
		public List<Metric> getMetrics ()
		{
			return metrics;
		}
		
		public void startElement (string uri, string localName, string qName, IAttributes atts)// throws SAXException
		{
			base.StartElement (uri, localName, qName, atts);
			
			if (localName.CompareTo ("metric") == 0) {
				inMetric = true;
				 if (metrics == null)
					 metrics = new List<Metric> ();
				values = new List<Value> ();
			} else if (localName.CompareTo ("id") == 0 && inMetric)
				inId = true;
			else if (localName.CompareTo ("identifier") == 0 && inMetric)
				inIdentifier = true;
			else if (localName.CompareTo ("host") == 0 && inMetric)
				inHost = true;
			else if (localName.CompareTo ("plugin") == 0 && inMetric)
				inPlugin = true;
			else if (localName.CompareTo ("plugin_instance") == 0 && inMetric)
				inPluginInstance = true;
			else if (localName.CompareTo ("type") == 0 && inMetric)
				inType = true;
			else if (localName.CompareTo ("type_instance") == 0 && inMetric)
				inTypeInstance = true;
			else if (localName.CompareTo ("limits") == 0 && inMetric) {
				inLimits = true;
				limits = new List<Limit> ();
			} else if (localName.CompareTo ("limit") == 0 && inLimits)
				inLimit = true;
			else if (localName.CompareTo ("metric_column") == 0 && inLimit)
				inMetricColumn = true;
			else if (localName.CompareTo ("max") == 0 && inLimit)
				inMax = true;
			else if (localName.CompareTo ("min") == 0 && inLimit)
				inMin = true;
			else if (localName.CompareTo ("value") == 0 && inMetric) {
				if (!inValueRoot) {
					inValueRoot = true;
					int result;
					int.TryParse (atts.GetValue ("interval"), out result);
					interval = result;
					column = atts.GetValue ("column");
					int.TryParse (atts.GetValue ("start"), out result);
					start = result;
					floatValues = new List<float> (); 
				} else // inner <value>
					inValue = true;
			} 
		}
		
		public void endElement (string uri, string localName, string qName)// throws SAXException
		{
			base.EndElement (uri, localName, qName);
			
			if (localName.CompareTo ("metric") == 0) {
				inMetric = false;
				metrics.Add (new Metric (id, identifier, host, plugin,
						pluginInstance, type, typeInstance, limits, values));
				id = identifier = host = plugin = null; 
				pluginInstance = type = typeInstance = null;
				limits = null;
			} else if (localName.CompareTo ("id") == 0 && inMetric)
				inId = false;
			else if (localName.CompareTo ("identifier") == 0 && inMetric)
				inIdentifier = false;
			else if (localName.CompareTo ("host") == 0 && inMetric)
				inHost = false;
			else if (localName.CompareTo ("plugin") == 0 && inMetric)
				inPlugin = false;
			else if (localName.CompareTo ("plugin_instance") == 0 && inMetric)
				inPluginInstance = false;
			else if (localName.CompareTo ("type") == 0 && inMetric)
				inType = false;
			else if (localName.CompareTo ("type_instance") == 0 && inMetric)
				inTypeInstance = false;
			else if (localName.CompareTo ("limits") == 0 && inMetric)
				inLimits = false;
			else if (localName.CompareTo ("limit") == 0 && inLimits) {
				inLimit = false;
				limits.Add (new Limit (metricColumn, max, min));
				metricColumn = null;
				max = min = 0;
			} else if (localName.CompareTo ("metric_column") == 0 && inLimit)
				inMetricColumn = false;
			else if (localName.CompareTo ("max") == 0 && inLimit)
				inMax = false;
			else if (localName.CompareTo ("min") == 0 && inLimit)
				inMin = false;
			else if (localName.CompareTo ("value") == 0 && inMetric) {
				if (inValue)
					inValue = false;
				else {
					values.Add (new Value (interval, column, start, floatValues));
					floatValues = null;
					column = null;
					interval = start = 0;
					inValueRoot = false;
				}
			}
		}
		
		public void characters (char[] ch, int start, int length)// throws SAXException
		{
			base.Characters (ch, start, length);
			string text = new string (ch, start, length);
			
			if (inMetric) {
				if (inId)
					id = text;
				else if (inIdentifier)
					identifier = text;
				else if (inHost)
					host = text;
				else if (inPlugin)
					plugin = text;
				else if (inPluginInstance)
					pluginInstance = text;
				else if (inType)
					type = text;
				else if (inTypeInstance)
					typeInstance = text;
				else if (inLimits) {
					if (inLimit) {
						if (inMetricColumn)
							metricColumn = text;
						else if (inMax) {
							int result;
							int.TryParse (text, out result);
							max = result;
						} else if (inMin) {
							int result;
							int.TryParse (text, out result);
							min = result;
						}
					}
				} else if (inValueRoot) {
					if (inValue) {
						float result;
						float.TryParse (text.Replace ("\"", ""), out result);
						floatValues.Add (result);
					}
				} 
			}
		}
	}
}