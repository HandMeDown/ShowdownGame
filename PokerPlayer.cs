using ShowdownGame.Enums;
using System;
using System.Collections.Generic;

namespace ShowdownGame
{
    public class PokerPlayer
    {
        public string Name;
        public string[] Cards;
        public List<Card> Hand = new List<Card>();

        public PokerPlayer(string name, string cards)
        {
            this.Name = name; 
            this.Cards = cards.Split(',');

            //convert cards string array to Card object
            for(var i = 0; i < Cards.Length; i++)
            {
                //remove any potential whitespace
                var item = Cards[i].Trim();

                //suit index should be last character of string
                var suitIndex = item.Length - 1;

                //convert to Card object and add to Hand
                var newCard = new Card(item.Substring(0, suitIndex).ToUpper(), item.Substring(suitIndex));
                Hand.Add(newCard);
            }

        }


    }
}