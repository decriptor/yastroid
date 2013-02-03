using Android.App;
using Android.OS;
using Android.Views;
using Android.Database.Sqlite;
using Android.Content;

using YaSTroid.Groups;
using YaSTroid.Servers;
using Android.Widget;

namespace YaSTroid
{
	[Activity (Label = "YaSTroid", MainLauncher = true)]
	public class Yastroid : Activity
	{
		ISharedPreferences _settings;
		ServerGroupListFragment _serverGroupListFragment;
		ServerListFragment _serverListFragment;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.main);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
	        
	        // Initialize preferences
	        _settings = GetPreferences(FileCreationMode.Private);
	        string user = _settings.GetString("username", null);
	        string pass = _settings.GetString("password", null);
	        
	        if (user == null || pass == null) {
	        	//Toast.makeText(Yastroid.this, "Credentials are either not set or invalid", Toast.LENGTH_SHORT).show();
	        }
	        
	        // Force database upgrade if needed
	        InitializeDatabase ();
	        
			_serverGroupListFragment = new ServerGroupListFragment();
			FragmentManager.BeginTransaction ().Add (Resource.Id.server_group_fragment, _serverGroupListFragment, "GROUP_FRAGMENT").Commit();

			_serverListFragment = new ServerListFragment();
			FragmentManager.BeginTransaction().Add(Resource.Id.main_server_fragment, _serverListFragment, "SERVER_FRAGMENT").Commit();
	    }

		void InitializeDatabase ()
		{
			var dbhelper = new YastroidDatabase (this);
			var database = dbhelper.WritableDatabase;
			database.Close ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu, menu);
			//SearchView searchView = (SearchView)menu.FindItem(Resource.Id.menu_search).ActionView;
			// TODO Configure the search info and add any event listeners
	    	return base.OnCreateOptionsMenu(menu);
	    }
	    
	    public bool onOptionsItemSelected(IMenuItem item)
	    {
	    	switch (item.ItemId)
			{
		    	case Resource.Id.preferences:
		    		return true;
				case Resource.Id.menu_add_group:
					AddGroup();
					return true;
				case Resource.Id.menu_add_server:
					AddServer();
					return true;
	    	}

	    	return false;
	    }

		void AddGroup()
		{
			var intent = new Intent(this, typeof(GroupAddActivity));
			StartActivity(intent);
		}

		void AddServer ()
		{
			Intent intent = new Intent(this, typeof(ServerAddActivity));
			int thisShouldBeTheCurrentlySelectedGroup = 0;
			intent.PutExtra("GROUP_ID", thisShouldBeTheCurrentlySelectedGroup);
			StartActivity(intent);
		}
	}
}
