using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ItechArt.Repositories;

public class EntityLoadStrategy<TEntity>
    where TEntity : class
{
    public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }


    public EntityLoadStrategy(
        Expression<Func<TEntity, object>> first,
        params Expression<Func<TEntity, object>>[] rest)
    {
        var includes = new List<Expression<Func<TEntity, object>>>(rest) { first };
        Includes = includes;
    }
}