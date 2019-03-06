using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular
{
    public partial class Game
    {
        void GameInit()
        {


            Rooms = new Dictionary<string, Room>();
            Items = new Dictionary<string, StoryItem>();
            Scenes = new Dictionary<Scene, GameScene>();
            Choices = new Dictionary<Choice, PlayerChoice>();
            Gamestats = new GameStatistics();
            GameLog = new List<string>(50);


            #region create Game Objects
            //rooms, items and exits

            #region Room 201
            Room room201 = new 
                Room(
                    name:"Room 201", 
                    description: @"As you look around the hotel room you see an old TV with rabbit ears that looks like it came straight "
                                 +"out of the 1950's. Against the wall there is a beat up night stand with a little drawer built into it "
                                 +"and an old {phone} on top. Next to it is a lumpy old {bed} that looks like it's seen better days with a "
                                 +"dark brown stain on the sheets and a funny smell coming from it. There is an obnoxious orange {couch} in "
                                 +"the corner next to a small {window} smudged with sticky purple hand prints, the stuffing is coming out of "
                                 +"the cushions which are also spotted with purple, and the floor is covered with {cans} of Fast Eddies Colon "
                                 +"Cleanse. The door that leads to the hallway is to the east. "
                                 +"There is a door to the west that leads to that sweet sweet porcelain throne.", 
                    keyValue: "room201", 
                    timeVisited: 0
                );

            Exit room201_westExit = new
               Exit(
                   locationKey: "room201",
                   destinationKey: "room201_bathroom",
                   isLocked: false,
                   keyName: "none",
                   name: "Bathroom Door",
                   description: "The bathroom door in room 201"
                   );

            Exit room201_eastExit = new
               Exit(
                   locationKey: "room201",
                   destinationKey: "westHallway",
                   isLocked: false,
                   keyName: "none",
                   name: "Main Door",
                   description: "The Main door in room 201"
                   );

            room201.Exits["West"] = room201_westExit;
            room201.Exits["East"] = room201_eastExit;

            TV room201_tv = new
                TV(
                    locationKey: "room201",
                    name: "TV",
                    description: "an old TV with rabbit ears that looks like it came straight out of the 1950's.",
                    examine: "One of the dials on the TV has fallen off, but it still works. Kick back and enjoy the wonders of technology."
                   );
            Prop room201_nightStand = new
                Prop(
                    locationKey: "room201",
                    name: "Night Stand",
                    description: "Against the wall there is a beat up night stand with a little drawer built into it.",
                    examine: "",
                    keyValue: "room201_nightStand"
                     );
            Container nightStand_drawer = new
                Container(
                           locationKey: "room201",
                           name: "Drawer",
                           description: "small wooden drawer.",
                           examine: "",
                           keyValue: "room201_nightStand_drawer"
                         );

            Coins pocketChange = new Coins("room201_nightStand_drawer", "pocket change", "A whole 84 cents!", keyValue: "pocketChange");
            pocketChange.DescriptionInRoom = "Some pocket change is lying on the ground.";

            nightStand_drawer.Items.Add(pocketChange);

            room201_nightStand.Container = nightStand_drawer;

            room201.AddItem(room201_tv);
            room201.AddItem(room201_nightStand);
            room201.AddItem(nightStand_drawer);


            Rooms["room201"] = room201;
            #endregion

            #region Room 201 Bathroom
            Room room201_bathroom = new
                Room(
                    name: "Bathroom",
                    description: @"You crack open the door to the bathroom and it looks like it's seen better days. From the smell of it, it looks like "
                                 +"someone beat you to it and narrowly escaped a hard fought battle an eight pound burrito. The {sink} is old and yellowed. "
                                 +"and caked with brown muck in the corners. The {mirror} is cracked and something is written on it red. You can't quite "
                                 +"make it out. But you don't care...you've gotta take a shit! You rush to be the first in line to make a deposit in the "
                                 +"porcelain bank {toilet}. But just as you are about to Drop it like it's hot you notice there is an an angry {Great Dane} "
                                 +"guarding the toilet and he looks hungry! You quickly shut the door and somehow manage to not lose your shit (literally). "
                                 +"Looks like you have to find somewhere else to go if you value your junk...and your life.",
                    keyValue: "room201_bathroom",
                    timeVisited: 0
                );

            Exit room201_bathroom__eastExit = new
               Exit(
                   locationKey: "room201_bathroom",
                   destinationKey: "room201",
                   isLocked: false,
                   keyName: "none",
                   name: "Bathroom Door",
                   description: "The bathroom door in room 201"
                   );

            room201_bathroom.Exits["East"] = room201_bathroom__eastExit;

            Rooms["room201_bathroom"] = room201_bathroom;

            #endregion

            #region West Hallway
            Room westHallway = new
                Room(
                    name: "West Hallway",
                    description: @"You eagerly enter the hallway leaving your room behind you to the West. The glow of the yellow fluorescent lights "
                                 +"are complimented by the well worn red carpet. The diamond pattern urges you forward. To the North you see room 202 "
                                 +"to the North East Room 203. To the east the diamond pattern stretches into more hallway. There is a small alcove with "
                                 +"a vending machine.",
                    keyValue: "westHallway",
                    timeVisited: 0
                );

            Exit westHallway_westExit = new
               Exit(
                   locationKey: "westHallway",
                   destinationKey: "room201",
                   isLocked: false,
                   keyName: "none",
                   name: "Main Door",
                   description: "The Main door in room 201"
                   );

            westHallway.Exits["West"] = westHallway_westExit;



            Rooms["westHallway"] = westHallway;

            #endregion

            //new DoorKey(locationKey: "livingRoom", name: "Aiden's Key", description: "Aiden's room key", examineResponse: "A simple key that fits in the lock to Aiden's door...")





            //create default player
            Room playerStartingLocation = room201;
            Player player = new Player("Jonny Rotten", "A strapping young lad with a rotten disposition.", playerStartingLocation, 100);
            
            
            Inventory playerInventory = new Inventory("playerInventory","Inventory", "Your trusty backpack.", 10, 0);

            player.BackPack = playerInventory;
            
            //inventory is basically a special 'room' with an id below 0 that holds player items
            Rooms.Add("inventory", (Room)playerInventory);


            //Default inventory items
            player.BackPack.AddItem(new StoryItem(name:"Pocket Lint", description:"Your favorite piece of pocket lint, don't spend it all in one place!", locationKey:"inventory",isPortable:true, examine:"Your favorite piece of pocket lint, don't spend it all in one place!"));

            //Add the player to the game
            AddPlayer(player);

            //Add any room and inventory items to the global list of game items
            foreach (Room room in Rooms.Values)
            {
                foreach (StoryItem item in room.RoomItems)
                {
                    Items.Add(item.KeyValue, item);
                    if (item is Container)
                    {
                        var chest = item as Container;
                        foreach (StoryItem loot in chest.Items)
                        {
                            Items.Add(loot.KeyValue, loot);
                        }
                    }
                }

            }


            #endregion

            AddScenes();

            AddChoices();


        }



        #region create Game Scenes

        public void AddScenes()
        {

            GameScene intro = new GameScene("intro");
            intro.SceneText.Enqueue(@"You wake up with a pounding headache in a shabby looking hotel room "
                    + "surrounded by a bunch of empty cans.You've got a taste in your mouth like a dirty old "
                    + "rat crawled in and died in there. Disoriented, you roll out of the bed you woke up in, barely "
                    + "avoiding some questionable stains on the sheets, as you stumble to your feet sending cans flying "
                    + "like bowling pins in your wake.You bend over to take a closer look at the pile of crushed aluminum. "
                    + "You read one of the labels: \"Fast Eddie's Colon Cleanse: When in doubt flush it out!\"\"Side effects "
                    + "may include: Dizzines, vomiting, diarrhea, cold sweats, hallucinations, intense panic, paranoia, permanent "
                    + "tongue discoloration, mild brain damage, amnesia, bowel implosion, and occasionally hiccups\". The can has "
                    + "a purple iridescent sludge oozing out of it that's really similar to the shade of purple that your hands "
                    + "are. Come to think of it, you vaguely remember signing up for a test group that was supposed to try out "
                    + "a new health drink.Looks like your part time job as a barrista just wasn't paying the bills, nothing "
                    + "like easy money! The thing is you don't remember anything about going to a hotel last night, and you "
                    + "definitely don't remember anything about drinking a 24 pack of Fast Eddie's Colon Cleanse. Your stomach "
                    + "starts to feel a little uneasy, but never mind that, it's time to spend some of that hard earned cash! "
                    + "You reach into your wallet and realize in that moment that you don't even remember your name. You look at "
                    + "your license and focus your still hazy eyes and barely make out that it says...\n\n ");
            intro.SceneAction = delegate (GameController controller)
            {
                controller.ActiveChoice = Choice.PlayerName;
                controller.SetGameState("PlayerChoice");

            };

            GameScene introLetter = new GameScene("IntroLetter");
            introLetter.SceneText.Enqueue($"Dear <firstName>,\n\n Thank you so much for signing up to try out our exciting new drink! "
                                + "We hope you don't mind but we've taken the liberty of putting you up for the night "
                                + "in one of our sponsors hotels with a generous supply of Fast Eddie's to keep you company.\n\n "
                                );
            introLetter.SceneAction = delegate (GameController controller)
            {
                controller.SetGameState("Explore");

            };

            GameScene Bathroom201FirstVisit = new GameScene("Bathroom201FirstVisit");
            Bathroom201FirstVisit.SceneText.Enqueue(@"You crack open the door to the bathroom and it looks like it's seen better days. From the smell of it, it looks like "
                     + "someone beat you to it and narrowly escaped a hard fought battle with an eight pound burrito. The {sink} is old and yellowed. "
                     + "and caked with brown muck in the corners. The {mirror} is cracked and something is written on it red. You can't quite "
                     + "make it out. But you don't care...you've gotta take a shit! You rush to be the first in line to make a deposit in the "
                     + "porcelain bank {toilet}. But just as you are about to Drop it like it's hot you notice there is an an angry {Great Dane} "
                     + "guarding the toilet and he looks hungry! You quickly shut the door and somehow manage to not lose your shit (literally). "
                     + "Looks like you have to find somewhere else to go if you value your junk...and your life.");
            Bathroom201FirstVisit.SceneAction = delegate (GameController controller)
            {
                //put the player back in the previous room as the story suggests the player quickly shuts the door 
                //and leaves the bathroom
                Player player = GameObject.GetComponent<Player>("player");
                Room destination = GameObject.GetComponent<Room>("room201");

                controller.Game.Player.PlayerLocation = destination;
                controller.SetGameState("Explore");

            };

            Scenes[Scene.Intro] = intro;
            Scenes[Scene.IntroLetter] = introLetter;
            Scenes[Scene.Bathroom201FirstVisit] = Bathroom201FirstVisit;



        }

        #endregion


        #region create Player Choices

        public void AddChoices()
        {
            PlayerChoice askPlayerName = new PlayerChoice("PlayerName");

            askPlayerName.ChoicePrompt = delegate (GameController controller, string userInput)
            {
                Player player = controller.Game.Player;

                if (userInput != "")
                {
                    player.FirstName = userInput;
                    return true;
                }

                GameController.InputResponse.Append("What is your name");
                return false;

            };

            askPlayerName.ChoiceResult = delegate (GameController controller)
            {
                GameScene scene = Scenes[Scene.IntroLetter];
                string scenePage = scene.SceneText.Dequeue();
                scenePage = scenePage.Replace("<firstName>", Player.FirstName);
                scene.SceneText.Enqueue(scenePage);
                controller.ActiveStoryScene = Scene.IntroLetter;
                controller.SetGameState("StoryScene");
            };

            Choices[Choice.PlayerName] = askPlayerName;

        }

        #endregion

    }
}
