using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        IMongoCollection<T> Collection { get; }

        Task<T> FindAsync(string id);

        Task<T> GetOneAsync(FilterDefinition<T> filter);

        Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filter, IEnumerable<string> fields);

        Task<T> InsertAsync(T t);

        Task<T> UpsertAsync(T t);

        Task<T> UpdateAsync(T t);

        Task<T> UpdateByFilterAsync(T t, FilterDefinition<T> filter);

        Task<T> UpdatePartialAsync(string id, UpdateDefinition<T> definition);

        Task<T> UpdatePartialByFilterAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition);

        Task<bool> DeleteAsync(string id, bool physicalDeletion = false);

        Task<bool> DeleteManyAsync(FilterDefinition<T> filter, bool physicalDeletion = false);

        Task<bool> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition);

        Task<bool> AnyAsync(FilterDefinition<T> filter);

        Task UpdateManyPartialByFilterAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition);

        Task InsertManyAsync(List<T> ts);

        Task<long> CountAsync(FilterDefinition<T> filter);
    }
}