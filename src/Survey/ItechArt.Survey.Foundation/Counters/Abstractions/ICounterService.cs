using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Counters.Abstractions;

public interface ICounterService
{
    Task<Counter> GetCounterAsync();

    Task<Counter> IncrementCounterAsync();
}