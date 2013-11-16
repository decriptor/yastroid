using SQLite;

namespace MobileShared
{
	public class Group
	{

//		const string CREATE_GROUP_TABLE = "CREATE TABLE "
//			+ GROUP_TABLE_NAME + " ("
//				+ "_id INTEGER PRIMARY KEY AUTOINCREMENT, "
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public int Icon { get; set; }
	}
}

