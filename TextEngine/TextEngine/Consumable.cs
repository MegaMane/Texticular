using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class Consumable: StoryItem
    {
        public bool IsEdible;
        public int healthBoost;


        public Consumable(int id, string name, string description, int locationid, String ExamineResponse, int healthBoost, bool portable = true, int weight = 0) :
            base(id, name, description, locationid, ExamineResponse, portable,weight) 
        { 
            this.IsEdible = true;
            this.healthBoost = healthBoost;
        }
        
    }
}
