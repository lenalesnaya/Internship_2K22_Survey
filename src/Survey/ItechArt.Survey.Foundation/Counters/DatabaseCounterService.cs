using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Counters.Abstractions;

namespace ItechArt.Survey.Foundation.Counters;

public class DatabaseCounterService : ICounterService
{
    private readonly IUnitOfWork _unitOfWork;


    public DatabaseCounterService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Counter> GetCounterAsync()
    {
        var repository = _unitOfWork.GetRepository<Counter>();
        var counter = await repository.GetFirstOrDefaultAsync();

        if(counter == null)
        {
            counter = CreateCounter(0);
        }

        return counter;
    }

    public async Task<Counter> IncrementCounterAsync()
    {
        var repository = _unitOfWork.GetRepository<Counter>();
        var counter = await repository.GetFirstOrDefaultAsync();

        if (counter == null)
        {
            counter = CreateCounter(1);
            repository.Add(counter);
        }
        else
        {
            counter.Value += 1;
        }
        await _unitOfWork.SaveChangesAsync();

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