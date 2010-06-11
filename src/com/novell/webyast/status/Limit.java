package com.novell.webyast.status;

public class Limit {
	private String metricColumn;
	private int max;
	private int min;
	
	public Limit (String metricColumn, int max, int min)
	{
		this.metricColumn = metricColumn;
		this.max = max;
		this.min = min;
	}

	public int getMax() {
		return max;
	}

	public String getMetricColumn() {
		return metricColumn;
	}

	public int getMin() {
		return min;
	}
	
	
}
