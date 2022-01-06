using System.Threading.Tasks;
using ItechArt.Survey.DomainModel.Abstractions;

namespace ItechArt.Repositories.Abstractions;

public interface IRepository<TEntity>
    : IReadonlyRepository<TEntity>
    where TEntity : BaseEntity
{
    Task AddAsync(TEntity model);

    Task Update(TEntity model);

    Task RemoveAsync(TEntity model);

    Task RemoveAsync(int id);
}