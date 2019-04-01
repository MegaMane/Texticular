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

namespace Texticular.Commands.Tests
{
    [TestClass()]
    public class GameTests
    {


        [TestMethod()]
        public void TakeItemTest()
        {
            Assert.IsTrue(GameObject.Objects.Count == 0);

            Room Subway = new Room("Subway", "It smells like piss and feels like you might get stabbed. Oh the wonders of public transport...", "subway");
            Player testPlayer = new Player(playerName: "Jonny Rotten", description: "A strapping young lad with a rotten disposition.", playerLocation: Subway);

            var testItem = new StoryItem
            (
                name: "Pack of Cigarettes",
                description: "A half smoked pack of cigarettes. You don't recognize the brand.",
                keyValue: "cigs"
            );

            var locker = new Chest("subway", "Public Locker", "a public locker, it costs 1 dollar for one time use.", "subwayLocker", 5);

            Subway.AddItem(testItem);
            Subway.AddItem(locker);

            //still need to determine what type of interface and command to build

            ITakeable target;

            target = testItem as ITakeable;

            if (target != null)
            {
                TakeCommand tCommand = new TakeCommand(target);
                tCommand.Execute();
                Console.WriteLine(GameLog.DisplayResponse());
            }
            else
            {
                Console.WriteLine("You can't take the locker");
            }
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