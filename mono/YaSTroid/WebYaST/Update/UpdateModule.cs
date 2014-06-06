//import com.novell.webyast.*;
//import org.json.*;
using Org.Json;

namespace YaSTroid.WebYaST.Update
{
	public class UpdateModule
	{
		private Server server;

		public UpdateModule(Server server)
		{
			this.server = server;
		}

		public int getNumberOfAvailableUpdates()// throws Exception
		{
			string jsonStr = new RestClient().getMethod(server, "/patches.json");
			JSONArray updates = new JSONArray(jsonStr);

			return updates.Length();
		}
	}
}