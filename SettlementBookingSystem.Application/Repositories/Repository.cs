using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly FilterDefinition<T> _excludeDeletedFilter;
        private readonly FindOptions _findOptions;

        protected IMongoDatabase Database { get; }
        
        public IMongoCollection<T> Collection { get; }

        public Repository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            Collection = Database.GetCollection<T>(collectionName);
            _excludeDeletedFilter = Builders<T>.Filter.Eq(f => f.IsActive, true);
            _findOptions = new FindOptions { Collation = new Collation("en", strength: CollationStrength.Secondary) };
        }

        public async Task<T> InsertAsync(T t)
        {
            t.CreatedOn = DateTime.UtcNow;
            t.UpdatedOn = DateTime.UtcNow;

            await Collection.InsertOneAsync(t);
            return t;
        }

        public async Task<T> UpsertAsync(T t)
        {
            t.UpdatedOn = DateTime.UtcNow;

            var builder = Builders<T>.Filter;
            var filter = builder.Eq(pf => pf.Id, t.Id);

            var updatedT = await Collection.FindOneAndReplaceAsync(filter, t, new FindOneAndReplaceOptions<T>
            {
                IsUpsert = true
            });
            return updatedT;
        }

        public async Task<T> FindAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(f => f.Id, id) & _excludeDeletedFilter;

            return (await Collection.FindAsync<T>(filter)).FirstOrDefault();
        }

        public async Task<T> GetOneAsync(FilterDefinition<T> filter)
        {
            filter &= _excludeDeletedFilter;

            return (await Collection.FindAsync<T>(filter)).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filter, IEnumerable<string> fields)
        {
            filter &= _excludeDeletedFilter;

            if (fields != null && fields.Any())
            {
                var projection = ConvertFieldNamesToProjection(fields);
                return await Collection.Find<T>(filter, _findOptions).Project<T>(projection).ToListAsync();
            }

            return await Collection.Find<T>(filter, _findOptions).ToListAsync();
        }

        public async Task<T> UpdateAsync(T t)
        {
            t.UpdatedOn = DateTime.UtcNow;

            var filter = Builders<T>.Filter.Eq(f => f.Id, t.Id) & _excludeDeletedFilter;

            var updatedT = await Collection.FindOneAndReplaceAsync(filter, t, new FindOneAndReplaceOptions<T>
            {
                ReturnDocument = ReturnDocument.After
            });

            return updatedT;
        }

        public async Task<T> UpdateByFilterAsync(T t, FilterDefinition<T> filter)
        {
            filter &= _excludeDeletedFilter;

            var updatedT = await Collection.FindOneAndReplaceAsync(filter, t, new FindOneAndReplaceOptions<T>
            {
                ReturnDocument = ReturnDocument.After
            });

            return updatedT;
        }

        public async Task<T> UpdatePartialAsync(string id, UpdateDefinition<T> definition)
        {
            definition = definition.Set(t => t.UpdatedOn, DateTime.UtcNow);

            var filter = Builders<T>.Filter.Eq(f => f.Id, id) & _excludeDeletedFilter;

            var updatedT = await Collection.FindOneAndUpdateAsync(filter, definition, new FindOneAndUpdateOptions<T>
            {
                ReturnDocument = ReturnDocument.After
            });

            return updatedT;
        }

        public async Task<bool> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition)
        {
            definition = definition.Set(t => t.UpdatedOn, DateTime.UtcNow);

            filter &= _excludeDeletedFilter;

            var updatedT = await Collection.UpdateManyAsync(filter, definition);

            return updatedT.IsAcknowledged;
        }

        public async Task<T> UpdatePartialByFilterAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition)
        {
            definition = definition.Set(t => t.UpdatedOn, DateTime.UtcNow);

            filter &= _excludeDeletedFilter;

            var updatedT = await Collection.FindOneAndUpdateAsync(filter, definition, new FindOneAndUpdateOptions<T>
            {
                ReturnDocument = ReturnDocument.After
            });

            return updatedT;
        }

        public async Task UpdateManyPartialByFilterAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition)
        {
            definition = definition.Set(t => t.UpdatedOn, DateTime.UtcNow);

            filter &= _excludeDeletedFilter;

            await Collection.UpdateManyAsync(filter, definition);
        }

        public async Task<bool> DeleteAsync(string id, bool physicalDeletion = false)
        {
            var filter = Builders<T>.Filter.Eq(f => f.Id, id) & _excludeDeletedFilter;

            if (physicalDeletion)
            {
                var deletedT = await Collection.FindOneAndDeleteAsync(filter);
                return deletedT != null;
            }

            var updateDefinition = Builders<T>.Update
                .Set(t => t.IsActive, false)
                .Set(t => t.UpdatedOn, DateTime.UtcNow);
            var updatedT = await Collection.FindOneAndUpdateAsync(filter, updateDefinition);
            return updatedT != null;
        }

        public async Task<bool> DeleteManyAsync(FilterDefinition<T> filter, bool physicalDeletion = false)
        {
            filter &= _excludeDeletedFilter;

            if (physicalDeletion)
            {
                await Collection.DeleteManyAsync(filter);
            }
            else
            {
                var updateDefinition = Builders<T>.Update.Set(t => t.IsActive, false).Set(t => t.UpdatedOn, DateTime.UtcNow);
                await Collection.UpdateManyAsync(filter, updateDefinition);
            }

            return true;
        }

        public async Task<bool> AnyAsync(FilterDefinition<T> filter)
        {
            filter &= _excludeDeletedFilter;
            return await Collection.Find(filter).AnyAsync();
        }

        public async Task InsertManyAsync(List<T> ts)
        {
            foreach (var t in ts)
            {
                t.CreatedOn = DateTime.UtcNow;
                t.UpdatedOn = DateTime.UtcNow;
            }
            await Collection.InsertManyAsync(ts);
        }

        public async Task<long> CountAsync(FilterDefinition<T> filter)
        {
            filter &= _excludeDeletedFilter;
            return await Collection.CountDocumentsAsync(filter);
        }


        private static ProjectionDefinition<T> ConvertFieldNamesToProjection(IEnumerable<string> fields)
        {
            var projection = Builders<T>.Projection.Include("_id");
            return fields.Where(f => !string.IsNullOrEmpty(f)).Aggregate(projection, (current, field) => current.Include(field));
        }
    }
}
