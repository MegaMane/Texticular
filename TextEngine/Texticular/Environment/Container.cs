using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Environment
{
    public class Container : StoryItem
    {
        public List<StoryItem> Items;
        bool IsOpen { get; set; } = false;

        public Container(string locationKey, string name, string description, string keyValue = "", string examine = "", Container container = null)
            : base(name, description, locationKey, isPortable: false, examine: examine, weight: 99, keyValue: keyValue)
        {
            Items = new List<StoryItem>();

            Commands["open"] = openContainer;

            Commands["close"] = closeContainer;
            Commands["shut"] = closeContainer;
        }

        void openContainer(GameController controller)
        {
            IsOpen = true;
            controller.InputResponse.Append($"You open the {Description} and look inside.\n\n ");
            foreach (StoryItem item in Items)
            {
                controller.InputResponse.Append($"{item.Name}:{item.Description}\n ");
                //place the item in the room so the player can take it
                item.LocationKey = this.LocationKey;
            }
            

        }


        void closeContainer(GameController controller)
        {
            IsOpen = false;
            controller.InputResponse.Append($"You shut the {Description}\n ");
            foreach (StoryItem item in Items)
            {
                //set the location of the item back to the parent container so it can no longer be taken by the player
                //because it is not visible in the current room
                item.LocationKey = this.KeyValue;
            }

        }


    }

}
