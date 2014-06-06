
namespace YaSTroid.WebYaST.Status
{
	public class Limit
	{
		private string metricColumn;
		private int max;
		private int min;
		
		public Limit (string metricColumn, int max, int min)
		{
			this.metricColumn = metricColumn;
			this.max = max;
			this.min = min;
		}

		public int getMax() {
			return max;
		}

		public string getMetricColumn() {
			return metricColumn;
		}

		public int getMin() {
			return min;
		}
	}
}
