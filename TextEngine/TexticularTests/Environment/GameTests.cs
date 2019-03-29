using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticular.Environment.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GameLogTest()
        {
            //Arrange

            string firstName = "Jon";
            string lastName = "";

            Console.WriteLine(GameLog.FirstCharToUpper(firstName) + " " + GameLog.FirstCharToUpper(lastName));

            //Act
            GameLog.UserInput = ("Some Input From the User");
            GameLog.Append("Some Text\n");
            GameLog.Append("Some More Text");

            Console.WriteLine("Displaying Game Response");
            Console.Write(GameLog.DisplayResponse());

            Console.Write("\n------------------------------------------------------------\n");
            Console.WriteLine("Displaying Game Log");
            foreach(var line in GameLog.ViewLog())
            {
                Console.WriteLine(line);
            }

            Console.Write("\n-----------------------END----------------------------------\n");

            GameLog.UserInput = ("Some Input on their second turn");
            GameLog.Append("Round Two Text\n");
            GameLog.Append("Some More Round Two Text");

            Console.WriteLine("Displaying Game Response");
            Console.Write(GameLog.DisplayResponse());

            Console.Write("\n------------------------------------------------------------\n");
            Console.WriteLine("Displaying Game Log");
            foreach (var line in GameLog.ViewLog())
            {
                Console.WriteLine(line);
            }

            Console.Write("\n-----------------------END----------------------------------\n");

            //Assert
            Assert.AreEqual(GameLog.InputResponse.ToString(),"");

        }

        [TestMethod()]
        public void RoomTest()
        {
            //Arrange

            Room MyOffice = new Room("Positive Tech Office", "A hot stuffy little office that smells faintly of farts.", "Otis");
            StoryItem CarnageMug = new StoryItem
                (name: "Carnage Mug",
                description: "A red ceramic mug that looks like the marvel villain Carnage",
                keyValue: "carnageMug",
                contextualDescription: "There is a red mug that looks like a Carnage head sitting on one of the desks");
            MyOffice.AddItem(CarnageMug);

            //Act
            Console.WriteLine("Looking at room...");
            MyOffice.Look();
            Console.Write(GameLog.DisplayResponse());

            Console.Write("\n-------------------Objects Created----------------------------------------\n\n");
            foreach (KeyValuePair<string, GameObject> Item in GameObject.Objects)
            {
                Console.WriteLine($"DictKey: {Item.Key}: {Item.Value}");
            }
        }

        [TestMethod()]
        public void PlayerTest()
        {
            Room playerStartingLocation = new Room("Positive Tech Office", "A hot stuffy little office that smells faintly of farts.", "Otis");
            Player testPlayer = new Player(playerName: "Jonny Rotten", description: "A strapping young lad with a rotten disposition.", playerLocation: playerStartingLocation);
            Console.Write(testPlayer.ToString());
        }

        [TestMethod()]
        public void ContainerTest()
        {
            //test unlocking the container
            //test adding and removing items

            //Arrange
            Room TestRoom = new Room("Bedroom", "A room with little action figures stacked up to the ceiling. A nerd must live here.", "masterBedroom");
            Player testPlayer = new Player(playerName: "Jonny Rotten", description: "A strapping young lad with a rotten disposition.", playerLocation: TestRoom);
            Key SafeCode = new Key("Sentry Safe Code", "A post it note with the code to the safe: 89054", "masterBedroom", consumeText: "You enter the safecode.\n");
            Chest chest = new Chest("masterBedroom", "Sentry Safe", "a digital safe for keeping things tucked away", "sentrySafe", 5);
            testPlayer.BackPack.AddItem(SafeCode);

            chest.Key = SafeCode;
            chest.IsLocked = true;

            //Act
            chest.Open();

            Console.Write(GameLog.DisplayResponse());
            chest.Unlock(chest.Key);
            Console.Write(GameLog.DisplayResponse());

            chest.Open();

            chest.AddItem( new StoryItem
            (
                name: "Carnage Mug",
                description: "A red ceramic mug that looks like the marvel villain Carnage",
                keyValue: "carnageMug",
                contextualDescription: "There is a red mug that looks like a Carnage head tucked in the corner",
                slotsOccupied: 4
            ));

            chest.AddItem(new StoryItem
            (
                name: "Stick Of Gum",
                description: "Your last piece of juicy fruit",
                keyValue: "gumStick"
            ));

            chest.AddItem(new StoryItem
            (
                name: "Vape Pen",
                description: "It's caked in resin and cotton candy flavor!",
                keyValue: "vapePen"
            ));

            chest.Close();

            Console.WriteLine(chest.ToString());

            chest.Open();
            chest.RemoveItem("gumStick");
            chest.AddItem((GameObject.Objects["vapePen"] as StoryItem));
            Console.WriteLine(chest.ToString());


            //Assert
            Assert.AreEqual(chest.Items.Count, 2);
            
        }

        [TestMethod()]
        public void InventoryTest()
        {

        }



        [TestMethod()]
        public void DoorTest()
        {
            

        }

        [TestMethod()]
        public void CointTest()
        {


        }

        [TestMethod()]
        public void TVTest()
        {


        }

        [TestMethod()]
        public void VendingMachineTest()
        {


        }


    }
}