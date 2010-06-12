package com.novell.android.yastroid;

import java.util.ArrayList;
import java.util.Collection;

import com.arnodenhond.graphview.GraphView;
import com.novell.webyast.status.Log;
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
        
        Bundle b = getIntent().getExtras();
        String resource_type = b.getString("RESOURCE_TYPE");
        /*
        float[] values = null;
        String[] verlabels = null;
        String[] horlabels = null;

 
//        /* hardcoding Network just to show the graph capability for now because
//         * the embeded below does not work yet
//         * 
//         *
//        if (resource_type.equals("Network"))
        {
    		// TODO: Use a ProgressDialog
    		Server yastServer =
    			new Server (b.getInt("SERVER_ID"),
    					b.getString("SERVER_NAME"),
    					b.getString("SERVER_SCHEME"),
    					b.getString("SERVER_HOSTNAME"),
    					b.getInt("SERVER_PORT"),
    					b.getString("SERVER_USER"),
    					b.getString("SERVER_PASS"));

    		Metric metric = null;
    		try {
    			Collection<com.novell.webyast.status.Graph> graphs = yastServer.getStatusModule ().getGraphs();
    			boolean boo = yastServer.getStatusModule ().isHealthy(Metric.MEMORY, graphs);
    			boo = yastServer.getStatusModule ().isHealthy(Metric.CPU, graphs);
    			boo = yastServer.getStatusModule ().isHealthy(Metric.DISK, graphs);
    			boo = yastServer.getStatusModule ().isHealthy(Metric.NETWORK, graphs);
    			
    			Collection<Metric> networkMetrics = yastServer.getStatusModule ().getMetric(Metric.NETWORK);
    			// FIXME: We are using "eth0" in the meantime, but we should be able to show the other interfaces
    			// Also, each interface has different types, we are hard-coding "if_packets"
    			String id = null;
    			for (Metric m : networkMetrics) {
    				if (m.getTypeInstance () != null && m.getTypeInstance().compareTo("eth0") == 0
    						&& m.getType () != null && m.getType().compareTo("if_packets") == 0) {
    					id = m.getId ();
    					break;
    				}
    			}
    			metric = yastServer.getStatusModule ().getMetricData(id, 0, 0);
    		} catch (Exception e) {
    			System.out.println(e.getMessage());
    		}
    		// XXX
        	if (metric != null) {
        		// FIXME: GraphArrayList<E>orts one value only,
//        		/ArrayList<Value> xmlValues = (ArrayList<Value>) metric.getValues ();
//	            values = xmlValues.get(0).getValues ().toArray(arg0)
//	            verlabels = new String[] { "6", "4", "2", "0" };
//	            horlabels = new String[] { "2:00", "2:01", "2:02", "2:03" };
//	            GraphView graphView = new GraphView(this, values, "MByte/s", horlabels, verlabels, GraphView.LINE);
//	        	
//	            setContentView(graphView);
        	}
        }
        else
        {
            setContentView(R.layout.display_resource);

            TextView v = (TextView) findViewById(R.id.resource_type);
            v.setText(b.getString("RESOURCE_TYPE"));
            
            Spinner spinner = (Spinner) findViewById(R.id.spinner);

            this.mAdapter = ArrayAdapter.createFromResource(this, R.array.times_array,
                    android.R.layout.simple_spinner_dropdown_item);

            spinner.setAdapter(this.mAdapter);

            OnItemSelectedListener spinnerListener = new myOnItemSelectedListener(this,this.mAdapter);
            spinner.setOnItemSelectedListener(spinnerListener);
            
            //CustomGraphView gv = (CustomGraphView) findViewById(R.id.graph_view);
            values = new float[] { 2.0f, 1.5f, 2.5f, 1.0f , 3.0f, 3.1f, 3.2f };
            verlabels = new String[] { "6", "4", "2", "0" };
            horlabels = new String[] { "2:00", "2:01", "2:02", "2:03" };
            //gv.setCustomGraphViewParms(values, "MByte/s", horlabels, verlabels, CustomGraphView.LINE);
        }*/

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
        float[] values = new float[] { 2.0f, 1.5f, 2.5f, 1.0f , 3.0f, 3.1f, 3.2f };
        String[] verlabels = new String[] { "6", "4", "2", "0" };
        String[] horlabels = new String[] { "2:00", "2:01", "2:02", "2:03" };
        gv.setCustomGraphViewParms(values, "MByte/s", horlabels, verlabels, CustomGraphView.LINE); 
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
