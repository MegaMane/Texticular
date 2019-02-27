using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Texticular.Environment
{
    public class Exit : StoryItem
    {
        public string DestinationKey;
        public bool IsLocked;
        public string KeyName;


        //public Exit(string locationKey, Direction exitPosition, string destinationKey, string name = "Exit", string description = "Exit") :
        //    base(name, description)
        //{
        //    LocationKey = locationKey;
        //    ExitPosition = exitPosition;
        //    DestinationKey = destinationKey;
        //    IsLocked = false;
        //}

        [JsonConstructor]
        public Exit(string locationKey, string destinationKey, bool isLocked, string keyName="none", string name = "Exit", string description = "Exit", string keyValue="") 
            :base (name, description)
        {
            LocationKey = locationKey;
            KeyValue = keyValue;
            DestinationKey = destinationKey;
            IsLocked = isLocked;
            KeyName = keyName;

            Commands["open"] = openDoor;
            Commands["unlock"] = openDoor;
        }

        void openDoor(GameController controller)
        {
            Player player = controller.Game.Player;

            if (player.LocationKey != this.LocationKey)
            {
                GameController.InputResponse.Append("I don't see that door.\n");
                return;
            }

            if (IsLocked)
            {
               DoorKey doorKey = (DoorKey)controller.checkInventory(new string[] {KeyName});
               if (doorKey != null)
                {
                    GameController.InputResponse.Append($"{this.Name} opens...\n");
                    IsLocked = false;
                    player.BackPack.ConsumeItem(doorKey);
                    player.PlayerLocation = controller.Game.Rooms[DestinationKey];
                    controller.Parse("look");
                    return;
                }

               else
                {
                    GameController.InputResponse.Append("You don't have the key\n");
                }
            }
                //if the player has the correct key in inventory
            
        }

        public override string ToString()
        {
            return base.ToString() + $"Destination:{DestinationKey}\nIsLocked: {IsLocked.ToString()}\nKeyName: {KeyName}\n\n";
            //base code below
            //return $"{this.GetType().Name}\n----------------------\nGame ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: {LocationKey}\n";
        }


    }
}
