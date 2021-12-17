using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Counters.Abstractions;

namespace ItechArt.Survey.Foundation.Counters
{
    public class CounterService : ICounterService
    {
        private int _counter;


        public CounterService()
        {
            _counter = 0;
        }


        public Counter IncrementCounter()
        {
            _counter += 1;

            var counter = GetCounter();

            return counter;
        }

        public Counter GetCounter()
        {
            var counter = new Counter
            {
                Value = _counter
            };

            return counter;
        }
    }
}