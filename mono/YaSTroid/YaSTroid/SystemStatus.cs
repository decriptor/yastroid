using Android.App;
using Android.Graphics.Drawables;

namespace YaSTroid
{
	public class SystemStatus
	{
		public const int LOG_STATUS = 0;
		public const int NETWORK_STATUS = 1;
		public const int MEMORY_STATUS = 2;
		public const int DISK_STATUS = 3;
		public const int CPU_STATUS = 4;
		public const int STATUS_GREEN = 100;
		public const int STATUS_RED = 101;
		private Application app;
		private int systemType;
		private int status;
		private Drawable icon;
		private string name;
		
		public SystemStatus(Application app, int systemType, int status)
		{
			this.app = app;
			setSystemType(systemType);
			setStatus(status);
		}
		
		public SystemStatus(Application app, int systemType) : this (app, systemType, 0)
		{
		}
		
		public SystemStatus(Application app, string name)
		{
			this.app = app;
			this.name = name;
		}
		
		public void setSystemType(int type)
		{
			this.systemType = type;
			switch (type) {
			case NETWORK_STATUS:
				name = app.Resources.GetString (Resource.String.network_status_text);
				break;
			case MEMORY_STATUS:
				name = app.Resources.GetString(Resource.String.memory_status_text);
				break;
			case DISK_STATUS:
				name = app.Resources.GetString(Resource.String.disk_status_text);
				break;
			case CPU_STATUS:
				name = app.Resources.GetString(Resource.String.cpu_status_text);
				break;
			default:
				name = "";
				break;
			}
		}
		
		public void setStatus(int status)
		{
			this.status = status;
			switch (status) {
			case STATUS_GREEN:
				icon = app.Resources.GetDrawable(Resource.Drawable.status_green);
				break;
			case 0:
				icon = null;
				break;
			default:
				icon = app.Resources.GetDrawable(Resource.Drawable.status_red);
				break;
			}
		}
		
		public int getSystemType()
		{
			return this.systemType;
		}
		
		public int getStatus()
		{
			return this.status;
		}
		
		public string getName()
		{
			return this.name;
		}
		
		public Drawable getIcon()
		{
			return this.icon;
		}
	}
}
