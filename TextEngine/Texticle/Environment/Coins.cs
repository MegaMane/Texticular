using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class Coins:StoryItem
    {
        int Value { get; set; }


        public Coins(string locationKey, string name, string description, int value, string examineResponse = "", bool isPortable = true, int weight = 0, string keyValue = "")
            : base(name, description, locationKey, isPortable, examineResponse, weight, keyValue)
        {
            Value = value;
        }

        //public void useCoins(ParseTree tokens)
        //{
        //    GameController.InputResponse.AppendFormat($"You use the {Name}.\n");
        //}
    }
}
