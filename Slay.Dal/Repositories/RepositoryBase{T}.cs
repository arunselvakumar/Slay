using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Driver;

namespace Slay.Dal.Repositories
{
	public abstract class RepositoryBase<T>
	{
		private readonly string _userName;

		private readonly string _password;

		private readonly string _host;

		private readonly string _databaseId;

		private readonly string _collectionId;

		protected MongoClient MongoClient;

		protected IMongoCollection<T> Collection;

		protected RepositoryBase(string databaseId, string collectionId)
		{
			this._host = "slay-dev.documents.azure.com";
			this._userName = "slay-dev";
			this._password = "eci6ORyI50QMBkziPp1SAl23ZaB9qGW7fiCWx1q68vDCEA9KzZlZpBO19E5zJJSolRgRSQsZbfH8l3fUDAafAA==";

			_databaseId = databaseId;
			_collectionId = collectionId;

			this.InitializeDatabaseConnection();
		}

		private void InitializeDatabaseConnection()
		{
			var mongoClientSettings = new MongoClientSettings
			{
				Server = new MongoServerAddress(_host, 10255),
				UseSsl = true,
				SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12}
			};

			var mongoIdentity = new MongoInternalIdentity(_databaseId, _userName);
			var mongoIdentityEvidence = new PasswordEvidence(_password);

			mongoClientSettings.Credentials = new List<MongoCredential>()
			{
				new MongoCredential("SCRAM-SHA-1", mongoIdentity, mongoIdentityEvidence)
			};

			MongoClient = new MongoClient(mongoClientSettings);

			Collection = MongoClient.GetDatabase(_databaseId).GetCollection<T>(_collectionId);
		}
	}
}