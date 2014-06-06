using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Org.Xml.Sax;
using System.Collections.Generic;
using YaSTroid.WebYaST.Status;
using System.Threading.Tasks;

namespace YaSTroid
{
	[Activity (Label = "SystemStatusActivity")]
	public class SystemStatusActivity : ListActivity
	{
		private StatusListAdapter statusListAdapter;
		private Task systemStatusView;
		private ProgressDialog systemStatusListProgress = null;
		private StatusModule statusModule;
		private	List<Graph> graphs;
		private List<YaSTroid.WebYaST.Status.Log> logs;
	
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.system_status);
			statusListAdapter = new StatusListAdapter(this, Resource.Layout.system_status_list_item, new List<SystemStatus>());
			ListAdapter = statusListAdapter;

			systemStatusView = new Task (buildList);
			systemStatusView.Start ();
			systemStatusListProgress = ProgressDialog.Show(this, "Please wait...",
					"Retrieving data...", true);
		}

		protected override void OnResume()
		{
			base.OnResume();
			//buildList();
		}
	
		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			Intent statusIntent = null;
			SystemStatus systemStatus = null;
			
			base.OnListItemClick(l, v, position, id);
			systemStatus = statusListAdapter.statii [position];
			statusIntent = new Intent(this, typeof(DisplayResourceActivity));
			statusIntent.PutExtras(Intent.Extras);
			switch (systemStatus.getSystemType()) {
			case SystemStatus.NETWORK_STATUS:
				statusIntent.PutExtra("RESOURCE_TYPE", GetString(Resource.String.network_status_text));
		        break;
			case SystemStatus.MEMORY_STATUS:
				statusIntent.PutExtra("RESOURCE_TYPE", GetString(Resource.String.memory_status_text));
		        break;
			case SystemStatus.DISK_STATUS:
				statusIntent.PutExtra("RESOURCE_TYPE", GetString(Resource.String.disk_status_text));
		        break;
			case SystemStatus.CPU_STATUS:
				statusIntent.PutExtra("RESOURCE_TYPE", GetString(Resource.String.cpu_status_text));
		        break;
			default:
				statusIntent = null;
				break;
			//case SystemStatus.SYSTEM_MSGS_STATUS:
		        //statusIntent = new Intent(SystemStatusActivity.this, SystemMessagesActivity.class);
		        //break;
			}
			if (statusIntent != null) {
				StartActivity(statusIntent);
			}
		}
		
		protected void buildList()
		{
			Intent statusIntent;
			Server yastServer;
			Bundle b;
			
			statusIntent = Intent;
			b = statusIntent.Extras;
			yastServer = new Server(b);
			statusModule = yastServer.getStatusModule();
			try {
				graphs = statusModule.getGraphs();
			} catch(SAXException ex) {
				graphs = null;
			}
			
			try {
				logs = statusModule.getLogs();
			} catch(Exception ex) {
				// Unable to get logs
				logs = null;
				Android.Util.Log.Error("SystemStatusActivity", ex.Message);
			}

			Task returnRes = new Task (() => {
				populateAdapter ();
				systemStatusListProgress.Dismiss ();
			});
			RunOnUiThread(returnRes.Start);
		}
		
		private void populateAdapter()
		{
			SystemStatus status;
			int statusID;

			statusListAdapter.Clear();
			if(statusModule.isHealthy(Metric.NETWORK, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(this.Application, SystemStatus.NETWORK_STATUS, statusID);
			statusListAdapter.Add(status);
			if(statusModule.isHealthy(Metric.MEMORY, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(this.Application, SystemStatus.MEMORY_STATUS, statusID);
			statusListAdapter.Add(status);
			if(statusModule.isHealthy(Metric.DISK, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(this.Application, SystemStatus.DISK_STATUS, statusID);
			statusListAdapter.Add(status);
			if(statusModule.isHealthy(Metric.CPU, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(this.Application, SystemStatus.CPU_STATUS, statusID);
			statusListAdapter.Add(status);

			// Display Logs
			if (logs == null) {
				status = new SystemStatus(Application, "Cannot get logs from server");
				statusListAdapter.Add(status);
			} else {
				foreach (YaSTroid.WebYaST.Status.Log l in logs) {
					status = new SystemStatus (Application, l.getDescription());
					statusListAdapter.Add(status);
				}
			}
			statusListAdapter.NotifyDataSetChanged();
		}
	}
}
