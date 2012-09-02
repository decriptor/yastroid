package com.novell.webyast.status;

import java.util.Collection;
import java.util.Collections;

//FIXME: JUnit this
public class SingleGraph {
	
	private boolean cummulated;
	private boolean linegraph;
	private String headline;
	private Collection<Line> lines;
	
	public SingleGraph (boolean cummulated, boolean linegraph, 
			String headline, Collection<Line> lines)
	{
		this.cummulated = cummulated;
		this.linegraph = linegraph;
		this.headline = headline;
		this.lines = lines;
	}
	
	public boolean isCummulated () 
	{
		return cummulated;
	}

	public boolean isLinegraph ()
	{
		return linegraph;
	}

	public String getHeadline ()
	{
		return headline;
	}
	
	public Collection<Line> getLines ()
	{
		return Collections.unmodifiableCollection (lines);
	}

}
