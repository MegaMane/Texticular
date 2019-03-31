using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticle.Environment
{
    public class Key:StoryItem, IConsumable
    {
        
        public Key(string name, string description, string locationKey, string examineResponse = "", string keyValue = "", string contextualDescription = "", string consumeText="")
            : base(name, description, locationKey, isPortable: true, examineResponse: examineResponse, weight: 0, keyValue: keyValue, contextualDescription: contextualDescription)
        {
            ConsumeText = consumeText;
        }

        public string ConsumeText { get; set; }
        public void Consume()
        {
            GameLog.Append(ConsumeText);
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
                    if (door.Key.KeyValue == this.KeyValue)
                    {

                        player.BackPack.RemoveItem(this);
                        this.LocationKey = door.KeyValue;
                        this.Consume();
                        GameLog.Append($"{door.Name} opens...");
                        door.IsLocked = false;

                        //Room destination = GameObject.GetComponent<Room>(door.DestinationKey);
                        //player.PlayerLocation = destination;
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
