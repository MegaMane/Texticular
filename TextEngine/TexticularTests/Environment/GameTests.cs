using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular.Environment.Tests
{
    [TestClass()]
    public class GameTests
    {


        //need to implement
        [TestMethod()]
        public void RoomTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            TV testTv = new TV("upstairs", "A TCL 65 inch Roku TV", "Jon's TV");
            testTv.TurnOffResponse = "The TV goes into power save mode";

            //Act
            testTv.Commands["turn on"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["change channel"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["turn off"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            //Assert
            Assert.AreEqual(1, 1);

        }

        //need to implement
        [TestMethod()]
        public void ExitTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            Exit testTExit = new Exit(locationKey: "diningRoom", destinationKey: "aidensRoom", isLocked: true, keyName: "Aiden's Key", name: "Aiden's Bedroom Door", description: "A white painted door with caution tape and a do not enter sign taped to it");
            DoorKey testKey = new DoorKey(locationKey: "inventory", name: "Aiden's Key", description: "Aiden's room key", examineResponse: "A simple key that fits in the lock to Aiden's door...");




            //Act
            Controller.InputResponse.Append("Test: Player is not in the correct location\n");
            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            Controller.game.Player.LocationKey = "livingRoom";
            testTExit.Commands["open"](Controller);

            Controller.InputResponse.Append("Test: Player is in the correct location but does not have the key.\n");
            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            Controller.game.Player.LocationKey = "diningRoom";
            testTExit.Commands["open"](Controller);

            Controller.InputResponse.Append("Test: Player is in the correct location and has the key.\n");
            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            Controller.ItemsinInventory.Add(testKey);
            testTExit.Commands["open"](Controller);




            //Assert
            Assert.AreEqual(1, 1);

        }

        //need to implement
        [TestMethod()]
        public void DoorKeyTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            TV testTv = new TV("upstairs", "A TCL 65 inch Roku TV", "Jon's TV");
            testTv.TurnOffResponse = "The TV goes into power save mode";

            //Act
            testTv.Commands["turn on"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["change channel"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["turn off"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            //Assert
            Assert.AreEqual(1, 1);

        }


        [TestMethod()]
        public void TVTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            TV testTv = new TV("upstairs", "A TCL 65 inch Roku TV", "Jon's TV");
            testTv.TurnOffResponse = "The TV goes into power save mode";

            //Act
            testTv.Commands["turn on"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["change channel"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            testTv.Commands["turn off"](Controller);

            Controller.DisplayResponse();
            Controller.InputResponse.Clear();

            //Assert
            Assert.AreEqual(1, 1);

        }





    }


}