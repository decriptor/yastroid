package com.novell.webyast;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

public class RestClient {
    
	// FIXME: JUnit this
	public String getMethod (String url) throws Exception {
        HttpClient client = new DefaultHttpClient();
        HttpGet get = new HttpGet(url);
        HttpResponse response;
        response = client.execute(get);
        HttpEntity entity = response.getEntity();
        if (entity == null)
                return null;
       
        InputStream in = entity.getContent ();
        BufferedReader reader = new BufferedReader (new InputStreamReader (in));
        StringBuilder sb = new StringBuilder ();
        String line = null;
        while ((line = reader.readLine ()) != null)
                sb.append (line + "\n");
        
        return sb.toString ();
	}
}
