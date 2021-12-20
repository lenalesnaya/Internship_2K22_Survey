using System.Data;
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


        public Counter GetCounter()
        {
            var counter = CreateCounterInstance(_counter);

            return counter;
        }

        public Counter IncrementCounter()
        {
            _counter += 1;

            var counter = CreateCounterInstance(_counter);

            return counter;
        }


        private static Counter CreateCounterInstance(int value)
        {
            var counter = new Counter
            {
                Value = value
            };

            return counter;
        }

    }
}