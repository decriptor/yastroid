using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;

namespace YaSTroid
{
	public class SystemMessagesAdapter : ArrayAdapter<SystemMessage>
	{
		private List<SystemMessage> messages;
		private Context context;

		public SystemMessagesAdapter(Context context, int textViewResourceId, List<SystemMessage> messages) : base (context, textViewResourceId, messages)
		{
			this.messages = messages;
			this.context = context;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			View messageView = convertView;
			if (messageView == null) {
				LayoutInflater vi = (LayoutInflater) context
					.GetSystemService(Context.LayoutInflaterService);
				messageView = vi.Inflate(Resource.Layout.system_messages_row, null);
			}

			SystemMessage s = messages[position];
			if (s != null) {
				TextView tt = (TextView) messageView.FindViewById(Resource.Id.toptext);
				TextView bt = (TextView) messageView.FindViewById(Resource.Id.bottomtext);
				if (tt != null) {
					tt.Text = s.getMessage();
				}
				if (bt != null) {
					bt.Text = s.getModule() + ", " + s.getDate();
				}
			}
			return messageView;
		}
	}
}
