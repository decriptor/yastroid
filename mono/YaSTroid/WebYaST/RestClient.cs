
using System;
using System.Text;
using System.Net;
using System.IO;

namespace WebYaST
{
	class RestClient
	{
		public string GetMethod(Server server, string resourcePath)
		{
//			Authenticator.setDefault(new Authenticator() {
//				protected PasswordAuthentication getPasswordAuthentication() {
//					return new PasswordAuthentication(user, pass.toCharArray());
//				}
//			});


			UriBuilder uri = new UriBuilder(server.Scheme, server.Hostname, server.Port);
			uri.Path = resourcePath;
			uri.UserName = server.User;
			uri.Password = server.Password;

			var credentials = new CredentialCache();
			credentials.Add(uri.Uri, "Basic", new NetworkCredential(server.User, server.Password));

			HttpWebRequest webRequest = HttpWebRequest.Create (uri.Uri) as HttpWebRequest;
			webRequest.ContentType = "text/html";
			webRequest.Method = "GET";
			webRequest.Accept = "*/*";
			webRequest.UserAgent = "Mozilla/5.0 ( compatible ) ";
			webRequest.AllowAutoRedirect = true;
			webRequest.Credentials = credentials;


			webRequest.BeginGetResponse ((ar) =>
			{
				var request = (HttpWebRequest)ar.AsyncState;

				using (var response = (HttpWebResponse)request.EndGetResponse(ar))
				{
					Stream receiveStream = response.GetResponseStream();
					StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

					response.Close();
					readStream.Close();
				}
			}, webRequest);
//			HttpURLConnection c = (HttpURLConnection) new URL(scheme, hostname, port, resourcePath).openConnection();
//			//URLEncoder.encode(resourcePath, "UTF8")
//			c.setAllowUserInteraction(true);
//			c.setRequestProperty("User-Agent","Mozilla/5.0 ( compatible ) ");
//			c.setRequestProperty("Accept","*/*");
//			c.setRequestProperty("Content-type","text/html");
//			c.setRequestMethod("GET");
//			c.setUseCaches(false);
//			c.setDoInput(true);
//			c.setDoOutput(false);
//			c.connect();
//			
//			BufferedReader br = new BufferedReader(new InputStreamReader(c.getInputStream()));
//			string inputLine;
//			stringBuilder sb = new stringBuilder();
//			while ((inputLine = br.readLine()) != null)
//				sb.append(inputLine + "\n");
//			br.close();
//			return sb.tostring();
			return null;
		}		
	}
}

