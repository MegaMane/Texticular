﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texticle.Actors;

namespace Texticle.Environment
{
    public abstract class GameObject
    {
        public delegate string GameActionDelegate(GameObject target);
        private Random _random;
        private static int _nextGameID = 0;
        protected StringBuilder ActionResponse;
        public static Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();
        private static Dictionary<string, GameObject> UsedObjects = new Dictionary<string, GameObject>();
        public virtual string LocationKey { get; set; }
        public virtual bool IsVisible { get; set; }

        public static void Consume(string keyVal)
        {
            GameObject usedObject = Objects[keyVal];
            Objects.Remove(keyVal);
            UsedObjects.Add(keyVal, usedObject);
        }

        public static T GetComponent<T>(string objIdentifier) where T : GameObject
        {
            GameObject obj;

            var objKey = objIdentifier;
            var objName = objIdentifier;

            //search by key
            bool objectFound = Objects.TryGetValue(objKey, out obj);

            //search by value.Name
            if (!objectFound)
            {
                foreach (GameObject gObject in Objects.Values)
                {
                    if (objName.ToLower() == gObject.Name.ToLower())
                    {
                        obj = gObject;
                        break;
                    }
                }
            }

            if (obj != null && obj is T)
            {
                return (T)obj;
            }

            return null;
        }



        public int ID { get; private set; }


        public string KeyValue { get; set; }
        public string Name { get; set; }
        public string FullName { get { return (String.Join(" ",Adjectives) + " " + Name).Trim(); } }
        public List<string> Adjectives { get; set; }
        public string Description { get; set; }
        public string ExamineResponse { get; set; }
        public Dictionary<string, GameActionDelegate> Commands { get; set; }



        public GameObject():this("gameObject", "gameObject")
        {

        }



        public GameObject(string name, string description, string keyValue = "", bool isVisible = true)
        {
            ID = ++_nextGameID;
            ActionResponse = new StringBuilder();
            _random = new Random();

            KeyValue = keyValue == "" ? createStringKey(name) + "_" + this.ID.ToString() : keyValue;
            Name = name;
            Description = description;

            Adjectives = new List<string>();

            Commands = new Dictionary<string, GameActionDelegate>();
            Commands["look"] = Look;
            Commands["examine"] = Examine;

            IsVisible = true;

            Objects[this.KeyValue] = this;

        }

        public int GetRandomInt(int startNumberInclusive=0, int endNumberExclusive=int.MaxValue)
        {
            return _random.Next(startNumberInclusive, endNumberExclusive);
        }


        public override string ToString()
        {
            return $"\n-----------------------------------------------------\n"
                  +$"Class: {this.GetType().Name}\n-----------------------------------------------------\n" 
                  +$"Game ID: {ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\n";
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


        public virtual string Look(GameObject target)
        {
            if (!IsVisible)
            {
                return $"You don't see {Name} here.";
            }


            ActionResponse.Clear();


            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;


            if (LocationKey == "inventory")
            {
                ActionResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n", Description);
            }

            else if (LocationKey == player.LocationKey)
            {
                ActionResponse.Append(Description);
            }


            else
            {
                ActionResponse.Append($"There is no {Name} here.\n");
            }

            return ActionResponse.ToString();

        }

        public virtual string Examine(GameObject target)
        {
            if (!IsVisible)
            {
                return $"You don't see {Name} here.";
            }

            ActionResponse.Clear();
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;
            string response;

            if (ExamineResponse == "" || ExamineResponse == null)
            {
                response = Description;
            }

            else { response = ExamineResponse; }

            if (LocationKey == "inventory")
            {
                ActionResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n", response);
            }

            else if (LocationKey == player.LocationKey)
            {
                ActionResponse.Append(response);
            }

            else
            {
                ActionResponse.AppendFormat($"There is no {Name} here.\n");
            }

            return ActionResponse.ToString();
        }



    }



}

