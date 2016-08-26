using PT.Algorithm.Model;
using PT.Interfaces;

namespace PT.Algorithm
{
    public class MonteCarlo<T> where T : IMarkable, IRandomGenerated, new()
    {
        private readonly int _n;
        private readonly T _item;

        public MonteCarlo(T item, int n)
        {
            _item = item;
            _n = n;
        }

        public MonteCarloResult Solve()
        {
            int bigger = 0;
            int exact = 0;
            IMark itemMark = _item.GetMark();
            for (int i = 0; i < _n; i++)
            {
                T randomItem = new T();
                randomItem.Generate();
                int comparison = itemMark.CompareTo(randomItem.GetMark());
                if (comparison > 0)
                {
                    ++bigger;
                }
                else if (comparison == 0)
                {
                    ++exact;
                }
            }
            return new MonteCarloResult((double)bigger/ _n, (double)exact / _n);
        }
    }
}
