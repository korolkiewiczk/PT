using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT
{
    class Program
    {
        static void Main(string[] args)
        {
            CardLayout layout1 =new CardLayout(new Card[]
            {
                new Card(CardColor.Spades, CardType.C7),
                new Card(CardColor.Diamonds, CardType.C9),
                new Card(CardColor.Clubs, CardType.J),
                new Card(CardColor.Spades, CardType.K),
                new Card(CardColor.Spades, CardType.C2),
                new Card(CardColor.Spades, CardType.C2),
            });

            CardLayout layout2 = new CardLayout(new Card[]
            {
                new Card(CardColor.Spades, CardType.C7),
                new Card(CardColor.Diamonds, CardType.C9),
                new Card(CardColor.Clubs, CardType.Q),
                new Card(CardColor.Spades, CardType.K),
                new Card(CardColor.Spades, CardType.C2),
                new Card(CardColor.Spades, CardType.C2),
            });

            CardLayout layout3 = new CardLayout(new Card[]
            {
                new Card(CardColor.Spades, CardType.C7),
                new Card(CardColor.Diamonds, CardType.C8),
                new Card(CardColor.Clubs, CardType.C9),
                new Card(CardColor.Spades, CardType.C10),
                new Card(CardColor.Spades, CardType.J),
                new Card(CardColor.Spades, CardType.Q),
                new Card(CardColor.Diamonds, CardType.Q),
            });

            CardLayout[] layouts = {layout1, layout2, layout3};

            foreach (var layout in layouts)
            {
                Console.WriteLine(layout);
                var mark = layout.GetMark();
                Console.WriteLine(mark.PokerLayout);
                Console.WriteLine(mark.PowerOfCards);
                Console.WriteLine(mark.BestLayout);
                Console.WriteLine("---");
            }
        }
    }
}
