package com.novell.webyast;

import org.json.*;
import org.apache.http.client.*;

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
}
