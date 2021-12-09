using ItechArt.Survey.DomainModel.Models;

namespace ItechArt.Survey.Foundation.Abstractions.Services
{
    public interface ICounterService
    {
        IncrementCounter IncrementCounter(ref int counter);

        IncrementCounter CounterStatus(int counter);
    }
}