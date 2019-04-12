using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Actors;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Chest : Container
    {


        public virtual bool IsLocked { get; set; } 

        public Key Key { get; set; }



        public Chest(string locationKey, string name, string description, string keyValue = "", int maxSlots = 10, string contextualDescription = "")
            : base(locationKey,name, description, keyValue, maxSlots, contextualDescription)
        {
            IsOpen = false;
            IsLocked = false;
        }


        public override string Open(GameObject target)
        {
            ActionResponse.Clear();

            if (IsLocked)
            {
                ActionResponse.Append($"The {Name} is securely locked. You need the key\n\n ");
            }

            else
            {
                IsOpen = true;
                ActionResponse.Append($"You open the {Name} and look inside...\n\n ");
                foreach (StoryItem item in Items)
                {
                    ActionResponse.Append($"{item.Name}: {item.Description}\n ");
                }

            }

            return ActionResponse.ToString();
        }






        public virtual string Unlock(Key key)
        {
            ActionResponse.Clear();

            if (key.LocationKey == "inventory" && key.KeyValue == this.Key.KeyValue)
            {
                Player player = GameObject.GetComponent<Player>("player");
                player.BackPack.RemoveItem(Key);
                ActionResponse.Append(Key.Consume());
                ActionResponse.Append($"{Name} is unlocked...");
                IsLocked = false;


            }

            else
            {
                ActionResponse.Append("You don't have the right key.");
            }

            return ActionResponse.ToString();
        }


    }

}
