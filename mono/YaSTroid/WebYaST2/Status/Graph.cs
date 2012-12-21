
using System.Collections.Generic;

namespace WebYaST.Status
{
	public class Graph
	{
		string id;
		int yScale;
		string yLabel;
		int yMax;
		int yDecimalPlaces;
		List<SingleGraph> singleGraphs;
		
		public Graph (string id, int yScale, string yLabel, int yMax,
				int yDecimalPlaces, List<SingleGraph> singleGraphs)
		{
			this.id = id;
			this.yScale = yScale;
			this.yLabel = yLabel;
			this.yMax = yMax;
			this.yDecimalPlaces = yDecimalPlaces;
			this.singleGraphs = singleGraphs;
		}

		public string getId ()
		{
			return id;
		}

		public string getyLabel ()
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

		public List<SingleGraph> getSingleGraphs ()
		{
			return singleGraphs; // should be immutable collection
		}
		
		public static List<Graph> FromXmlData (string xmlData)
		{
			GraphContentHandler contentHandler = new GraphContentHandler ();
			//Xml.parse (xmlData, contentHandler);
			return contentHandler.getGraphs ();
		}
	}
}