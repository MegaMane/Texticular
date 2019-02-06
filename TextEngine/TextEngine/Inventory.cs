namespace TextEngine
{
    public class Inventory:Room
    {
        public int Slots { get; set; }
        public int ItemCount { get; set; }


        public Inventory(int id, string name, string description, int slots, int itemCount):
            base(id, name, description)
        {
            this.Slots = slots;
            this.ItemCount = itemCount;
        }

    }
}