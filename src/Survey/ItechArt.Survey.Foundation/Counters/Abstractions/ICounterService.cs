using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Counters.Abstractions;

public interface ICounterService
{
    Counter GetCounter();

    Counter IncrementCounter();
}