
namespace WebYaST.Status
{
	/***
	 * Represents the health of a particular element in the server.
	 * Known elements are: Network, Memory, CPU and Disk. 
	 * 
	 * @author Mario Carrion
	 *
	 */
	public class Health
	{	
		public const int ERROR = 0;
		public const int HEALTHY = 1;
		public const int UNHEALTHY = 2;
		
		// FIXME: These values are temporal, ids are translated
		public const string NETWORK_ID = "Network";
		public const string CPU_ID = "CPU";
		public const string DISK_ID = "Disk";
		public const string MEMORY_ID = "Memory";
		
		int maxLimit;
		int minLimit;
		string headline;
		string label;
		
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
		
		/* (non-Javadoc)
		 * 
		 * 
		 * 
		 * @see java.lang.Object#equals(java.lang.Object)
		 */
		public override bool Equals(object o)
		{
			bool retVal = false;
			string compstring;
			Health compHealth;
			
			if(o != null && o is Health)
			{
				compHealth = (Health)o;
				if(compHealth != null) {
					compstring = compHealth.getHeadline();
					retVal = compstring.ToLower() == getHeadline().ToLower();
				}
			}
			return retVal;
		}
	}
}
