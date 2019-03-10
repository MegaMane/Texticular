using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.GameEngine;

namespace Texticular.Environment
{
    public class Container : StoryItem
    {
        public List<StoryItem> Items;
        public bool IsOpen { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public int SlotsFull { get; set; } = 0;
        public int MaxSlots { get; set; } = 10;

        public Container(string locationKey, string name, string description, string keyValue = "", string examine = "", Container container = null)
            : base(name, description, locationKey, isPortable: false, examine: examine, weight: 99, keyValue: keyValue)
        {
            Items = new List<StoryItem>();

            Commands["open"] = openContainer;

            Commands["close"] = closeContainer;
            Commands["shut"] = closeContainer;
        }

        void openContainer(ParseTree tokens)
        {

            IsOpen = true;
            GameController.InputResponse.Append($"You open the {Description} and look inside.\n\n ");
            foreach (StoryItem item in Items)
            {
                GameController.InputResponse.Append($"{item.Name}:{item.Description}\n ");
            }
            

        }
        void closeContainer(ParseTree tokens)
        {
            IsOpen = false;
            GameController.InputResponse.Append($"You shut the {Description}\n ");
        }

        public bool AddItem(StoryItem item)
        {
            int SlotsRequested = item.SlotsOccupied;

            SlotsFull += SlotsRequested;

            if (SlotsFull <= MaxSlots)
            {
                Items.Add(item);
                item.LocationKey = this.KeyValue;
                return true;
            }

            SlotsFull -= SlotsRequested;
            return false;
        }

        public bool RemoveItem(StoryItem item)
        {
            int SlotsEmptied = item.SlotsOccupied;

            SlotsFull -= SlotsEmptied;


            Items.Remove(item);
            return true;
            


        }


    }

}
