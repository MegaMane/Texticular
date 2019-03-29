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
    public class Chest : Container, IUnlockable
    {


        public virtual bool IsLocked { get; set; } 

        public Key Key { get; set; }



        public Chest(string locationKey, string name, string description, string keyValue = "", int maxSlots = 10, string contextualDescription = "")
            : base(locationKey,name, description, keyValue, maxSlots, contextualDescription)
        {
            IsOpen = false;
            IsLocked = false;
        }


        public override void Open()
        {
            if (IsLocked)
            {
                GameLog.Append($"The {Name} is securely locked. You need the key\n\n ");
            }

            else
            {
                IsOpen = true;
                GameLog.Append($"You open the {Name} and look inside...\n\n ");
                foreach (StoryItem item in Items)
                {
                    GameLog.Append($"{item.Name}: {item.Description}\n ");
                }

            }


        }






        public virtual void Unlock(Key key)
        {
            if (key.LocationKey == "inventory" && key.KeyValue == this.Key.KeyValue)
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
