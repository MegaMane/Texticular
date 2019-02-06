﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public class Player:GameObject
    {
        public Room PlayerLocation { get; set; }
        public int Health { get; set; }
        public Inventory BackPack;

        public Player(int id, string name, string description, Room playerlocation, int health):
            base(id, name, description)
        {
            this.PlayerLocation = playerlocation;
            this.Health = health;

            
        }
    }
}
