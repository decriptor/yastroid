using Android.OS;
using Android.Preferences;

namespace YaSTroid
{
	public class Preferences : PreferenceActivity
	{
		/* Called when the activity is first created */
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			AddPreferencesFromResource(Resource.Xml.preferences);
		}
	}
}
