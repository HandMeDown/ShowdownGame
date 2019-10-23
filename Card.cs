using System;
using System.Collections.Generic;
using System.Text;

namespace ShowdownGame
{

    class Card
    {
        public int Value;
        public int Suit;
        public Card(string value, string suit)
        {
            Value = MapNumber(value);
            Suit = MapSuit(suit);
        }

        private int MapSuit(string suit)
        {
            throw new NotImplementedException();
        }

        private int MapNumber(string value)
        {
            throw new NotImplementedException();
        }
    }
}
