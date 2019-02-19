using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Environment
{
    public class Prop: StoryItem
    {
        public Container Container;



        public Prop (string locationKey, string name, string description,  string keyValue = "", string examine="", Container container = null)
            :base(name, description, locationKey, isPortable:false, examine:examine, weight:99,keyValue: keyValue)
        {
            this.Container = container;

            Commands["take"] = takeProp;
            Commands["get"] = takeProp;
            Commands["pick up"] = takeProp;
            Commands["grab"] = takeProp;

            Commands["open"] = openProp;
        }

        void takeProp(GameController controller)
        {
            controller.InputResponse.Append($"The {Name} won't budge.");
        }

        void openProp(GameController controller)
        {
            if(this.Container != null)
            {
                Container.Commands["open"](controller);
            }

            else
            {
                controller.InputResponse.Append($"Try as you might you can't open the {Name}");
            }
        }

    }


}

