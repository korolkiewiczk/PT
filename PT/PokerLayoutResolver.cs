using System;
using System.Linq;

namespace PT
{
    class PokerLayoutResolver
    {
        private CardLayout _layout;

        private CardLayout _bestLayout;
        private PokerLayouts _pokerLayout = PokerLayouts.HighCard;

        public CardLayout BestLayout => _bestLayout;

        public PokerLayouts PokerLayout => _pokerLayout;



        public PokerLayoutResolver(CardLayout layout)
        {
            _layout = layout;
            Init();
        }


        private int[] bucketsType = new int[13];
        private int[] bucketsColor = new int[4];

        private void Init()
        {
            GetBuckets();

            var bestFlush = GetFlush();

            int bestTwo;
            int secondTwo;
            int bestThree;
            int bestFour;
            int bestStraight;
            GetPokerLayouts(out bestTwo, out secondTwo, out bestThree, out bestFour, out bestStraight);

            _pokerLayout = GetPockerLayouts(bestFlush, bestTwo, secondTwo, bestThree, bestFour, bestStraight);

            _bestLayout = GetBestLayout(_pokerLayout, bestFlush, bestTwo, secondTwo, bestThree, bestFour, bestStraight);
        }

        private CardLayout GetBestLayout(PokerLayouts pokerLayout, int bestFlush, int bestTwo, int secondTwo, int bestThree, int bestFour, int bestStraight)
        {
            if (pokerLayout == PokerLayouts.None) return _layout;
            CardLayout newCardLayout = new CardLayout(new Card[5]);
            int j = 0;
            for (int i = 0; i < _layout.Cards.Length; i++)
            {
                var card = _layout.Cards[i];
                int type = (int)card.CardType;
                int color = (int)card.CardColor;
                bool assign = false;
                if (pokerLayout == PokerLayouts.Poker && bestStraight == type && bestFlush == color)
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.FourOfKind && bestFour == type)
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.FullHouse && (bestThree == type || bestTwo == type))
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.Flush && bestFlush == color)
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.Straight && (bestStraight <= type && bestStraight <= type + 5))
                {
                    if (!newCardLayout.Cards.Any(x=>(int)x.CardType == type))
                        assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.Pair && bestThree == type)
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.TwoPair && (bestTwo == type || secondTwo == type))
                {
                    assign = true;
                }
                else
                if (pokerLayout == PokerLayouts.Pair && bestTwo == type)
                {
                    assign = true;
                }
                if (assign)
                    newCardLayout.Cards[j++] = _layout.Cards[i];
            }
            var newArray = _layout.Cards.OrderByDescending(x => x).ToArray();
            for (int i = 0; i < newArray.Length && j < 5; i++)
            {
                if (!newCardLayout.Cards.Contains(newArray[i]))
                    newCardLayout.Cards[j++] = newArray[i];
            }
            if (j < 5) throw new Exception("Number of cards in new layout < 5");
            return newCardLayout;
        }

        private PokerLayouts GetPockerLayouts(int bestFlush, int bestTwo, int secondTwo, int bestThree, int bestFour, int bestStraight)
        {
            if (Ok(bestFlush) && Ok(bestStraight))
                return PokerLayouts.Poker;

            if (Ok(bestFour))
                return PokerLayouts.FourOfKind;

            if (Ok(bestThree) && Ok(bestTwo))
                return PokerLayouts.FullHouse;

            if (Ok(bestFlush))
                return PokerLayouts.Flush;

            if (Ok(bestStraight))
                return PokerLayouts.Straight;

            if (Ok(bestThree))
                return PokerLayouts.ThreeOfKind;

            if (Ok(bestTwo) && Ok(secondTwo))
                return PokerLayouts.TwoPair;

            if (Ok(bestTwo))
                return PokerLayouts.Pair;

            return PokerLayouts.HighCard;
        }

        private bool Ok(int val)
        {
            return val >= 0;
        }

        private void GetBuckets()
        {
            foreach (var card in _layout.Cards)
            {
                ++bucketsType[(int)card.CardType];
                ++bucketsColor[(int)card.CardColor];
            }
        }

        private int GetFlush()
        {
            int bestFlush = -1;
            for (int i = 0; i < bucketsColor.Length; i++)
            {
                int colorTimes = bucketsColor[i];
                if (colorTimes >= 5)
                {
                    bestFlush = i;
                }
            }
            return bestFlush;
        }

        private void GetPokerLayouts(out int bestTwo, out int secondTwo, out int bestThree, out int bestFour, out int bestStraight)
        {
            bestTwo = -1;
            secondTwo = -1;
            bestThree = -1;
            bestFour = -1;
            bestStraight = -1;

            if (_layout.Cards.Length < 5) return;

            int straightCounter = -1;
            for (int i = 0; i < bucketsType.Length; i++)
            {
                int typeTimes = bucketsType[i];
                if (typeTimes == 2)
                {
                    if (bestTwo != -1)
                        secondTwo = bestTwo;
                    bestTwo = i;
                }

                else if (typeTimes == 3) bestThree = i;
                else if (typeTimes == 4) bestFour = i;

                if (typeTimes > 0 && straightCounter == -1)
                {
                    if (bucketsType[12] > 0 && i == 0)
                        straightCounter = 2;
                    else
                        straightCounter = 1;
                }
                else if (typeTimes > 0 && straightCounter > 0)
                {
                    ++straightCounter;
                }
                else if (typeTimes <= 0)
                {
                    straightCounter = -1;
                }
                if (straightCounter >= 5)
                {
                    bestStraight = i;
                }
            }
        }
    }
}
