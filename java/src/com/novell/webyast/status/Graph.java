package com.novell.webyast.status;

import java.util.Collection;
import java.util.Collections;

import org.xml.sax.SAXException;

import android.util.Xml;

//FIXME: JUnit this
public class Graph {
	private String id;
	private int yScale;
	private String yLabel;
	private int yMax;
	private int yDecimalPlaces;
	private Collection<SingleGraph> singleGraphs;
	
	public Graph (String id, int yScale, String yLabel, int yMax,
			int yDecimalPlaces, Collection<SingleGraph> singleGraphs)
	{
		this.id = id;
		this.yScale = yScale;
		this.yLabel = yLabel;
		this.yMax = yMax;
		this.yDecimalPlaces = yDecimalPlaces;
		this.singleGraphs = singleGraphs;
	}

	public String getId ()
	{
		return id;
	}

	public String getyLabel ()
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
	
	public static Collection<Graph> FromXmlData (String xmlData) throws SAXException
	{
		GraphContentHandler contentHandler = new GraphContentHandler ();
		Xml.parse (xmlData, contentHandler);
		return contentHandler.getGraphs ();
	}
}
