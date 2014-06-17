
using System;
using RestSharp;

namespace YaSTroid.WebYaST
{
	public class RestClient
	{
		void Login ()
		{
			string resource = "accounts/sign_in";
		}

		// FIXME: JUnit this
		public string getMethod(string scheme, string hostname, int port, string resourcePath, string user, string pass)
		{
//			Authenticator a = new Authenticator ();
//			a.
////			new Authenticator() {
////				protected PasswordAuthentication getPasswordAuthentication() {
////					return new PasswordAuthentication(user, pass.toCharArray());
////				}
////			});
//
//			Authenticator.SetDefault (a);
//
//			HttpURLConnection c = new HttpURLConnection (); //(HttpURLConnection) new URL(scheme,
////					hostname, port, resourcePath).openConnection();
//			//URLEncoder.encode(resourcePath, "UTF8")
//			c.SetAllowUserInteraction(true);
//			c.SetRequestProperty("User-Agent","Mozilla/5.0 ( compatible ) ");
//			c.SetRequestProperty("Accept","*/*");
//			c.SetRequestProperty("Content-type","text/html");
//			c.SetRequestMethod("GET");
//			c.SetUseCaches(false);
//			c.SetDoInput(true);
//			c.SetDoOutput(false);
//			c.Connect();
//
//			BufferedReader inStream = new BufferedReader(new InputStreamReader(c
//					.getInputStream()));
//			string inputLine;
//			StringBuilder sb = new StringBuilder();
//
//			while ((inputLine = inStream.readLine()) != null)
//				sb.append(inputLine + "\n");
//			inStream.Close();
//			return sb.ToString();
			return "";
		}

		public string getMethod (Server server, string resourcePath)
		{
			return GetMethod (server, resourcePath);
//			return getMethod(server.getScheme(), server.getHostname(), server.getPort(), resourcePath, server.getUser(), server.getPass());
		}

		public string GetMethod (Server server, string resourcePath)
		{
			UriBuilder uri = new UriBuilder (server.getScheme (), server.getHostname ());
			uri.Port = server.getPort ();

			var client = new RestSharp.RestClient (uri.ToString ());
			client.Authenticator = new HttpBasicAuthenticator (server.getUser (), server.getPass ());

			var request = new RestRequest (resourcePath, Method.GET);
			var response = client.Execute (request);

			return response.Content;
		}


//		// easily add HTTP Headers
//		request.AddHeader("header", "value");

//		// execute the request
//		RestResponse response = client.Execute(request);
//		var content = response.Content; // raw content as string

//		// or automatically deserialize result
//		// return content type is sniffed but can be explicitly set via RestClient.AddHandler();
//		RestResponse<Person> response2 = client.Execute<Person>(request);
//		var name = response2.Data.Name;
//
//		// easy async support
//		client.ExecuteAsync(request, response => {
//			Console.WriteLine(response.Content);
//		});

//		// async with deserialization
//		var asyncHandle = client.ExecuteAsync<Person>(request, response => {
//			Console.WriteLine(response.Data.Name);
//		});

	}
}