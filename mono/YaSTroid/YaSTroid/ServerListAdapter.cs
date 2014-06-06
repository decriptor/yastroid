using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	public class ServerListAdapter : ArrayAdapter<Server>
	{
		private List<Server> servers;
		private Context context;

		public ServerListAdapter(Context context, int textViewResourceId, List<Server> servers) : base (context, textViewResourceId,  servers)
		{
			this.servers = servers;
			this.context = context;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View serverView = convertView;
			if (serverView == null) {
				LayoutInflater vi = (LayoutInflater) context
					.GetSystemService(Context.LayoutInflaterService);
				serverView = vi.Inflate(Resource.Layout.serverlistrow, null);
			}

			Server s = servers[position];
			if (s != null) {
				TextView tt = serverView.FindViewById<TextView>(Resource.Id.toptext);
				TextView bt = serverView.FindViewById<TextView>(Resource.Id.bottomtext);
				if (tt != null) {
					tt.Text = s.getName();
				}
				if (bt != null) {
					bt.Text = s.getFullUrl();
				}
			}
			return serverView;
		}
	}
}
