using System.Collections.Generic;
using Texticle.Environment;

namespace Texticle.Actors
{
    public class Inventory:Room
    {
        public int Slots { get; set; }
        public int ItemCount { get; set; }
        private List<StoryItem> usedItems;


        public Inventory(string keyValue, string name, string description, int slots, int itemCount, int timeVisited = 0)
   
        {
            this.Slots = slots;
            this.ItemCount = itemCount;
            usedItems = new List<StoryItem>();
        }

        new public bool AddItem(StoryItem item)
        {
            if (ItemCount < Slots)
            {
                ItemCount++;
                return true;
            }

            return false;
        }


        public void ConsumeItem(StoryItem item)
        {
            item.LocationKey = "usedItem";
            ItemCount--;
            usedItems.Add(item);

        }

    }
}