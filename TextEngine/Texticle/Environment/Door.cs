using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Actors;
using Texticle.Engine;


namespace Texticle.Environment
{
    public class Door : GameObject, IUnlockable
    {
        public string DestinationKey;
        public bool IsLocked;
        public Key Key{ get; set; }


        //public Exit(string locationKey, Direction exitPosition, string destinationKey, string name = "Exit", string description = "Exit") :
        //    base(name, description)
        //{
        //    LocationKey = locationKey;
        //    ExitPosition = exitPosition;
        //    DestinationKey = destinationKey;
        //    IsLocked = false;
        //}

        //public Exit(string locationKey, string destinationKey, bool isLocked, string keyName="none", string name = "Exit", string description = "Exit", string keyValue="") 
        //    :base (name, description)
        //{
        //    LocationKey = locationKey;
        //    KeyValue = keyValue;
        //    DestinationKey = destinationKey;
        //    IsLocked = isLocked;
        //    KeyName = keyName;

        //    Commands["open"] = openDoor;
        //    Commands["unlock"] = openDoor;
        //}

        //void openDoor(ParseTree tokens)
        //{
        //    Player player = GameObject.GetComponent<Player>("player");
        //    Room currentLocation = player.PlayerLocation;


        //    if (player.LocationKey != this.LocationKey)
        //    {
        //        GameController.InputResponse.Append("I don't see that door.\n");
        //        return;
        //    }

        //    //If the door is locked check to see if the player has the correct key in inventory
        //    if (IsLocked)
        //    {
        //        Inventory inventory = GameObject.GetComponent<Inventory>("inventory");
        //        DoorKey doorKey = null;

        //        foreach (StoryItem item in inventory.RoomItems)
        //        {
        //            if(item.Name == this.KeyName)
        //            {
        //                doorKey = (DoorKey)item;
        //                break;
        //            }
        //        }
                
        //       if (doorKey != null)
        //        {
        //            GameController.InputResponse.Append($"{this.Name} opens...\n");
        //            Room destination = GameObject.GetComponent<Room>(DestinationKey);
        //            IsLocked = false;
        //            player.BackPack.ConsumeItem(doorKey);
        //            player.PlayerLocation = destination;
        //            return;
        //        }

        //       else
        //        {
        //            GameController.InputResponse.Append("You don't have the key\n");
        //        }
        //    }
        //        //if the player has the correct key in inventory
            
        //}

        public override string ToString()
        {
            return base.ToString() + $"Destination:{DestinationKey}\nIsLocked: {IsLocked.ToString()}\nKeyName: {Key.Name}\n\n";
            //base code below
            //return $"{this.GetType().Name}\n----------------------\nGame ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: {LocationKey}\n";
        }

        public void Unlock(Key key)
        {
            if(key.LocationKey == "inventory" && key.KeyValue == this.Key.KeyValue)
            {
                Player player = GameObject.GetComponent<Player>("player");
                player.BackPack.RemoveItem(Key);
                Key.Consume();
                GameLog.Append($"{Name} opens...");
                IsLocked = false;

            }

            else
            {
                GameLog.Append("You don't have the right key.");
            }
        }
    }
}
