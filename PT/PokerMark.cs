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

        public PokerMark(PokerLayouts pokerLayout, int powerOfCards)
        {
            _pokerLayout = pokerLayout;
            _powerOfCards = powerOfCards;
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
