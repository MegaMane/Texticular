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
        public bool IsOpen { get; set; } = false;

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
            UpdateItems(controller);

            IsOpen = true;
            controller.InputResponse.Append($"You open the {Description} and look inside.\n\n ");
            foreach (StoryItem item in Items)
            {
                controller.InputResponse.Append($"{item.Name}:{item.Description}\n ");
            }
            

        }


        void closeContainer(GameController controller)
        {
            UpdateItems(controller);
            IsOpen = false;
            controller.InputResponse.Append($"You shut the {Description}\n ");

        }

        void UpdateItems(GameController controller)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].LocationKey == "inventory") Items.RemoveAt(i);
            }

            //for (int i = 0; i < controller.Game.Items.Count; i++)
            //{
            //    if (Items[i].LocationKey == KeyValue) Items.Add(controller.Game.Items[i]);
            //}
        }


    }

}
