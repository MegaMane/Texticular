using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class ItemKey: StoryItem
    {
        public bool IsPortable { get; set; }
        public String ExamineResponse { get; set; }
        public int Weight { get; set; }
        public Dictionary<string, string> actions;


        public ItemKey(int id, string name, string description, int locationid) :
             base(id, name, description, locationid, null, true, 0)
        {
            IsPortable = true;
            Weight = 0;
            actions = new Dictionary<string, string>();
        }

        public ItemKey(int id, string name, string description, int locationid, String ExamineResponse, bool portable = true, int weight = 0) :
             base(id, name, description, locationid, ExamineResponse, portable, weight)
        {
            this.IsPortable = portable;
            this.ExamineResponse = ExamineResponse;
            this.Weight = weight;
            actions = new Dictionary<string, string>();
        }


    }
}
