using System.Collections.Generic;
using Texticle.Environment;
using Texticle.Engine;
using System.Collections;

namespace Texticle.Environment
{
    public class Inventory:Container
    {



        public Inventory():
            base(locationKey:"player",name: "Inventory", description:"trusty black backback", keyValue:"inventory")

        {
            MaxSlots = 20;
            IsOpen = true;
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

        public override void Close()
        {
            GameLog.Append("You zip up your backpack tight.\n\n ");
        }



    }
}