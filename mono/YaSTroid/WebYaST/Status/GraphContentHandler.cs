using System;
using System.Collections.Generic;
using Org.Xml.Sax;
using Org.Xml.Sax.Helpers;

namespace YaSTroid.WebYaST.Status
{
	public class GraphContentHandler : DefaultHandler
	{
		private List<Graph> graphs;

		private bool inGraph;
		
		// <graph> flags
		private bool inId;
		private bool inYScale;
		private bool inYLabel;
		private bool inYMax;
		private bool inYDecimalPlaces;
		private bool inSingleGraphs;
		
		// <single_graphs> flags
		private bool inSingleGraph;
		
		// <single_graph> flags
		private bool inCummulated;
		private bool inLinegraph;
		private bool inHeadline;
		private bool inLines;
		
		// <lines> flags
		private bool inLine;
		
		// <line> flags
		private bool inMetricId;
		private bool inMetricColumn;
		private bool inLabel;
		private bool inLimits;
		
		// <limits> flags
		private bool inMaxLimit;
		private bool inMinLimit;
		private bool inReached;
		
		// <graph> values
		private string id;
		private int yScale;
		private string yLabel;
		private int yMax;
		private int yDecimalPlaces;
		private List<SingleGraph> singleGraphs;
		
		// <single_graph> values
		private bool cummulated;
		private bool linegraph;
		private string headline;
		private List<Line> lines;
		
		// <line> values
		private string metricId;
		private string metricColumn;
		private string label;
		
		// <limits> values
		private int maxLimit;
		private int minLimit;
		private bool reached;
		
		public List<Graph> getGraphs ()
		{
			return graphs;
		}

		public void startElement (string uri, string localName, string qName, IAttributes atts)// throws SAXException
		{
			base.StartElement (uri, localName, qName, atts);

			if (localName.CompareTo ("graph") == 0) {
				 inGraph = true;
				 if (graphs == null)
					 graphs = new List<Graph> ();
				 singleGraphs = new List<SingleGraph> ();
			} else if (localName.CompareTo ("id") == 0 && inGraph)
				inId = true;
			 else if (localName.CompareTo ("y_scale") == 0 && inGraph)
				inYScale = true;
			 else if (localName.CompareTo ("y_label") == 0 && inGraph)
				 inYLabel = true;
			 else if (localName.CompareTo ("y_max") == 0 && inGraph)
				 inYMax = true;
			 else if (localName.CompareTo ("y_decimal_places") == 0 && inGraph)
				 inYDecimalPlaces = true;
			 else if (localName.CompareTo ("single_graphs") == 0 && inGraph) {
				 inSingleGraphs = true;
				 singleGraphs = new List<SingleGraph> ();
			 } else if (localName.CompareTo ("single_graph") == 0 && inSingleGraphs) {
				 inSingleGraph = true;
				 lines = new List<Line> ();
			 } else if (localName.CompareTo ("cummulated") == 0 && inSingleGraph)
				 inCummulated = true;
			 else if (localName.CompareTo ("linegraph") == 0 && inSingleGraph)
				 inLinegraph = true;
			 else if (localName.CompareTo ("headline") == 0 && inSingleGraph)
				 inHeadline = true;
			 else if (localName.CompareTo ("lines") == 0 && inSingleGraph) {
				 inLines = true;
				 lines = new List<Line> ();
			 } else if (localName.CompareTo ("line") == 0 && inLines)
				 inLine = true;
			 else if (localName.CompareTo ("metric_id") == 0 && inLine)
				 inMetricId = true;
			 else if (localName.CompareTo ("metric_column") == 0 && inLine)
				 inMetricColumn = true;
			 else if (localName.CompareTo ("label") == 0 && inLine)
				 inLabel = true;
			 else if (localName.CompareTo ("limits") == 0 && inLine)
				 inLimits = true;
			 else if (localName.CompareTo ("max") == 0 && inLimits)
				 inMaxLimit = true;
			 else if (localName.CompareTo ("min") == 0 && inLimits)
				 inMinLimit = true;
			 else if (localName.CompareTo ("reached") == 0 && inLimits)
				 inReached = true;
		}
		
