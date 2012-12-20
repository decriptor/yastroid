using System;

namespace WebYaST.Update
{
	public class UpdateModule
	{
		Server server;
		
		public UpdateModule(Server server)
		{
			this.server = server;
		}
		
		public int getNumberOfAvailableUpdates()
		{
			string jsonStr = new RestClient().GetMethod(server, "/patches.json");
//			JSONArray updates = new JSONArray(jsonStr);
//			
//			return updates.length();
			return 0;
		}
	}
}

