using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ItechArt.Repositories.Abstractions;

namespace ItechArt.Repositories;

public class EntityLoadStrategy<TEntity> : IEntityLoadStrategy<TEntity>
    where TEntity : class
{
    public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }


    public EntityLoadStrategy(
        Expression<Func<TEntity, object>> include,
        params Expression<Func<TEntity, object>>[] otherIncludes)
    {
        Includes = new List<Expression<Func<TEntity, object>>>(otherIncludes) { include };
    }
}