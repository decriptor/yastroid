package com.novell.webyast.status;

import java.util.ArrayList;
import java.util.Collection;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

public class GraphContentHandler extends DefaultHandler {
	
	private Collection<Graph> graphs;

	private boolean inGraph;
	
	// <graph> flags
	private boolean inId;
	private boolean inYScale;
	private boolean inYLabel;
	private boolean inYMax;
	private boolean inYDecimalPlaces;
	private boolean inSingleGraphs;
	
	// <single_graphs> flags
	private boolean inSingleGraph;
	
	// <single_graph> flags
	private boolean inCummulated;
	private boolean inLinegraph;
	private boolean inHeadline;
	private boolean inLines;
	
	// <lines> flags
	private boolean inLine;
	
	// <line> flags
	private boolean inMetricId;
	private boolean inMetricColumn;
	private boolean inLabel;
	private boolean inLimits;
	
	// <limits> flags
	private boolean inMaxLimit;
	private boolean inMinLimit;
	private boolean inReached;
	
	// <graph> values
	private String id;
	private int yScale;
	private String yLabel;
	private int yMax;
	private int yDecimalPlaces;
	private Collection<SingleGraph> singleGraphs;
	
	// <single_graph> values
	private boolean cummulated;
	private boolean linegraph;
	private String headline;
	private Collection<Line> lines;
	
	// <line> values
	private String metricId;
	private String metricColumn;
	private String label;
	
	// <limits> values
	private int maxLimit;
	private int minLimit;
	private boolean reached;
	
	public Collection<Graph> getGraphs ()
	{
		return graphs;
	}

	public void startElement (String uri, String localName, String qName, Attributes atts) throws SAXException
	{
		super.startElement (uri, localName, qName, atts);

		if (localName.compareTo ("graph") == 0) {
			 inGraph = true;
			 if (graphs == null)
				 graphs = new ArrayList<Graph> ();
			 singleGraphs = new ArrayList<SingleGraph> ();
		} else if (localName.compareTo ("id") == 0 && inGraph)
			inId = true;
		 else if (localName.compareTo ("y_scale") == 0 && inGraph)
			inYScale = true;
		 else if (localName.compareTo ("y_label") == 0 && inGraph)
			 inYLabel = true;
		 else if (localName.compareTo ("y_max") == 0 && inGraph)
			 inYMax = true;
		 else if (localName.compareTo ("y_decimal_places") == 0 && inGraph)
			 inYDecimalPlaces = true;
		 else if (localName.compareTo ("single_graphs") == 0 && inGraph) {
			 inSingleGraphs = true;
			 singleGraphs = new ArrayList<SingleGraph> ();
		 } else if (localName.compareTo ("single_graph") == 0 && inSingleGraphs) {
			 inSingleGraph = true;
			 lines = new ArrayList<Line> ();
		 } else if (localName.compareTo ("cummulated") == 0 && inSingleGraph)
			 inCummulated = true;
		 else if (localName.compareTo ("linegraph") == 0 && inSingleGraph)
			 inLinegraph = true;
		 else if (localName.compareTo ("headline") == 0 && inSingleGraph)
			 inHeadline = true;
		 else if (localName.compareTo ("lines") == 0 && inSingleGraph) {
			 inLines = true;
			 lines = new ArrayList<Line> ();
		 } else if (localName.compareTo ("line") == 0 && inLines)
			 inLine = true;
		 else if (localName.compareTo ("metric_id") == 0 && inLine)
			 inMetricId = true;
		 else if (localName.compareTo ("metric_column") == 0 && inLine)
			 inMetricColumn = true;
		 else if (localName.compareTo ("label") == 0 && inLine)
			 inLabel = true;
		 else if (localName.compareTo ("limits") == 0 && inLine)
			 inLimits = true;
		 else if (localName.compareTo ("max") == 0 && inLimits)
			 inMaxLimit = true;
		 else if (localName.compareTo ("min") == 0 && inLimits)
			 inMinLimit = true;
		 else if (localName.compareTo ("reached") == 0 && inLimits)
			 inReached = true;
	}
	
