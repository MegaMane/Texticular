using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular
{
    public partial class Game
    {
        void GameInit(string filePath = @"..\..\JsonFiles\JsonRoomTest.json")
        {


            this.Rooms = new Dictionary<string, Room>();
            this.Items = new List<GameObject> ();
            this.gameRooms = new List<Room> ();
            this.Gamestats = new Gamestats();
            this.GameLog = new List<string>(50);


            //rooms, items and exits
            LoadGameObjects(filePath);



            //create default player
            Player player = new Player("Jonny Rotten", "A strapping young lad with a rotten disposition.", Rooms["diningRoom"], 100);
            Inventory playerInventory = new Inventory("playerInventory","Inventory", "Your trusty backpack.", 10, 0);

            player.BackPack = playerInventory;
            
            //inventory is basically a special 'room' with an id below 0 that holds player items
            Rooms.Add("inventory", (Room)playerInventory);


            //Default inventory items
            player.BackPack.Items.Add(new StoryItem("inventory", "Lint", "Your favorite peace of pocket lint, don't spend it all in one place!", "Special", true, "Your favorite piece of pocket lint, don't spend it all in one place!"));
            player.BackPack.ItemCount += 1;


            //Add any inentory items to the global list of game items
            foreach (StoryItem item in player.BackPack.Items)
            {
                Items.Add(item);
            }

            AddPlayer(player);
            Rooms["diningRoom"].TimesVisited += 1;

            var testTv = new TV("livingRoom", "A large  flat screen tv");
            testTv.TurnOffResponse = "The TV goes black";
            Items.Add(testTv);
            Rooms["livingRoom"].AddItem(testTv);
            Gamestats.player = player;

            #region useItems


            #endregion


        }
    }
}
