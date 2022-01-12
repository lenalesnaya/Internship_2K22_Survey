using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using ItechArt.Repositories.Abstractions;

namespace ItechArt.Repositories;

public class EntityLoadStrategy<TEntity>
     : IEntityLoadStrategy<TEntity>
     where TEntity : class
{
     private IReadOnlyCollection<Expression<Func<TEntity, object>>> _includes;


     public EntityLoadStrategy(params Expression<Func<TEntity, object>>[] includes)
     {
          _includes = includes;
     }


     public IEnumerator<Expression<Func<TEntity, object>>> GetEnumerator()
     {
          return _includes.GetEnumerator();
     }

     IEnumerator IEnumerable.GetEnumerator()
     {
          return GetEnumerator();
     }
}