using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular
{
    public abstract class GameObject
    {

        private static int _nextGameID = 0;

        public int ID { get; protected set; }
        public String KeyValue { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ExamineResponse { get; set; }
        public String LocationKey { get; set; }



        private GameObject()
        {
            ID = ++_nextGameID;
            Name = "Game Object";
            Description = "Object Description";
            KeyValue = "gamObject" + ID.ToString();
        }


        public GameObject(string name, string description)
        {
            ID = ++_nextGameID;
            KeyValue = createStringKey(name) + "_" + this.ID.ToString();
            Name = name;
            Description = description;
            ExamineResponse = description;
        }


        public GameObject(string name, string description, string LocationKey=null, string KeyValue="")
        {
            this.KeyValue = KeyValue == "" ? createStringKey(name) + "_" + this.ID.ToString() : KeyValue;
            ID = ++_nextGameID;
            Name = name;
            Description = description;
            this.LocationKey = LocationKey;
        }

        public GameObject(string name, string description, string examineResponse, string LocationKey, string KeyValue = "")
        {
            this.KeyValue = KeyValue == "" ? createStringKey(name) + "_" + this.ID.ToString() : KeyValue;
            ID = ++_nextGameID;
            Name = name;
            Description = description;
            this.LocationKey = LocationKey;
            ExamineResponse = examineResponse;
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


    }
}
