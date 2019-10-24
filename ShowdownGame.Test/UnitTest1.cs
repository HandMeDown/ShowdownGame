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
        public void Example1()
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
        public void Example2()
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
        public void Example3()
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
        public void Example4()
        {
            //Arrange
            string input = @"../../../AppData/example4Data.txt";

            //Act
            var testWinners = DetermineWinnerFromFile(input);

            //Assert
            Assert.IsFalse(testWinners.Count > 1);
            Assert.AreEqual("Jen", testWinners[0].Name);
        }

        public List<PokerPlayer> DetermineWinnerFromFile (string input)
        {
            List<PokerPlayer> testPlayers = Program.CreatePlayers(input);
            return Program.DetermineWinners(testPlayers);
        }
    }
}
