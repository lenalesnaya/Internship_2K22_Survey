using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.CounterServices.Abstractions;

namespace ItechArt.Survey.Foundation.CounterServices
{
    public sealed class CounterService : ICounterService
    {
        private static int _counter;


        public Counter IncrementCounter()
        {
            _counter += 1;

            var counter = new Counter
            {
                Value = _counter
            };

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