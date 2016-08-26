using System;
using PT.Interfaces;

namespace PT.Poker.Model
{
    public class CardSet : IEncounter, IRandomGenerated<RandomSetDefinition>
    {
        //my layout is always first
        private CardLayout[] _cardLayouts;
        private int _compareMyLayout;

        private static Random _random = new Random();

        public CardSet(CardLayout[] cardLayouts)
        {
            _cardLayouts = cardLayouts;
            Update();
        }

        public void Generate(RandomSetDefinition arg)
        {
            byte[,] usedCards = new byte[4, 13];

            _cardLayouts=new CardLayout[arg.NumOfPlayers];
            if (arg.MyLayout.Size != 2) throw new Exception();
            Set(usedCards,arg.MyLayout.Cards[0]);
            Set(usedCards, arg.MyLayout.Cards[1]);
            GenerateRandomCards(usedCards, arg.MyLayout, 2, 5);
            for (int i = 1; i < arg.NumOfPlayers; i++)
            {
                
            }

            Update();
        }

        private void Set(byte[,] array, CardColor color, CardType type)
        {
            array[(int) color, (int) type] = 1;
        }

        private void Set(byte[,] array, Card card)
        {
            Set(array, card.CardColor, card.CardType);
        }

        private Card RandomCard(byte[,] array)
        {
            int k = 1000;
            do
            {
                int color = _random.Next(4);
                int type = _random.Next(13);
                if (array[color, type] == 0)
                {
                    Card result=new Card((CardColor)color, (CardType)type);
                    Set(array, result);
                    return result;
                }
            } while ((--k)>0);
            throw new Exception();
        }

        private CardLayout GenerateRandomCards(byte[,] array, CardLayout layout, int start, int end)
        {
            var size = end - start + 1;
            if (layout == null)
            {
                layout = new CardLayout(new Card[size]);
            }
            else
            {
                if (layout.Size < size)
                {
                    var newCards = new Card[size];
                    Array.Copy(layout.Cards, newCards, layout.Size);
                    layout = new CardLayout(newCards);
                }
            }
            for (int i = start; i <= end; i++)
            {
                layout.Cards[i] = RandomCard(array);
            }

            return layout;
        }

        private void Update()
        {
            var myLayout = GetMyLayout();
            bool win = true;
            for (int i = 1; i < _cardLayouts.Length; i++)
            {
                var comparison = myLayout.CompareTo(_cardLayouts[i]);
                if (comparison < 0)
                {
                    _compareMyLayout = -1;
                }
                if (comparison == 0)
                {
                    win = false;
                }
            }

            _compareMyLayout = win ? 1 : 0;
        }

        public bool IsWinning
        {
            get { return _compareMyLayout == 1; }
        }

        public bool IsLoosing
        {
            get { return _compareMyLayout == -1; }
        }

        private CardLayout GetMyLayout()
        {
            return _cardLayouts[0];
        }
    }
}
