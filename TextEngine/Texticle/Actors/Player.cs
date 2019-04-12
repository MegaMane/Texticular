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

        public event OnPlayerLocationChanged PlayerLocationChanged;
        private Room _playerLocation;
        public Room PlayerLocation
        {
            get
            {
                return _playerLocation;
            }
            set
            {


                if (PlayerLocationChanged != null)
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
        public int Money { get; set; }
        private string _firstName ="";
        public string FirstName { get { return _firstName; } set { _firstName = GameLog.FirstCharToUpper(value); } }
        private string _lastName;
        public string LastName { get { return _lastName; } set { _lastName = GameLog.FirstCharToUpper(value); } }
        private string _playerName;
        public string PlayerName { get { return _playerName; } private set { _playerName = value; } }

        public Player(string description, Room playerLocation, string playerName = "????", int health =100, int money=0):
            base(name:"Player", description:description, keyValue:"player")
        {
            PlayerName = playerName;
            PlayerLocation = playerLocation;
            Health = health;
            BackPack = new Inventory();
            Money = money;

        }

        public void SetName(string firstName, string lastName="")
        {
            FirstName = firstName;
            LastName = lastName;
            _playerName = (GameLog.FirstCharToUpper(firstName) + " " + GameLog.FirstCharToUpper(firstName)).Trim();
        }

        public void ConsumeItem(StoryItem item)
        {
            throw new NotImplementedException();

        }

        public void OpenBackpack()
        {
            BackPack.Open(BackPack);
        }


        public void Help()
        {

            GameLog.Append("\n\n----------------------\n" 
                           +"Command List\n" 
                           +"----------------------\n\n");
                GameLog.Append(
                "Go, Walk, Move: Typing any of these will move the character in the direction specified.\n"
                + "Look: take a look at your sorroundings and list any obvious exits and visible items.\n"
                + "Examine: Take a closer look at an object.\n"
                + "Get, Take, Grab, Pick Up: Typing any of these will attempt to pick up the \n\t\t\t  specified object and add it to your invnetory.\n"
                + "Drop: Drop the specified object at the players current position. \n      Some objects my persist in the location they were dropped.\n"
                + "Inventor or Backpack: View your Inventory.\n\n"
                );


        }

        public override string ToString()
        {
            return $"\n-----------------------------------------------------\n"
                  + $"Class: {this.GetType().Name}\n-----------------------------------------------------\n"
                  + $"Game ID: {ID}\nKeyValue: {KeyValue}\nName: {PlayerName}\nDescription: {Description}\n"
                  +$"Health: {Health}\nMoney: {Money}\nLocation: {PlayerLocation.Name}";
        }




    }
}
