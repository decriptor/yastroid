using Android.Content;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	public class StatusListAdapter : ArrayAdapter<SystemStatus>
	{
		private ArrayList<SystemStatus> statii;
		private Context context;
		
		public StatusListAdapter(Context context, int textViewResourceId, ArrayList<SystemStatus> statii) : base(context, textViewResourceId, statii)
		{
			this.statii = statii;
			this.context = context;
		}
		
		public override View getView(int position, View convertView, ViewGroup parent)
		{
			View systemStatusView = convertView;
			TextView name;
			ImageView icon;
			SystemStatus systemStatus;
			
			if (systemStatusView == null) {
				LayoutInflater vi = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
				systemStatusView = vi.Inflate(Resource.Layout.system_status_list_item, null);
			}
			
			systemStatus = statii[position];
			if (systemStatus != null) {
				name = systemStatusView.FindViewById<TextView>(Resource.Id.system_status_text);
				icon = systemStatusView.FindViewById<ImageView>(Resource.Id.system_status_icon);
				if (name != null) {
					name.Text = systemStatus.getName();
				}
				if (icon != null) {
					icon.SetImageDrawable(systemStatus.getIcon());
				}
			}
			return systemStatusView;
		}	
	}
}