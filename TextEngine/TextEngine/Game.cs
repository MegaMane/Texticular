using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public partial class Game
    {

        public Dictionary<string, Room> Rooms;
        public List<StoryItem> Items;
        public Player Player;
        //public Dictionary<string, int> Location;
        public List<string> gameLog;
        public List<Scene> Scenes;
        public Sequence objectID;
        public Gamestats gamestats;


        public Game()
        {
            GameInit();
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
