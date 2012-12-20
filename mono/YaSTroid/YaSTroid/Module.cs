namespace YaSTroid
{
	public class Module
	{
		string name;
		string display;
		int icon;

		public Module( string name, string display, int icon )
		{
			this.name = name;
			this.display = display;
			this.icon = icon;
		}
		
		public string getName() {
			return name;
		}
		
		public string getDisplay() {
			return display;
		}

		public int getIcon() {
			return icon;
		}

	}
}
