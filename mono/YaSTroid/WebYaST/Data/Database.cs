// Largely taken from Xamarin's FieldService Pre-built app

using System;
using System.Threading;
using SQLite;
using System.Threading.Tasks;

namespace MobileShared.Data
{
	public static class Database
	{
		static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database.db");

		static bool initialized = false;
		static readonly Type [] tableTypes = new Type []
		{
			typeof(Server),
			typeof(Group),
		};

		public static Task Initialize (CancellationToken cancellationToken)
		{
			return CreateDatabase(new SQLiteAsyncConnection(Path, true), cancellationToken);
		}

		public static SQLiteAsyncConnection GetConnection (CancellationToken cancellationToken)
		{
			var connection = new SQLiteAsyncConnection (Path, true);
			if(!initialized)
			{
				CreateDatabase(connection, cancellationToken).Wait();
			}
			return connection;
		}

		static Task CreateDatabase (SQLiteAsyncConnection connection, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew (() =>
			{
				//Create the tables
				var createTask = connection.CreateTablesAsync (tableTypes);
				createTask.Wait ();

				initialized = true;
			});
		}
	}
}
