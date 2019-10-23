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

               // Console.WriteLine(name + " " + line);

                //create new PokerPlayer object
                var newPlayer = new PokerPlayer(name, line);

                //add newPlayer to list of gamePlayers
                gamePlayers.Add(newPlayer);

                name = null;
                gotName = false;
;                    
            }
            //gamePlayers.ForEach(i => Console.Write("{0}\t {1}\t", i.Name, i.Cards));
            //foreach (var currentPlayer in gamePlayers)
            //{
            //    var currentHand = currentPlayer.EvaluateHand();
            //}
            Console.ReadLine();
        }
    }
}
