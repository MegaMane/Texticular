using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Actors;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Container : GameObject, IOpenable, IEnumerable<StoryItem>
    {
        public List<StoryItem> Items;
        public virtual bool IsOpen { get; set; }

        public int SlotsFull { get; set; } = 0;
        public int MaxSlots { get; set; }
        public int ItemCount { get; private set; }

  
        public string ContextualDescription { get; set; }
        public string LocationKey { get; private set; }

        public Container(string locationKey, string name, string description, string keyValue = "", int maxSlots = 10, string contextualDescription = "")
            : base(name, description,  keyValue)
        {
            Items = new List<StoryItem>();
            MaxSlots = maxSlots;
            LocationKey = locationKey;
            IsOpen = false;
        }


        public virtual void Open()
        {


            IsOpen = true;
            GameLog.Append($"You open the {Name} and look inside...\n\n ");
            foreach (StoryItem item in Items)
            {
                GameLog.Append($"{item.Name}: {item.Description}\n ");
            }

            


        }
        public virtual void Close()
        {
            IsOpen = false;
            GameLog.Append($"You shut the {Name}\n ");
        }


        public bool AddItem(StoryItem item)
        {
            if (IsOpen)
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
            return false;
        }

        public bool RemoveItem(StoryItem item)
        {
            if (IsOpen)
            {
                int SlotsEmptied = item.SlotsOccupied;

                SlotsFull -= SlotsEmptied;


                Items.Remove(item);
                ItemCount--;
                return true;

            }

            return false;


        }


        public bool RemoveItem(string itemKey)
        {
            StoryItem item = Items.Find(i => i.KeyValue == itemKey);

            if (IsOpen)
            {
                int SlotsEmptied = item.SlotsOccupied;

                SlotsFull -= SlotsEmptied;


                Items.Remove(item);
                ItemCount--;
                return true;

            }

            return false;


        }



        public override string ToString()
        {
            StringBuilder results = new StringBuilder();
            results.Append(base.ToString());
            results.Append("\n\n--------------------Items in Container------------------------------------\n\n");
            foreach (StoryItem item in Items)
            {
                results.Append($"{item.Name}: {item.Description}\n");
            }

            return results.ToString();
        }

        public IEnumerator<StoryItem> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
