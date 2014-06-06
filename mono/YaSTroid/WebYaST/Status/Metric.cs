
//import java.util.Collection;
//
//import org.xml.sax.SAXException;
//
//import android.util.Xml;
using System.Collections.Generic;
using Android.Util;

namespace YaSTroid.WebYaST.Status
{
	public class Metric
	{	
		public const int NETWORK = 0;
		public const int CPU = 1;
		public const int DISK = 2;
		public const int MEMORY = 3;
		
		public const string NETWORK_PLUGIN = "interface";
		public const string CPU_PLUGIN = "cpu";
		public const string DISK_PLUGIN = "df";
		public const string MEMORY_PLUGIN = "memory";
		
		private string id;
		private string identifier;
		private string host;
		private string plugin;
		private string pluginInstance;
		private string type;
		private string typeInstance;
		private List<Limit> limits;
		
		private List<Value> values;
		
		public Metric (string id, string identifier, string host, 
				string plugin, string pluginInstance, string type, 
			string typeInstance, List<Limit> limits,
			List<Value> values)
		{
			this.id = id;
			this.identifier = identifier;
			this.host = host;
			this.plugin = plugin;
			this.pluginInstance = pluginInstance;
			this.type = type;
			this.typeInstance = typeInstance;
			this.limits = limits;
			this.values = values;
		}

		public string getIdentifier() {
			return identifier;
		}

		public string getHost() {
			return host;
		}

		public string getId() {
			return id;
		}

		public string getPlugin() {
			return plugin;
		}

		public List<Limit> getLimits() {
			return limits;
		}

		public string getTypeInstance() {
			return typeInstance;
		}

		public string getPluginInstance() {
			return pluginInstance;
		}

		public string getType() {
			return type;
		}
		
		public List<Value> getValues () {
			return values;
		}
		
		public static List<Metric> FromXmlData (string xmlData)// throws SAXException
		{
			MetricContentHandler contentHandler = new MetricContentHandler ();
			Xml.Parse (xmlData, contentHandler);
			return contentHandler.getMetrics ();
		}
	}
}
