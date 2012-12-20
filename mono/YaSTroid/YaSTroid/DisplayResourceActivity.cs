
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
	    protected int mPos;
	    protected string mSelection;
	    protected int length = 5; // default length (in minutes)
		float[] values = new float[] { 0f, 3f, 2f, 3f, 3f, 1f, 5f, 2f, 4f, 4f, 3f, 2f};
		int timeStamp = 0;
		int timeStampStart = 0;
	    Metric metric;
	    Runnable graphView;
		private ProgressDialog fetchMetricDataProgress = null;
		protected bool data_pull_in_progress = false;

	    /**
	     * ArrayAdapter connects the spinner widget to array-based data.
	     */
	    protected ArrayAdapter<CharSequence> mAdapter;

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

			if (resource_type == "Network")  {
				try {
					statusModule = yastServer.GetStatusModule();
					networkMetrics = statusModule.getMetric(Metric.NETWORK);
	                                // FIXME: We are using "eth0" in the meantime, but we should be able to show the other interfaces
	                                // Also, each interface has different types, here we are hard-coding "if_packets" (aka, rx and tx)
					string id = null;
					foreach (Metric m in networkMetrics) {
						if (m.getTypeInstance () != null && m.getTypeInstance() == "eth0"
								&& m.getType () != null && m.getType() == "if_packets") {
							id = m.getId ();
							break;
						}
					}
	                                timeStamp = (int) (new Date ().getTime () / 1000);
	                                timeStampStart = timeStamp - (60 * length);
	                                metric = statusModule.getMetricData(id, 
	                                                timeStampStart,
	                                                timeStamp);
					if (metric != null) {
					// FIXME: GraphArrayList<E>orts one value only,
			        		ArrayList<Value> xmlValues = (ArrayList<Value>) metric.getValues ();
	                                        // FIXME: We are using the first value, however the graph should show all the values available, for
	                                        // example "if_packets" has "tx" and "rx" values.
				            float [] fvalues = xmlValues.get(0).getValues ().toArray(new Float[0]);
	                                    values = new float [fvalues.Length];
				            for (int x=0; x<fvalues.Length; x++) {
				            	values[x] = fvalues[x].floatValue();
				            }
					}
				} catch (Exception e) {
					Console.WriteLine(e);
				}
			}
			RunOnUiThread(returnRes);
	    }

//		private Runnable returnRes = new Runnable() {
//			@Override
//			public void run() {
//				buildGraph();
//				fetchMetricDataProgress.dismiss();
//			}
//		};

		void buildGraph()
		{
			CustomGraphView gv = FindViewById<CustomGraphView>(Resource.Id.graph_view);
	        gv.setCustomGraphViewParms(values, "MByte/s", 
	        		getHorizontalLabels (timeStamp, length), getVerticalLabels(values), CustomGraphView.LINE);
	        data_pull_in_progress = false;
			mAdapter.NotifyDataSetChanged();
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
	        
	        Bundle b = Intent.Extras;
	        string resource_type = b.GetString("RESOURCE_TYPE");

	        SetContentView(Resource.Layout.display_resource);

	        TextView v = FindViewById<TextView>(Resource.Id.resource_type);
	        v.Text = b.GetString("RESOURCE_TYPE");
	        
	        Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

	        mAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.times_array,
	                Resource.Layout.simple_spinner_dropdown_item);

	        spinner.Adapter = mAdapter;

	        OnItemSelectedListener spinnerListener = new myOnItemSelectedListener(this,this.mAdapter);
	        spinner.setOnItemSelectedListener(spinnerListener);
	        
	        data_pull_in_progress = true;
