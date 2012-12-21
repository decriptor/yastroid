
using System;
using System.Text;

namespace WebYaST
{
	class RestClient
	{
		public string GetMethod(string scheme, string hostname, int port, string resourcePath, string user, string pass)
		{
//			Authenticator.setDefault(new Authenticator() {
//				protected PasswordAuthentication getPasswordAuthentication() {
//					return new PasswordAuthentication(user, pass.toCharArray());
//				}
//			});

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
		
		public string GetMethod (Server server, string resourcePath)
		{
			return GetMethod(server.Scheme, server.Hostname, server.Port, resourcePath, server.User, server.Password);
		}

	}
}

