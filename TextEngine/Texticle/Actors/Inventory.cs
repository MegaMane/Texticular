using System.Collections.Generic;
using Texticle.Environment;

namespace Texticle.Actors
{
    public class Inventory:Container
    {



        public Inventory():
            base(locationKey:"player", name:"Inventory", description:"trusty black backback", keyValue:"inventory")

        {

        }



    }
}