package com.novell.webyast.update;

import com.novell.webyast.*;
import org.json.*;

public class UpdateModule {
	private Server server;

	public UpdateModule(Server server) {
		this.server = server;
	}

	public int getNumberOfAvailableUpdates() throws Exception {
		String jsonStr = new RestClient().getMethod(server.getScheme(), server
				.getHostname(), server.getPort(), "/patches.json", server
				.getUser(), server.getPass());
		JSONArray updates = new JSONArray(jsonStr);

		return updates.length();
	}
}
