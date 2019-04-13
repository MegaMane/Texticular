using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticle.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Actors;

namespace Texticle.Environment.Tests
{
    [TestClass()]
    public class StoryItemTests
    {
        Player Player;
        Room TestRoom;
        Dictionary<string, GameObject> Items;


        [TestMethod()]
        public void CantTakeWhenYouAlreadyHaveItem()
        {

            Player.BackPack.AddItem((StoryItem)Items["1"]);
            var results = (Items["1"] as StoryItem).Take(null);
            Console.WriteLine(results);
            Assert.AreEqual(results, $"You already have the item {Items["1"].FullName}.\n");
        }

        [TestMethod()]
        public void CantTakeItemInDifferentLocation()
        {

            StoryItem item = (Items["1"] as StoryItem);
            Console.WriteLine(item.Take(item));
            Assert.IsTrue(item.LocationKey != Player.BackPack.LocationKey);
        }



        [TestMethod()]
        public void CanTakeItem()
        {

            StoryItem item = (Items["1"] as StoryItem);
            Player.PlayerLocation.AddItem(item);
            Console.WriteLine(item.Take(item));
            Assert.IsTrue(item.LocationKey == Player.BackPack.KeyValue);
        }



        public StoryItemTests()
        {

            TestRoom = new Room("Otis Office", "A hot stuffy little office that smells faintly of farts.", "otis");
            Player = new Player(playerName: "Jonny Rotten", description: "A strapping young lad with a rotten disposition.", playerLocation: TestRoom);


            Items = new Dictionary<string, GameObject>()
            {
                {   "1",
                    new StoryItem()
                    {
                        Name="Mug",
                        Description="A red ceramic mug that looks like the Marvel Villain Carnage",
                        Adjectives=new List<string>() {"Carnage"}

                    }
                },
                {   "2",
                    new StoryItem()
                    {
                        Name="Mug",
                        Description="A faded yellow mug with a small chip in the side.",
                        Adjectives=new List<string>() {"Faded", "Yellow"}

                    }
                },
                {  "3",
                    new StoryItem()
                    {
                        Name="Duck",
                        Description="A rubber duck. It looks like a duck.",
                        Adjectives=new List<string>() {"Yellow"}

                    }
                },


                {   "5",
                    new StoryItem()
                    {
                        Name="Monkey",
                        Description="A monkey who cray.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },

                {   "6",
                    new StoryItem()
                    {
                        Name="Monk",
                        Description="A warrior monk who smelled one too many gonesh sticks.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },
                 {  "7",
                    new StoryItem()
                    {
                        Name="Duck",
                        Description="Just like the yellow duck, but uglier.",
                        Adjectives=new List<string>() {"Ugly","Yellow"}

                    }
                },
            };
            

        }
    }
}