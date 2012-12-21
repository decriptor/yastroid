using System.Collections.Generic;

namespace WebYaST.Status
{
	public class GraphContentHandler
	{	
		List<Graph> graphs;

		bool inGraph;
		
		// <graph> flags
		bool inId;
		bool inYScale;
		bool inYLabel;
		bool inYMax;
		bool inYDecimalPlaces;
		bool inSingleGraphs;
		
		// <single_graphs> flags
		bool inSingleGraph;
		
		// <single_graph> flags
		bool inCummulated;
		bool inLinegraph;
		bool inHeadline;
		bool inLines;
		
		// <lines> flags
		bool inLine;
		
		// <line> flags
		bool inMetricId;
		bool inMetricColumn;
		bool inLabel;
		bool inLimits;
		
		// <limits> flags
		bool inMaxLimit;
		bool inMinLimit;
		bool inReached;
		
		// <graph> values
		string id;
		int yScale;
		string yLabel;
		int yMax;
		int yDecimalPlaces;
		List<SingleGraph> singleGraphs;

		// <single_graph> values
		bool cummulated;
		bool linegraph;
		string headline;
		List<Line> lines;
		
		// <line> values
		string metricId;
		string metricColumn;
		string label;
		
		// <limits> values
		int maxLimit;
		int minLimit;
		bool reached;
		
		public List<Graph> getGraphs ()
		{
			return graphs;
		}

//		public void startElement (string uri, string localName, string qName, Attributes atts)
//		{
//			base.startElement (uri, localName, qName, atts);
//
//			if (localName.compareTo ("graph") == 0) {
//				 inGraph = true;
//				 if (graphs == null)
//					 graphs = new List<Graph> ();
//				 singleGraphs = new List<SingleGraph> ();
//			} else if (localName.compareTo ("id") == 0 && inGraph)
//				inId = true;
//			 else if (localName.compareTo ("y_scale") == 0 && inGraph)
//				inYScale = true;
//			 else if (localName.compareTo ("y_label") == 0 && inGraph)
//				 inYLabel = true;
//			 else if (localName.compareTo ("y_max") == 0 && inGraph)
//				 inYMax = true;
//			 else if (localName.compareTo ("y_decimal_places") == 0 && inGraph)
//				 inYDecimalPlaces = true;
//			 else if (localName.compareTo ("single_graphs") == 0 && inGraph) {
//				 inSingleGraphs = true;
//				 singleGraphs = new List<SingleGraph> ();
//			 } else if (localName.compareTo ("single_graph") == 0 && inSingleGraphs) {
//				 inSingleGraph = true;
//				 lines = new List<Line> ();
//			 } else if (localName.compareTo ("cummulated") == 0 && inSingleGraph)
//				 inCummulated = true;
//			 else if (localName.compareTo ("linegraph") == 0 && inSingleGraph)
//				 inLinegraph = true;
//			 else if (localName.compareTo ("headline") == 0 && inSingleGraph)
//				 inHeadline = true;
//			 else if (localName.compareTo ("lines") == 0 && inSingleGraph) {
//				 inLines = true;
//				 lines = new List<Line> ();
//			 } else if (localName.compareTo ("line") == 0 && inLines)
//				 inLine = true;
//			 else if (localName.compareTo ("metric_id") == 0 && inLine)
//				 inMetricId = true;
//			 else if (localName.compareTo ("metric_column") == 0 && inLine)
//				 inMetricColumn = true;
//			 else if (localName.compareTo ("label") == 0 && inLine)
//				 inLabel = true;
//			 else if (localName.compareTo ("limits") == 0 && inLine)
//				 inLimits = true;
//			 else if (localName.compareTo ("max") == 0 && inLimits)
//				 inMaxLimit = true;
//			 else if (localName.compareTo ("min") == 0 && inLimits)
//				 inMinLimit = true;
//			 else if (localName.compareTo ("reached") == 0 && inLimits)
//				 inReached = true;
//		}
		
