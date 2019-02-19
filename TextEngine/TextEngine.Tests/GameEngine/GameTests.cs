using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Texticular.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void LoadGameObjectsTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            
            //Act
            foreach(KeyValuePair<string, Room> gameRoom in ActiveGame.Rooms)
            {
                Console.Write(gameRoom.ToString());
            }
            //Assert
            Assert.Fail();
        }
    }
}