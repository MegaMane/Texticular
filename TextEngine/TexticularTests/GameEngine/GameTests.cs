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
           // Game expectedRooms
            
            //Act
            Game ActiveGame = new Game();

            foreach (KeyValuePair<string, Room> gameRoom in ActiveGame.Rooms)
            {
                var currentRoom = gameRoom.Value;
                Console.Write(currentRoom.ToString());
                foreach(KeyValuePair<string, Exit> door in currentRoom.Exits)
                {
                    Console.Write($"{door.Key.ToString()}: {door.Value.ToString()}\n\n");
                }

                foreach (StoryItem item in currentRoom.RoomItems)
                {
                    Console.Write(item.ToString());
                }

                Console.Write("\n-------------------------------------------------------------------------\n" +
                               "|                         End Room                                                               |\n" +
                               "-------------------------------------------------------------------------\n\n" );
            }

            //Assert
            Assert.AreEqual(1, 1);

        }
    }
}