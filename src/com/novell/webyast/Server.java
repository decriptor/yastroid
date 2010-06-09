package com.novell.webyast;

import org.json.*;
import org.apache.http.client.*;

import com.novell.webyast.status.StatusModule;

public class Server {
	
	private String url;
	
	public Server (String url)
	{
		this.url = url;
	}
	
	public String getUrl ()
	{
		return url;
	}
	// 
	FIXME: JUnit this
	public StatusModule getStatusModule ()
	{
		return new StatusModule (this);
	}
}
