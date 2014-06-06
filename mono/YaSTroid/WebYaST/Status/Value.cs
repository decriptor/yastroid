using System.Collections.Generic;

namespace YaSTroid.WebYaST.Status
{
	public class Value
		{
		private int interval;
		private string column;
		private int start;
		private List<float> values;
		
		public Value (int interval, string column, int start, List<float> values)
		{
			this.interval = interval;
			this.column = column;
			this.start = start;
			this.values = values;
		}

		public int getInterval() {
			return interval;
		}

		public string getColumn() {
			return column;
		}

		public int getStart() {
			return start;
		}
		
		public List<float> getValues ()
		{
			return values;
		}
	}
}
