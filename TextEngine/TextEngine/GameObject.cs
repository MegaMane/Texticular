using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public abstract class GameObject
    {
        public int ID { get; private set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int LocationID { get; set; }

        public GameObject(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        public GameObject(int id, string name, string description, int locationid)
        {
            ID = id;
            Name = name;
            Description = description;
            LocationID = locationid;
        }
    }
}
