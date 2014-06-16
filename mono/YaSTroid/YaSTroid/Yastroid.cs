using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	[Activity (Label = "Yastroid", MainLauncher = true)]
	public class Yastroid : Activity
	{	
		private ISharedPreferences settings;
		private SQLiteDatabase database;
		private YastroidOpenHelper dbhelper;
	    /** Called when the activity is first created. */

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.main);
	        
	        // Initialize preferences
			settings = GetPreferences (FileCreationMode.Private);
			string user = settings.GetString("username", null);
			string pass = settings.GetString("password", null);
	        
	        if (user == null || pass == null) {
				Toast.MakeText(this, "Credentials are either not set or invalid", ToastLength.Short).Show();
	        }
	        
	        // Force database upgrade if needed
	        dbhelper = new YastroidOpenHelper(this);
	        database = dbhelper.WritableDatabase;
			database.Close();
	        
	        // Preliminary Group work. The ability to add/delete groups needs
	        // to be finish.  Adding servers to groups is still completely missing.
			Intent intent = new Intent (this, typeof(GroupListActivity));
	        //Intent intent = new Intent(Yastroid.this, ServerListActivity.class);
			StartActivity(intent);
	    }
	        
		public override bool OnCreateOptionsMenu(IMenu menu)
	    {
			MenuInflater inflater = MenuInflater;
			inflater.Inflate(Resource.Menu.menu, menu);
	    	return true;
	    }
	    
		public bool onOptionsItemSelected(IMenuItem item)
	    {
			switch (item.ItemId)
			{
			case Resource.Id.preferences:
	    		return true;
	    	}
	    	return false;
	    }
	    
	}
}
