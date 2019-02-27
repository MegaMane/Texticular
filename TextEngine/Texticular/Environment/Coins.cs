using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Environment
{
    public class Coins:StoryItem
    {
        int value { get; set; }


        public Coins(string locationKey, string name, string description, string examineResponse = "", bool isPortable = true, int weight = 0, string keyValue = "")
            : base(name, description, locationKey, isPortable, examineResponse, weight, keyValue)
        {
            Commands["use"] = useCoins;
        }

        public void useCoins(GameController controller)
        {
            GameController.InputResponse.AppendFormat($"You use the {Name}.\n");
        }
    }
}
