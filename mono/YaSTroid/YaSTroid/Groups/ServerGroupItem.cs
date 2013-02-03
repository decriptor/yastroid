namespace YaSTroid.Groups
{
	public class ServerGroupItem
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public int Icon { get; private set; }

		public ServerGroupItem(int id ,string name, string description, int icon)
		{
			Id = id;
			Name = name;
			Description = description;
			Icon = icon;
		}
		
	}
}
