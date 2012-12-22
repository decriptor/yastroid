using Android.App;
using Android.OS;
using Android.Views;
using System;
using WebYaST.Status;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace YaSTroid
{
	[Activity (Label = "ServerActivity")]
	public class ServerActivity : ListActivity
	{
		ProgressDialog moduleListProgress;
		Server yastServer;
		List<Module> moduleList = null;
		ModuleAdapter moduleAdapter;
		//Runnable moduleView;

		/*
		 * (non-Javadoc)
		 * 
		 * @see android.app.Activity#onCreate(android.os.Bundle)
		 */
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.server);
			yastServer = new Server(Intent.Extras);
			
			moduleList = new List<Module>();
			this.moduleAdapter = new ModuleAdapter(this, Resource.Layout.module_list_row, moduleList);
			
			// Set the Header on this Activity
			View header = LayoutInflater.Inflate(Resource.Layout.server_header, null);
			TextView serverName = (TextView)header.FindViewById(Resource.Id.server_name);
			TextView serverHost = (TextView)header.FindViewById(Resource.Id.server_address);
			//TextView serverUptime = (TextView)header.FindViewById(Resource.Id.server_uptime); // Not available yet
			serverName.Text = yastServer.getName();
			serverHost.Text = yastServer.Hostname;
			//serverUptime.setText("Uptime: " + "8 days, 8 Hours"); //Not available yet
			
			ListView lv = ListView;
			lv.AddHeaderView(header);
			ListAdapter = moduleAdapter;

			
//			moduleView = new Runnable() {
//				@Override
//				public void run() {
//					getModules();
//				}
//			};
			
//			Thread thread = new Thread(null, moduleView, "ModuleListBackground");
//			thread.start();
			getModules();
			moduleListProgress = ProgressDialog.Show(this, "Please wait...", "Building Module list...", true);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			//getModules();
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);
			Intent intent = null;
			// Ignore first item showing the server name and IP
			if (position != 0) {
				Module m = moduleList[position - 1];
				String n = m.getName();
				
				if (n == "UPDATE") {
					// Do something with the updates
				} else if (n == "HEALTH") {
					intent = new Intent(this, typeof(SystemStatusActivity));
					intent.PutExtras(Intent.Extras);
					StartActivity(intent);
				} else {
					// Do nothing?
				}
			}
		}
		
		void getModules()
		{
			moduleList = new List<Module>();
			String moduleName = "SETME";
			// Get Updates
			Module update = null;
			moduleName = "UPDATE";
			int availableUpdates = 0;
			
			// TODO: Add icons for each module, which change dynamically
			// (for example, green icon when system is healthy, red
			// otherwise)
			
			try {
				availableUpdates = yastServer.GetUpdateModule().getNumberOfAvailableUpdates();
				update = new Module(moduleName, availableUpdates + " updates available", Resource.Drawable.yast_system);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			if(update != null)
				moduleList.Add(update);
			
			
			// Get Health
			Module systemHealth = null;
			moduleName = "HEALTH";
			
			// TODO: Figure out available modules dynamically
			// TODO: Populate health text dynamically

			try {
				// TODO: Clicking this message should show you full details of the status
				switch(yastServer.GetStatusModule().getHealthSummary())
				{
					case Health.ERROR:
					systemHealth = new Module(moduleName, "Cannot read system status", Resource.Drawable.status_red);
						break;
					case Health.UNHEALTHY:
					systemHealth = new Module(moduleName, "System is not healthy", Resource.Drawable.status_red);
						break;
					case Health.HEALTHY:
					systemHealth = new Module(moduleName, "Everything's shiny, Cap'n. Not to fret.", Resource.Drawable.status_green);
						break;
				}
			} 
			catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			if (systemHealth != null)
				moduleList.Add(systemHealth);
			
			RunOnUiThread(() => {
				moduleAdapter.Clear();
				if (moduleList != null && moduleList.Count > 0) {
					moduleAdapter.NotifyDataSetChanged();
					for (int i = 0; i < moduleList.Count; i++) {
						moduleAdapter.Add(moduleList[i]);
					}
				}
				moduleListProgress.Dismiss();
				moduleAdapter.NotifyDataSetChanged();
			});
		}
	}
}
