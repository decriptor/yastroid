package com.novell.webyast.status;

import java.util.Collection;
import java.util.Collections;

//FIXME: JUnit this
public class Graph {
	private String id;
	private int yScale;
	private int yLabel;
	private int yMax;
	private int yDecimalPlaces;
	private Collection<SingleGraph> singleGraphs;

	public String getId ()
	{
		return id;
	}

	public int getyLabel ()
	{
		return yLabel;
	}

	public int getyMax ()
	{
		return yMax;
	}

	public int getyScale ()
	{
		return yScale;
	}

	public int getyDecimalPlaces ()
	{
		return yDecimalPlaces;
	}
	
	public Collection<SingleGraph> getSingleGraphs ()
	{
		return Collections.unmodifiableCollection (singleGraphs);
	}
}
