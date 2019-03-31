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
        [TestMethod()]
        public void ParserTest()
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

            //the room and the player

            //player inventory

            //items that are located in the room
            Dictionary <string, GameObject> nouns = GameObject.Objects.Where(k => k.Value.LocationKey == "otis")
                                                                      .ToDictionary(kv => kv.Key,
                                                                                    kv => kv.Value);
            //exits in the room

            //items that are in the players inventory

            Parser testParser = new Parser(nouns);
        }
    }
}
