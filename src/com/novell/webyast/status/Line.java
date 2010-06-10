package com.novell.webyast.status;

//FIXME: JUnit this
public class Line {
	private String metricId;
	private String metricColumn;
	private String label;
	private int maxLimit;
	private int minLimit;
	private boolean reached;
	
	public Line (String metricId, String metricColumn, String label, 
			int maxLimit, int minLimit, boolean reached)
	{
		this.metricId = metricId;
		this.metricColumn = metricColumn;
		this.label = label;
		this.maxLimit = maxLimit;
		this.minLimit = minLimit;
		this.reached = reached;
	}

	public String getMetricId () 
	{
		return metricId;
	}
	
	public String getMetricColumn ()
	{
		return metricColumn;
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
