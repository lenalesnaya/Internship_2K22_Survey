using ItechArt.Survey.DomainModel;

namespace ItechArt.Repositories
{
        public interface ICounterRepository
        {
            Counter GetCounter();
            void UpdateCounter(Counter counter);
        }
}
