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
            throw new System.NotImplementedException();
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
            return val > 0;
        }

        private void GetBuckets()
        {
            foreach (var card in _layout.Cards)
            {
                ++bucketsType[(int) card.CardType];
                ++bucketsColor[(int) card.CardColor];
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
                    if (bucketsType[13] > 0 && i == 0)
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
