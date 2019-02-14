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