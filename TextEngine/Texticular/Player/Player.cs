using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular
{
    public class Player:GameObject
    {
        public event LocationChangedHandler OnPlayerLocationChanged;
        private Room _playerLocation;
        public Room PlayerLocation
        {
            get
            {
                return _playerLocation;
            }
            set
            {


                if(OnPlayerLocationChanged != null)
                {
                    LocationChangedEventArgs args = new LocationChangedEventArgs();
                    args.CurrentLocation = _playerLocation;
                    args.NewLocation = value;
                    OnPlayerLocationChanged(this, args);
                }

                _playerLocation = value;
                LocationKey = value.KeyValue;
            }
         }
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
