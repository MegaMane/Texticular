using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;


namespace Texticle.Environment
{
    public class Prop: GameObject
    {



        public Prop (string name, string description, string locationKey, string keyValue = "", string examineResponse="")
            :base(name, description, keyValue)
        {
            LocationKey = locationKey;
            ExamineResponse = examineResponse;

            Commands["take"] = Take;
            Commands["put"] = Put;
        }


        public string Take(GameObject target=null)
        {
            ActionResponse.Clear();
            ActionResponse.Append($"The {Name} won't budge.");
            return ActionResponse.ToString();
        }

        public string Put(GameObject target)
        {
            if (target == null)
            {
                return "Put it where?";
            }
            else
                return Take();
        }



    }


}

