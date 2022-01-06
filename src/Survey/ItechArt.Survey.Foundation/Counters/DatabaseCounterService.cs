using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Counters.Abstractions;

namespace ItechArt.Survey.Foundation.Counters;

public class DatabaseCounterService : ICounterService
{
    public Counter GetCounter()
    {
        return new Counter();
    }

    public Counter IncrementCounter()
    {
        return new Counter();
    }
}