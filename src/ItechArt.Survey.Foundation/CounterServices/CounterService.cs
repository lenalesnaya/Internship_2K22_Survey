using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.CounterServices.Abstractions;

namespace ItechArt.Survey.Foundation.CounterServices
{
    public class CounterService : ICounterService
    {
        private static int _counter;
        public Counter IncrementCounter()
        {
            _counter = _counter + 1;
            var model = new Counter
            {
                Value = _counter
            };

            return model;
        }

        public Counter GetCounter()
        {
            var model = new Counter
            {
                Value = _counter
            };

            return model;
        }
    }
}