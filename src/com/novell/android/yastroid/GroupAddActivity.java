package com.novell.android.yastroid;

import static com.novell.android.yastroid.YastroidOpenHelper.*;

import android.app.Activity;
import android.content.ContentValues;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class GroupAddActivity extends Activity {

	private SQLiteDatabase database;
	private Button addButton;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.groupadd);

		YastroidOpenHelper helper = new YastroidOpenHelper(this);
		database = helper.getWritableDatabase();
		
		this.addButton = (Button)this.findViewById(R.id.button_add_group);
		this.addButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if(addGroup()) {
					finish();
				}
			}
		});
	}

	private boolean addGroup() {
		boolean result = false;
		try {
			
			String name = ((EditText)findViewById(R.id.edit_group_name)).getText().toString();
			String description = ((EditText)findViewById(R.id.edit_group_description)).getText().toString();
			String icon = ((EditText)findViewById(R.id.edit_group_icon)).getText().toString();
			
			
			if (name.length() == 0 || description.length() == 0 || icon.length() == 0) {
				Toast.makeText(GroupAddActivity.this, "One or more fields are empty", Toast.LENGTH_SHORT).show();
				return false;
			}

			ContentValues values = new ContentValues();
			values.put(GROUP_NAME, name);
			values.put(GROUP_DESCRIPTION, description);
			values.put(GROUP_ICON, icon);
			
			database.insert(GROUP_TABLE_NAME, "null", values);
			database.close();
			Log.i("addGroup", name + " group has been added.");
			result = true;
		} catch (Exception e) {
			Log.e("addGroup", e.getMessage());
		}
		Toast.makeText(GroupAddActivity.this, "Group Added", Toast.LENGTH_SHORT).show();
		return result;
	}
}
