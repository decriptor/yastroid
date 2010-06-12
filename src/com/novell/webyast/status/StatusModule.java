package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;

import org.xml.sax.SAXException;

import com.novell.webyast.RestClient;
import com.novell.webyast.Server;

//FIXME: JUnit this

/***
 * 
 * Gets status information.
 * 
 * - Graphs: health status
 * - Logs: retrieving logs - in progress
 * - Metrics: data to generate graphs - missing
 * 
 * @author Mario Carrion
 *
 */
public class StatusModule {
	
	private Server server;

	public StatusModule (Server server)
	{
		this.server = server;
	}
	
	/***
	 * Gets all available metrics. The returned value represents
	 * the data returned by /metrics 
	 * 
	 * @return
	 * @throws SAXException
	 */
	public Collection<Metric> getMetrics () throws SAXException
	{
		String xmlData = null;

		try {
			xmlData = new RestClient ().getMethod (server, "/metrics");
		} catch (Exception e) {
			return null;
		}
		
		if (xmlData == null)
			return null;
		
		return Metric.FromXmlData (xmlData);
	}
	
	/*** 
	 * Gets the metric specific metrics associated to the type
	 * 
	 * @param metricType Use Metric.NETWORK, Metric.CPU, Metric.MEMORY, Metric.DISK
	 * @return collection of specific metrics
	 */
	public Collection<Metric> getMetric (int metricType)
	{
		Collection<Metric> metrics = null;

		try {
			metrics = getMetrics ();
		} catch (SAXException e) {
			return null;
		}
		
		String plugin = null;
		switch (metricType) {
		case Metric.CPU:
			plugin = Metric.CPU_PLUGIN;
			break;
		case Metric.DISK:
			plugin = Metric.DISK_PLUGIN;
			break;
		case Metric.MEMORY:
			plugin = Metric.MEMORY_PLUGIN;
			break;
		case Metric.NETWORK:
			plugin = Metric.NETWORK_PLUGIN;
			break;
		default:
			return null;
		}
		
		Collection<Metric> collection = new ArrayList<Metric> ();
		for (Metric m : metrics) {
			if (m.getPlugin ().compareTo (plugin) == 0)
				collection.add (m);
		}
		
		return collection;
	}

	public Metric getMetricData (String id, int start, int stop) throws SAXException
	{
		String xmlData = null;

		try {
			String startStop = "";
			if (start > 0)
				startStop = "start=" + start;
			if (stop > 0)
				startStop = (startStop.compareTo("") == 0 ? "" : "&") + "stop=" + stop;
			
			xmlData = new RestClient ().getMethod (server, "/metrics/" + id + startStop);
		} catch (Exception e) {
			return null;
		}
		
		if (xmlData == null)
			return null;
		
		Collection<Metric> metrics = Metric.FromXmlData (xmlData);
		if (metrics.size() == 0)
			return null;
		
		return ((ArrayList<Metric>) metrics).get (0);
	}
	
	/***
	 * Gets the data returned by the graphs method.
	 * 
	 * @return Collection of Graph objects
	 * @throws SAXException
	 */
	public Collection<Graph> getGraphs () throws SAXException
	{
		String xmlData = null;

		try {
			xmlData = new RestClient ().getMethod (server, "/graphs?checklimits=true");
		} catch (Exception e) {
			return null;
		}
		
		if (xmlData == null)
			return null;
		
		return Graph.FromXmlData (xmlData);
	}
	
	public boolean isHealthy (int element, Collection<Graph> graphs)
	{
		if (graphs == null)
			return false;
		
		String id = null;
		switch (element) {
		case Metric.CPU:
			id = Health.CPU_ID;
			break;
		case Metric.DISK:
			id = Health.DISK_ID;
			break;
		case Metric.MEMORY:
			id = Health.MEMORY_ID;
			break;
		case Metric.NETWORK:
			id = Health.NETWORK_ID;
			break;
		default:
			return false;
		}
		
		for (Graph g : graphs) {
			if (g.getId ().compareTo (id) == 0) {
				for (SingleGraph sg : g.getSingleGraphs()) {
					for (Line l : sg.getLines())
						if (l.isReached())
							return false;
				}
			}
		}
		
		return true;
	}
	
	/***
	 * Gets a summary of the health status.
	 * Returned values are Health.Error, Health.Healthy and Health.Unhealthy 
	 * 
	 * @return health status
	 */
	public int getHealthSummary ()
	{
		Collection<Health> fullHealth = getFullHealth ();
		if (fullHealth == null)
			return Health.ERROR;
		else if (fullHealth.size () == 0)
			return Health.HEALTHY;
		else 
			return Health.UNHEALTHY;
	}

	/***
	 * Gets a collection of Health objects. The server is not healthy
	 * when the collection is null or not empty.
	 * If the collection is null there was a connection failure. 
	 * When is not null, the elements in the collection represent the 
	 * failures.
	 * 
	 * @return collection of Health objects
	 */
	public Collection<Health> getFullHealth ()
	{
		Collection<Health> collection = new ArrayList<Health> ();
		Collection<Graph> graphs = null;
		
		try {
			graphs = getGraphs ();
		} catch (SAXException e) {
			return null;
		}
		if (graphs == null)
			return null;
		
		for (Graph g : graphs) {
			for (SingleGraph sg : g.getSingleGraphs()) {
				for (Line l : sg.getLines()) {
					if (l.isReached()) {
						collection.add (new Health (l.getMaxLimit(),
								                    l.getMinLimit(),
								                    sg.getHeadline(),
								                    l.getLabel()));
					}
				}
			}
		}
		
		return collection;
	}
	
	/***
	 * Gets all available logs. 
	 * 
	 * This doesn't return the content of the logs, it only
	 * lists the available logs. To get the content use getLog()
	 * 
	 * @return collection of logs
	 * @throws SAXException 
	 */
	public Collection<Log> getLogs () throws SAXException
	{
		String xmlData = null;

		try {
			xmlData = new RestClient ().getMethod (server, "/logs");
		} catch (Exception e) {
			return null;
		}
		
		if (xmlData == null)
			return null;
		
		return Log.FromXmlData (xmlData);
	}
	
	/*** 
	 * Gets the content of the log
	 * 
	 * @param id
	 * @param posBegin
	 * @param lines
	 * @return log
	 */
	public Log getLog (String id, int posBegin, int lines)
	{
		// we will use server.getUrl() + "logs" + id + "?pos_begin=" + posBegin + "&" + "lines=" + lines
		
		// FIXME: Returning canned response
		return new Log ("system", "/var/log/messages", 
				"System messages", 2, "this is the content of the log");
	}
	
}
