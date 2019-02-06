using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class StoryItem:GameObject
    {
        public bool IsPortable { get; set; }
        public String ExamineResponse { get; set; }
        public int Weight { get; set; }
        public Dictionary <string, string> actions;
        

        public StoryItem(int id, string name, string description, int locationid, String ExamineResponse, bool portable = true, int weight = 0):
            base(id, name, description, locationid)
        {
            this.IsPortable = portable;
            this.ExamineResponse = ExamineResponse;
            this.Weight = weight;
            actions = new Dictionary<string, string>();
        }
    }
}
