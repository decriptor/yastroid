using Android.Views;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace YaSTroid
{
	public class ModuleAdapter : ArrayAdapter<Module>
	{
		List<Module> modules;
		Context context;

		public ModuleAdapter(Context context, int textViewResourceId, List<Module> modules) : base(context, textViewResourceId, modules)
		{
			this.modules = modules;
			this.context = context;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View moduleView = convertView;
			if (moduleView == null) {
				LayoutInflater vi = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
				moduleView = vi.Inflate(Resource.Layout.module_list_row, null);
			}
			
			
			Module m = modules[position];
			if (m != null) {
				ImageView icon = moduleView.FindViewById<ImageView>(Resource.Id.module_icon);
				TextView name = moduleView.FindViewById<TextView>(Resource.Id.module_name);
				if (icon != null) {
					icon.SetImageResource(m.getIcon());
					//icon.setImageResource(R.drawable.yast_system);
				}
				if (name != null) {
					name.Text = m.getDisplay();
				}
			}
			
			return moduleView;
		}
	}
}
