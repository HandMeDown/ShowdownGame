using System;
using System.Collections.Generic;
using System.IO;

namespace ShowdownGame
{
    class Program
    {
        static void Main(string[] args)
        {

            var gamePlayers = new List<PokerPlayer>();

            string name = null;
            bool gotName = false;
            //read player data from file
            foreach (string line in File.ReadLines(@"../../../AppData/gameData.txt"))
            {

                //create new player, grabbing name first followed by cards
                if (!gotName)
                {
                    name = line;
                    gotName = true;
                    continue;
                }

                //create new PokerPlayer object
                var newPlayer = new PokerPlayer(name, line);

                //add newPlayer to list of gamePlayers
                gamePlayers.Add(newPlayer);

                name = null;
                gotName = false;
            }

            List<PokerPlayer> gameWinners = DetermineWinners(gamePlayers);

            foreach (PokerPlayer player in gamePlayers)
            {
                Console.WriteLine(player.Name);
                foreach (string card in player.Cards)
                {
                    //Console.Write("{0}{1}" + ",", card.Value.ToString(), card.Suit.ToString());
                    Console.Write(card);
                }
                Console.WriteLine();
            }
            if (gameWinners.Count > 1)
            {
                Console.WriteLine("Game tied.  Split the pot.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(gameWinners[0].Name + " wins");
            }
            Console.ReadLine();
        }

        private static List<PokerPlayer> DetermineWinners(List<PokerPlayer> gamePlayers)
        {
            HandRankEvaluator winningHand = null;
            List<PokerPlayer> winningPlayers = new List<PokerPlayer>();

            foreach (var currentPlayer in gamePlayers)
            {
                Console.WriteLine(currentPlayer.Name);
                var currentPlayerHand = new HandRankEvaluator(currentPlayer);

                //if hands out-rank each other
                if (winningHand == null || currentPlayerHand.CurrentHandRank > winningHand.CurrentHandRank)
                {
                    winningHand = currentPlayerHand;
                    winningPlayers.Clear();
                    winningPlayers.Add(currentPlayer);
                }
                //in case of tie
                else if (currentPlayerHand.CurrentHandRank == winningHand.CurrentHandRank)
                {
                    // if flush
                    if (currentPlayerHand.CurrentHandRank == Enums.HandRank.FLUSH)
                    {
                        //must clear current winners as we are using a null check above
                        winningPlayers.Clear();
                        List<PokerPlayer> KickerWinners = KickerLogic(currentPlayer, currentPlayerHand, winningHand);

                        //loop through KickerWinners adding to winningPlayers
                        foreach(PokerPlayer winner in KickerWinners)
                        {
                            winningPlayers.Add(winner);
                        }
                    }

                    // if Three of A Kind
                    if (currentPlayerHand.CurrentHandRank == Enums.HandRank.THREE_OF_A_KIND)
                    {
                        //compare value of 3 of kind set
                        if(currentPlayerHand.ThreeOfKindValue > winningHand.ThreeOfKindValue)
                        {
                            winningPlayers.Clear();
                            winningPlayers.Add(currentPlayer);
                        }
                        //if value is same
                        else
                        {
                            //must clear current winners as we are using a null check above
                            winningPlayers.Clear();
                            List<PokerPlayer> KickerWinners = KickerLogic(currentPlayer, currentPlayerHand, winningHand);

                            //loop through KickerWinners adding to winningPlayers
                            foreach(PokerPlayer winner in KickerWinners)
                            {
                                winningPlayers.Add(winner);
                            }
                        }

                    }
                }
            }
            return winningPlayers;
        }

        private static List<PokerPlayer> KickerLogic(PokerPlayer currentPlayer, HandRankEvaluator currentPlayerHand, HandRankEvaluator winningHand)
        {
            List<PokerPlayer> kickerWinners = new List<PokerPlayer>();

            int count = 0;

            int noOfCards = winningHand.CardsNotInPlay.Count;

            //loop for length of remaining cards

            for (int i = noOfCards - 1; i >= 0; i--)
            {
                if (winningHand.CardsNotInPlay[noOfCards - 1].Value > currentPlayerHand.CardsNotInPlay[i].Value)
                {
                    kickerWinners.Clear();
                    kickerWinners.Add(winningHand.Player);
                }
                else if (winningHand.CardsNotInPlay[noOfCards - 1].Value == currentPlayerHand.CardsNotInPlay[i].Value)
                {
                    count++;
                }

            }

            if (count == 5)
            {
                //tie - add currentPlayer to Winners collection
                kickerWinners.Add(currentPlayer);
            }

            return kickerWinners;
        }
    }
}
