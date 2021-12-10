using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.CounterServices.Abstractions
{
    public interface ICounterService
    {
        Counter IncrementCounter();

        Counter GetCounter();
    }
}