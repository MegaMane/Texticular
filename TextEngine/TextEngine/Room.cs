namespace TextEngine
{
    public class Room:GameObject
    {
        static int roomCount = 0;
        public string AltDescription { get; set; }
        public int TimesVisited { get; set; }
        public Exit[] Exits;

        public Room(int id, string name, string description):
            base(id, name, description)
        {
            roomCount += 1;
            TimesVisited = 0;
            Exits = new Exit[8];
            Exits[0] = new Exit(-1, this.ID, Direction.North, null);
            Exits[1] = new Exit(-1, this.ID, Direction.Northeast, null);
            Exits[2] = new Exit(-1, this.ID, Direction.East, null);
            Exits[3] = new Exit(-1, this.ID, Direction.Southeast, null);
            Exits[4] = new Exit(-1, this.ID, Direction.South, null);
            Exits[5] = new Exit(-1, this.ID, Direction.Southwest, null);
            Exits[6] = new Exit(-1, this.ID, Direction.West, null);
            Exits[7] = new Exit(-1, this.ID, Direction.Northwest, null);

        }


        public Room(int id, string name, string description, Exit[] Exits) :
            base(id, name, description)
        {
            roomCount += 1;
            this.Exits = Exits;
            TimesVisited = 0;
        }


        public Room(int id, string name, string description, string AltDescription="", int TimesVisited = 0) :
            base(id, name, description)
        {
            roomCount += 1;
            this.TimesVisited = TimesVisited;
            this.AltDescription = AltDescription;
            Exits = new Exit[8];
            Exits[0] = new Exit(-1, this.ID, Direction.North, null);
            Exits[1] = new Exit(-1, this.ID, Direction.Northeast, null);
            Exits[2] = new Exit(-1, this.ID, Direction.East, null);
            Exits[3] = new Exit(-1, this.ID, Direction.Southeast, null);
            Exits[4] = new Exit(-1, this.ID, Direction.South, null);
            Exits[5] = new Exit(-1, this.ID, Direction.Southwest, null);
            Exits[6] = new Exit(-1, this.ID, Direction.West, null);
            Exits[7] = new Exit(-1, this.ID, Direction.Northwest, null);
        }


        public Room(int id, string name, string description, Exit[] Exits, string AltDescription, int TimesVisited =0) :
            base(id, name, description)
        {
            roomCount += 1;
            this.Exits = Exits;
            this.TimesVisited = TimesVisited;
            this.AltDescription = AltDescription;
        }



        public void AddExit(Exit exit)
        {
            this.Exits[(int)exit.ExitPosition] = exit;
        }
    }
}