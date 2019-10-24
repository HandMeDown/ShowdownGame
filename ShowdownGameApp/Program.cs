using System;
using System.Collections.Generic;
using System.IO;

namespace ShowdownGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = @"../../../AppData/gameData.txt";
            List<PokerPlayer> gamePlayers = CreatePlayers(input);

            List<PokerPlayer> gameWinners = DetermineWinners(gamePlayers);

            foreach (PokerPlayer player in gamePlayers)
            {
                Console.WriteLine(player.Name);
                foreach (string card in player.Cards)
                {
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

        public static List<PokerPlayer> CreatePlayers(string inputFilePath)
        {
            var gamePlayers = new List<PokerPlayer>();

            string name = null;
            bool gotName = false;
            //read player data from file
            foreach (string line in File.ReadLines(inputFilePath))
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

            return gamePlayers;
        }

        public static List<PokerPlayer> DetermineWinners(List<PokerPlayer> gamePlayers)
        {
            HandRankEvaluator winningHand = null;
            List<PokerPlayer> winningPlayers = new List<PokerPlayer>();

            foreach (var currentPlayer in gamePlayers)
            {
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
                        List<PokerPlayer> KickerWinners = KickerLogic(currentPlayerHand, winningHand);

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
                            List<PokerPlayer> KickerWinners = KickerLogic(currentPlayerHand, winningHand);

                            //loop through KickerWinners adding to winningPlayers
                            foreach(PokerPlayer winner in KickerWinners)
                            {
                                winningPlayers.Add(winner);
                            }
                        }

                    }

                    // if one pair
                    if (currentPlayerHand.CurrentHandRank == Enums.HandRank.ONE_PAIR)
                    {
                        if (currentPlayerHand.OnePairValue > winningHand.OnePairValue)
                        {
                            winningPlayers.Clear();
                            winningPlayers.Add(currentPlayer);
                        }
                        //if value is same
                        else if (currentPlayerHand.OnePairValue == winningHand.OnePairValue)
                        {
                            //must clear current winners as we are using a null check above
                            winningPlayers.Clear();
                            List<PokerPlayer> KickerWinners = KickerLogic(currentPlayerHand, winningHand);

                            //loop through KickerWinners adding to winningPlayers
                            foreach (PokerPlayer winner in KickerWinners)
                            {
                                winningPlayers.Add(winner);
                            }
                        }
                    }

                    //if high card
                    if (currentPlayerHand.CurrentHandRank == Enums.HandRank.HIGH_CARD)
                    {
                        if(currentPlayerHand.HighCardValue > winningHand.HighCardValue)
                        {
                            winningHand = currentPlayerHand;
                            winningPlayers.Clear();
                            winningPlayers.Add(currentPlayer);
                        }
                        else if (currentPlayerHand.HighCardValue == winningHand.HighCardValue)
                        {
                            //must clear current winners as we are using a null check above
                            winningPlayers.Clear();
                            List<PokerPlayer> KickerWinners = KickerLogic(currentPlayerHand, winningHand);

                            //loop through KickerWinners adding to winningPlayers
                            foreach (PokerPlayer winner in KickerWinners)
                            {
                                winningPlayers.Add(winner);
                            }
                        }
                        else
                        {
                            winningPlayers.Clear();
                            winningPlayers.Add(winningHand.Player);
                        }
                    }
                }
            }
            return winningPlayers;
        }

        private static List<PokerPlayer> KickerLogic(HandRankEvaluator currentPlayerHand, HandRankEvaluator winningHand)
        {

            List<PokerPlayer> kickerWinners = new List<PokerPlayer>();

            int count = 0;
            //Check the hand in reverse order
            for (int i = winningHand.CardsNotInPlay.Count - 1; i >= 0; i--)
            {
                if (winningHand.CardsNotInPlay[i].Value < currentPlayerHand.CardsNotInPlay[i].Value)
                {
                    kickerWinners.Add(currentPlayerHand.Player);
                    break;
                }
                else if (winningHand.CardsNotInPlay[i].Value == currentPlayerHand.CardsNotInPlay[i].Value)
                {
                    count++;
                }
                
                if (count == winningHand.CardsNotInPlay.Count)
                {

                    
                    kickerWinners.Add(currentPlayerHand.Player);
                    kickerWinners.Add(winningHand.Player);
                }
            }

            return kickerWinners;

        }
    }
}
