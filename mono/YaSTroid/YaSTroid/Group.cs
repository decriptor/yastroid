
namespace YaSTroid
{
	public class Group
	{
		private int id;
		private string name;
		private string description;
		private int icon;

		public Group(int id, string name, string description, int icon)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.icon = icon;
		}
		
		public string getName()
		{
			return name;
		}
		
		public string getDescription()
		{
			return description;
		}

		public int getId()
		{
			return id;
		}
		
		public int getIcon()
		{
			return icon;
		}
	}
}
