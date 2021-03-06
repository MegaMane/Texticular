﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.GameEngine;

namespace Texticular.Environment
{
    public class DoorKey:StoryItem
    {
        public DoorKey(string locationKey, string name, string description, string examineResponse="", bool isPortable = true, int weight=0, string keyValue = "") 
            :base(name, description,locationKey, isPortable, examineResponse, weight, keyValue)
        {
            Commands["use"] = useKey;
        }

        void useKey(ParseTree tokens)
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;


            if (LocationKey == "inventory")
            {
                //the key is taken from the players inventory
                //the player is moved to the destination for the door
                foreach (Exit door in currentLocation.Exits.Values)
                {
                    if (door.KeyName.ToLower() == this.Name.ToLower())
                    {
                        Room destination = GameObject.GetComponent<Room>(door.DestinationKey);
                        GameController.InputResponse.Append($"{door.Name} opens...");
                        door.IsLocked = false;
                        player.BackPack.ConsumeItem(this);
                        player.PlayerLocation = destination;
                        return;
                    }
                }

                //their are no doors the key opens in the current room
                GameController.InputResponse.Append($"{Name} doesn't fit into any of the locks.");


            }

            else if (LocationKey == player.LocationKey)
            {

                GameController.InputResponse.Append("You need to be holding the key to use it");

            }

            else
            {
                GameController.InputResponse.Append("Keep searching...You don't have that item\n");

            }

        }
    }
}
