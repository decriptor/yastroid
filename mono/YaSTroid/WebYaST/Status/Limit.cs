
namespace WebYaST.Status
{
	public class Limit
	{
		string metricColumn;
		int max;
		int min;
		
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