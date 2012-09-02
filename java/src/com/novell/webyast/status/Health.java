package com.novell.webyast.status;

// FIXME: JUnit this

/***
 * Represents the health of a particular element in the server.
 * Known elements are: Network, Memory, CPU and Disk. 
 * 
 * @author Mario Carrion
 *
 */
public class Health {
	
	public static final int ERROR = 0;
	public static final int HEALTHY = 1;
	public static final int UNHEALTHY = 2;
	
	// FIXME: These values are temporal, ids are translated
	public static final String NETWORK_ID = "Network";
	public static final String CPU_ID = "CPU";
	public static final String DISK_ID = "Disk";
	public static final String MEMORY_ID = "Memory";
	
	private int maxLimit;
	private int minLimit;
	private String headline;
	private String label;
	
	public Health (int maxLimit, int minLimit, String headline, String label)
	{
		this.maxLimit = maxLimit;
		this.minLimit = minLimit;
		this.headline = headline;
		this.label = label;
	}
	
	public void setHeadline(String headline) {
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
	public String getHeadline() {
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
	public String getLabel () 
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
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object o) {
		boolean retVal = false;
		String compString;
		Health compHealth;
		
		if(o != null && o instanceof Health) {
			compHealth = (Health)o;
			if(compHealth != null) {
				compString = compHealth.getHeadline();
				retVal = compString.equalsIgnoreCase(this.getHeadline());
			}
		}
		return retVal;
	}
}
