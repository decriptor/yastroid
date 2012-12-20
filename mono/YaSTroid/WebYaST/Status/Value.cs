using System.Collections.Generic;

namespace WebYaST.Status
{
	public class Value
	{
		int interval;
		string column;
		int start;
		List<float> values;
		
		public Value (int interval, string column, int start, List<float> values)
		{
			this.interval = interval;
			this.column = column;
			this.start = start;
			this.values = values;
		}

		public int GetInterval() {
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
