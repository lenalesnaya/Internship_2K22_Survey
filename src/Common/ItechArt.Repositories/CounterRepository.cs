using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories
{
        public class CounterRepository : ICounterRepository
        {
            private readonly SurveyDbContext _context;

            public CounterRepository(SurveyDbContext context)
            {
                _context = context;
            }

            public Counter GetCounter()
            {
                return _context.Counters.Find();
            }

            public void UpdateCounter(Counter counter)
            {
                _context.Entry(counter).State = EntityState.Modified;
            }
        }
    }
