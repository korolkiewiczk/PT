using System;
using System.Collections.Generic;
using PT.Poker.Model;

namespace PTAC
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            bool completed = false;
            CardColor? waitingColor = null;
            CardType? waitingCardType = null;
            int numOfPlayers = 6;
            Console.Write(numOfPlayers + ">");
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
                        waitingColor = null;
                        waitingCardType = null;
                        cards.Clear();
                        Console.WriteLine("CLEAR");
                        Console.Write(numOfPlayers + ">");
                    }

                    if (key.KeyChar == '`' && cards.Count > 0)
                    {
                        waitingColor = null;
                        waitingCardType = null;
                        cards.RemoveAt(cards.Count - 1);
                        Console.WriteLine("REMLAST");
                        Console.Write(numOfPlayers + ">");
                    }

                    int? newNumOfPlayers = GetNumOfPlayers(key);
                    if (newNumOfPlayers != null)
                        numOfPlayers = newNumOfPlayers.Value;

                    CardColor? color = CheckColor(key);
                    CardType? cardType = CheckCard(key);
                    if (color != null)
                    {
                        waitingColor = color;
                        Console.Write(color + " ");
                    }

                    if (cardType != null)
                    {
                        waitingCardType = cardType;
                        Console.Write(cardType + " ");
                    }

                    if (color == null && cardType == null)
                    {
                        waitingColor = null;
                        waitingCardType = null;

                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new String(' ', Console.BufferWidth - 1));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(numOfPlayers + ">");
                    }

                    if (waitingColor != null && waitingCardType != null)
                    {
                        Console.WriteLine();
                        cards.Add(new Card(waitingColor.Value, waitingCardType.Value));
                        //Console.WriteLine(new CardLayout(cards));
                        var defColor = Console.BackgroundColor;
                        for (int i = 0; i < cards.Count; i++)
                        {
                            if (i >= 2 && cards.Count >= 5)
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;

                            PrintCard(cards[i]);
                        }
                        Console.WriteLine();
                        Console.BackgroundColor = defColor;
                        Console.Write(numOfPlayers + ">");

                        waitingColor = null;
                        waitingCardType = null;
                    }
                }
            }
        }

        private static void PrintCard(Card card)
        {
            var strcard = card.ToString();
            Console.Write(" ");
            var def = Console.ForegroundColor;
            Console.Write(strcard[0]);
            if (card.CardColor == CardColor.Hearts || card.CardColor == CardColor.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(strcard[1]);
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
                case ConsoleKey.Subtract:
                    return CardType.J;
                case ConsoleKey.Backspace:
                    return CardType.K;
            }

            if (key.KeyChar == '=')
                return CardType.Q;

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

