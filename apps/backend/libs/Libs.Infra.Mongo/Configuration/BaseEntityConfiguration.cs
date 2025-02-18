using System;
using FwksLabs.Libs.Core.Types;
using MongoDB.Bson.Serialization;

namespace FwksLabs.Libs.Infra.Mongo.Configuration;
public abstract class BaseEntityConfiguration<TEntity>
    where TEntity : BaseEntity<Guid>
{
    protected BaseEntityConfiguration()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
            BsonClassMap.RegisterClassMap<TEntity>(Configure);
    }

    public abstract void Configure(BsonClassMap<TEntity> mapper);

    //public virtual void Configure(BsonClassMap<TEntity> mapper)
    //{
    //    mapper.AutoMap();
    //    mapper.MapIdProperty(x => x.Id);
    //    mapper.MapProperty(x => x.ReferenceId);
    //}
}