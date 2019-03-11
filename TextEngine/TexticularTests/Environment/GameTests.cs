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
        [TestMethod()]
        public void VendingMachineTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            VendingMachine testMachine =
                new VendingMachine
                (
                    name: "west",
                    description: "The vending machine is in stark contrast to the rest of your well worn surroundings."
                                 + "It's shiny and new and stocked to the brim with Fast Eddie's colon cleanse and a few other odds and ends."
                                 + "A marquee taunts you in large bold letters \" Fast Eddie's: When in doubt, flush it out!\"",
                    locationKey: "westHallway",
                    keyValue: "westVendingMachine",
                    timeVisited: 0
                 );

            /*
             * 
             * -----------------------------
                Vending Machine
                -----------------------------

                A1: Fast Eddies Special Edition      $0.75 
                A2: Hungry Muncher Trail Mix         $1.25
                A3: Gently Used Underwear            $2.75
                A4: Fast Eddies: Special Edition     $0.75 
                A5: Fast Eddies: Special Edition     $0.75 
                A6: Fast Eddies: Special Edition     $0.75 

                1. Make a Selection
                2. Put Money In
                3. Nevermind


                >>
            */

            //Assert
            Assert.AreEqual(1, 1);


        }





        /*

        //need to implement
        [TestMethod()]
        public void RoomTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            Room myRoom = new Room("Room 204", "A shabby hotel room with a red and orange couch","romm204", 0);



            //Act
            Console.WriteLine(myRoom.Description);
            Controller.Render();
            GameController.InputResponse.Clear();



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

            Room myRoom = new Room("Room 204", "A shabby hotel room with a red and orange couch", keyValue:"room204", timeVisited:0);
            Room destination = new Room("Room 203", "Another shabby hotel room with a red and orange couch", keyValue: "room203", timeVisited: 0);
            Exit testTExit = new Exit(locationKey: "room204", destinationKey: "room203", isLocked: true, keyName: "Hotel Room Key", name: "The door to room 203", description: "A white painted door with caution tape and a do not enter sign taped to it");

            myRoom.Exits["North"] = testTExit;

            DoorKey testKey = new DoorKey(locationKey: "inventory", name: "Hotel Room Key", description: "Hotel Room Key", examineResponse: "A key card that opens room 203...");

            ActiveGame.Rooms["room204"] = myRoom;
            ActiveGame.Rooms["room203"] = destination;
            ActiveGame.Items.Add(testKey.KeyValue,testKey);
            




            //Act
            GameController.InputResponse.Append("\nTest: Player is not in the correct location\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            Controller.Game.Player.PlayerLocation = destination;
            testTExit.Commands["open"](Controller);



            GameController.InputResponse.Append("\nTest: Player is in the correct location but does not have the key.\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            Controller.Game.Player.PlayerLocation = myRoom;
            testTExit.Commands["open"](Controller);

            GameController.InputResponse.Append("\nTest: Player is in the correct location and has the key.\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            Controller.ItemsinInventory.Add(testKey);
            testTExit.Commands["open"](Controller);




            //Assert
            Assert.AreEqual(Controller.Game.Player.PlayerLocation, destination);

        }

        //need to implement
        [TestMethod()]
        public void DoorKeyTest()
        {
            //Arrange
            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);

            Room testRoom = new Room("Room 204", "A shabby hotel room with a red and orange couch", keyValue: "room204", timeVisited: 0);
            Room destination = new Room("Room 203", "Another shabby hotel room with a red and orange couch", keyValue: "room203", timeVisited: 0);
            Room anotherRoom = new Room("Unreachable Room", "A room the player can't reach", keyValue: "unreachableRoom", timeVisited: 0);

            Exit testTExit = new Exit(locationKey: "room204", destinationKey: "room203", isLocked: true, keyName: "Hotel Room Key", name: "The door to room 203", description: "A white painted door with caution tape and a do not enter sign taped to it");

            testRoom.Exits["North"] = testTExit;

            DoorKey testKey = new DoorKey(locationKey: "inventory", name: "Hotel Room Key", description: "Hotel Room Key", examineResponse: "A key card that opens room 203...");
            DoorKey anotherKey = new DoorKey(locationKey: "unreachableRoom", name: "Hidden Key", description: "A Hidden Key", examineResponse: "A key that can't be used...");

            ActiveGame.Rooms["room204"] = testRoom;
            ActiveGame.Rooms["room203"] = destination;
            ActiveGame.Rooms["unreachableRoom"] = anotherRoom;
            ActiveGame.Items.Add(testKey.KeyValue,testKey);
            ActiveGame.Items.Add(anotherKey.KeyValue,anotherKey);

            //start the player in the wrong location
            Controller.Game.Player.PlayerLocation = anotherRoom;


            //Act
            GameController.InputResponse.Append("\nTest: The Key does not open any doors in the current location\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            
            testKey.Commands["use"](Controller);

            
            GameController.InputResponse.Append("\nTest: Player Needs to be holding the key to use it.\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            //place the player in the correct room but remove the key from inventory and place it in the room as well
            Controller.Game.Player.PlayerLocation = testRoom;
            testKey.LocationKey = testRoom.KeyValue;
            testKey.Commands["use"](Controller);



            GameController.InputResponse.Append("\nTest: Player does not have the key.\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            //try to use a key that exists in the list of game objects 
            //but is not in the current room or in the players inventory
            Controller.Game.Player.PlayerLocation = testRoom;
            anotherKey.Commands["use"](Controller);

            GameController.InputResponse.Append("\nTest: Player has the key and is in the correct location.\n");
            Controller.Render();
            GameController.InputResponse.Clear();

            testKey.LocationKey = "inventory";
            Controller.ItemsinInventory.Add(testKey);
            testKey.Commands["use"](Controller);
            Controller.Render();




            //Assert
            Assert.AreEqual(Controller.Game.Player.PlayerLocation, destination);

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

            Controller.Render();
            GameController.InputResponse.Clear();

            testTv.Commands["change channel"](Controller);

            Controller.Render();
            GameController.InputResponse.Clear();

            testTv.Commands["turn off"](Controller);

            Controller.Render();
            GameController.InputResponse.Clear();

            //Assert
            Assert.AreEqual(1, 1);

        }



    */

        }


    }