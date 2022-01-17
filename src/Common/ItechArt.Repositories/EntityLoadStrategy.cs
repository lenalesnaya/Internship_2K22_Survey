using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ItechArt.Repositories;

public class EntityLoadStrategy<TEntity>
    where TEntity : class
{
    public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }


    public EntityLoadStrategy(
        Expression<Func<TEntity, object>> include,
        params Expression<Func<TEntity, object>>[] otherInclude)
    {
        Includes = new List<Expression<Func<TEntity, object>>>(otherInclude) { include };
    }
}