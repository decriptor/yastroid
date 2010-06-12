package com.novell.android.yastroid;

import java.util.ArrayList;

import android.content.Context;
import android.content.res.Resources;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class ModuleAdapter extends ArrayAdapter<Module> {

	private ArrayList<Module> modules;
	private Context context;

	public ModuleAdapter(Context context, int textViewResourceId,
			ArrayList<Module> modules) {
		super(context, textViewResourceId, modules);
		this.modules = modules;
		this.context = context;
	}
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		
		View moduleView = convertView;
		if (moduleView == null) {
			LayoutInflater vi = (LayoutInflater) context
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			moduleView = vi.inflate(R.layout.module_list_row, null);
		}
		
		
		Module m = modules.get(position);
		if (m != null) {
			ImageView icon = (ImageView) moduleView.findViewById(R.id.module_icon);
			TextView name = (TextView) moduleView.findViewById(R.id.module_name);
			if (icon != null) {
				icon.setImageResource(m.getIcon());
				//icon.setImageResource(R.drawable.yast_system);
			}
			if (name != null) {
				name.setText(m.getDisplay());
			}
		}
		
		return moduleView;
	}
}
