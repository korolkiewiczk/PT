using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT
{
    class PokerCardPowerResolver
    {
        private CardLayout _layout;

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
