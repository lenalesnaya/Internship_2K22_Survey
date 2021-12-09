using ItechArt.Survey.DomainModel.Models;
using ItechArt.Survey.Foundation.Abstractions.Services;

namespace ItechArt.Survey.Foundation.Services
{
    public class CounterService : ICounterService
    {
        public IncrementCounter IncrementCounter(ref int counter)
        {
            var model = new IncrementCounter
            {
                Counter = ++counter
            };

            return model;
        }

        public IncrementCounter CounterStatus(int counter)
        {
            var model = new IncrementCounter
            {
                Counter = counter
            };

            return model;
        }
    }
}