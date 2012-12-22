
using Android.App;

using WebYaST.Status;
using Android.OS;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using System;

namespace YaSTroid
{
	[Activity (Label = "DisplayResourceActivity")]
	public class DisplayResourceActivity : Activity
	{
	    /**
	     * Fields to contain the current position and display contents of the spinner
	     */
	    int _position;
	    string _selection;
	    int _length = 5; // default length (in minutes)
		float[] _values = new float[] { 0f, 3f, 2f, 3f, 3f, 1f, 5f, 2f, 4f, 4f, 3f, 2f};
		int _timeStamp = 0;
		int _timeStampStart = 0;
	    Metric _metric;
		ProgressDialog _fetchMetricDataProgress = null;
		bool _data_pull_in_progress = false;
		Spinner _spinner;
		ArrayAdapter<string> _adapter;

	    /**
	     *  The initial position of the spinner when it is first installed.
	     */
	    public static int DEFAULT_POSITION = 2;

	    public void fetchMetricData()
		{
			Bundle b;
			Server yastServer;
			string resource_type;
			StatusModule statusModule;
		    List<Metric> networkMetrics;
			
			b = Intent.Extras;
			resource_type = b.GetString("RESOURCE_TYPE");
			yastServer = new Server(b);
			_metric = null;
			// TODO: figure out how many data points we want to graph on this small screen

			if (resource_type == "Network")
			{
				try
				{
					statusModule = yastServer.GetStatusModule();
					networkMetrics = statusModule.getMetric(Metric.NETWORK);
                    // FIXME: We are using "eth0" in the meantime, but we should be able to show the other interfaces
                    // Also, each interface has different types, here we are hard-coding "if_packets" (aka, rx and tx)
					string id = null;
					foreach (Metric m in networkMetrics)
					{
						if (m.getTypeInstance () != null && m.getTypeInstance() == "eth0"
								&& m.getType () != null && m.getType() == "if_packets")
						{
							id = m.getId ();
							break;
						}
					}

					var ts = DateTime.UtcNow - new DateTime(1970,1,1);
                    _timeStamp = (int)(ts.TotalMilliseconds / 1000);
                    _timeStampStart = _timeStamp - (60 * _length);
                    _metric = statusModule.getMetricData(id, _timeStampStart, _timeStamp);

					if (_metric != null)
					{
						// FIXME: GraphArrayList<E>orts one value only,
		        		List<Value> xmlValues = _metric.getValues ();
                        // FIXME: We are using the first value, however the graph should show all the values available, for
                        // example "if_packets" has "tx" and "rx" values.
			            float [] fvalues = xmlValues[0].getValues ().ToArray();
						_values = new float [fvalues.Length];

			            for (int x=0; x<fvalues.Length; x++)
						{
			            	_values[x] = fvalues[x];
			            }
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}

			RunOnUiThread(() =>
			{
				buildGraph();
				_fetchMetricDataProgress.Dismiss();
			});
	    }

		void buildGraph()
		{
			var gv = FindViewById<CustomGraphView>(Resource.Id.graph_view);
	        gv.setCustomGraphViewParms(_values, "MByte/s", 
	        		getHorizontalLabels (_timeStamp, _length), getVerticalLabels(_values), CustomGraphView.LINE);
	        _data_pull_in_progress = false;
			_adapter.NotifyDataSetChanged();
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.display_resource);
	        
	        Bundle b = Intent.Extras;
	        string resource_type = b.GetString("RESOURCE_TYPE");

	        TextView resourceType = FindViewById<TextView>(Resource.Id.resource_type);
	        resourceType.Text = b.GetString("RESOURCE_TYPE");
	        
	        _adapter = new ArrayAdapter<string>(this, Resource.Array.times_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);
			_spinner = FindViewById<Spinner>(Resource.Id.spinner);
	        _spinner.Adapter = _adapter;
			_spinner.ItemSelected += HandleSpinnerItemSelected;
	        
	        _data_pull_in_progress = true;
//	        graphView = new Runnable() {
//				@Override
//				public void run() {
//					fetchMetricData();
//				}
//			};
//			Thread thread = new Thread(graphView, "GraphBackground");
//			thread.start();
			fetchMetricData();
			_fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...",
					"Building graph...", true);
	    }

		void HandleSpinnerItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			if(_data_pull_in_progress)
				return;

			_data_pull_in_progress = true;
			_position = e.Position;
			// TODO: Figure out how to convert our random strings into real corresponding lengths.
			if (_position == 0)
				_length = 5;
			else if (_position == 1)
				_length = 15;
			else if (_position == 2)
				_length = 60;
			else
				_length = 720;
			
//			Thread thread = new Thread(graphView, "GraphBackground");
//			thread.start();
			fetchMetricData ();
			_fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...",
			                                              "Rebuilding graph...", true);
			/*
	         * Set the value of the text field in the UI
	         */
			//TextView resultText = (TextView)FindViewById(Resource.Id.SpinnerResult);
			//resultText.setText("Display a graph here of the last " + DisplayResourceActivity.this.mSelection);

		}
	    
	    string[] getHorizontalLabels (int timeStamp, int length)
	    {
	    	long tStop = timeStamp * 1000L;
	    	long tStart = (timeStamp - (60L * length)) * 1000;
	    	int quarter = (int) ((tStop - tStart) / 4);
	    	
			//FIXME
//	    	return new string[] {
//	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart)),
//	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + quarter)),
//	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + (quarter * 2))),
//	    			new SimpleDateFormat ("HH:mm").format (new Date (tStop))
//	    	};
			return new string[] { "1", "2", "3", "4"};
	    }

	    string[] getVerticalLabels (float[] values)
	    {    	
	    	float maximum = 0;
	    	float minimum = 0; 
	    	foreach (float f in values) {
	    		if (f > maximum)
	    			maximum = f;
	    		else if (f < minimum)
	    			minimum = f;
	    	}

	    	float quarter = (maximum - minimum) / 4;
	    	
	        return new string[] {
	        		string.Format("%.1f", maximum),
	        		string.Format("%.1f", minimum + (quarter * 2)),
	        		string.Format("%.1f", minimum + quarter),
	        		string.Format("%.1f", minimum)
	        	};
	    }
	}
}
