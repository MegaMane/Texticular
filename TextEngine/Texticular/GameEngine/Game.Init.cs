using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;

namespace Texticular
{
    public partial class Game
    {
        void GameInit()
        {


            this.Rooms = new Dictionary<string, Room>();
            this.Items = new List<StoryItem> ();
            this.gameRooms = new List<Room> ();
            this.Gamestats = new Gamestats();
            this.GameLog = new List<string>(50);


            //rooms, items and exits
            this.Rooms = new Dictionary<string, Room>()
            {

                { "diningRoom",new Room {
                                         KeyValue ="diningRoom",
                                         Name="the Dining Room",
                                         Description="The place where meals should be eaten, instead of in front of the tv.",
                                         TimesVisited=0,
                                         Exits=new Dictionary<string, Exit>
                                         {
                                            {
                                                "Northwest", new Exit(locationKey:"diningRoom", destinationKey:"aidensRoom", isLocked:true, keyName:"aidensKey", name:"Aiden's Bedroom Door", description:"A white painted door with caution tape and a do not enter sign taped to it")
                                            },
                                            {
                                                "North", new Exit(locationKey:"diningRoom", destinationKey:"livingRoom", isLocked:false, name:"diningRoom_Ex_livingRoom")
                                            }

                                         }
                                        }
                },
                { "livingRoom",new Room {
                                         KeyValue ="livingRoom",
                                         Name="the Living Room",
                                         Description="A large tv, old Ikea coffee table that looks like it's seen better days, and a weathered blue couch sitting on a carpet used for pooping by the beloved dogs.",
                                         TimesVisited=0,
                                         Exits=new Dictionary<string, Exit>
                                         {
                                            {
                                                "South", new Exit(locationKey:"livingRoom", destinationKey:"diningRoom", isLocked:false, name:"livingRoom_Ex_diningRoom")
                                            }

                                         },
                                         RoomItems=new List<StoryItem>
                                         {
                                             {
                                                 new TV(locationKey:"livingRoom", description:"A flat screen tv")
                                             },
                                             {
                                                 new DoorKey(locationKey:"livingRoom", name:"aidensKey" , description:"Aiden's room key",examineResponse:"A simple key that fits in the lock to Aiden's door...")
                                             }
                                         }

                                        }
        
                },
                { "aidensRoom",new Room {
                                         KeyValue ="aidensRoom",
                                         Name="Aiden's Room",
                                         Description="The ultimate gamers nest and puppy safehaven.",
                                         TimesVisited=0,
                                         Exits=new Dictionary<string, Exit>
                                         {
                                            {
                                                "Southwest", new Exit(locationKey:"aidensRoom", destinationKey:"diningRoom", isLocked:false, name:"aidensRoom_Ex_diningRoom")
                                            }

                                         },
                                         RoomItems=new List<StoryItem>
                                         {
                                             {
                                                 new TV(locationKey:"aidensRoom", description:"Another  flat screen tv")
                                             }
                                         }

                                        }

                }
            };
                


            //create default player
            Player player = new Player("Jonny Rotten", "A strapping young lad with a rotten disposition.", Rooms["diningRoom"], 100);
            Inventory playerInventory = new Inventory("playerInventory","Inventory", "Your trusty backpack.", 10, 0);

            player.BackPack = playerInventory;
            
            //inventory is basically a special 'room' with an id below 0 that holds player items
            Rooms.Add("inventory", (Room)playerInventory);


            //Default inventory items
            player.BackPack.AddItem(new StoryItem(name:"Lint", description:"Your favorite piece of pocket lint, don't spend it all in one place!", locationKey:"inventory",isPortable:true, examine:"Your favorite piece of pocket lint, don't spend it all in one place!"));


            //Add any room and inventory items to the global list of game items
            foreach (Room room in Rooms.Values)
            {
                foreach(StoryItem item in room.RoomItems)
                {
                    Items.Add(item);
                }
                
            }


            AddPlayer(player);
            
            

            #region useItems


            #endregion


        }
    }
}
