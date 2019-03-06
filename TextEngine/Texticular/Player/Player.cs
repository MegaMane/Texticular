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
        public event PlayerLocationChangedEventHandler PlayerLocationChanged;
        private Room _playerLocation;
        public Room PlayerLocation
        {
            get
            {
                return _playerLocation;
            }
            set
            {


                if(PlayerLocationChanged != null)
                {
                    PlayerLocationChangedEventArgs args = new PlayerLocationChangedEventArgs();
                    args.CurrentLocation = _playerLocation;
                    args.NewLocation = value;
                    PlayerLocationChanged(this, args);
                }

                _playerLocation = value;
                LocationKey = value.KeyValue;
            }
         }
        public int Health { get; set; }
        public Inventory BackPack;
        private string _firstName ="";
        public string FirstName { get { return _firstName; } set { _firstName = GameController.FirstCharToUpper(value); } } 
        public string LastName { get; set; } = "";

        public Player(string name, string description, Room playerlocation, int health):
            base(name, description, examineResponse:"", LocationKey:playerlocation.KeyValue, KeyValue:"player")
        {
            this.PlayerLocation = playerlocation;
            this.Health = health;

            
        }




    }
}