		public void endElement (string uri, string localName, string qName)
		{
//			base.endElement (uri, localName, qName);
//
//			if (localName.compareTo ("graph") == 0) {
//				 inGraph = false;
//				 graphs.Add (new Graph (id, yScale, yLabel, yMax,
//						 yDecimalPlaces, singleGraphs));
//				 id = yLabel = null;
//				 yScale = yMax = yDecimalPlaces = 0;
//			} else if (localName.compareTo ("id") == 0 && inGraph)
//				inId = false; 
//			else if (localName.compareTo ("y_scale") == 0 && inGraph)
//				inYScale = false;
//			 else if (localName.compareTo ("y_label") == 0 && inGraph)
//				 inYLabel = false;
//			 else if (localName.compareTo ("y_max") == 0 && inGraph)
//				 inYMax = false;
//			 else if (localName.compareTo ("y_decimal_places") == 0 && inGraph)
//				 inYDecimalPlaces = false;
//			 else if (localName.compareTo ("single_graphs") == 0 && inGraph) {
//				 inSingleGraphs = false;
//			 }  else if (localName.compareTo ("single_graph") == 0 && inSingleGraphs) {
//				 inSingleGraph = false;
//				 singleGraphs.Add (new SingleGraph (cummulated, linegraph, headline, lines));
//				 cummulated = linegraph = false;
//				 headline = null;
//			 } else if (localName.compareTo ("cummulated") == 0 && inSingleGraph)
//				 inCummulated = false;
//			 else if (localName.compareTo ("linegraph") == 0 && inSingleGraph)
//				 inLinegraph = false;
//			 else if (localName.compareTo ("headline") == 0 && inSingleGraph)
//				 inHeadline = false;
//			 else if (localName.compareTo ("lines") == 0 && inSingleGraph)
//				 inLines = false;
//			 else if (localName.compareTo ("line") == 0 && inLines) {
//				 inLine = false;
//				 lines.Add (new Line (metricId, metricColumn, label, maxLimit, minLimit, reached));
//				 metricId = metricColumn = label = null;
//				 maxLimit = minLimit = 0;
//				 reached = false;
//			 } else if (localName.compareTo ("metric_id") == 0 && inLine)
//				 inMetricId = false;
//			 else if (localName.compareTo ("metric_column") == 0 && inLine)
//				 inMetricColumn = false;
//			 else if (localName.compareTo ("label") == 0 && inLine)
//				 inLabel = false;
//			 else if (localName.compareTo ("limits") == 0 && inLine)
//				 inLimits = false;
//			 else if (localName.compareTo ("max") == 0 && inLimits)
//				 inMaxLimit = false;
//			 else if (localName.compareTo ("min") == 0 && inLimits)
//				 inMinLimit = false;
//			 else if (localName.compareTo ("reached") == 0 && inLimits)
//				 inReached = false;
		}
		
		public void characters (char[] ch, int start, int length)
		{
//			base.characters (ch, start, length);
//			string text = new string (ch, start, length);
//			
//			if (inGraph) {
//				if (inId)
//					id = text;
//				else if (inYScale)
//					yScale = Integer.parseInt (text);
//				else if (inYLabel)
//					yLabel = text;
//				else if (inYMax)
//					yMax = Integer.parseInt (text);
//				else if (inYDecimalPlaces)
//					yDecimalPlaces = Integer.parseInt (text);
//				else if (inSingleGraphs) {
//					if (inSingleGraph) {
//						if (inCummulated)
//							cummulated = bool.parsebool (text);
//						else if (inLinegraph)
//							linegraph = bool.parsebool (text);
//						else if (inHeadline)
//							headline = text;
//						else if (inLines) {
//							if (inLine) {
//								if (inMetricId)
//									metricId = text;
//								else if (inMetricColumn)
//									metricColumn = text;
//								else if (inLabel)
//									label = text;
//								else if (inLimits) {
//									if (inMaxLimit)
//										maxLimit = Integer.parseInt (text);
//									else if (inMinLimit)
//										minLimit = Integer.parseInt (text);
//									else if (inReached)
//										reached = bool.parsebool (text);
//								}
//							}
//						}
//					}
//				}
//			}

		}

	}
}
