using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Container : StoryItem
    {
        public List<StoryItem> Items;
        public bool IsOpen { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public int SlotsFull { get; set; } = 0;
        public int MaxSlots { get; set; }
        public int ItemCount { get; private set; }

        public Container(string locationKey, string name, string description, string keyValue = "", int maxSlots = 10, string contextualDescription = "")
            : base(name, description, locationKey, isPortable: false, weight: 99, keyValue: keyValue, contextualDescription: contextualDescription)
        {
            Items = new List<StoryItem>();
            MaxSlots = maxSlots;
        }

        public override void Take()
        {
            GameLog.Append($"The {this.Description} is firmly attached. ");
        }

        public override void Drop()
        {
            GameLog.Append($"You cant drop {this.Description} since you don't possess it.");
        }

        public override void Put(string target)
        {
            GameLog.Append($"This isn't russian dolls man! You can't put the {this.Description} in the {target}!.");
        }

        void Open()
        {

            IsOpen = true;
            GameLog.Append($"You open the {Description} and look inside.\n\n ");
            foreach (StoryItem item in Items)
            {
                GameLog.Append($"{item.Name}:{item.Description}\n ");
            }
            

        }
        void Close(ParseTree tokens)
        {
            IsOpen = false;
            GameLog.Append($"You shut the {Description}\n ");
        }


        public bool AddItem(StoryItem item)
        {
            int SlotsRequested = item.SlotsOccupied;

            SlotsFull += SlotsRequested;

            if (SlotsFull <= MaxSlots)
            {
                Items.Add(item);
                item.LocationKey = this.KeyValue;
                ItemCount++;
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
            ItemCount--;
            return true;
            


        }


    }

}
