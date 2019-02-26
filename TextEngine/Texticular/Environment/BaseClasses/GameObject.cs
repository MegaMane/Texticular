using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.GameEngine;

namespace Texticular.Environment
{
    public abstract class GameObject
    {

        private static int _nextGameID = 0;

        public event ItemLocationChangedEventHandler LocationChanged;

        public int ID { get; private set; }
        public String KeyValue { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ExamineResponse { get; set; }
        private String _locationKey;
        public String LocationKey
        {
            get { return _locationKey; }
            set
            {

                OnLocationChanged(_locationKey,value);

                _locationKey = value;

                
            }
        }

        protected virtual void OnLocationChanged(string currentLocation, string newLocation)
        {
            if (LocationChanged != null)
            {
                ItemLocationChangedEventArgs args = new ItemLocationChangedEventArgs();
                args.CurrentLocation = currentLocation;
                args.NewLocation = newLocation;
                LocationChanged(this, args);
            }
        }

        private Dictionary<string, Action<GameController>> commands;
        public Dictionary<string, Action<GameController>> Commands { get { return commands; } protected set { commands = value; } }


        public GameObject(string name, string description)
        {
            commands = new Dictionary<string, Action<GameController>>();
            ID = ++_nextGameID;
            KeyValue = createStringKey(name) + "_" + this.ID.ToString();
            Name = name;
            Description = description;
            ExamineResponse = description;
            Commands["look"] = look;
            Commands["examine"] = examine;
        }


        public GameObject(string name, string description, string examineResponse, string LocationKey, string KeyValue = "")
        {
            commands = new Dictionary<string, Action<GameController>>();
            this.KeyValue = KeyValue == "" ? createStringKey(name) + "_" + this.ID.ToString() : KeyValue;
            ID = ++_nextGameID;
            Name = name;
            Description = description;
            this.LocationKey = LocationKey;
            ExamineResponse = examineResponse;
            Commands["look"] = look;
            Commands["examine"] = examine;
        }

        public override string ToString()
        {
            return $"Class: {this.GetType().Name}\n----------------------\nGame ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: {LocationKey}\nExamine Response: {ExamineResponse}\n";
        }



        protected string createStringKey(string input, string casing = "camel")
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            string[] words = input.Trim(new char[] { ' ', '\'' }).Split(' ');
            string result = "";

            if (casing == "camel")
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        result += (words[i].ToLower().Trim());
                    }
                    else
                    {
                        result += (words[i].First().ToString().ToUpper() + words[i].Substring(1)).Trim();
                    }

                }

            }

            else if (casing == "pascal")
            {
                foreach (string word in words)
                {
                    result += (word.First().ToString().ToUpper() + word.Substring(1)).Trim();
                }

            }

            return result;
        }


        void examine(GameController controller)
        {
            Player player = controller.Game.Player;
            string response;

            if (ExamineResponse == "" || ExamineResponse == null)
            {
                response = Description;
            }

            else { response = ExamineResponse; }

            if (LocationKey == "inventory")
            {
                controller.InputResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n", response);
                return;
            }

            else if(LocationKey == player.LocationKey)
            {
                controller.InputResponse.Append(response);
                return;
            }



            controller.InputResponse.AppendFormat($"There is no {Name} here.\n");


        }


        void look(GameController controller)
        {
            Player player = controller.Game.Player;


            if (LocationKey == "inventory")
            {
                controller.InputResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n", Description);
                return;
            }

            else if (LocationKey == player.LocationKey)
            {
                controller.InputResponse.Append(Description);
                return;
            }



            controller.InputResponse.AppendFormat($"There is no {Name} here.\n");





        }

    }
}
