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
            Commands["Open"] = openDoor;
        }

        void openDoor(GameController controller)
        {
            //if the key is in the players possesion
                //if the player is in the same room as the door the key opens
                    //the key is taken from the players inventory
                    //the player is moved to the destination for the door
            //else the player needs to pick up the key first
        }
    }
}
