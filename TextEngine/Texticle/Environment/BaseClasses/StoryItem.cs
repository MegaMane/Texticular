using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;


namespace Texticle.Environment
{
    public class StoryItem : GameObject
    {
        public bool IsPortable { get; set; }
        public int Weight { get; set; }


        //The description that should be used inside the room.
        //The item is on the bed the item is on the floor etc.
        public string ContextualDescription { get; set; }
        public int SlotsOccupied { get; private set; }
        private string _locationKey;
        public override string LocationKey
        {
            get { return _locationKey; }
            set
            {

                OnLocationChanged(_locationKey, value);

                _locationKey = value;

                //once an object is moved from it's origin location simplify its 
                //contextual description.
                ContextualDescription = $"There is a {Name} here.";


            }
        }

        public event OnItemLocationChanged LocationChanged;

        protected virtual void OnLocationChanged(string currentLocation, string newLocation)
        {
            if (LocationChanged != null)
            {
                ItemLocationChangedEventArgs args = new ItemLocationChangedEventArgs();
                args.CurrentLocation = currentLocation;
                args.NewLocation = newLocation;
                LocationChanged(this, args);
            }
        }

        public StoryItem():this("storyItem","storyItem")
        {

        }

        public StoryItem(string name, string description, string locationKey=null, bool isPortable = true, string examineResponse="", int weight = 0, string keyValue="", string contextualDescription="", int slotsOccupied=1) 
            :base(name, description, keyValue)
        {
            _locationKey = locationKey;
            IsPortable = isPortable;
            ExamineResponse = examineResponse ==""? description: examineResponse;
            Weight = weight;
            ContextualDescription = contextualDescription == ""? ExamineResponse: contextualDescription;
            SlotsOccupied = slotsOccupied;

            Commands["take"] = Take;
            Commands["drop"] = Drop;
            Commands["put"] = Put;


            
        }

        public override string ToString()
        {
            return base.ToString() + $"Contextual Description: {ContextualDescription}\n";;
        }


        public virtual string Take(GameObject target)
        {
            ActionResponse.Clear();

            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;

            if (LocationKey != player.LocationKey)
            {


                if (LocationKey == player.BackPack.KeyValue)
                {
                    ActionResponse.Append($"You already have the item {FullName}.\n");
                    return ActionResponse.ToString();
                }


                //if the item is inside an open container at the players current location
                foreach (var item in currentLocation)
                {
                    if(item is Container && this.LocationKey == item.KeyValue && (item as Container).IsOpen)
                    {
                        Container chest = (Container)item;

                        if ((player.BackPack.MaxSlots - player.BackPack.SlotsFull) >= this.SlotsOccupied)
                        {
                            chest.RemoveItem(this);
                            player.BackPack.AddItem(this);
                            ActionResponse.Append($"{FullName} taken.\n");
                            
                        }

                        else
                        {
                            ActionResponse.Append($"You don't have any space for {FullName} in your inventory! Try dropping something you don't need.\n");
                        }

                        return ActionResponse.ToString();
                    }
                }


                ActionResponse.Append($"There is no {Name} here to take.\n");
                return ActionResponse.ToString();
            }

            if (IsPortable)
            {
                if ((player.BackPack.MaxSlots - player.BackPack.SlotsFull) >= this.SlotsOccupied)
                {
                    player.PlayerLocation.RemoveItem((GameObject)this);
                    player.BackPack.AddItem(this);
                    ActionResponse.Append($"{FullName} taken.\n");
                   
                }

                else
                {
                    ActionResponse.Append($"You don't have any space for {FullName} in your inventory! Try dropping something you don't need.\n");
                }
            }

            else
            {
                ActionResponse.Append($"You try to take {FullName} but it won't budge!\n");
            }

            return ActionResponse.ToString();
            
        }


        public virtual string Drop(GameObject target=null)
        {
            ActionResponse.Clear();

            Player player = GameObject.GetComponent<Player>("player");

            if (LocationKey == "inventory")
            {

                player.BackPack.RemoveItem(this);
                player.PlayerLocation.AddItem(this);
                ActionResponse.Append($"You dropped the {Name} like it's hot.\n");
            }

            else
            {
                ActionResponse.Append($"You don't have a {Name} to drop.\n");
            }

            return ActionResponse.ToString();
        }

        public virtual string Put(GameObject target)
        {

            ActionResponse.Clear();
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;
            Container inventory = player.BackPack;



            //check if the direct object is in the players possesion or the current room
            if (LocationKey == player.BackPack.KeyValue || LocationKey == currentLocation.KeyValue)
            {
                //check if the indirect object is in the room and not locked
                if (target.LocationKey == currentLocation.KeyValue)
                {
                    if (target is Container)
                    {
                        Container chest = (Container)target;
                        if ((chest as Chest).IsLocked)
                        {
                            ActionResponse.Append($"The {target.Name} is locked.");
                        }

                        else
                        {
                            //check if there is enough space in the container
                            bool itemAdded = chest.AddItem(this);

                            //if so then change the items location to the container and let the player know
                            if (itemAdded) ActionResponse.Append($"You put the {this.Name} in the {target.Name}");
                            //if not let the player know the item did not fit
                            else ActionResponse.Append($"The {this.Name} doesn't fit in the {target.Name}");
                        }
                    }

                }

                else
                {
                    ActionResponse.Append($"I don't see {target.Name} here.");
                }

            }

            else
            {
                ActionResponse.Append($"There is no {this.Name} here!");
            }


            return ActionResponse.ToString();

        }



    }
}
