using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ItechArt.Repositories.Abstractions;

public interface IEntityLoadStrategy<TEntity> : IEnumerable<Expression<Func<TEntity, object>>>
    where TEntity : class
{
    IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; }
}