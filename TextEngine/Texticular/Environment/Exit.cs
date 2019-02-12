using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Texticular.Environment
{
    public class Exit : GameObject
    {
        public string DestinationKey;
        public bool IsLocked;
        public string KeyName;


        //public Exit(string locationKey, Direction exitPosition, string destinationKey, string name = "Exit", string description = "Exit") :
        //    base(name, description)
        //{
        //    LocationKey = locationKey;
        //    ExitPosition = exitPosition;
        //    DestinationKey = destinationKey;
        //    IsLocked = false;
        //}

        [JsonConstructor]
        public Exit(string locationKey, string destinationKey, bool isLocked, string keyName="none", string name = "Exit", string description = "Exit", string KeyValue="") 
            :base(name, description, locationKey, KeyValue)
        {
            DestinationKey = destinationKey;
            IsLocked = isLocked;
            KeyName = keyName;


        }

        public override string ToString()
        {
            return base.ToString() + $"Destination:{DestinationKey}\nIsLocked: {IsLocked.ToString()}\nKeyName: {KeyName}\n\n";
            //base code below
            //return $"{this.GetType().Name}\n----------------------\nGame ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: {LocationKey}\n";
        }


    }
}
