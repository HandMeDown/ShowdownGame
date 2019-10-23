using ShowdownGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowdownGame
{
    public class HandRankEvaluator
    {

        private int totalHearts;
        private int totalSpades;
        private int totalDiamonds;
        private int totalClubs;

        public HandRank CurrentHandRank;
        public int HighCardValue;
        public List<Card> SortedHand;

        public HandRankEvaluator(List<Card> hand)
        {
            SortedHand = HandSortedByRank(hand);
            CurrentHandRank = CalculateRank(SortedHand);
            
            //if HIGH_CARD get value
            if (CurrentHandRank == HandRank.HIGH_CARD)
            {
                HighCardValue = SortedHand[4].Value;
            }
        }


        public HandRank CalculateRank(List<Card> hand)
        {

            if (IsFlush(hand))
            {
                return HandRank.FLUSH;
            }
            else if (IsThreeOfAKind(hand))
            {
                return HandRank.THREE_OF_A_KIND;
            }
            else if (IsOnePair(hand))
            {
                return HandRank.ONE_PAIR;
            }
            //TODO: determine what exactly to return here
            else return HandRank.HIGH_CARD;
        }

        public List<Card> HandSortedByRank(List<Card> hand)
        {
            List<Card> sortedHand = hand.OrderBy(c => c.Value).ToList();
            return sortedHand;
        }

        private bool IsFlush(List<Card> hand)
        {
            //count suits, if any come back == 5 player has a Flush
            CountSuits(hand);

            //TODO = setHighestCard

            return totalHearts == 5 || totalSpades == 5 || totalDiamonds == 5 || totalClubs == 5;
        }

        private bool IsThreeOfAKind(List<Card> hand)
        {
            //check for 2 2 2 3 4
            bool beginningMatch = SortedHand[0].Value == SortedHand[1].Value &&
                SortedHand[1].Value == SortedHand[2].Value;

            //check for 2 3 3 3 4
            bool middleMatch = SortedHand[1].Value == SortedHand[2].Value &&
                SortedHand[2].Value == SortedHand[3].Value;

            //check for 2 3 4 4 4 nb.doesn't account for full house, out of scope
            bool endMatch = SortedHand[2].Value == SortedHand[3].Value &&
                SortedHand[3].Value == SortedHand[4].Value;

            return beginningMatch || middleMatch || endMatch;
        }

        private bool IsOnePair(List<Card> hand)
        {
            // check 2 2 3 4 5
            bool aaxyz = SortedHand[0].Value == SortedHand[1].Value;

            //check 2 3 3 4 5
            bool xaayz = SortedHand[1].Value == SortedHand[2].Value;

            //check 2 3 4 4 5
            bool xyaaz = SortedHand[2].Value == SortedHand[3].Value;

            // 2 3 4 5 5
            bool xyzaa = SortedHand[3].Value == SortedHand[4].Value;

            return aaxyz || xaayz || xyaaz || xyzaa;
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