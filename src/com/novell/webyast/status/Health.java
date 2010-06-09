package com.novell.webyast.status;

//FIXME: JUnit this
public class Health {
	private int maxLimit;
	private int minLimit;
	private String headline; // For example "Memory"
	private String label; // For example "free" "cached" "used"
	
	public Health (int maxLimit, int minLimit, String headline, String label)
	{
		this.maxLimit = maxLimit;
		this.minLimit = minLimit;
		this.headline = headline;
		this.label = label;
	}

	public int getMaxLimit() {
		return maxLimit;
	}

	public String getHeadline() {
		return headline;
	}

	public String getLabel() {
		return label;
	}

	public int getMinLimit() {
		return minLimit;
	}
}
