package com.novell.webyast;

import junit.framework.TestCase;
import com.novell.webyast.update.*;

public class ServerTest extends TestCase {

	protected void setUp() throws Exception {
		super.setUp();
	}

	protected void tearDown() throws Exception {
		super.tearDown();
	}

	public void testServer() {
		fail("Not yet implemented");
	}

	public void testSomeStuff() throws Exception {
		Server server = new Server ("http", "137.65.132.19", 4984, "root", "sandy");
		UpdateModule updateMod = server.getUpdateModule();
		assertEquals(5, updateMod.getNumberOfAvailableUpdates());
	}
}
