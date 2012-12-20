using Android.Views;
using System.Runtime.Remoting.Contexts;
using Android.Widget;

namespace YaSTroid
{
	public class ServerListAdapter : ArrayAdapter<Server>
	{
		ArrayList<Server> servers;
		Context context;

		public ServerListAdapter(Context context, int textViewResourceId, ArrayList<Server> servers) : base(context, textViewResourceId, servers)
		{
			this.servers = servers;
			this.context = context;
		}

		public override View getView(int position, View convertView, ViewGroup parent)
		{
			View serverView = convertView;
			if (serverView == null)
			{
				LayoutInflater vi = (LayoutInflater) context.getSystemService(Context.LayoutInflaterService);
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
