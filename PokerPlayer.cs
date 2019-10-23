using ShowdownGame.Enums;
using System;
using System.Collections.Generic;

namespace ShowdownGame
{
    public class PokerPlayer
    {
        public string Name;
        private string[] Cards;
        private HandRank Rank;
        private List<Card> Hand;

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
                var newCard = new Card(item.Substring(0, suitIndex), item.Substring(suitIndex));
                Hand.Add(newCard);
            }

           // this.Rank = new HandRankEvaluator.calculateRank(Hand);
        }


    }
}