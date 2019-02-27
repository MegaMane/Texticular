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

            Commands["look"] = look;
        }


        [JsonConstructor]
        public Room(string name, string description, string keyValue, int timeVisited) 
            : base(name, description, examineResponse:"", LocationKey:null, KeyValue: keyValue)
        {
            TimesVisited = timeVisited;
            roomCount++;

            Commands["look"] = look;
            Commands["examine"] = look;
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

        void look(GameController controller)
        {
            Room currentRoom = this;
            Game game = controller.Game;

            //location description
            GameController.InputResponse.AppendFormat("You are in {0}: {1}", currentRoom.Name, currentRoom.Description);
            foreach (StoryItem item in game.Items.Values)
            {
                if(item.LocationKey == currentRoom.KeyValue && !String.IsNullOrEmpty(item.DescriptionInRoom))
                {
                    GameController.InputResponse.Append(item.DescriptionInRoom );
                }
            }

            GameController.InputResponse.Append(" \n\n ");

            //list items
            GameController.InputResponse.Append("You see:\n ");
            string itemString = "";
            foreach (StoryItem item in game.Items.Values)
            {
                if (item.LocationKey == currentRoom.KeyValue)
                {

                     itemString += item.Name + " : " + item.Description + "\n ";
                }

            }

            GameController.InputResponse.Append(itemString != "" ? itemString + "\n " : "Nothing\n\n ");

            //exits
            GameController.InputResponse.Append("Obvious Exits:\n ");

            var exits = from KeyValpair in currentRoom.Exits
                        orderby (Direction)Enum.Parse(typeof(Direction), KeyValpair.Key) ascending
                        select KeyValpair;

            foreach (KeyValuePair<string, Exit> exit in exits)
            {


                if (exit.Value.IsLocked)
                {
                    GameController.InputResponse.AppendFormat("To the {0} you see: {1}\n ", exit.Key, exit.Value.Name);
                }

                else
                {
                    GameController.InputResponse.AppendFormat("To the {0} you see: {1}\n ", exit.Key, game.Rooms[exit.Value.DestinationKey].Name);
                }


            }


        }

    }
}
