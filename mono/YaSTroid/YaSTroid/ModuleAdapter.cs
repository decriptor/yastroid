using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace YaSTroid
{
	public class ModuleAdapter : ArrayAdapter<Module>
	{
		private List<Module> modules;
		private Context context;

		public ModuleAdapter(Context context, int textViewResourceId, List<Module> modules) : base(context, textViewResourceId, modules)
		{
			this.modules = modules;
			this.context = context;
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{	
			View moduleView = convertView;
			if (moduleView == null) {
				LayoutInflater vi = (LayoutInflater) context
					.GetSystemService(Context.LayoutInflaterService);
				moduleView = vi.Inflate(Resource.Layout.module_list_row, null);
			}
			
			Module m = modules[position];
			if (m != null) {
				ImageView icon = (ImageView) moduleView.FindViewById(Resource.Id.module_icon);
				TextView name = (TextView) moduleView.FindViewById(Resource.Id.module_name);
				if (icon != null) {
					icon.SetImageResource (m.getIcon());
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
