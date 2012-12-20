using Android.Views;
using Android.Content;
using Android.Widget;

namespace YaSTroid
{
	public class SystemMessagesAdapter : ArrayAdapter<SystemMessage> {

	private ArrayList<SystemMessage> messages;
	private Context context;

	public SystemMessagesAdapter(Context context, int textViewResourceId, ArrayList<SystemMessage> messages) : base(context, textViewResourceId, messages)
	{
		this.messages = messages;
		this.context = context;
	}

	public override View getView(int position, View convertView, ViewGroup parent) {
		View messageView = convertView;
		if (messageView == null) {
			LayoutInflater vi = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
			messageView = vi.Inflate(Resource.Layout.system_messages_row, null);
		}

		SystemMessage s = messages[position];
		if (s != null) {
			TextView tt = messageView.FindViewById<TextView>(Resource.Id.toptext);
			TextView bt = messageView.FindViewById<TextView>(Resource.Id.bottomtext);
			if (tt != null) {
				tt.Text = s.getMessage();
			}
			if (bt != null) {
				bt.Text = (s.getModule() + ", " + s.getDate());
			}
		}
		return messageView;
	}
}
}
