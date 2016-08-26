using System.Linq;
using PT.Poker.Model;

namespace PT.Poker.Resolving
{
    public class PokerCardPowerResolver
    {
        private readonly CardLayout _layout;

        public PokerCardPowerResolver(CardLayout layout)
        {
            _layout = layout;
        }

        public int Resolve()
        {
            return _layout.Cards.Sum(x => 1 << ((int) x.CardType));
        }
    }
}
