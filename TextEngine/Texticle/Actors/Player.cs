using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Engine;

namespace Texticle.Actors
{
    public class Player:GameObject
    {

        //public event PlayerLocationChangedEventHandler PlayerLocationChanged;
        public String LocationKey { get; private set; }
        private Room _playerLocation;
        public Room PlayerLocation
        {
            get
            {
                return _playerLocation;
            }
            set
            {

                /*
                if(PlayerLocationChanged != null)
                {
                    PlayerLocationChangedEventArgs args = new PlayerLocationChangedEventArgs();
                    args.CurrentLocation = _playerLocation;
                    args.NewLocation = value;
                    PlayerLocationChanged(this, args);
                }
                */
                _playerLocation = value;
                LocationKey = value.KeyValue;
            }
         }
         
        public int Health { get; set; }
        public Inventory BackPack;
        public int Money { get; set; } = 0;
        private string _firstName ="";
        public string FirstName { get { return _firstName; } set { _firstName = value.First().ToString().ToUpper() + value.Substring(1); } } 
        public string LastName { get; set; } = "";

        public Player(string name, string description, int health):
            base(name, description, keyValue:"player")
        {
            //this.PlayerLocation = playerlocation;
            this.Health = health;
           // Commands["help"] = help;

            //Commands["inventory"] = inventory;
           // Commands["backpack"] = inventory;
           // Commands["inventory"] = inventory;

        }

        /*
        void help(ParseTree tokens)
        {
            if (tokens.DirectObject == null && tokens.IndirectObject == null)
            {
                GameController.InputResponse.Append("\n\n----------------------\n" +
                                     "Command List\n" +
                                     "----------------------\n\n");
                GameController.InputResponse.Append(
                "Go, Walk, Move: Typing any of these will move the character in the direction specified.\n"
                + "Look: take a look at your sorroundings and list any obvious exits and visible items.\n"
                + "Examine: Take a closer look at an object.\n"
                + "Get, Take, Grab, Pick Up: Typing any of these will attempt to pick up the \n\t\t\t  specified object and add it to your invnetory.\n"
                + "Drop: Drop the specified object at the players current position. \n      Some objects my persist in the location they were dropped.\n"
                + "Inventory: Open the player Inventory.\n\n"
                );
            }

            else
            {
                GameController.InputResponse.Append("I don't understand. If you wan't help just type help!\n");
            }

        }

        void inventory(ParseTree tokens)
        {


            if (tokens.DirectObject != null && tokens.IndirectObject != null)
            {
                GameController.InputResponse.Append("The inventory command is not valid with any other combination of words. Try typing 'Inventory', 'Backpack', or 'Inv'\n ");
                return;
            }

            GameController.InputResponse.Append("\n Inventory\n ");
            GameController.InputResponse.Append("------------------------------------------------------\n\n ");

            foreach (var item in GameObject.Objects.Values)
            {
                if (item.LocationKey == "inventory")
                {
                    GameController.InputResponse.AppendFormat("{0} : {1}\n ", item.Name, item.Description);
                }



            }

            GameController.InputResponse.Append("\n ");
        }


    */


    }
}
