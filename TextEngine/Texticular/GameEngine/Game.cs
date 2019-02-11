using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Texticular
{
    public partial class Game
    {

        public Dictionary<string, Room> Rooms;
        public List<StoryItem> Items;
        private List<Room> gameRooms;
        public Player Player;
        //public Dictionary<string, int> Location;
        public List<string> GameLog;
        //public List<Scene> Scenes;
        public Gamestats Gamestats;
        GameController Controller;


        public Game()
        {
            GameInit();
        }

        public Game(string filePath)
        {
            GameInit(filePath);
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
                foreach(StoryItem item in gameRoom.Items)
                {
                    Items.Add(item);
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


        public void AddRoom(Room newRoom)
        {
            throw new NotImplementedException();
        }

        public void AddItem(StoryItem newItem)
        {
            this.Items.Add(newItem);
        }

        public void AddPlayer(Player newPlayer)
        {
            this.Player = newPlayer;
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
