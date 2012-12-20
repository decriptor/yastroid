using System;
using System.Collections.Generic;

namespace WebYaST.Status
{
	public class StatusModule
	{
		Server server;

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
		public List<Metric> getMetrics ()
		{
			string xmlData = null;
			
			try {
				xmlData = new RestClient ().GetMethod (server, "/metrics");
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
		public List<Metric> getMetric (int metricType)
		{
			List<Metric> metrics = null;
			
			try {
				metrics = getMetrics ();
			} catch (Exception e) {
				return null;
			}
			
			string plugin = null;
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
			
			List<Metric> collection = new List<Metric> ();
			foreach (Metric m in metrics) {
				if (m.getPlugin () == plugin)
					collection.Add (m);
			}
			
			return collection;
		}
		
		public Metric getMetricData (string id, int start, int stop)
		{
			string xmlData = null;
			
			try {
				string startStop = "";
				if (start > 0)
					startStop = "?start=" + start;
				if (stop > 0)
					startStop += (startStop == "" ? "?" : "&") + "stop=" + stop;
				
				xmlData = new RestClient ().GetMethod (server, "/metrics/" + id + startStop);
			} catch (Exception e) {
				return null;
			}
			
			if (xmlData == null)
				return null;
			
			List<Metric> metrics = Metric.FromXmlData (xmlData);
			if (metrics.Count == 0)
				return null;
			
			return metrics[0];
		}
		
		/***
		 * Gets the data returned by the graphs method.
		 * 
		 * @return Collection of Graph objects
		 * @throws SAXException
		 */
		public List<Graph> getGraphs ()
		{
			string xmlData = null;
			
			try {
				xmlData = new RestClient ().GetMethod (server, "/graphs?checklimits=true");
			} catch (Exception e) {
				return null;
			}
			
			if (xmlData == null)
				return null;
			
			return Graph.FromXmlData (xmlData);
		}
		
		public bool isHealthy (int element, List<Graph> graphs)
		{
			if (graphs == null)
				return false;
			
			string id = null;
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
			
			foreach (Graph g in graphs) {
				if (g.getId () == id) {
					foreach (SingleGraph sg in g.getSingleGraphs()) {
						foreach (Line l in sg.getLines())
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
			List<Health> fullHealth = getFullHealth ();
			if (fullHealth == null)
				return Health.ERROR;
			if (fullHealth.Count == 0)
				return Health.HEALTHY;
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
		public List<Health> getFullHealth ()
		{
			List<Health> collection = new List<Health> ();
			List<Graph> graphs = null;
			
			try {
				graphs = getGraphs ();
			} catch (Exception e) {
				return null;
			}
			if (graphs == null)
				return null;
			
			foreach (Graph g in graphs) {
				foreach (SingleGraph sg in g.getSingleGraphs()) {
					foreach (Line l in sg.getLines()) {
						if (l.isReached()) {
							collection.Add (new Health (l.getMaxLimit(),
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
		public List<Log> getLogs ()
		{
			string xmlData = null;
			
			try {
				xmlData = new RestClient ().GetMethod (server, "/logs");
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
		public Log getLog (string id, int posBegin, int lines)
		{
			// we will use server.getUrl() + "logs" + id + "?pos_begin=" + posBegin + "&" + "lines=" + lines
			
			// FIXME: Returning canned response
			return new Log ("system", "/var/log/messages", 
			                "System messages", 2, "this is the content of the log");
		}
		
	}
}

