using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Texticular.Environment
{

    public class Rooms
    {
        [JsonProperty(ItemIsReference = true)]
        public List<Room> RoomList { get; set; }
    }


    public class Room : GameObject
    {
        private static int roomCount = 0;
        public static int RoomCount { get { return roomCount; } }

        public int TimesVisited { get; set; }

        [JsonProperty(ItemIsReference = true)]
        public List<StoryItem> RoomItems = new List<StoryItem>();

        //public List<GameObject> Props = new List<GameObject>();

        [JsonProperty(ItemIsReference = true)]
        public Dictionary<string, Exit> Exits = new Dictionary<string,Exit>();

        public Room() : this( "Test Room", "Test Description")
        {
            

        }

        public Room(string name, string description) :base(name, description)
        {
            TimesVisited = 0;
            roomCount++;
        }


        [JsonConstructor]
        public Room(string name, string description, string keyValue, int timeVisited) : base(name, description, keyValue)
        {
            TimesVisited = timeVisited;
            roomCount++;
        }




        public void AddItem(StoryItem item)
        {
            //Set the items location to this room
            item.LocationKey = this.KeyValue;
            RoomItems.Add(item);
        }



        public override string ToString()
        {
            return base.ToString() + $"TimesVisited: {TimesVisited}\n\n";
        }
    }
}
