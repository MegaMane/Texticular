using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Texticular.Environment
{
    public class StoryItem : GameObject
    {
        public bool IsPortable { get; set; }
        public int Weight { get; set; }


        [JsonConstructor]
        public StoryItem(string name, string description, string locationKey = null, bool isPortable = false, string examine="", int weight = 0, string keyValue="") 
            :base(name, description, examine, locationKey, keyValue)
        {
            IsPortable = isPortable;
            Weight = weight;
            Commands["take"] = takeItem;
            Commands["get"] = takeItem;
            Commands["pick up"] = takeItem;
            Commands["grab"] = takeItem;
        }



        void takeItem(GameController controller)
        {
            
            if (IsPortable)
            {

            }
            controller.InputResponse.Append($"The {Name} won't budge.");
        }

    }
}
