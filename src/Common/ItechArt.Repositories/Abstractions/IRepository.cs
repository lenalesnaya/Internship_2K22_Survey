using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IRepository<TEntity> : IReadonlyRepository<TEntity>
    where TEntity : class, IEntityId
{
    Task AddAsync(TEntity model);

    Task UpdateByIdAsync(int id);

    void Update(TEntity model);

    Task RemoveByIdAsync(int id);

    void Remove(TEntity model);
}