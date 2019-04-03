using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Room:GameObject, IEnumerable<GameObject>
    {
        private static int roomCount = 0;
        public static int RoomCount { get { return roomCount; } }
        public List<GameObject> Items = new List<GameObject>();




        public Dictionary<string, Door> Exits { get; set; }

        public Room(string name, string description, string keyValue)
            : base(name, description, keyValue)
        {
            Exits = new Dictionary<string, Door>();
            roomCount++;

        }




        public void AddItem(GameObject item)
        {
            //Set the items location to this room
            item.LocationKey = this.KeyValue;
            Items.Add(item);
        }



        public void RemoveItem(GameObject item)
        {

            Items.Remove(item);
        }


        public void RemoveItem(string itemKey)
        {
            GameObject item = Items.Find(i => i.KeyValue == itemKey);
            Items.Remove(item);
           
        }


        public override string ToString()
        {
            return base.ToString();
        }

        public string Look()
        {
            ActionResponse.Clear();

            Room currentRoom = this;
            //Game game = controller.Game;

            //location description
            ActionResponse.Append($"You are in {currentRoom.Name}: {currentRoom.Description}");
            foreach (var item in currentRoom.Items)
            {

                if (item is StoryItem)
                {
                    ActionResponse.Append((item as StoryItem).ContextualDescription);
                }
            }

            return ActionResponse.ToString();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
