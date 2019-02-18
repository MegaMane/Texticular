using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;

namespace Texticular
{
    public class Player:GameObject
    {
        private Room _playerLocation;
        public Room PlayerLocation { get { return _playerLocation; } set { _playerLocation = value; LocationKey = value.KeyValue; } }
        public int Health { get; set; }
        public Inventory BackPack;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public Player(string name, string description, Room playerlocation, int health):
            base(name, description)
        {
            this.PlayerLocation = playerlocation;
            this.Health = health;

            
        }
    }
}
