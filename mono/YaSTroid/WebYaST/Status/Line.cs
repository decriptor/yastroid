
namespace YaSTroid.WebYaST
{

	//FIXME: JUnit this
	public class Line
	{
		private string metricId;
		private string metricColumn;
		private string label;
		private int maxLimit;
		private int minLimit;
		private bool reached;
		
		public Line (string metricId, string metricColumn, string label, 
				int maxLimit, int minLimit, bool reached)
		{
			this.metricId = metricId;
			this.metricColumn = metricColumn;
			this.label = label;
			this.maxLimit = maxLimit;
			this.minLimit = minLimit;
			this.reached = reached;
		}

		public string getMetricId () 
		{
			return metricId;
		}
		
		public string getMetricColumn ()
		{
			return metricColumn;
		}

		public string getLabel () 
		{
			return label;
		}

		public int getMaxLimit () 
		{
			return maxLimit;
		}

		public int getMinLimit () 
		{
			return minLimit;
		}

		public bool isReached () 
		{
			return reached;
		}
	}
}
