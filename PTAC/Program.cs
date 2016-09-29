using System;
using System.Collections.Generic;
using System.Linq;
using PT.Algorithm;
using PT.Algorithm.Model;
using PT.Poker.Model;

namespace PTAC
{
    class Program
    {
        private const int MonteCarloIterations = 1000;

        private static CardColor? _waitingColor;
        private static CardType? _waitingCardType;

        private static List<Card> _cards = new List<Card>();
        private static int? _numOfPlayers = 6;


        static void Main(string[] args)
        {
            ShowNumOfPlayers();

            //test
            _cards.Add(new Card(CardColor.Spades, CardType.C2));
            _cards.Add(new Card(CardColor.Clubs, CardType.C3));
            _cards.Add(new Card(CardColor.Spades, CardType.A));
            _cards.Add(new Card(CardColor.Diamonds, CardType.C7));
            _cards.Add(new Card(CardColor.Clubs, CardType.Q));
            _cards.Add(new Card(CardColor.Hearts, CardType.K));
            _cards.Add(new Card(CardColor.Spades, CardType.C9));
            //end test

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.F12)
                {
                    break;
                }
                else
                {
                    if (key.Key == ConsoleKey.Escape)
                    {
                        ClearCards();
                    }

                    if (key.KeyChar == '`' && _cards.Count > 0)
                    {
                        RemoveLastCard();
                    }

                    int? newNumOfPlayers = GetNumOfPlayers(key);
                    if (newNumOfPlayers != null)
                    {
                        _numOfPlayers = newNumOfPlayers.Value;
                        SolveAndShow();
                        Console.WriteLine();

                        ShowNumOfPlayers();
                        continue;
                    }

                    if (_cards.Count == 7)
                    {
                        ShowMax();
                        continue;
                    }

                    SetCardParts(key);

                    AddCardsIfAnyAndShowBoard();
                }
            }
        }

        private static void ShowMax()
        {
            ShowBoard();
            Console.WriteLine("MAX");
        }

        private static void SolveAndShow()
        {
            if (_cards.Count >= 5)
            {
                CardLayout layout = new CardLayout(_cards);
                var mark = (PokerMark)layout.GetMark();
                ShowLayout(mark.BestLayout.Cards, true);
                Console.Write(" === " + mark.PokerLayout + "   ~" + mark.PowerOfCards + "~");
            }
            else
            {
                Console.Write("Cards to go " + (5 - _cards.Count));
            }

            var result = ComputeMonteCarloResult();
            ShowMonteCarloResult(result);
        }

        private static MonteCarloResult ComputeMonteCarloResult()
        {
            CardSet cardSet = new CardSet();
            RandomSetDefinition arg = new RandomSetDefinition
            {
                MyLayout = new CardLayout(_cards.Take(2).ToArray()),
                NumOfPlayers = _numOfPlayers.Value,
                Board = _cards.Skip(2).Take(5).ToArray()
            };
            MonteCarlo<CardSet, RandomSetDefinition> monteCarlo =
                new MonteCarlo<CardSet, RandomSetDefinition>(cardSet, MonteCarloIterations, arg);

            MonteCarloResult result = monteCarlo.Solve();
            return result;
        }

        private static void ShowMonteCarloResult(MonteCarloResult result)
        {
            Console.WriteLine();
            Console.Write(string.Format("{0}% - {1}% - {2}%", result.Better*100, result.Exact * 100, result.Smaller * 100));
        }

        private static void SetCardParts(ConsoleKeyInfo key)
        {
            CardColor? color = CheckColor(key);
            CardType? cardType = CheckCard(key);

            if (color != null)
            {
                _waitingColor = color;
                Console.Write(color + " ");
            }

            if (cardType != null)
            {
                _waitingCardType = cardType;
                Console.Write(cardType + " ");
            }

            if (color == null && cardType == null)
            {
                CancelKey();
            }
        }

        private static void AddCardsIfAnyAndShowBoard()
        {
            if (_waitingColor != null && _waitingCardType != null)
            {
                var card = new Card(_waitingColor.Value, _waitingCardType.Value);
                if (!_cards.Contains(card))
                {
                    _cards.Add(card);
                }
                else
                {
                    Console.Write("USED");
                }

                ClearWaitingFields();

                ShowBoard();
            }
        }

        private static void ShowBoard()
        {
            Console.WriteLine();
            //
            ShowLayout(_cards);
            Console.WriteLine();
            //
            SolveAndShow();
            Console.WriteLine();
            //
            ShowNumOfPlayers();
        }

        private static void ShowLayout(IReadOnlyList<Card> cards, bool markFirstCards = false)
        {
            var defColor = Console.BackgroundColor;
            for (int i = 0; i < cards.Count; i++)
            {
                if (i >= 2 && cards.Count >= 5 && !markFirstCards)
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                else if (markFirstCards && cards.Count >= 5 && i <= 5)
                    Console.BackgroundColor = ConsoleColor.DarkRed;

                PrintCard(cards[i]);
            }

            Console.BackgroundColor = defColor;
        }

        private static void ShowNumOfPlayers()
        {
            Console.Write(_numOfPlayers + ">");
        }

        private static void CancelKey()
        {
            ClearWaitingFields();

            ClearConsoleLine();
            ShowNumOfPlayers();
        }

        private static void ClearConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.BufferWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private static void RemoveLastCard()
        {
            ClearWaitingFields();
            _cards.RemoveAt(_cards.Count - 1);
            Console.WriteLine("REMLAST");
            ShowNumOfPlayers();
        }

        private static void ClearCards()
        {
            ClearWaitingFields();
            _cards.Clear();
            Console.WriteLine("CLEAR");
            ShowNumOfPlayers();
        }

        private static void ClearWaitingFields()
        {
            _waitingColor = null;
            _waitingCardType = null;
        }

        private static void PrintCard(Card card)
        {
            var strcard = card.ToString();
            Console.Write(" ");
            var def = Console.ForegroundColor;
            int ind = 1;
            Console.Write(strcard[0]);
            if (card.CardType == CardType.C10)
            {
                Console.Write(strcard[1]);
                ind = 2;
            }
            if (card.CardColor == CardColor.Hearts || card.CardColor == CardColor.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(strcard[ind]);
            Console.Write(" ");
            Console.ForegroundColor = def;
        }

        private static int? GetNumOfPlayers(ConsoleKeyInfo key)
        {
            if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
            {
                return (int)key.Key - (int)ConsoleKey.F1 + 1;
            }
            return null;
        }

        private static CardType? CheckCard(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    return CardType.A;
                case ConsoleKey.D2:
                    return CardType.C2;
                case ConsoleKey.D3:
                    return CardType.C3;
                case ConsoleKey.D4:
                    return CardType.C4;
                case ConsoleKey.D5:
                    return CardType.C5;
                case ConsoleKey.D6:
                    return CardType.C6;
                case ConsoleKey.D7:
                    return CardType.C7;
                case ConsoleKey.D8:
                    return CardType.C8;
                case ConsoleKey.D9:
                    return CardType.C9;
                case ConsoleKey.D0:
                    return CardType.C10;
                case ConsoleKey.Backspace:
                    return CardType.K;
            }

            if (key.KeyChar == '=')
                return CardType.Q;

            if (key.KeyChar == '-')
                return CardType.J;

            return null;
        }

        private static CardColor? CheckColor(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Q:
                    return CardColor.Spades;
                case ConsoleKey.W:
                    return CardColor.Clubs;
                case ConsoleKey.E:
                    return CardColor.Hearts;
                case ConsoleKey.R:
                    return CardColor.Diamonds;
            }

            return null;
        }
    }
}

