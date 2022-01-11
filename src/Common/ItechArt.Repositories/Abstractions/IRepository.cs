using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : class
{
    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}