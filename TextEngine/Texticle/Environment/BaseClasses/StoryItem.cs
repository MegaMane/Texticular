using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;


namespace Texticle.Environment
{
    public class StoryItem : GameObject, ITakeable, IDropable,IExaminable
    {
        public bool IsPortable { get; set; }
        public int Weight { get; set; }

        //The item is on the bed the item is on the floor etc.
        public string ContextualDescription { get; set; }
        public int SlotsOccupied { get; private set; }
        public string ExamineResponse { get; set; }
        private string _locationKey;
        public override string LocationKey
        {
            get { return _locationKey; }
            set
            {

                OnLocationChanged(_locationKey, value);

                _locationKey = value;


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


        public StoryItem(string name, string description, string locationKey=null, bool isPortable = true, string examineResponse="", int weight = 0, string keyValue="", string contextualDescription="", int slotsOccupied=1) 
            :base(name, description, keyValue)
        {
            _locationKey = locationKey;
            IsPortable = isPortable;
            ExamineResponse = examineResponse ==""? description: examineResponse;
            Weight = weight;
            ContextualDescription = contextualDescription == ""? ExamineResponse: contextualDescription;
            SlotsOccupied = slotsOccupied;
            
        }

        public override string ToString()
        {
            return base.ToString() + $"Contextual Description: {ContextualDescription}\n";;
        }



        public virtual void Take()
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;

            if (LocationKey != player.LocationKey)
            {


                if (LocationKey == player.BackPack.KeyValue)
                {
                    GameLog.InputResponse.AppendFormat($"You already have the item {Name}.\n");
                    return;
                }


                //if the item is inside an open container at the players current location
                foreach (var item in currentLocation)
                {
                    if(item is Container && this.LocationKey == item.KeyValue && (item as Container).IsOpen)
                    {
                        Container chest = (Container)item;

                        if ((player.BackPack.MaxSlots - player.BackPack.SlotsFull) <= this.SlotsOccupied)
                        {
                            chest.RemoveItem(this);
                            player.BackPack.AddItem(this);
                            GameLog.InputResponse.AppendFormat($"{Name} taken.\n");
                            
                        }

                        else
                        {
                            GameLog.InputResponse.AppendFormat($"You don't have any space for {Name} in your inventory! Try dropping something you don't need.\n");
                        }

                        return;
                    }
                }


                GameLog.InputResponse.AppendFormat($"There is no {Name} here to take.\n");
                return;
            }

            if (IsPortable)
            {
                if ((player.BackPack.MaxSlots - player.BackPack.SlotsFull) <= this.SlotsOccupied)
                {
                    player.PlayerLocation.RemoveItem((GameObject)this);
                    player.BackPack.AddItem(this);
                    GameLog.InputResponse.AppendFormat($"{Name} taken.\n");
                   
                }

                else
                {
                    GameLog.InputResponse.AppendFormat($"You don't have any space for {Name} in your inventory! Try dropping something you don't need.\n");
                }
            }

            else
            {
                GameLog.InputResponse.AppendFormat($"You try to take {Name} but it won't budge!\n");
            }
            
        }


        public virtual void Drop()
        {
            throw new NotImplementedException();
        }

        public virtual void Put(string target)
        {
            //call targets target.AddItem(this) method and if it returns false let the player know it won't fit
            throw new NotImplementedException();
        }

        public void Look()
        {
            throw new NotImplementedException();
        }

        public void Examine()
        {
            throw new NotImplementedException();
        }
        /*
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
*/

    }
}
