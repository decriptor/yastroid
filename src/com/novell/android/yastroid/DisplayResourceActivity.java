package com.novell.android.yastroid;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;

import com.novell.webyast.status.Metric;
import com.novell.webyast.status.Value;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.AdapterView.OnItemSelectedListener;

public class DisplayResourceActivity extends Activity {

    /**
     * Fields to contain the current position and display contents of the spinner
     */
    protected int mPos;
    protected String mSelection;
    protected int length = 5; // default length (in minutes)

    /**
     * ArrayAdapter connects the spinner widget to array-based data.
     */
    protected ArrayAdapter<CharSequence> mAdapter;

    /**
     *  The initial position of the spinner when it is first installed.
     */
    public static final int DEFAULT_POSITION = 2;

    @Override
    public void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);

		float[] values = new float[] { 0f, 3f, 2f, 3f, 3f, 1f, 5f, 2f, 4f, 4f, 3f, 2f};
		int timeStamp = 0;
		int timeStampStart = 0;
        
        Bundle b = getIntent().getExtras();
        String resource_type = b.getString("RESOURCE_TYPE");

		if (resource_type.equals("Network"))  {
			// TODO: figure out how many data points we want to graph on this small screen
			// TODO: Use a ProgressDialog
			Server yastServer =	new Server (b);
		
			Metric metric = null;
			try {		
				Collection<Metric> networkMetrics = yastServer.getStatusModule ().getMetric(Metric.NETWORK);
				// FIXME: We are using "eth0" in the meantime, but we should be able to show the other interfaces
				// Also, each interface has different types, here we are hard-coding "if_packets" (aka, rx and tx)
				String id = null;
				for (Metric m : networkMetrics) {
					if (m.getTypeInstance () != null && m.getTypeInstance().compareTo("eth0") == 0
							&& m.getType () != null && m.getType().compareTo("if_packets") == 0) {
						id = m.getId ();
						break;
					}
				}
				timeStamp = (int) (new Date ().getTime () / 1000);
				timeStampStart = timeStamp - (60 * length);
				metric = yastServer.getStatusModule ().getMetricData(id, 
						timeStampStart,
						timeStamp);
				
				if (metric != null) {
					// FIXME: GraphArrayList<E>orts one value only,
					ArrayList<Value> xmlValues = (ArrayList<Value>) metric.getValues ();
					// FIXME: We are using the first value, however the graph should show all the values available, for
					// example "if_packets" has "tx" and "rx" values.
					Float [] fvalues = xmlValues.get(0).getValues ().toArray(new Float[0]);
					values = new float [fvalues.length];
					for (int x=0; x<fvalues.length; x++) {
						values[x] = fvalues[x].floatValue();
					}
				}
				
			} catch (Exception e) {
				System.out.println(e.getMessage());
			}
		}
		
        setContentView(R.layout.display_resource);

        TextView v = (TextView) findViewById(R.id.resource_type);
        v.setText(b.getString("RESOURCE_TYPE"));
        
        Spinner spinner = (Spinner) findViewById(R.id.spinner);

        this.mAdapter = ArrayAdapter.createFromResource(this, R.array.times_array,
                android.R.layout.simple_spinner_dropdown_item);

        spinner.setAdapter(this.mAdapter);

        OnItemSelectedListener spinnerListener = new myOnItemSelectedListener(this,this.mAdapter);
        spinner.setOnItemSelectedListener(spinnerListener);
        
        CustomGraphView gv = (CustomGraphView) findViewById(R.id.graph_view);
        gv.setCustomGraphViewParms(values, "MByte/s", 
        		getHorizontalLabels (timeStamp, length), getVerticalLabels(values), CustomGraphView.LINE);        
    }
    
    private String[] getHorizontalLabels (int timeStamp, int length)
    {
    	long tStop = timeStamp * 1000L;
    	long tStart = (timeStamp - (60L * length)) * 1000;
    	int quarter = (int) ((tStop - tStart) / 4);
    	
    	return new String[] {
    			new SimpleDateFormat ("HH:mm").format (new Date (tStart)),
    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + quarter)),
    			new SimpleDateFormat ("HH:mm").format (new Date (tStart + (quarter * 2))),
    			new SimpleDateFormat ("HH:mm").format (new Date (tStop))
    	};
    }

    private String[] getVerticalLabels (float[] values)
    {    	
    	float maximum = 0;
    	float minimum = 0; 
    	for (float f : values) {
    		if (f > maximum)
    			maximum = f;
    		else if (f < minimum)
    			minimum = f;
    	}

    	float quarter = (maximum - minimum) / 4;
    	
        return new String[] {
        		String.format("%.1f", maximum),
        		String.format("%.1f", minimum + (quarter * 2)),
        		String.format("%.1f", minimum + quarter),
        		String.format("%.1f", minimum)
        	};
    }

    /**
     *  A callback listener that implements the
     *  {@link android.widget.AdapterView.OnItemSelectedListener} interface
     *  For views based on adapters, this interface defines the methods available
     *  when the user selects an item from the View.
     *
     */
    public class myOnItemSelectedListener implements OnItemSelectedListener {

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
        public void onItemSelected(AdapterView<?> parent, View v, int pos, long row) {

            DisplayResourceActivity.this.mPos = pos;
            DisplayResourceActivity.this.mSelection = parent.getItemAtPosition(pos).toString();
            // TODO: Update this.length and redraw graph
            /*
             * Set the value of the text field in the UI
             */
            TextView resultText = (TextView)findViewById(R.id.SpinnerResult);
            resultText.setText("Display a graph here of the last " + DisplayResourceActivity.this.mSelection);
        }

        /**
         * The definition of OnItemSelectedListener requires an override
         * of onNothingSelected(), even though this implementation does not use it.
         * @param parent - The View for this Listener
         */
        public void onNothingSelected(AdapterView<?> parent) {

            // do nothing

        }
    }

    public int getSpinnerPosition() {
        return this.mPos;
    }

    public void setSpinnerPosition(int pos) {
        this.mPos = pos;
    }

    public String getSpinnerSelection() {
        return this.mSelection;
    }

    public void setSpinnerSelection(String selection) {
        this.mSelection = selection;
    }
}
