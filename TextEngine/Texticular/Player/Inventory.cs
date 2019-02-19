using System.Collections.Generic;
using Texticular.Environment;

namespace Texticular
{
    public class Inventory:Room
    {
        public int Slots { get; set; }
        public int ItemCount { get; set; }
        private List<StoryItem> usedItems;


        public Inventory(string keyValue, string name, string description, int slots, int itemCount, int timeVisited = 0):
            base(name, description, keyValue, timeVisited)
        {
            this.Slots = slots;
            this.ItemCount = itemCount;
            usedItems = new List<StoryItem>();
        }

        new public bool AddItem(StoryItem item)
        {
            if (ItemCount < Slots)
            {
                RoomItems.Add(item);
                ItemCount++;
                return true;
            }

            return false;
        }


        public void ConsumeItem(StoryItem item)
        {
            item.LocationKey = "usedItem";
            RoomItems.Remove(item);
            ItemCount--;
            usedItems.Add(item);

        }

    }
}