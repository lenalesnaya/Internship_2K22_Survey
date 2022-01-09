using ItechArt.Repositories.Abstractions;

namespace ItechArt.Survey.DomainModel.Abstractions;

public abstract class BaseEntity : IEntityId
{
    public int Id { get; set; }
}