		public void endElement (string uri, string localName, string qName)// throws SAXException
		{
			base.EndElement (uri, localName, qName);
			
			if (localName.CompareTo ("graph") == 0) {
				 inGraph = false;
				graphs.Add (new Graph (id, yScale, yLabel, yMax,
						 yDecimalPlaces, singleGraphs));
				 id = yLabel = null;
				 yScale = yMax = yDecimalPlaces = 0;
			} else if (localName.CompareTo ("id") == 0 && inGraph)
				inId = false; 
			else if (localName.CompareTo ("y_scale") == 0 && inGraph)
				inYScale = false;
			 else if (localName.CompareTo ("y_label") == 0 && inGraph)
				 inYLabel = false;
			 else if (localName.CompareTo ("y_max") == 0 && inGraph)
				 inYMax = false;
			 else if (localName.CompareTo ("y_decimal_places") == 0 && inGraph)
				 inYDecimalPlaces = false;
			 else if (localName.CompareTo ("single_graphs") == 0 && inGraph) {
				 inSingleGraphs = false;
			 }  else if (localName.CompareTo ("single_graph") == 0 && inSingleGraphs) {
				 inSingleGraph = false;
				singleGraphs.Add (new SingleGraph (cummulated, linegraph, headline, lines));
				 cummulated = linegraph = false;
				 headline = null;
			 } else if (localName.CompareTo ("cummulated") == 0 && inSingleGraph)
				 inCummulated = false;
			 else if (localName.CompareTo ("linegraph") == 0 && inSingleGraph)
				 inLinegraph = false;
			 else if (localName.CompareTo ("headline") == 0 && inSingleGraph)
				 inHeadline = false;
			 else if (localName.CompareTo ("lines") == 0 && inSingleGraph)
				 inLines = false;
			 else if (localName.CompareTo ("line") == 0 && inLines) {
				 inLine = false;
				lines.Add (new Line (metricId, metricColumn, label, maxLimit, minLimit, reached));
				 metricId = metricColumn = label = null;
				 maxLimit = minLimit = 0;
				 reached = false;
			 } else if (localName.CompareTo ("metric_id") == 0 && inLine)
				 inMetricId = false;
			 else if (localName.CompareTo ("metric_column") == 0 && inLine)
				 inMetricColumn = false;
			 else if (localName.CompareTo ("label") == 0 && inLine)
				 inLabel = false;
			 else if (localName.CompareTo ("limits") == 0 && inLine)
				 inLimits = false;
			 else if (localName.CompareTo ("max") == 0 && inLimits)
				 inMaxLimit = false;
			 else if (localName.CompareTo ("min") == 0 && inLimits)
				 inMinLimit = false;
			 else if (localName.CompareTo ("reached") == 0 && inLimits)
				 inReached = false;
		}
		
		public void characters (char[] ch, int start, int length)// throws SAXException
		{
			base.Characters (ch, start, length);
			string text = new string (ch, start, length);
			
			if (inGraph) {
				if (inId)
					id = text;
				else if (inYScale) {
					int result;
					Int32.TryParse (text, out result);
					yScale = result;
				}
				else if (inYLabel)
					yLabel = text;
				else if (inYMax) {
					int result;
					Int32.TryParse (text, out result);
					yMax = result;
				} else if (inYDecimalPlaces) {
					int result;
					Int32.TryParse (text, out result);
					yDecimalPlaces = result;
				}
				else if (inSingleGraphs) {
					if (inSingleGraph) {
						if (inCummulated) {
							bool result;
							bool.TryParse (text, out result);
							cummulated = result;
						} else if (inLinegraph) {
							bool result;
							bool.TryParse (text, out result);
							linegraph = result;
						}
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
									if (inMaxLimit) {
										int result;
										Int32.TryParse (text, out result);
										maxLimit = result;
									} else if (inMinLimit) {
										int result;
										Int32.TryParse (text, out result);
										minLimit = result;
									} else if (inReached) {
										bool result;
										bool.TryParse (text, out result);
										reached = result;
									}
								}
							}
						}
					}
				}
			}

		}

	}
}
