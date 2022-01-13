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
        var counter = Task.Run(() => CreateCounter(_counter));

        return counter;
    }

    public Task<Counter> IncrementCounterAsync()
    {
        _counter += 1;

        var counter =  Task.Run(() => CreateCounter(_counter));

        return counter;
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