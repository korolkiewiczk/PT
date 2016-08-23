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
            bool isFlush = false;
            foreach (var card in _layout.Cards)
            {
                ++bucketsType[(int) card.CardType];
                ++bucketsColor[(int) card.CardColor];
            }
            foreach (int colorTimes in bucketsColor)
            {
                if (colorTimes >= 5)
                {
                    isFlush = true;
                    break;
                }
            }

            for (int i = 0; i < bucketsType.Length; i++)
            {
                
            }
        }
    }
}
