using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticle.Environment
{
    public class DoorKey:StoryItem, IConsumable
    {
        public DoorKey(string name, string description, string locationKey, string examineResponse = "", string keyValue = "", string contextualDescription = "")
            : base(name, description, locationKey, isPortable: true, examineResponse: examineResponse, weight: 0, keyValue: keyValue, contextualDescription: contextualDescription)
        {

        }

        public void Consume()
        {
            GameObject.Consume(this.KeyValue);
        }

        void UseKey()
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;


            if (LocationKey == "inventory")
            {
                //the key is taken from the players inventory
                //the player is moved to the destination for the door
                foreach (Door door in currentLocation.Exits.Values)
                {
                    if (door.Key.Name.ToLower() == this.Name.ToLower())
                    {
                        Room destination = GameObject.GetComponent<Room>(door.DestinationKey);
                        player.BackPack.RemoveItem(this);
                        this.Consume();
                        GameLog.Append($"{door.Name} opens...");
                        door.IsLocked = false;

                        player.PlayerLocation = destination;
                        return;
                    }
                }

                //their are no doors the key opens in the current room
                GameLog.Append($"{Name} doesn't fit into any of the locks.");


            }

            else if (LocationKey == player.LocationKey)
            {

                GameLog.Append("You need to be holding the key to use it");

            }

            else
            {
                GameLog.Append("Keep searching...You don't have that item\n");

            }

        }
    }
}
