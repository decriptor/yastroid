using System;

using WebYaST.Status;
using WebYaST.Update;
using System.Text;

namespace WebYaST
{
	public class Server
	{
		public Server(string scheme, string hostname, int port, string user, string pass) {
			Hostname = hostname;
			User = user;
			Password = pass;
			Port = port;
			Scheme = scheme;
		}
		
		public string Hostname { get; private set; }
		public int Port { get; private set; }
		public string User { get; private set; }
		public string Password { get; private set; }
		public string Scheme { get; private set; }

		public UpdateModule GetUpdateModule() {
			return new UpdateModule (this);
		}
		
		public StatusModule GetStatusModule ()
		{
			return new StatusModule (this);
		}
	}
}

