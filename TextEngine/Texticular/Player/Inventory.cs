namespace Texticular
{
    public class Inventory:Room
    {
        public int Slots { get; set; }
        public int ItemCount { get; set; }


        public Inventory(string keyValue, string name, string description, int slots, int itemCount, int timeVisited = 0):
            base(name, description, keyValue, timeVisited)
        {
            this.Slots = slots;
            this.ItemCount = itemCount;
        }

    }
}