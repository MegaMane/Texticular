using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Texticular.Environment;

namespace Texticular
{
    public partial class Game
    {


        public Dictionary<string, Room> Rooms;
        public Dictionary<string, StoryItem> Items;
        private List<Room> gameRooms;
        public Player Player;
        public List<string> GameLog;
        public Gamestats Gamestats;



        public Game()
        {
            GameInit();
        }



        public void LoadGameObjects(string filePath)
        {
            gameRooms = new List<Room>();

            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                gameRooms = (List<Room>)serializer.Deserialize(file, typeof(List<Room>));
            }

            foreach (Room gameRoom in gameRooms)
            {
                //Populate the Rooms dictionary
                Rooms[gameRoom.KeyValue] = gameRoom;

                //populate the items list with any items that exist in the room
                foreach(StoryItem item in gameRoom.RoomItems)
                {
                    Items.Add(item.KeyValue,item);
                }
            }



        }

        public void SaveGame()
        {
            JsonSerializer Saveserializer = new JsonSerializer();
            Saveserializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"..\..\JsonFiles\JsonRoomTest_Out.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Saveserializer.Serialize(writer, gameRooms);
            }
        }


        public void AddPlayer(Player newPlayer)
        {
            Player = newPlayer;
            Rooms[Player.PlayerLocation.KeyValue].TimesVisited += 1;
            Gamestats.Player = this.Player;
        }

        public void Save(string saveName)
        {
            throw new NotImplementedException();
        }
        public void Load(string saveName)
        {
            throw new NotImplementedException();
        }

    }
}