	public void endElement (String uri, String localName, String qName) throws SAXException
	{
		super.endElement (uri, localName, qName);
		
		if (localName.compareTo ("graph") == 0) {
			 inGraph = false;
			 graphs.add (new Graph (id, yScale, yLabel, yMax,
					 yDecimalPlaces, singleGraphs));
			 id = yLabel = null;
			 yScale = yMax = yDecimalPlaces = 0;
		} else if (localName.compareTo ("id") == 0 && inGraph)
			inId = false; 
		else if (localName.compareTo ("y_scale") == 0 && inGraph)
			inYScale = false;
		 else if (localName.compareTo ("y_label") == 0 && inGraph)
			 inYLabel = false;
		 else if (localName.compareTo ("y_max") == 0 && inGraph)
			 inYMax = false;
		 else if (localName.compareTo ("y_decimal_places") == 0 && inGraph)
			 inYDecimalPlaces = false;
		 else if (localName.compareTo ("single_graphs") == 0 && inGraph) {
			 inSingleGraphs = false;
		 }  else if (localName.compareTo ("single_graph") == 0 && inSingleGraphs) {
			 inSingleGraph = false;
			 singleGraphs.add (new SingleGraph (cummulated, linegraph, headline, lines));
			 cummulated = linegraph = false;
			 headline = null;
		 } else if (localName.compareTo ("cummulated") == 0 && inSingleGraph)
			 inCummulated = false;
		 else if (localName.compareTo ("linegraph") == 0 && inSingleGraph)
			 inLinegraph = false;
		 else if (localName.compareTo ("headline") == 0 && inSingleGraph)
			 inHeadline = false;
		 else if (localName.compareTo ("lines") == 0 && inSingleGraph)
			 inLines = false;
		 else if (localName.compareTo ("line") == 0 && inLines) {
			 inLine = false;
			 lines.add (new Line (metricId, metricColumn, label, maxLimit, minLimit, reached));
			 metricId = metricColumn = label = null;
			 maxLimit = minLimit = 0;
			 reached = false;
		 } else if (localName.compareTo ("metric_id") == 0 && inLine)
			 inMetricId = false;
		 else if (localName.compareTo ("metric_column") == 0 && inLine)
			 inMetricColumn = false;
		 else if (localName.compareTo ("label") == 0 && inLine)
			 inLabel = false;
		 else if (localName.compareTo ("limits") == 0 && inLine)
			 inLimits = false;
		 else if (localName.compareTo ("max") == 0 && inLimits)
			 inMaxLimit = false;
		 else if (localName.compareTo ("min") == 0 && inLimits)
			 inMinLimit = false;
		 else if (localName.compareTo ("reached") == 0 && inLimits)
			 inReached = false;
	}
	
	public void characters (char[] ch, int start, int length) throws SAXException
	{
		super.characters (ch, start, length);
		String text = new String (ch, start, length);
		
		if (inGraph) {
			if (inId)
				id = text;
			else if (inYScale)
				yScale = Integer.parseInt (text);
			else if (inYLabel)
				yLabel = text;
			else if (inYMax)
				yMax = Integer.parseInt (text);
			else if (inYDecimalPlaces)
				yDecimalPlaces = Integer.parseInt (text);
			else if (inSingleGraphs) {
				if (inSingleGraph) {
					if (inCummulated)
						cummulated = Boolean.parseBoolean (text);
					else if (inLinegraph)
						linegraph = Boolean.parseBoolean (text);
					else if (inHeadline)
						headline = text;
					else if (inLines) {
						if (inLine) {
							if (inMetricId)
								metricId = text;
							else if (inMetricColumn)
								metricColumn = text;
							else if (inLabel)
								label = text;
							else if (inLimits) {
								if (inMaxLimit)
									maxLimit = Integer.parseInt (text);
								else if (inMinLimit)
									minLimit = Integer.parseInt (text);
								else if (inReached)
									reached = Boolean.parseBoolean (text);
							}
						}
					}
				}
			}
		}

	}

}
