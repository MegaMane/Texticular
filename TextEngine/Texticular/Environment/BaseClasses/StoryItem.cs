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
            Player player = controller.game.Player;

            if (LocationKey != player.LocationKey)
            {
                if (LocationKey == "inventory")
                {
                    controller.InputResponse.AppendFormat($"You already have the item {Name}.\n");
                }

                else
                {
                    controller.InputResponse.AppendFormat($"There is no {Name} here to take.\n");
                }
                
                return;
            }

            if (IsPortable)
            {
                if (player.BackPack.ItemCount < player.BackPack.Slots)
                {
                    LocationKey = "inventory";
                    controller.InputResponse.AppendFormat($"{Name} taken.\n");
                    player.BackPack.ItemCount += 1;
                }

                else
                {
                    controller.InputResponse.AppendFormat($"You don't have any space for {Name} in your inventory! Try dropping something you don't need.\n");
                }
            }

            else
            {
                controller.InputResponse.AppendFormat($"You try to take {Name} but it won't budge!\n");
            }
            
        }

    }
}
