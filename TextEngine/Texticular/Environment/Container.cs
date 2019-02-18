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
        bool IsOpen = false;

        public Container(String name, String description) : base(name, description)
        {
            Items = new List<StoryItem>();

            Commands["open"] = openContainer;
        }

        void openContainer(GameController controller)
        {
            IsOpen = true;
            controller.InputResponse.Append($"You open the {Description}\n\n");
            foreach (StoryItem item in Items)
            {
                controller.InputResponse.Append($"{item.Description}\n");
            }
            

        }
    }

}
