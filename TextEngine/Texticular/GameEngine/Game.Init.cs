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
            Gamestats = new Gamestats();
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

            #endregion

            //Add any room and inventory items to the global list of game items
            foreach (Room room in Rooms.Values)
            {
                foreach(StoryItem item in room.RoomItems)
                {
                    Items.Add(item.KeyValue,item);
                    if (item is Container)
                    {
                        var chest = item as Container;
                        foreach(StoryItem loot in chest.Items)
                        {
                            Items.Add(loot.KeyValue,loot);
                        }
                    }
                }
                
            }


           
            
            

           


        }


    }
}
