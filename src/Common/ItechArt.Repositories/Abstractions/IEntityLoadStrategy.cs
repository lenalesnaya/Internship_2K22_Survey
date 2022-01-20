using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ItechArt.Repositories.Abstractions;

public interface IEntityLoadStrategy<TEntity>
    where TEntity : class
{
    IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }
}