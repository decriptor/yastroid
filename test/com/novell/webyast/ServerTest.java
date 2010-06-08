package com.novell.webyast;

import junit.framework.TestCase;

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

	public void testGetUrl() {
		Server server = new Server ("test");
		assertEquals("test", server.getUrl());
	}

}
