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
        public string DescriptionInRoom { get; set; }


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

            Commands["drop"] = dropItem;

            Commands["put"] = putItem;
        }



        void takeItem(GameController controller)
        {
            Player player = controller.Game.Player;

            if (LocationKey != player.LocationKey)
            {
                if (LocationKey == "inventory")
                {
                    controller.InputResponse.AppendFormat($"You already have the item {Name}.\n");
                }

                //if the item is in a container and the container is in the players current location
                else if (controller.Game.Items.ContainsKey(LocationKey))
                {
                    if(controller.Game.Items[LocationKey] is Container && controller.Game.Items[LocationKey].LocationKey == player.LocationKey)
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

        void dropItem(GameController controller)
        {
            Player player = controller.Game.Player;

            if (LocationKey == "inventory")
            {
                LocationKey = player.PlayerLocation.KeyValue;
                controller.InputResponse.AppendFormat($"You dropped the {Name} like it's hot.\n");
                player.BackPack.ItemCount -= 1;
            }

            else
            {
                controller.InputResponse.AppendFormat($"You don't have a {Name} to drop.\n");
            }
        }

        void putItem(GameController controller)
        {
            //check the target after the word in/on
            //if it's a container check if it's open and put the item in
            controller.InputResponse.AppendFormat($"put {Name} in the target?\n");

        }

    }
}
