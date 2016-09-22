using System;
using System.Collections.Generic;

namespace PT.Poker.Model
{
    public struct Card : IComparable<Card>
    {
        private readonly CardType _cardType;
        private readonly CardColor _cardColor;

        public CardType CardType
        {
            get { return _cardType; }
        }

        public CardColor CardColor
        {
            get { return _cardColor; }
        }

        public Card(CardColor cardColor, CardType cardType)
        {
            _cardColor = cardColor;
            _cardType = cardType;
        }

        public override int GetHashCode()
        {
            return (int)CardType * 4 + (int)CardColor;
        }

        public int CompareTo(Card other)
        {
            return CardType.CompareTo(other.CardType);
        }

        public override string ToString()
        {
            string type = ((int)CardType + 2) > 10 ? CardType.ToString() : ((int)CardType + 2).ToString();
            Dictionary<CardColor, string> dict = new Dictionary<CardColor, string>()
            {
                {CardColor.Clubs, "♣"},
                {CardColor.Diamonds, "♦"},
                {CardColor.Hearts, "♥"},
                {CardColor.Spades, "♠"},
            };
            return type + dict[CardColor];
        }
    }
}