//	        graphView = new Runnable() {
//				@Override
//				public void run() {
//					fetchMetricData();
//				}
//			};
			Thread thread = new Thread(graphView, "GraphBackground");
			thread.start();
			fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...",
					"Building graph...", true);
	    }
	    
	    private string[] getHorizontalLabels (int timeStamp, int length)
	    {
	    	long tStop = timeStamp * 1000L;
	    	long tStart = (timeStamp - (60L * length)) * 1000;
	    	int quarter = (int) ((tStop - tStart) / 4);
	    	
	    	return new string[] {
	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart)),
	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + quarter)),
	    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + (quarter * 2))),
	    			new SimpleDateFormat ("HH:mm").format (new Date (tStop))
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
	        		string.format("%.1f", maximum),
	        		string.format("%.1f", minimum + (quarter * 2)),
	        		string.format("%.1f", minimum + quarter),
	        		string.format("%.1f", minimum)
	        	};
	    }

	    /**
	     *  A callback listener that : the
	     *  {@link android.widget.AdapterView.OnItemSelectedListener} interface
	     *  For views based on adapters, this interface defines the methods available
	     *  when the user selects an item from the View.
	     *
	     */
	    public class myOnItemSelectedListener : OnItemSelectedListener
		{

	        /*
	         * provide local instances of the mLocalAdapter and the mLocalContext
	         */

	        ArrayAdapter<CharSequence> mLocalAdapter;
	        Activity mLocalContext;

	        /**
	         *  Constructor
	         *  @param c - The activity that displays the Spinner.
	         *  @param ad - The Adapter view that
	         *    controls the Spinner.
	         *  Instantiate a new listener object.
	         */
	        public myOnItemSelectedListener(Activity c, ArrayAdapter<CharSequence> ad) {

	          this.mLocalContext = c;
	          this.mLocalAdapter = ad;

	        }

	        /**
	         * When the user selects an item in the spinner, this method is invoked by the callback
	         * chain. Android calls the item selected listener for the spinner, which invokes the
	         * onItemSelected method.
	         *
	         * @see android.widget.AdapterView.OnItemSelectedListener#onItemSelected(
	         *  android.widget.AdapterView, android.view.View, int, long)
	         * @param parent - the AdapterView for this listener
	         * @param v - the View for this listener
	         * @param pos - the 0-based position of the selection in the mLocalAdapter
	         * @param row - the 0-based row number of the selection in the View
	         */
	        public void onItemSelected(AdapterView<string> parent, View v, int pos, long row) {

	        	if(data_pull_in_progress)
	        		return;
	        	data_pull_in_progress = true;
	            this.mPos = pos;
	            this.mSelection = parent.GetItemAtPosition(pos);
	            // TODO: Figure out how to convert our random strings into real corresponding lengths.
	            if (pos == 0)
	            	this.length = 5;
	            else if (pos == 1)
	            	this.length = 15;
	            else if (pos == 2)
	            	this.length = 60;
	            else
	            	this.length = 720;
	            
	    		Thread thread = new Thread(graphView, "GraphBackground");
	    		thread.start();
	    		fetchMetricDataProgress = ProgressDialog.Show(this, "Please wait...",
	    				"Rebuilding graph...", true);
	            /*
	             * Set the value of the text field in the UI
	             */
	            //TextView resultText = (TextView)FindViewById(Resource.Id.SpinnerResult);
	            //resultText.setText("Display a graph here of the last " + DisplayResourceActivity.this.mSelection);
	        }

	        /**
	         * The definition of OnItemSelectedListener requires an override
	         * of onNothingSelected(), even though this implementation does not use it.
	         * @param parent - The View for this Listener
	         */

	      //  public void onNothingSelected(AdapterView<> parent) {

	            // do nothing

//	        }
	    }

	    public int getSpinnerPosition()
		{
	        return this.mPos;
	    }

	    public void setSpinnerPosition(int pos)
		{
	        this.mPos = pos;
	    }

	    public string getSpinnerSelection()
		{
	        return this.mSelection;
	    }

	    public void setSpinnerSelection(string selection)
		{
	        this.mSelection = selection;
	    }
	}
}
