using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Environment
{
    class DoorKey:StoryItem
    {
        public DoorKey(string locationKey, string name, string description, string examineResponse="", bool isPortable = true, int weight=0, string keyValue = "") 
            :base(name, description,locationKey, isPortable, examineResponse, weight, keyValue)
        {
            Commands["use"] = useKey;
        }

        void useKey(GameController controller)
        {
            Player player = controller.game.Player;

            if (LocationKey == "inventory")
            {
                //if the key is in the players possesion
                //the key is taken from the players inventory
                //the player is moved to the destination for the door

            }

            else if (LocationKey == player.LocationKey)
            {

                controller.InputResponse.Append("You need to be holding the key to use it");

            }

            else
            {
                controller.InputResponse.Append("Keep searching...You don't have that item\n");

            }

        }
    }
}
