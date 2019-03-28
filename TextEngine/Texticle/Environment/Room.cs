using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Room:GameObject
    {
        private static int roomCount = 0;
        public static int RoomCount { get { return roomCount; } }
        public List<GameObject> Items = new List<GameObject>();




        public Dictionary<string, Door> Exits = new Dictionary<string, Door>();

        public Room(string name, string description, string keyValue)
            : base(name, description, keyValue)
        {

            roomCount++;

        }




        public void AddItem(StoryItem item)
        {
            //Set the items location to this room
            item.LocationKey = this.KeyValue;
            Items.Add(item);
        }



        public void RemoveItem(StoryItem item)
        {

            Items.Remove(item);
        }


        public override string ToString()
        {
            return base.ToString();
        }

        public void Look()
        {
            Room currentRoom = this;
            //Game game = controller.Game;

            //location description
            GameLog.Append($"You are in {currentRoom.Name}: {currentRoom.Description}");
            foreach (StoryItem item in currentRoom.Items)
            {
                if (!String.IsNullOrEmpty(item.ContextualDescription))
                {
                    GameLog.Append(item.ContextualDescription);
                }
            }


        }
    }
}
