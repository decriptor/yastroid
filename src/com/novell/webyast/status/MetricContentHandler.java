package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;


public class MetricContentHandler extends DefaultHandler {
	private Collection<Metric> metrics;
	
	private boolean inMetric;
	
	private boolean inId;
	private boolean inIdentifier;
	private boolean inHost;
	private boolean inPlugin;
	private boolean inPluginInstance;
	private boolean inType;
	private boolean inTypeInstance;
	private boolean inLimits;
	private boolean inLimit;
	private boolean inMetricColumn;
	private boolean inMax;
	private boolean inMin;
	
	private String id;
	private String identifier;
	private String host;
	private String plugin;
	private String pluginInstance;
	private String type;
	private String typeInstance;	
	private Collection<Limit> limits;
	
	private String metricColumn;
	private int max;
	private int min;
	
	public Collection<Metric> getMetrics ()
	{
		return metrics;
	}
	
	public void startElement (String uri, String localName, String qName, Attributes atts) throws SAXException
	{
		super.startElement (uri, localName, qName, atts);
		
		if (localName.compareTo ("graph") == 0) {
			inMetric = true;
			 if (metrics == null)
				 metrics = new ArrayList<Metric> ();
		} else if (localName.compareTo ("id") == 0 && inMetric)
			inId = true;
		else if (localName.compareTo ("identifier") == 0 && inMetric)
			inIdentifier = true;
		else if (localName.compareTo ("host") == 0 && inMetric)
			inHost = true;
		else if (localName.compareTo ("plugin") == 0 && inMetric)
			inPlugin = true;
		else if (localName.compareTo ("plugin_instance") == 0 && inMetric)
			inPluginInstance = true;
		else if (localName.compareTo ("type") == 0 && inMetric)
			inType = true;
		else if (localName.compareTo ("type_instance") == 0 && inMetric)
			inTypeInstance = true;
		else if (localName.compareTo ("limits") == 0 && inMetric) {
			inLimits = true;
			limits = new ArrayList<Limit> ();
		} else if (localName.compareTo ("limit") == 0 && inLimits)
			inLimit = true;
		else if (localName.compareTo ("metric_column") == 0 && inLimit)
			inMetricColumn = true;
		else if (localName.compareTo ("max") == 0 && inLimit)
			inMax = true;
		else if (localName.compareTo ("min") == 0 && inLimit)
			inMin = true;
	}
	
	public void endElement (String uri, String localName, String qName) throws SAXException
	{
		super.endElement (uri, localName, qName);
		
		if (localName.compareTo ("graph") == 0) {
			inMetric = false;
			metrics.add (new Metric (id, identifier, host, plugin,
					pluginInstance, type, typeInstance, limits, null)); // FIXME: Add values
			id = identifier = host = plugin = null; 
			pluginInstance = type = typeInstance = null;
			limits = null;
		} else if (localName.compareTo ("id") == 0 && inMetric)
			inId = false;
		else if (localName.compareTo ("identifier") == 0 && inMetric)
			inIdentifier = false;
		else if (localName.compareTo ("host") == 0 && inMetric)
			inHost = false;
		else if (localName.compareTo ("plugin") == 0 && inMetric)
			inPlugin = false;
		else if (localName.compareTo ("plugin_instance") == 0 && inMetric)
			inPluginInstance = false;
		else if (localName.compareTo ("type") == 0 && inMetric)
			inType = false;
		else if (localName.compareTo ("type_instance") == 0 && inMetric)
			inTypeInstance = false;
		else if (localName.compareTo ("limits") == 0 && inMetric)
			inLimits = false;
		else if (localName.compareTo ("limit") == 0 && inLimits) {
			inLimit = false;
			limits.add (new Limit (metricColumn, max, min));
			metricColumn = null;
			max = min = 0;
		} else if (localName.compareTo ("metric_column") == 0 && inLimit)
			inMetricColumn = false;
		else if (localName.compareTo ("max") == 0 && inLimit)
			inMax = false;
		else if (localName.compareTo ("min") == 0 && inLimit)
			inMin = false;
	}
	
	public void characters (char[] ch, int start, int length) throws SAXException
	{
		super.characters (ch, start, length);
		String text = new String (ch, start, length);
		
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
					else if (inMax)
						max = Integer.parseInt (text);
					else if (inMin)
						min = Integer.parseInt (text);
				}
			}
		}
	}
}
