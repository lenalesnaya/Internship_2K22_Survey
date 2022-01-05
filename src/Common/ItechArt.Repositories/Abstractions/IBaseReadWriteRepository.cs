using System.Threading.Tasks;
using ItechArt.Survey.DomainModel.Interfaces;

namespace ItechArt.Repositories.Abstractions;

public interface IBaseReadWriteRepository<TEntity>
    : IBaseReadonlyRepository<TEntity>
    where TEntity : class, IEntity
{
    void Save(TEntity model);

    Task SaveAsync(TEntity model);

    void SaveChanges();

    Task SaveChangesAsync();

    void Remove(TEntity model);

    Task RemoveAsync(TEntity model);

    void Remove(int id);

    Task RemoveAsync(int id);
}