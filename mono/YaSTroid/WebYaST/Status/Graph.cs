//import java.util.Collection;
//import java.util.Collections;
//
//import org.xml.sax.SAXException;
//
//import android.util.Xml;
using System.Collections.Generic;
using Android.Util;

namespace YaSTroid.WebYaST.Status
{
	//FIXME: JUnit this
	public class Graph
	{
		private string id;
		private int yScale;
		private string yLabel;
		private int yMax;
		private int yDecimalPlaces;
		private List<SingleGraph> singleGraphs;
		
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

		public IEnumerable<SingleGraph> getSingleGraphs ()
		{
			return singleGraphs.AsReadOnly ();
		}
		
		public static List<Graph> FromXmlData (string xmlData)
		{
			GraphContentHandler contentHandler = new GraphContentHandler ();
			Xml.Parse (xmlData, contentHandler);
			return contentHandler.getGraphs ();
		}
	}
}
