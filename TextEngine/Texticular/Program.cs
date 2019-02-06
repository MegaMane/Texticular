using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Newtonsoft.Json;
using System.IO;

namespace Texticular
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Room> rooms= new List<Room>();

            using (StreamReader file = File.OpenText(@"..\..\JsonFiles\JsonRoomTest.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                rooms = (List<Room>)serializer.Deserialize(file, typeof(List<Room>));
            }

            //products = JsonConvert.DeserializeObject<List<ProductJson>>(json);

            //fruits[1].SetPrice(99.00m);

            foreach (Room gameRoom in rooms)
            {
                Write(gameRoom.ToString());
                WriteLine();
                for(int i =0; i< gameRoom.Exits.Count; i++)
                {
                    Write(gameRoom.Exits[i].ToString());
                }
                WriteLine();
                for (int i = 0; i < gameRoom.Items.Count; i++)
                {
                    Write(gameRoom.Items[i].ToString());
                }
            }

            Console.Read();

            JsonSerializer Saveserializer = new JsonSerializer();
            Saveserializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"..\..\JsonFiles\JsonRoomTest_Out.json")) 
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Saveserializer.Serialize(writer, rooms);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }


        }

    }
}
