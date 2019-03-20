using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Texticular.GameEngine;

namespace Texticular.Environment
{
    public class StoryItem : GameObject
    {
        public bool IsPortable { get; set; }
        public int Weight { get; set; }
        public string DescriptionInRoom { get; set; }
        public int SlotsOccupied { get; set; } = 1;


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



        void takeItem(ParseTree tokens)
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;

            if (LocationKey != player.LocationKey)
            {


                if (LocationKey == "inventory")
                {
                    GameController.InputResponse.AppendFormat($"You already have the item {Name}.\n");
                    return;
                }


                //if the item is inside an open container at the players current location
                foreach (StoryItem item in currentLocation.RoomItems)
                {
                    if(item is Container && this.LocationKey == item.KeyValue && (item as Container).IsOpen)
                    {
                        Container chest = (Container)item;

                        if (player.BackPack.ItemCount < player.BackPack.Slots)
                        {
                            chest.RemoveItem(this);
                            LocationKey = "inventory";
                            GameController.InputResponse.AppendFormat($"{Name} taken.\n");
                            player.BackPack.ItemCount += 1;
                        }

                        else
                        {
                            GameController.InputResponse.AppendFormat($"You don't have any space for {Name} in your inventory! Try dropping something you don't need.\n");
                        }

                        return;
                    }
                }


                GameController.InputResponse.AppendFormat($"There is no {Name} here to take.\n");
                return;
            }

            if (IsPortable)
            {
                if (player.BackPack.ItemCount < player.BackPack.Slots)
                {
                    LocationKey = "inventory";
                    player.PlayerLocation.RoomItems.Remove(this);
                    GameController.InputResponse.AppendFormat($"{Name} taken.\n");
                    player.BackPack.ItemCount += 1;
                }

                else
                {
                    GameController.InputResponse.AppendFormat($"You don't have any space for {Name} in your inventory! Try dropping something you don't need.\n");
                }
            }

            else
            {
                GameController.InputResponse.AppendFormat($"You try to take {Name} but it won't budge!\n");
            }
            
        }

        void dropItem(ParseTree tokens)
        {
            Player player = GameObject.GetComponent<Player>("player");

            if (LocationKey == "inventory")
            {
                LocationKey = player.PlayerLocation.KeyValue;
                player.PlayerLocation.RoomItems.Add(this);
                GameController.InputResponse.AppendFormat($"You dropped the {Name} like it's hot.\n");
                player.BackPack.ItemCount -= 1;
            }

            else
            {
                GameController.InputResponse.AppendFormat($"You don't have a {Name} to drop.\n");
            }
        }

        void putItem(ParseTree tokens)
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;
            Room inventory = player.BackPack;
            GameObject target = GameObject.GetComponent<GameObject>(tokens.IndirectObjectKeyValue);

            
            
           
            
            //check if the direct object is in the players possesion or the current room
            if (LocationKey == player.BackPack.KeyValue || LocationKey == currentLocation.KeyValue)
            {
                //check if the indirect object is in the room and not locked
                if(target.LocationKey == currentLocation.KeyValue)
                {
                    if(target is Container)
                    {
                        Container chest = (Container)target;
                        if (chest.IsLocked)
                        {
                            GameController.InputResponse.Append($"The {tokens.IndirectObject} is locked.");
                        }

                        else
                        {
                            //check if there is enough space in the container
                            bool itemAdded = chest.AddItem(this);

                            //if so then change the items location to the container and let the player know
                            if (itemAdded) GameController.InputResponse.Append($"You put the {this.Name} in the {tokens.IndirectObject}");
                            //if not let the player know the item did not fit
                            else GameController.InputResponse.Append($"The {this.Name} doesn't fit in the {tokens.IndirectObject}");
                        }
                    }

                }

            }
            //GameController.InputResponse.AppendFormat($"put {tokens.DirectObject} in the {tokens.IndirectObject}?\n");

        }

    }
}
