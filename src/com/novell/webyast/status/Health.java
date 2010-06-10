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
	
	public static final int Error = 0;
	public static final int Healthy = 1;
	public static final int Unhealthy = 2;
	
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
}
