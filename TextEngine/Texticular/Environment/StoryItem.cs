using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Texticular
{
    public class StoryItem : GameObject
    {
        public bool IsPortable { get; set; }
        public int Weight { get; set; }

        Dictionary<string, Action<GameController>> commands;
        public Dictionary<string, Action<GameController>> Commands { get { return commands; } protected set { commands = value; } }


        public StoryItem(string name, string description) :base(name, description)
        {
            commands = new Dictionary<string, Action<GameController>>();
            IsPortable = false;
            Weight = 999;
        }

        [JsonConstructor]
        public StoryItem(string name, string description, string locationKey = null, bool isPortable = false, string examine="", int weight = 0, string keyValue="") 
            :base(name, description, examine, locationKey, keyValue)
        {
            commands = new Dictionary<string, Action<GameController>>();
            IsPortable = isPortable;
            Weight = weight;
        }

        
    }
}
