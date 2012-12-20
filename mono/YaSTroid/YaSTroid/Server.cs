using Android.OS;

namespace YaSTroid
{
	public class Server : WebYaST.Server
	{
		int id;
		string name;
		int groupid;

		public Server(int id, string name, string scheme, string hostname, int port, string user, string pass, int groupid) : base (scheme, hostname, port, user, pass)
		{
			this.id = id;
			this.name = name;
			this.groupid = groupid;
		}
		
		public Server(Bundle b) {
			this(b.GetInt("SERVER_ID"),
				b.GetString("SERVER_NAME"),
				b.GetString("SERVER_SCHEME"),
				b.GetString("SERVER_HOSTNAME"),
				b.GetInt("SERVER_PORT"),
				b.GetString("SERVER_USER"),
				b.GetString("SERVER_PASS"),
				b.GetInt("SERVER_GID"));
		}
		
		public string getName() {
			return name;
		}
		
		public string getFullUrl()
		{
			return Scheme + "://" + Hostname + ":" + Port;
		}

		public int getId() {
			return id;
		}
		
		public int getGroupId() {
			return groupid;
		}
	}
}
