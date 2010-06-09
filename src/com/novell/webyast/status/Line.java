package com.novell.webyast.status;

//FIXME: JUnit this
public class Line {
	private String metricId;
	private String label;
	private int maxLimit;
	private int minLimit;
	private boolean reached;

	public String getMetricId () 
	{
		return metricId;
	}

	public String getLabel () 
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

	public boolean isReached () 
	{
		return reached;
	}
}
