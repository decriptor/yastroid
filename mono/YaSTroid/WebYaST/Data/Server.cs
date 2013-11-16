using System;

using SQLite;

namespace MobileShared.Data
{
	public class Server
	{
		//const string CREATE_SERVER_TABLE = "CREATE TABLE "
		//	+ SERVERS_TABLE_NAME + " ("
		//		+ SERVERS_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, "
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Scheme { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string User { get; set; }
		public string Password { get; set; }

		[Indexed]
		public int GroupId { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Server: Id={0} -> {1}{2}{3}:{4}, User={5}, Password={6}, Group={7}]", Id, Name, Scheme, Host, Port, User, Password, GroupId);
		}

	}
}