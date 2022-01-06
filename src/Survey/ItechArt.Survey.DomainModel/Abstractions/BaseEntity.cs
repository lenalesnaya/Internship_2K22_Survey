using ItechArt.Survey.DomainModel.Interfaces;

namespace ItechArt.Survey.DomainModel.Abstractions;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}