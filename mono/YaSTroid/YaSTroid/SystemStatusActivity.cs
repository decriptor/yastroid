using Android.App;
using Android.OS;
using System.Collections.Generic;
using Android.Widget;
using WebYaST.Status;
using Android.Content;
using Android.Views;
using System;

namespace YaSTroid
{
	[Activity (Label = "SystemStatusActivity")]
	public class SystemStatusActivity : ListActivity
	{
		StatusListAdapter _adapter;
		//Runnable systemStatusView;
		ProgressDialog systemStatusListProgress = null;
		StatusModule statusModule;
		List<Graph> graphs;
		List<Log> logs;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.system_status);
			_adapter = new StatusListAdapter(this, Resource.Layout.system_status_list_item, new List<SystemStatus>());
			ListAdapter = _adapter;
//			systemStatusView = new Runnable() {
//				@Override
//				public void run() {
//					buildList();
//				}
//			};
			
//			Thread thread = new Thread(null, systemStatusView, "SystemStatusListBackground");
//			thread.start();
			systemStatusListProgress = ProgressDialog.Show(this, "Please wait...", "Retrieving data...", true);
			buildList();
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
			systemStatus = _adapter.GetItem(position) as SystemStatus;//  ListView.GetItemAtPosition(position) as SystemStatus;
	        statusIntent = new Intent(this, typeof(DisplayResourceActivity));
			statusIntent.PutExtras(Intent.Extras);
			switch (systemStatus.getSystemType())
			{
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
			//case SystemStatus.SYSTEM_MSGS_STATUS:
		        //statusIntent = new Intent(SystemStatusActivity.this, SystemMessagesActivity.class);
		        break;
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
			statusModule = yastServer.GetStatusModule();
			try {
				graphs = statusModule.getGraphs();
			} catch(Exception ex) {
				graphs = null;
			}
			
			try {
				logs = statusModule.getLogs();
			} catch(Exception ex) {
				// Unable to get logs
				logs = null;
				Console.WriteLine(ex.Message);
			}
			RunOnUiThread(() => {
				populateAdapter();
				systemStatusListProgress.Dismiss();
			});
		}
		
		private void populateAdapter()
		{
			SystemStatus status;
			int statusID;

			_adapter.Clear();
			if(statusModule.isHealthy(Metric.NETWORK, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(Application, SystemStatus.NETWORK_STATUS, statusID);
			_adapter.Add(status);
			if(statusModule.isHealthy(Metric.MEMORY, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(Application, SystemStatus.MEMORY_STATUS, statusID);
			_adapter.Add(status);
			if(statusModule.isHealthy(Metric.DISK, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(Application, SystemStatus.DISK_STATUS, statusID);
			_adapter.Add(status);
			if(statusModule.isHealthy(Metric.CPU, graphs))
				statusID = SystemStatus.STATUS_GREEN;
			else
				statusID = SystemStatus.STATUS_RED;
			status = new SystemStatus(Application, SystemStatus.CPU_STATUS, statusID);
			_adapter.Add(status);

			// Display Logs
			if (logs == null) {
				status = new SystemStatus(Application, "Cannot get logs from server");
				_adapter.Add(status);
			} else {
				foreach (Log l in logs) {
					status = new SystemStatus (Application, l.getDescription());
					_adapter.Add(status);
				}
			}
			_adapter.NotifyDataSetChanged();
		}
	}
}
