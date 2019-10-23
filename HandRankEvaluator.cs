using ShowdownGame.Enums;
using System;
using System.Collections.Generic;

namespace ShowdownGame
{
    public class HandRankEvaluator
    {

        private int totalHearts;
        private int totalSpades;
        private int totalDiamonds;
        private int totalClubs;

        public HandRank CurrentHandRank;

        public HandRankEvaluator(List<Card> Hand)
        {
            CurrentHandRank = CalculateRank(Hand);
        }

        public HandRank CalculateRank(List<Card> hand)
        {
            if (IsFlush(hand))
            {
                return HandRank.FLUSH;
            }
            //TODO: determine what exactly to return here
            else return HandRank.HIGH_CARD;
        }

        private bool IsFlush(List<Card> hand)
        {
            //count suits, if any come back == 5 player has a Flush
            CountSuits(hand);

            return totalHearts == 5 || totalSpades == 5 || totalDiamonds == 5 || totalClubs == 5;
        }

        private void CountSuits(List<Card> hand)
        {
            totalHearts = 0;
            totalSpades = 0;
            totalDiamonds = 0;
            totalClubs = 0;

            foreach (var card in hand)
            {
                if (card.Suit == Card.SUIT.HEARTS)
                    totalHearts++;
                else if (card.Suit == Card.SUIT.SPADES)
                    totalSpades++;
                else if (card.Suit == Card.SUIT.DIAMONDS)
                    totalDiamonds++;
                else if (card.Suit == Card.SUIT.CLUBS)
                    totalClubs++;
            }
        }
    }
}