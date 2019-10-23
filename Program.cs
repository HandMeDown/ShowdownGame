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
            //var winners = new List<PokerPlayer>();

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

                // Console.WriteLine(name + " " + line);

                //create new PokerPlayer object
                var newPlayer = new PokerPlayer(name, line);

                //add newPlayer to list of gamePlayers
                gamePlayers.Add(newPlayer);

                name = null;
                gotName = false;
            }
            //gamePlayers.ForEach(i => Console.Write("{0}\t {1}\t", i.Name, i.Cards));
            DetermineWinners(gamePlayers);
            Console.ReadLine();
        }

        private static List<PokerPlayer> DetermineWinners(List<PokerPlayer> gamePlayers)
        {
            HandRankEvaluator winningHand = null;
            List<PokerPlayer> winningPlayers = new List<PokerPlayer>();

            foreach (var currentPlayer in gamePlayers)
            {
                var currentPlayerHand = new HandRankEvaluator(currentPlayer.Hand);

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

                        //KickerLogic(currentPlayerHand, winningHand);

                        int count = 0;

                        for (int i = 4; i >= 0; i--)
                        {
                            if (currentPlayerHand.SortedHand[i].Value > winningHand.SortedHand[i].Value)
                            {
                                winningHand = currentPlayerHand;
                                winningPlayers.Add(currentPlayer);
                            }
                            else if (currentPlayerHand.SortedHand[i].Value == winningHand.SortedHand[i].Value)
                            {
                                count++;
                            }
                        }

                        if (count == 5)
                        {
                            //tie - add currentPlayer to Winners collection
                            winningPlayers.Add(currentPlayer);
                        }
                    }
                }
            }
            return winningPlayers;
        }
    }
}
