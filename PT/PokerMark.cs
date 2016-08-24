using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT
{
    struct PokerMark : IComparable<PokerMark>
    {
        private readonly PokerLayouts _pokerLayout;
        private readonly int _powerOfCards;
        private CardLayout _bestLayout;

        public PokerLayouts PokerLayout => _pokerLayout;

        public int PowerOfCards => _powerOfCards;

        public CardLayout BestLayout => _bestLayout;

        public PokerMark(PokerLayouts pokerLayout, int powerOfCards, CardLayout bestLayout)
        {
            _pokerLayout = pokerLayout;
            _powerOfCards = powerOfCards;
            _bestLayout = bestLayout;
        }

        public int CompareTo(PokerMark other)
        {
            if (_pokerLayout > other._pokerLayout)
            {
                return 1;
            }
            if (_pokerLayout == other._pokerLayout)
            {
                return _powerOfCards.CompareTo(other._powerOfCards);
            }
            return -1;
        }
    }
}
