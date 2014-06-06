using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Java.Text;
using Java.Util;
using YaSTroid.WebYaST.Status;
using System.Threading.Tasks;

namespace YaSTroid
{
	[Activity (Label = "DisplayResourceActivity")]
	public class DisplayResourceActivity : Activity
	{
	    /**
	     * Fields to contain the current position and display contents of the spinner
	     */
	    protected int mPos;
	    protected string mSelection;
	    protected int length = 5; // default length (in minutes)
		float[] values = new float[] { 0f, 3f, 2f, 3f, 3f, 1f, 5f, 2f, 4f, 4f, 3f, 2f};
		int timeStamp = 0;
		int timeStampStart = 0;
	    Metric metric;
		Task graphView;
		private ProgressDialog fetchMetricDataProgress = null;
		protected bool data_pull_in_progress = false;

	    /**
	     * ArrayAdapter connects the spinner widget to array-based data.
	     */
		protected ArrayAdapter<string> mAdapter;

	    /**
	     *  The initial position of the spinner when it is first installed.
	     */
	    public static int DEFAULT_POSITION = 2;

	    public void fetchMetricData() {
			Bundle b;
			Server yastServer;
			string resource_type;
			StatusModule statusModule;
			List<Metric> networkMetrics;
			
			b = Intent.Extras;
			resource_type = b.GetString("RESOURCE_TYPE");
			yastServer = new Server(b);
			metric = null;
	                // TODO: figure out how many data points we want to graph on this small screen

			if (resource_type == "Network")
			{
				try {
					statusModule = yastServer.getStatusModule();
					networkMetrics = statusModule.getMetric(Metric.NETWORK);
	                                // FIXME: We are using "eth0" in the meantime, but we should be able to show the other interfaces
	                                // Also, each interface has different types, here we are hard-coding "if_packets" (aka, rx and tx)
					string id = null;
					foreach (Metric m in networkMetrics) {
						if (m.getTypeInstance () != null && m.getTypeInstance().CompareTo("eth0") == 0
							&& m.getType () != null && m.getType().CompareTo("if_packets") == 0) {
							id = m.getId ();
							break;
						}
					}
					timeStamp = (int) (new Date ().Time / 1000);
	                                timeStampStart = timeStamp - (60 * length);
	                                metric = statusModule.getMetricData(id, 
	                                                timeStampStart,
	                                                timeStamp);
					if (metric != null) {
					// FIXME: GraphArrayList<E>orts one value only,
			        		List<Value> xmlValues = (List<Value>) metric.getValues ();
	                                        // FIXME: We are using the first value, however the graph should show all the values available, for
	                                        // example "if_packets" has "tx" and "rx" values.
						float [] fvalues = xmlValues[0].getValues ().ToArray();
						values = new float [fvalues.Length];
						for (int x=0; x<fvalues.Length; x++) {
							values[x] = fvalues[x];
				            }
					}
				} catch (Exception e) {
					Android.Util.Log.Error("DisplayResourceActivity", e.Message);
				}
			}
			Task returnRes = new Task (() => {
				buildGraph ();
				fetchMetricDataProgress.Dismiss ();
			});
			RunOnUiThread (returnRes.Start);
	    }

		private void buildGraph()
		{
			CustomGraphView gv = (CustomGraphView) FindViewById(Resource.Id.graph_view);
	        gv.setCustomGraphViewParms(values, "MByte/s", 
	        		getHorizontalLabels (timeStamp, length), getVerticalLabels(values), CustomGraphView.LINE);
	        data_pull_in_progress = false;
			mAdapter.NotifyDataSetChanged();
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
	        
			Bundle b = Intent.Extras;
			string resource_type = b.GetString("RESOURCE_TYPE");

			SetContentView(Resource.Layout.display_resource);

			TextView v = FindViewById<TextView>(Resource.Id.resource_type);
			v.Text = b.GetString("RESOURCE_TYPE");
	        
			Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

			this.mAdapter = new ArrayAdapter<string>(this, Resource.Array.times_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);

			spinner.Adapter = this.mAdapter;
			spinner.ItemSelected += (sender, e) => {
				Spinner s = sender as Spinner;

				if(data_pull_in_progress)
					return;
				data_pull_in_progress = true;
				mPos = s.SelectedItemPosition;
				mSelection = s.GetItemAtPosition (mPos).ToString ();
				// TODO: Figure out how to convert our random strings into real corresponding lengths.
				if (mPos == 0)
					length = 5;
				else if (mPos == 1)
					length = 15;
				else if (mPos == 2)
					length = 60;
				else
					length = 720;

				graphView = new Task (fetchMetricData);
				graphView.Start ();
				fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...", "Rebuilding graph...", true);
				/*
	             * Set the value of the text field in the UI
	             */
				//TextView resultText = (TextView)FindViewById(Resource.Id.SpinnerResult);
				//resultText.setText("Display a graph here of the last " + DisplayResourceActivity.this.mSelection);

			};
//			OnItemSelectedListener spinnerListener = new myOnItemSelectedListener(this,this.mAdapter);
//			spinner.SetOnItemSelectedListener(spinnerListener);
	        
	        data_pull_in_progress = true;

			graphView = new Task (fetchMetricData);
			graphView.Start ();
			fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...", "Building graph...", true);
	    }
	    
	    private string[] getHorizontalLabels (int timeStamp, int length)
	    {
	    	long tStop = timeStamp * 1000L;
	    	long tStart = (timeStamp - (60L * length)) * 1000;
	    	int quarter = (int) ((tStop - tStart) / 4);
	    	
	    	return new string[] {
	    			new SimpleDateFormat ("HH:mm").Format (new Date (tStart)),
	    			new SimpleDateFormat ("HH:mm").Format (new Date (tStart + quarter)),
	    			new SimpleDateFormat ("HH:mm").Format (new Date (tStart + (quarter * 2))),
	    			new SimpleDateFormat ("HH:mm").Format (new Date (tStop))
	    	};
	    }

	    private string[] getVerticalLabels (float[] values)
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
	        		String.Format("%.1f", maximum),
	        		String.Format("%.1f", minimum + (quarter * 2)),
	        		String.Format("%.1f", minimum + quarter),
	        		String.Format("%.1f", minimum)
	        	};
	    }

	    public int getSpinnerPosition() {
	        return this.mPos;
	    }

	    public void setSpinnerPosition(int pos) {
	        this.mPos = pos;
	    }

	    public string getSpinnerSelection() {
	        return this.mSelection;
	    }

	    public void setSpinnerSelection(string selection) {
	        this.mSelection = selection;
	    }
	}
}
