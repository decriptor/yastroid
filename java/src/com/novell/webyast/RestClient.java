package com.novell.webyast;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.Authenticator;
import java.net.HttpURLConnection;
import java.net.PasswordAuthentication;
import java.net.URL;
import java.net.URLEncoder;

public class RestClient {

	// FIXME: JUnit this
	public String getMethod(final String scheme, final String hostname, final int port,
			final String resourcePath, final String user, final String pass)
			throws Exception {
		Authenticator.setDefault(new Authenticator() {
			protected PasswordAuthentication getPasswordAuthentication() {
				return new PasswordAuthentication(user, pass.toCharArray());
			}
		});
		HttpURLConnection c = (HttpURLConnection) new URL(scheme,
				hostname, port, resourcePath).openConnection();
		//URLEncoder.encode(resourcePath, "UTF8")
		c.setAllowUserInteraction(true);
		c.setRequestProperty("User-Agent","Mozilla/5.0 ( compatible ) ");
		c.setRequestProperty("Accept","*/*");
		c.setRequestProperty("Content-type","text/html");
		c.setRequestMethod("GET");
		c.setUseCaches(false);
		c.setDoInput(true);
		c.setDoOutput(false);
		c.connect();

		BufferedReader in = new BufferedReader(new InputStreamReader(c
				.getInputStream()));
		String inputLine;
		StringBuilder sb = new StringBuilder();
		while ((inputLine = in.readLine()) != null)
			sb.append(inputLine + "\n");
		in.close();
		return sb.toString();
	}

	public String getMethod (Server server, final String resourcePath) throws Exception {
		return getMethod(server.getScheme(), server.getHostname(), server.getPort(), resourcePath, server.getUser(), server.getPass());
	}
}
