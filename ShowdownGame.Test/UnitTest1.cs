using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ShowdownGame.Test
{
    [TestClass]
    public class UnitTest1
    { 
        List<PokerPlayer> Players = new List<PokerPlayer>();

        [TestInitialize]
        public void TestInitialize()
        {
            Players.Clear();
        }

        [TestMethod]
        public void TestExampleScenario1()
        {
            //Arrange
            string input = @"../../../AppData/example1Data.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsFalse(testWinners.Count > 1);
            Assert.AreEqual("Bob", testWinners[0].Name);
        }

        [TestMethod]
        public void TestExampleScenario2()
        {
            //Arrange
            string input = @"../../../AppData/example2Data.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsFalse(testWinners.Count > 1);
            Assert.AreEqual("Bob", testWinners[0].Name);
        }

        [TestMethod]
        public void TestExampleScenario3()
        {
            //Arrange
            string input = @"../../../AppData/example3Data.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsFalse(testWinners.Count > 1);
            Assert.AreEqual("Jen", testWinners[0].Name);
        }

        [TestMethod]
        public void TestExampleScenario4()
        {
            //Arrange
            string input = @"../../../AppData/example4Data.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsFalse(testWinners.Count > 1);
            Assert.AreEqual("Jen", testWinners[0].Name);
        }

        [TestMethod]
        public void TestFlush()
        {
            //Arrange
            var currentHand = "2K, 3K, 4K, 5K, 6K";

            // Act
            var handRank = GetHandRank("FlushTest", currentHand);

            //Assert
            Assert.AreEqual("FLUSH", handRank.CurrentHandRank.ToString());
        }

        [TestMethod]
        public void TestThreeOfKind()
        {
            //Arrange
            var currentHand = "2H, 2C, 2D, 5S, 6S";

            // Act
            var handRank = GetHandRank("TestThreeOfKind", currentHand);

            //Assert
            Assert.AreEqual("THREE_OF_A_KIND", handRank.CurrentHandRank.ToString());
        }

        [TestMethod]
        public void TestOnePair()
        {
            //Arrange
            var currentHand = "2H, 2C, 3D, 5S, 6S";

            // Act
            var handRank = GetHandRank("TestThreeOfKind", currentHand);

            //Assert
            Assert.AreEqual("ONE_PAIR", handRank.CurrentHandRank.ToString());
        }

        [TestMethod]
        public void TestHighCard()
        {
            //Arrange
            var currentHand = "2H, 3D, 4C, 5S, 6S";

            // Act
            var handRank = GetHandRank("TestThreeOfKind", currentHand);

            //Assert
            Assert.AreEqual("HIGH_CARD", handRank.CurrentHandRank.ToString());
        }

        [TestMethod]
        public void TestHighCardValue()
        {
            //Arrange
            var currentHand = "2H, 3D, 8C, JS, AS";

            //Act
            var handRank = GetHandRank("TestThreeOfKind", currentHand);

            //Assert
            Assert.AreEqual("14", handRank.HighCardValue.ToString());
        }

        [TestMethod]
        public void TestSplitThePot()
        {

            //Arrange
            string input = @"../../../AppData/splitThePotData.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsTrue(testWinners.Count > 1);
        }

        public List<PokerPlayer> DetermineWinnerFromFile (string input)
        {
            List<PokerPlayer> testPlayers = Program.CreatePlayers(input);
            return Program.DetermineWinners(testPlayers);
        }

        public HandRankEvaluator GetHandRank(string name, string hand)
        {
            PokerPlayer player = new PokerPlayer(name, hand);
            return new HandRankEvaluator(player);
        }

    }
}
