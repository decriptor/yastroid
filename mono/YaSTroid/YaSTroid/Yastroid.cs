using Android.App;
using Android.OS;
using Android.Views;
using Android.Database.Sqlite;
using Android.Content;

namespace YaSTroid
{
	[Activity (Label = "YaSTroid", MainLauncher = true)]
	public class Yastroid : Activity
	{
		SharedPreferences settings;
		SQLiteDatabase database;
		YastroidOpenHelper dbhelper;
	    /** Called when the activity is first created. */

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.main);
	        
	        // Initialize preferences
	        settings = GetPreferences(Android.Content.FileCreationMode.Private);
	        string user = settings.GetString("username", null);
	        string pass = settings.GetString("password", null);
	        
	        if (user == null || pass == null) {
	        	//Toast.makeText(Yastroid.this, "Credentials are either not set or invalid", Toast.LENGTH_SHORT).show();
	        }
	        
	        // Force database upgrade if needed
	        dbhelper = new YastroidOpenHelper(this);
	        database = dbhelper.getWritableDatabase();
	        database.Close();
	        
	        // Preliminary Group work. The ability to add/delete groups needs
	        // to be finish.  Adding servers to groups is still completely missing.
	        Intent intent = new Intent(Yastroid, typeof(GroupListActivity));
	        //Intent intent = new Intent(Yastroid.this, ServerListActivity.class);
	        StartActivity(intent);
	    }

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
	    	MenuInflater inflater = MenuInflater;
	    	inflater.Inflate(Resource.Menu.menu, menu);
	    	return base.OnCreateOptionsMenu(menu);
	    }
	    
	    public bool onOptionsItemSelected(IMenuItem item)
	    {
	    	switch (item.ItemId) {
	    	case Resource.Id.preferences:
	    		return true;
	    	}
	    	return false;
	    }
	}
}
