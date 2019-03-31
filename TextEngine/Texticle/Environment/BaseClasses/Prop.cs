using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;


namespace Texticle.Environment
{
    public class Prop: GameObject, IExaminable
    {

        //public string LocationKey { get; private set; }
        public string ExamineResponse { get; set; }


        public Prop (string name, string description, string locationKey, string keyValue = "", string examineResponse="")
            :base(name, description, keyValue)
        {
            LocationKey = locationKey;
            ExamineResponse = examineResponse;
        }

        public void Examine()
        {
            throw new NotImplementedException();
        }

        public void Look()
        {
            throw new NotImplementedException();
        }

        void TakeProp(ParseTree tokens)
        {
            GameLog.Append($"The {Name} won't budge.");
        }



    }


}

