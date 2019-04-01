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

namespace Texticular.ParserTests.Tests
{
    [TestClass()]
    public class ParserTests
    {
        private Parser TestParser;

        [TestInitialize]
        public void Setup()
        {
            Room MyOffice = new Room("Otis Office", "A hot stuffy little office that smells faintly of farts.", "otis");
            Room DeveloperBullpen = new Room("Dev Den", "Cubicles full of action figures and the grown men who own them clicking away on their keyboards", "bullPen");
            Player testPlayer = new Player(playerName: "Jonny Rotten", description: "A strapping young lad with a rotten disposition.", playerLocation: MyOffice);

            var destinations = new Dictionary<string, Room>()
                {
                    { "otis",DeveloperBullpen},
                    { "bullPen",MyOffice}
                };

            Door officeDoor = new Door(destinations, name: "Otis Office Door", keyValue: "otisDoor", description: "The door to the Otis Office")
            {

                IsLocked = false,

            };

            StoryItem CarnageMug = new StoryItem
            (
                name: "Carnage Mug",
                description: "A red ceramic mug that looks like the marvel villain Carnage",
                keyValue: "carnageMug",
                contextualDescription: "There is a red mug that looks like a Carnage head sitting on one of the desks"
            );

            Money chumpChange = new Money();
            chumpChange.LocationKey = "subway";


            Chest chest = new Chest("masterBedroom", "Sentry Safe", "a digital safe for keeping things tucked away", "sentrySafe", 5);

            chest.Open();

            chest.AddItem(new StoryItem
            (
                name: "Vape Pen",
                description: "It's caked in resin and cotton candy flavor!",
                keyValue: "vapePen"
            ));

            chest.AddItem(new StoryItem
            (
                name: "Stick Of Gum",
                description: "Your last piece of juicy fruit",
                keyValue: "gumStick"
            ));

            MyOffice.AddItem(CarnageMug);
            MyOffice.AddItem(chest);
            MyOffice.AddItem(chumpChange);
            testPlayer.BackPack.AddItem(new StoryItem("Pocket Lint", "Your favorite piece of pocket lint", keyValue:"pocketLint"));


            //items that are in the players inventory
            var inventoryItems = GameObject.Objects.Where(k => k.Value.LocationKey == testPlayer.BackPack.KeyValue)
                                                   .ToDictionary(G => G.Key, G => G.Value);
            //items that are located in the room
            var nouns = GameObject.Objects.Where(k => k.Value.LocationKey == testPlayer.LocationKey)
                                           .ToDictionary(G => G.Key, G => G.Value);

            nouns = nouns.Concat(inventoryItems).ToDictionary(G => G.Key, G => G.Value);

            //exits in the room
            foreach (var door in testPlayer.PlayerLocation.Exits.Values)
            {
                nouns[door.KeyValue] = (GameObject)door;
            }

            //the room itself
            nouns[testPlayer.LocationKey] = (GameObject)testPlayer.PlayerLocation;


            TestParser = new Parser(nouns);
        }

        [TestMethod()]
        public void ParserTest()
        {

            ParseTree results = TestParser.Tokenize("open the sentry safe");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

            results = TestParser.Tokenize("drop the pocket lint");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

            results = TestParser.Tokenize("go north");
            Console.Write(results.ToString());


            Console.WriteLine("\n\n----------------------------------------------\n\n");

            results = TestParser.Tokenize("put the Carnage mug in the seNtry safe");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

            results = TestParser.Tokenize("put the arrow through the dragons heart!");
            Console.Write(results.ToString());


            Console.WriteLine("\n\n----------------------------------------------\n\n");

            results = TestParser.Tokenize("Hide under the bed");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

            //doesn't work correctly
            results = TestParser.Tokenize("Grab on to the apple");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

 
            results = TestParser.Tokenize("Go to the north");
            Console.Write(results.ToString());

            Console.WriteLine("\n\n----------------------------------------------\n\n");

            
            results = TestParser.Tokenize("Go to the west hallway");
            Console.Write(results.ToString());


        }
    }
}
