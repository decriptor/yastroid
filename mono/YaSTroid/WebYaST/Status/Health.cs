using System;


namespace YaSTroid.WebYaST.Status
{
	// FIXME: JUnit this

	/***
	 * Represents the health of a particular element in the server.
	 * Known elements are: Network, Memory, CPU and Disk. 
	 * 
	 * @author Mario Carrion
	 *
	 */
	public class Health : IComparable
	{
		public const int ERROR = 0;
		public const int HEALTHY = 1;
		public const int UNHEALTHY = 2;
		
		// FIXME: These values are temporal, ids are translated
		public const string NETWORK_ID = "Network";
		public const string CPU_ID = "CPU";
		public const string DISK_ID = "Disk";
		public const string MEMORY_ID = "Memory";
		
		private int maxLimit;
		private int minLimit;
		private string headline;
		private string label;
		
		public Health (int maxLimit, int minLimit, string headline, string label)
		{
			this.maxLimit = maxLimit;
			this.minLimit = minLimit;
			this.headline = headline;
			this.label = label;
		}
		
		public void setHeadline(string headline) {
			this.headline = headline;
		}

		/***
		 * Gets the maximum limit reached. 
		 * 
		 * If this value is greater than 0 it means the alarm is active, 
		 * it exceeds the configured limit.   
		 * 
		 * @return maximum limit
		 */
		public int getMaxLimit () 
		{
			return maxLimit;
		}

		/***
		 * Gets translated headline of unhealthy element
		 * 
		 * For example "Memory" or "Network"
		 * 
		 * @return translated headline
		 */
		public string getHeadline() {
			return headline;
		}

		/***
		 * Gets translated label of unhealthy element
		 * 
		 * For example "free", "cached" or "used", when the headline
		 * is "Memory"
		 * 
		 * @return translated label
		 */
		public string getLabel () 
		{
			return label;
		}

		/***
		 * Gets the minimum limit reached. 
		 * 
		 * If this value is greater than 0 it means the alarm is active, 
		 * it undercuts the configured limit.   
		 * 
		 * @return minimum limit
		 */
		public int getMinLimit ()
		{
			return minLimit;
		}
		
		public int CompareTo (object obj)
		{
			int retVal = -1;
			string compString;
			Health compHealth;

			if(obj != null && obj is Health) {
				compHealth = (Health)obj;
				if(compHealth != null) {
					compString = compHealth.getHeadline();
					retVal = compString.ToLower ().CompareTo (this.getHeadline().ToLower ());
				}
			}
			return retVal;
		}
	}
}
