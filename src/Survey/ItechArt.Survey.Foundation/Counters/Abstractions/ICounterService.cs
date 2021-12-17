using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Counters.Abstractions
{
    public interface ICounterService
    {
        Counter IncrementCounter();

        Counter SetCounterValue();
    }
}