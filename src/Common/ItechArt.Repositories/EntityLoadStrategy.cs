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
     public IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; }

     public EntityLoadStrategy(params Expression<Func<TEntity, object>>[] includes)
     {
          Includes = includes;
     }


     public IEnumerator<Expression<Func<TEntity, object>>> GetEnumerator()
     {
          return Includes.GetEnumerator();
     }

     IEnumerator IEnumerable.GetEnumerator()
     {
          return GetEnumerator();
     }
}