using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                             }
                                         }

                                        }
        
                },
            };
                


            //create default player
            Player player = new Player("Jonny Rotten", "A strapping young lad with a rotten disposition.", Rooms["diningRoom"], 100);
            Inventory playerInventory = new Inventory("playerInventory","Inventory", "Your trusty backpack.", 10, 0);

            player.BackPack = playerInventory;
            
            //inventory is basically a special 'room' with an id below 0 that holds player items
            Rooms.Add("inventory", (Room)playerInventory);


            //Default inventory items



            //Add any inentory items to the global list of game items
            foreach (StoryItem item in player.BackPack.RoomItems)
            {
                Items.Add(item);
            }

            AddPlayer(player);
            Rooms[player.LocationKey].TimesVisited += 1;
            Gamestats.player = player;

            #region useItems


            #endregion


        }
    }
}
