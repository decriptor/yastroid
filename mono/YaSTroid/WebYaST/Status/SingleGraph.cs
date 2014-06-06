using System.Collections.Generic;

namespace YaSTroid.WebYaST.Status
{
	//FIXME: JUnit this
	public class SingleGraph
	{	
		private bool cummulated;
		private bool linegraph;
		private string headline;
		private List<Line> lines;
		
		public SingleGraph (bool cummulated, bool linegraph, string headline, List<Line> lines)
		{
			this.cummulated = cummulated;
			this.linegraph = linegraph;
			this.headline = headline;
			this.lines = lines;
		}
		
		public bool isCummulated () 
		{
			return cummulated;
		}

		public bool isLinegraph ()
		{
			return linegraph;
		}

		public string getHeadline ()
		{
			return headline;
		}

		public IEnumerable<Line> getLines ()
		{
			return lines.AsReadOnly ();
		}
	}
}
