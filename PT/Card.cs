using System;
using System.Collections.Generic;

namespace PT
{
    struct Card : IComparable<Card>
    {
        public Card(CardColor cardColor, CardType cardType)
        {
            CardColor = cardColor;
            CardType = cardType;
        }

        public CardType CardType { get; }
        public CardColor CardColor { get; }

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
