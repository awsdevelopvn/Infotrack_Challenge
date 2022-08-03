using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Repositories;

public static class RepositoryRegistration
{
    public static void AddPersistenceRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = configuration.GetSection("MONGODB_CONNECTIONSTRING").Value; // EnvironmentHelper.GetString("MONGODB_CONNECTIONSTRING");
        var dbName = configuration.GetSection("MONGODB_DATABASE_NAME").Value; // EnvironmentHelper.GetString("MONGODB_DATABASE_NAME");
        var mongoClient = Singletons<IMongoClient>.GetOrAdd(dbConfiguration, s =>
        {
            ConventionRegistry.Register("camelCase", new ConventionPack { new CamelCaseElementNameConvention() }, type => true);
            ConventionRegistry.Register("ignoreExtraElements", new ConventionPack { new IgnoreExtraElementsConvention(true) }, type => true);
            ConventionRegistry.Register("enumStringConvention", new ConventionPack { new EnumRepresentationConvention(BsonType.String)  }, type => true); // Required
            return new MongoClient(s);
        });
        
        //BsonSerializer.RegisterSerializer(typeof(object), new CustomEnumSerializer());
        var db = mongoClient.GetDatabase(dbName);
        services.AddScoped<IRepository<User>>(s => new Repository<User>(db, "users"));
        services.AddScoped<IRepository<LogTraceEntity>>(s => new Repository<LogTraceEntity>(db, "logtraces"));
        services.AddScoped<IRepository<Article>>(s => new Repository<Article>(db, "articles"));
    }
}