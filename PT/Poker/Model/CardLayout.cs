using System;
using System.Collections.Generic;
using System.Linq;
using PT.Interfaces;
using PT.Poker.Resolving;

namespace PT.Poker.Model
{
    public class CardLayout : IComparable, IMarkable
    {
        private Card[] _cards;

        public Card[] Cards => _cards;

        public CardLayout(Card[] cards)
        {
            _cards = cards;
        }

        public CardLayout(IEnumerable<Card> cards)
        {
            _cards = cards.ToArray();
        }

        public int CompareTo(object other)
        {
            return GetMark().CompareTo(((IMarkable)other).GetMark());
        }

        public IMark GetMark()
        {
            PokerLayoutResolver resolver = new PokerLayoutResolver(this);

            PokerCardPowerResolver cardPowerResolver = new PokerCardPowerResolver(resolver.BestLayout);
            int layoutPower = cardPowerResolver.Resolve();

            return new PokerMark(resolver.PokerLayout, layoutPower, resolver.BestLayout);
        }

        public override string ToString()
        {
#if DEBUG
            return string.Join(" ", _cards).Replace("♠","S").Replace("♣","C").Replace("♥","H").Replace("♦","D");
#else
            return string.Join(" ", _cards);
#endif
        }
    }
}
