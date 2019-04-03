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


        public override string Open()
        {
            ActionResponse.Clear();

            ActionResponse.Append("\n Inventory\n ");
            ActionResponse.Append("------------------------------------------------------\n\n ");

            foreach (var item in Items)
            {

                ActionResponse.Append($"{item.Name} : {item.Description}\n ");
                
            }

            ActionResponse.Append("\n ");

            return ActionResponse.ToString();
        }

        public override string Close()
        {
            ActionResponse.Clear();

            ActionResponse.Append("You zip up your backpack tight.\n\n ");

            return ActionResponse.ToString();
        }



    }
}