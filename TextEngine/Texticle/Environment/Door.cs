using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Actors;
using Texticle.Engine;


namespace Texticle.Environment
{
    /// <summary>
    /// Door is a link between two Rooms or Game States
    /// the destination Room or State is determined based on the context
    /// of the players current location. Both Rooms reference the same door that connects them.
    /// </summary>
    public class Door : GameObject, IUnlockable, IOpenable
    {

        /// <summary>
        /// Reperesents a collection of destinatons using the Room KeyValue property 
        /// (unique Identifier) as its key and a Room (Destination) as its value.
        /// the correct destination can be determined based on the players location
        /// so the same door can be referenced by both the rooms it connects
        /// </summary>
        public Dictionary <string,Room> Destinations { get; set; }
        public bool IsLocked { get; set; }
        bool IsOpen = false;
        public Key Key{ get; set; }


        public Door() :
            base(name:"Exit", description:"Exit")
        {
            IsLocked = false;
            Key = null;
            Destinations = new Dictionary<string, Room>();
                 
        }

        public Door(Dictionary<string, Room> destinations, string name = "Exit", string description = "Exit", string keyValue = "", bool isLocked = false, Key key = null) :
            base(name, description, keyValue)
        {

            Destinations = destinations;
            IsLocked = isLocked;
            Key = key;

        }


        public string Open()
        {
            ActionResponse.Clear();

            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;
            Room destination;
            bool exitExists = Destinations.TryGetValue(currentLocation.KeyValue, out destination);

            if (! exitExists)
            {
                return "";
            }


            //If the door is locked check to see if the player has the correct key in inventory
            if (IsLocked)
            {

                Key doorKey=null;

                foreach (StoryItem item in player.BackPack)
                {
                    if (item.Name == this.Key.Name)
                    {
                        doorKey = (Key)item;
                        ActionResponse.Append(Unlock(doorKey));
                        break;
                    }
                }

                if (doorKey != null)
                {
                    IsOpen = true;
                    
                    player.PlayerLocation = destination;
                    
                }

                else
                {
                   ActionResponse.Append("You don't have the key\n");
                }

                return ActionResponse.ToString();
            }
            
            else
            {
                IsOpen = true;
                player.PlayerLocation = destination;
                return ActionResponse.ToString();
            }

        }


        public string Close()
        {
            ActionResponse.Clear();

            if(IsOpen)
            {
                ActionResponse.Append($"Wow! You really weren't raised in a barn. You politely close the {Name}. \n");
                IsOpen = false;
            }

            else
            {
                ActionResponse.Append($"The {Name} isn't open.\n");
            }

            return ActionResponse.ToString();
        }


        public string Unlock(Key key)
        {
            ActionResponse.Clear();

            if(key.LocationKey == "inventory" && key.KeyValue == this.Key.KeyValue)
            {
                Player player = GameObject.GetComponent<Player>("player");
                player.BackPack.RemoveItem(Key);
                Key.Consume();
                ActionResponse.Append($"{Name} opens...");
                IsLocked = false;

            }

            else
            {
                ActionResponse.Append("You don't have the right key.");
            }

            return ActionResponse.ToString();
        }


        public override string ToString()
        {
            string destinations = "--------------------Destinations-------------------\n\n";
            
            foreach(KeyValuePair<string, Room> link in Destinations)
            {
                destinations += $"Key: {link.Key} => Room Name: {link.Value.Name}\n";
            }
            
            return base.ToString() + destinations + $"\n\nIsLocked: {IsLocked.ToString()}\nKeyName: {Key.Name}\n\n";
            //base code below
            //return $"{this.GetType().Name}\n----------------------\nGame ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: {LocationKey}\n";
        }




    }
}
