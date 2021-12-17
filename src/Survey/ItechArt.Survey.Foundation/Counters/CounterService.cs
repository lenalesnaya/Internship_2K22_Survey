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

            var counter = SetCounterValue();

            return counter;
        }

        public Counter SetCounterValue()
        {
            var counter = GetCounterInstance();
            counter.Value = _counter;

            return counter;
        }


        private static Counter GetCounterInstance()
        {
            var counter = new Counter();

            return counter;
        }
    }
}