using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;


namespace Texticle.Environment
{
    public class Prop: GameObject, IViewable, ITakeable
    {

        //public string LocationKey { get; private set; }
        public string ExamineResponse { get; set; }


        public Prop (string name, string description, string locationKey, string keyValue = "", string examineResponse="")
            :base(name, description, keyValue)
        {
            LocationKey = locationKey;
            ExamineResponse = examineResponse;
        }

        public string Examine()
        {
            throw new NotImplementedException();
        }

        public string Look()
        {
            throw new NotImplementedException();
        }

        public string Take()
        {
            ActionResponse.Clear();
            ActionResponse.Append($"The {Name} won't budge.");
            return ActionResponse.ToString();
        }

        public string Put(string target)
        {
            return Take();
        }



    }


}

