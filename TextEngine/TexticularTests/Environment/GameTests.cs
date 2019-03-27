using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Engine;

namespace Texticular.Environment.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GameLogTest()
        {
            //Arrange


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

        }

        [TestMethod()]
        public void ContainerTest()
        {

        }

        [TestMethod()]
        public void InventoryTest()
        {

        }


        [TestMethod()]
        public void TakeItemTest()
        {
            //Player
            //Inventory
            //Room[item]
            //Room2 { no item}
            //Container
        }

        [TestMethod()]
        public void DropItemTest()
        {

        }

        [TestMethod()]
        public void PutItemTest()
        {
            //put in container
            //put in inventory

        }




    }
}