
using System.Collections.Generic;

namespace WebYaST.Status
{
	public class SingleGraph
	{
		bool cummulated;
		bool linegraph;
		string headline;
		List<Line> lines;
		
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
		
		public List<Line> getLines ()
		{
			return lines; //Collections.unmodifiableCollection (lines);
		}

	}
}
