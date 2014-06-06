using YaSTroid.WebYaST.Status;
using YaSTroid.WebYaST.Update;

namespace YaSTroid.WebYaST
{
	public class Server
	{	
		private string hostname;
		private string user;
		private string pass;
		private int port;
		private string scheme;
		
		public Server(string scheme, string hostname, int port, string user, string pass)
		{
			this.hostname = hostname;
			this.user = user;
			this.pass = pass;
			this.port = port;
			this.scheme = scheme;
		}
		
		public string getHostname()
		{
			return hostname;
		}
		
		public int getPort()
		{
			return port;
		}
		
		public string getScheme()
		{
			return scheme;
		}
		
		public string getUser()
		{
			return user;
		}
		
		public string getPass()
		{
			return pass;
		}

		// FIXME: JUnit this
		public UpdateModule getUpdateModule()
		{
			return new UpdateModule (this);
		}

		// FIXME: JUnit this
		public StatusModule getStatusModule ()
		{
			return new StatusModule (this);
		}
	}
}
