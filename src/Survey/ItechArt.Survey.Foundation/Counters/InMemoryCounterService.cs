using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Counters.Abstractions;

namespace ItechArt.Survey.Foundation.Counters;

public class InMemoryCounterService : ICounterService
{
    private int _counter;


    public InMemoryCounterService()
    {
        _counter = 0;
    }


    public Task<Counter> GetCounterAsync()
    {
        var counter = CreateCounter(_counter);

        return Task.FromResult(counter);
    }

    public Task<Counter> IncrementCounterAsync()
    {
        _counter += 1;

        var counter = CreateCounter(_counter);

        return Task.FromResult(counter);
    }


    private static Counter CreateCounter(int value)
    {
        var counter = new Counter
        {
            Value = value
        };

        return counter;
    }
}