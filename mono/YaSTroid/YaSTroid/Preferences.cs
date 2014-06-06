using Android.OS;
using Android.Preferences;
using Android.App;

namespace YaSTroid.WebYaST
{
	[Activity (Label = "DisplayResourceActivity")]
	public class Preferences : PreferenceActivity
	{
		/* Called when the activity is first created */
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			AddPreferencesFromResource (Resource.Xml.preferences);
		}
	}
}
