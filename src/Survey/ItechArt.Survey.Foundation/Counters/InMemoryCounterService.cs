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


    public async Task<Counter> GetCounterAsync()
    {
        var counter = await CreateCounterInstance(_counter);

        return counter;
    }

    public async Task<Counter> IncrementCounterAsync()
    {
        _counter += 1;

        var counter = await CreateCounterInstance(_counter);

        return counter;
    }


    private static async Task<Counter> CreateCounterInstance(int value)
    {
        var counter = new Counter
        {
            Value = value
        };

        return counter;
    }
}