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
			return Health.Error;
		else if (fullHealth.size () == 0)
			return Health.Healthy;
		else 
			return Health.Unhealthy;
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
	 */
	public Collection<Log> getLogs ()
	{
		// we will use server.getUrl() + "logs"
		return new ArrayList<Log> ();
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
