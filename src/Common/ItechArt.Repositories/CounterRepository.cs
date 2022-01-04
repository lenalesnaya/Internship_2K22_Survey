using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories
{
        public class CounterRepository : ICounterRepository
        {
            private readonly ItechArtDbContext _context;

            public CounterRepository(ItechArtDbContext context)
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
