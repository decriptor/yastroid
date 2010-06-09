package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;
import com.novell.webyast.Server;

//FIXME: JUnit this

/***
 * 
 * Gets status information.
 * 
 * - Graphs: health status - in progress
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
	
	public Collection<Graph> getGraphs ()
	{
		// we will use server.getUrl() + "graphs?checklimits=true"
		// FIXME: Use real data
		return new ArrayList<Graph> ();
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
	public Collection<Health> getHealth ()
	{
		Collection<Health> collection = new ArrayList<Health> ();
		
		for (Graph g : getGraphs ()) {
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
