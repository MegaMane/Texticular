using System.Collections.Generic;
using Texticle.Environment;
using Texticle.Engine;

namespace Texticle.Actors
{
    public class Inventory:Container
    {

        public new bool IsOpen { get; private set; }
        public new bool IsLocked { get; private set; }


        public Inventory():
            base(locationKey:"player", name:"Inventory", description:"trusty black backback", keyValue:"inventory")

        {
            IsLocked = false;
            IsOpen = true;
            MaxSlots = 20;
        }


        public override void Open()
        {


            GameLog.Append("\n Inventory\n ");
            GameLog.Append("------------------------------------------------------\n\n ");

            foreach (var item in Items)
            {

                GameLog.Append($"{item.Name} : {item.Description}\n ");
                
            }

            GameLog.Append("\n ");
        }

        public override void Lock()
        {
            GameLog.Append("You zip you backpack up tight.\n ");
        }

        public override void Unlock()
        {
            Open();
        }

    }
}