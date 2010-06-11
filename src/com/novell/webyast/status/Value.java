package com.novell.webyast.status;

import java.util.Collection;

public class Value {
	private int interval;
	private String column;
	private int start;
	public Collection<Float> values;
	
	public Value (int interval, String column, int start, Collection<Float> values)
	{
		this.interval = interval;
		this.column = column;
		this.start = start;
		this.values = values;
	}

	public int getInterval() {
		return interval;
	}

	public String getColumn() {
		return column;
	}

	public int getStart() {
		return start;
	}
	
	public Collection<Float> getValues ()
	{
		return values;
	}
}
