using System;
using System.Collections.Generic;
using System.Text;

namespace ShowdownGame
{
    public class Card
    {
        public int Value;
        public SUIT Suit;
        public enum SUIT
        {
            HEARTS,
            SPADES,
            DIAMONDS,
            CLUBS
        }
        public Card(string value, string suit)
        {
            Value = MapNumber(value);
            Suit = MapSuit(suit);
        }

        private SUIT MapSuit(string suit)
        {
            switch (suit)
            {
                case ("H"): return SUIT.HEARTS;
                case ("S"): return SUIT.SPADES;
                case ("D"): return SUIT.DIAMONDS;
                case ("C"): return SUIT.CLUBS;
                default:
                    {
                        //TODO: see if I can return something more meaningful here
                        return SUIT.HEARTS;
                    }

            }
        }

        private int MapNumber(string value)
        {
            switch (value)
            {
                case ("J"): return 11;
                case ("Q"): return 12;
                case ("K"): return 13;
                case ("A"): return 14;
                default:
                    {
                        //TODO: catch error if not able to convert
                        return (int.Parse(value));
                    }
            }
        }
    }
}
