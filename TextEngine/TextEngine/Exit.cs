using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class Exit:GameObject
    {
        public Direction ExitPosition;
        public Room Destination;
        public bool IsLocked;
        public ItemKey ExitKey;

        public Exit(int id, int locationid, Direction ExitPosition, Room Destination, string name="Exit", string description = "Exit") :
            base(id, name, description, locationid)
        {
            this.ExitPosition = ExitPosition;
            this.Destination = Destination;
            IsLocked = false;


        }

        public Exit(int id, string name, string description, int locationid, Direction ExitPosition, Room Destination, ItemKey exitKey, bool IsLocked = false) :
            base(id, name, description, locationid)
        {
            this.ExitPosition = ExitPosition;
            this.Destination = Destination;
            this.IsLocked = IsLocked;
            ExitKey = exitKey;

        }

    }
}
