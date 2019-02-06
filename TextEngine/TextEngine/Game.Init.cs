using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public partial class Game
    {
        void GameInit()
        {
            this.Rooms = new Dictionary<string, Room>();
            this.Items = new List<StoryItem>();
            this.Scenes = new List<Scene>();
            //this.Location = new Dictionary<string, int>();
            this.gamestats = new Gamestats();
            this.gameLog = new List<string>(50);
            this.objectID = new Sequence(0);

            

            //rooms
            Rooms.Add("backyard",new Room(objectID.Next, "Backyard", "The smallest little poo patch in the world"));
            Rooms.Add("masterBedroom", new Room(objectID.Next, "Master Bedroom", "A real pervert must live here."));
            Rooms.Add("masterBathroom", new Room(objectID.Next, "Master Bathroom", "A closet with a toilet and a shower large enough for a dwarf."));
            Rooms.Add("masterCloset", new Room(objectID.Next, "Master Closet", "A walk in closet filled with mostly empty electronics boxes and chocolate wrappers."));
            Rooms.Add("westHall", new Room(objectID.Next, "West Hall", "Not much here, just the west hall"));
            Rooms.Add("eastHall", new Room(objectID.Next, "East Hall", "The east hall, this is where the church of dust bunnies congregates (98% dog hair)."));
            Rooms.Add("livingRoom", new Room(objectID.Next, "Living Room", "A large tv, old Ikea coffee table that looks like it's seen better days, and a weathered blue couch sitting on a carpet used for pooping by the beloved dogs."));
            Rooms.Add("diningRoom", new Room(objectID.Next, "Dining Room", "The place where meals should be eaten, instead of in front of the tv."));
            Rooms.Add("backYard", new Room(objectID.Next, "Kitchen", "A small but mostly clean kitchen well stocked with spices."));
            Rooms.Add("spareRoom", new Room(objectID.Next, "Spare Room", "Piles of circuit boards, and art supplies and a modest futon."));
            Rooms.Add("aidensRoom", new Room(objectID.Next, "Aiden's Room", "The ultimate gamers nest and puppy safehaven."));
            Rooms.Add("garage", new Room(objectID.Next, "Garage", "A hot garage with a gym that hasn't seen use in years and a washer and dryer against the backwall. There is a bike workshop in the corner and lots of dog hair."));
            Rooms.Add("bathroom", new Room(objectID.Next, "Bathroom", "The main bathroom, mostly filled with Aiden's stuff and a baked in smell of farts."));

            //foreach (KeyValuePair<string, int> entry in Location)
            //{
            //    Console.WriteLine($"{entry.Key}, {entry.Value.ToString()} ");

            //}


            //create exits
            //room default exits all lead nowhere so you only need to add the ones you need

            ItemKey AidenRoomKey = new ItemKey(objectID.Next, "simpleKey", "Aiden's room key", Rooms["livingRoom"].ID);


            //Dining Room
            Rooms["diningRoom"].AddExit(new Exit(objectID.Next, "Aiden's Door", "A white painted door with caution tape and a do not enter sign taped to it", Rooms["diningRoom"].ID, Direction.Northwest, Rooms["aidensRoom"], AidenRoomKey, true));
            Rooms["diningRoom"].AddExit(new Exit(objectID.Next, Rooms["diningRoom"].ID, Direction.North, Rooms["livingRoom"]));


            //living Room
            Rooms["livingRoom"].AddExit(new Exit(objectID.Next, Rooms["livingRoom"].ID, Direction.South, Rooms["diningRoom"]));

            //Aidens Room
            Rooms["aidensRoom"].AddExit(new Exit(objectID.Next, Rooms["aidensRoom"].ID, Direction.Southwest, Rooms["diningRoom"]));
           


            //create items
            Consumable magicMedicine = new Consumable(objectID.Next, "Medicine", "Magic Medicine.", Rooms["livingRoom"].ID, "Magic Medicine: just what the doctor ordered! Full recovery from all that ails ya.", 100);
            magicMedicine.actions["use"] = "You suck down every last drop of the magic medicine and you feel alive! HP increases by 100.";

            AddItem(AidenRoomKey);
            AddItem(magicMedicine);
            AddItem(new StoryItem(objectID.Next, "Apple", "An apple a day...except this one has a worm in it", Rooms["backyard"].ID, "An apple a day...except this one has a worm in it"));
            AddItem(new StoryItem(objectID.Next, "Key", "An old skeleton key that looks like it might fit into a door", Rooms["diningRoom"].ID, "A chipped old skeleton key, the head of the key is carved like the All Seeing Eye. It almost seems to be faintly glowing."));


            //create event wiring


            //create default player
            Player player = new Player(9999, "Jonny Rotten", "A strapping young lad with a rotten disposition.", Rooms["diningRoom"], 100);
            Inventory playerInventory = new Inventory(-2, "Inventory", "Your trusty backpack.", 10, 0);

            player.BackPack = playerInventory;
            //inventory is basically a special 'room' with an id below 0 that holds player items
            Rooms.Add("inventory", (Room)playerInventory);
            
            //special ID below 0 no actual game objecst attached to these
            //Location.Add("usedItem", -3);
            //Location.Add("none", -1); //destroyed items

            //Default inventory items
            AddItem(new StoryItem(objectID.Next, "Lint", "Your favorite peace of pocket lint, don't spend it all in one place!", Rooms["inventory"].ID, "Your favorite piece of pocket lint, don't spend it all in one place!"));
            player.BackPack.ItemCount += 1;


            AddPlayer(player);
            Rooms["diningRoom"].TimesVisited += 1;

            gamestats.player = player;


            //create new file for event handler functions
        }
    }
}
