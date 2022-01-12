using System.Linq;
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
        var counter = (await _unitOfWork.GetRepository<Counter>().GetAllAsync()).FirstOrDefault();
        
        if (counter == null)
        {
            counter = new Counter{ Value = 0 }; 
            _unitOfWork.GetRepository<Counter>().Add(counter);
            await _unitOfWork.SaveChangesAsync();
        }

        return counter;
    }

    public async Task<Counter> IncrementCounterAsync()
    {
        var counter = (await _unitOfWork.GetRepository<Counter>().GetAllAsync()).FirstOrDefault();

        if (counter != null) counter.Value += 1;

        await _unitOfWork.SaveChangesAsync();

        return counter;
    }
}