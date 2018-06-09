namespace Slay.Dal.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Authentication;
    using System.Threading;
    using System.Threading.Tasks;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using Slay.DalContracts.Options;
    using Slay.Models.Entities.Interfaces;

    public abstract class RepositoryBase<T> where T : class, IEntity, new()
    {
        private readonly string _collectionId;

        private readonly string _databaseId;

        private readonly string _host;

        private readonly string _password;

        private readonly string _userName;

        protected IMongoCollection<T> Collection;

        protected MongoClient MongoClient;

        protected RepositoryBase(string databaseId, string collectionId)
        {
            this._host = "slay-dev.documents.azure.com";
            this._userName = "slay-dev";
            this._password = "eci6ORyI50QMBkziPp1SAl23ZaB9qGW7fiCWx1q68vDCEA9KzZlZpBO19E5zJJSolRgRSQsZbfH8l3fUDAafAA==";

            _databaseId = databaseId;
            _collectionId = collectionId;

            this.InitializeDatabaseConnection();
        }

        protected bool IsCollectionsExists => this.Collection != null;

        public virtual async Task<long> CountAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                var foo = this.IsCollectionsExists
                              ? await this.Collection.CountAsync(filter, cancellationToken: token)
                              : 0;

                return foo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<bool> ExistsAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                if (!this.IsCollectionsExists)
                {
                    return false;
                }

                var filteredCollection = await this.Collection.FindAsync(filter, cancellationToken: token);
                return await filteredCollection.AnyAsync(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                if (!this.IsCollectionsExists)
                {
                    return new T();
                }

                var filteredCollection = await this.Collection.FindAsync(
                                             Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)),
                                             cancellationToken: token);

                return await filteredCollection.FirstOrDefaultAsync(token);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter,
            PagingOptions pagingOptions,
            IList<SortingOptions> sortingOptions,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                if (!this.IsCollectionsExists)
                {
                    return Enumerable.Empty<T>();
                }

                var sortDefinitionBuilder = new SortDefinitionBuilder<T>();
                var sortDefinitions = new List<SortDefinition<T>>();

                if (sortingOptions.Any())
                {
                    sortDefinitions.AddRange(
                        sortingOptions.Select(
                            sortingOption => sortingOption.SortingMode == SortingMode.Ascending
                                                 ? sortDefinitionBuilder.Ascending(sortingOption.FieldName)
                                                 : sortDefinitionBuilder.Descending(sortingOption.FieldName)));
                }

                var sourceCollection = filter == null ? Collection.Find(_ => true) : Collection.Find(filter);

                sourceCollection.Sort(sortDefinitionBuilder.Combine(sortDefinitions));

                if (pagingOptions.Skip != null)
                {
                    sourceCollection = sourceCollection.Skip(pagingOptions.Skip);
                }

                if (pagingOptions.Limit != null)
                {
                    sourceCollection = sourceCollection.Limit(pagingOptions.Limit);
                }

                return await sourceCollection.ToListAsync(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<T> CreateAsync(T entity, CancellationToken token = default(CancellationToken))
        {
            try
            {
                await this.Collection.InsertOneAsync(entity, cancellationToken: token);

                return await this.GetByIdAsync(entity.Id.ToString(), token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<T> UpdateAsync(
            string id,
            T entity,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                await this.Collection.ReplaceOneAsync(
                    Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)),
                    entity,
                    cancellationToken: token);

                return await this.GetByIdAsync(entity.Id.ToString(), token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var entity = await this.GetByIdAsync(id, token);

                if (entity == null)
                {
                    return false;
                }

                entity.Delete();

                await this.Collection.ReplaceOneAsync(
                    Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)),
                    entity,
                    cancellationToken: token);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void InitializeDatabaseConnection()
        {
            var mongoClientSettings = new MongoClientSettings
                                          {
                                              Server = new MongoServerAddress(_host, 10255),
                                              UseSsl = true,
                                              SslSettings =
                                                  new SslSettings
                                                      {
                                                          EnabledSslProtocols =
                                                              SslProtocols.Tls12
                                                      }
                                          };

            var mongoIdentity = new MongoInternalIdentity(_databaseId, _userName);
            var mongoIdentityEvidence = new PasswordEvidence(_password);

            mongoClientSettings.Credentials =
                new List<MongoCredential>()
                    {
                        new MongoCredential("SCRAM-SHA-1", mongoIdentity, mongoIdentityEvidence)
                    };

            MongoClient = new MongoClient(mongoClientSettings);

            Collection = MongoClient.GetDatabase(_databaseId).GetCollection<T>(_collectionId);
        }
    }
}