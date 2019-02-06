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
        public bool IsEdible { get; set; }
        public int HealthBoost { get; set; }
        public String ExamineResponse { get; set; }
        public int Weight { get; set; }
        //actions verb => resulting action 
        public Dictionary<string, string> Actions;
        public ItemType TypeOfItem;


        public StoryItem(string name, string description) :base(name, description)
        {
            this.IsPortable = false;
            this.ExamineResponse = description;
            this.Weight = 999;
            Actions = new Dictionary<string, string>();
        }

        [JsonConstructor]
        public StoryItem(string locationKey, string name, string description, string typeOfItem, bool isPortable, string examine, int weight = 0 ) : base(name, description)
        {
            LocationKey = locationKey;
            IsPortable = isPortable;
            ExamineResponse = examine;
            Weight = weight;
            TypeOfItem = (ItemType)Enum.Parse(typeof(ItemType), typeOfItem);
            //Actions = new Dictionary<string, string>();
        }

        
    }
}
