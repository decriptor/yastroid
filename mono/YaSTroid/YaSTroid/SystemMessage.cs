namespace YaSTroid
{
	public class SystemMessage
	{
		string message;
		string module;
		string date;

		public SystemMessage( string message, string module, string date)
		{
			this.message = message;
			this.module = module;
			this.date = date;
		}
		
		public string getMessage() {
			return message;
		}

		public string getModule() {
			return module;
		}

		public string getDate() {
			return date;
		}
	}
}
