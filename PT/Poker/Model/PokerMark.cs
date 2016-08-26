using PT.Interfaces;

namespace PT.Poker.Model
{
    public struct PokerMark : IMark
    {
        public PokerLayouts PokerLayout { get; }

        public int PowerOfCards { get; }

        public CardLayout BestLayout { get; }

        public PokerMark(PokerLayouts pokerLayout, int powerOfCards, CardLayout bestLayout)
        {
            PokerLayout = pokerLayout;
            PowerOfCards = powerOfCards;
            BestLayout = bestLayout;
        }

        private int CompareTo(PokerMark other)
        {
            if (PokerLayout > other.PokerLayout)
            {
                return 1;
            }
            if (PokerLayout == other.PokerLayout)
            {
                return PowerOfCards.CompareTo(other.PowerOfCards);
            }
            return -1;
        }

        public int CompareTo(IMark other)
        {
            return CompareTo((PokerMark)other);
        }
    }
}
