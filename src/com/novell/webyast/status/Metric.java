package com.novell.webyast.status;

import java.util.Collection;

import org.xml.sax.SAXException;

import android.util.Xml;

public class Metric {
	
	public static final int NETWORK = 0;
	public static final int CPU = 1;
	public static final int DISK = 2;
	public static final int MEMORY = 3;
	
	public static final String NETWORK_PLUGIN = "interface";
	public static final String CPU_PLUGIN = "cpu";
	public static final String DISK_PLUGIN = "df";
	public static final String MEMORY_PLUGIN = "memory";
	
	private String id;
	private String identifier;
	private String host;
	private String plugin;
	private String pluginInstance;
	private String type;
	private String typeInstance;
	private Collection<Limit> limits;
	
	private Collection<Value> values;
	
	public Metric (String id, String identifier, String host, 
			String plugin, String pluginInstance, String type, 
			String typeInstance, Collection<Limit> limits,
			Collection<Value> values)
	{
		this.id = id;
		this.identifier = identifier;
		this.host = host;
		this.plugin = plugin;
		this.pluginInstance = pluginInstance;
		this.typeInstance = typeInstance;
		this.limits = limits;
		this.values = values;
	}

	public String getIdentifier() {
		return identifier;
	}

	public String getHost() {
		return host;
	}

	public String getId() {
		return id;
	}

	public String getPlugin() {
		return plugin;
	}

	public Collection<Limit> getLimits() {
		return limits;
	}

	public String getType_instance() {
		return typeInstance;
	}

	public String getPluginInstance() {
		return pluginInstance;
	}

	public String getType() {
		return type;
	}
	
	private Collection<Value> getValues ()
	{
		return values;
	}
	
	public static Collection<Metric> FromXmlData (String xmlData) throws SAXException {
		MetricContentHandler contentHandler = new MetricContentHandler ();
		Xml.parse (xmlData, contentHandler);
		return contentHandler.getMetrics ();
	}
}
