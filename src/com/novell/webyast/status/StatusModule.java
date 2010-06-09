package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;
import com.novell.webyast.Server;

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
	
	// FIXME: JUnit this
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
	
}
