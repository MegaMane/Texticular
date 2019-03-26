using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Texticle.Environment
{
    public abstract class GameObject
    {

        private static int _nextGameID = 0;
        public static Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

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
        public String KeyValue { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }





        public GameObject()
        {
            ID = ++_nextGameID;
            KeyValue = createStringKey("gameObject") + "_" + this.ID.ToString();
            Objects[this.KeyValue] = this;
        }



        public GameObject(string name, string description, string keyValue = "")
        {
            ID = ++_nextGameID;
            KeyValue = keyValue == "" ? createStringKey(name) + "_" + this.ID.ToString() : keyValue;
            Name = name;
            Description = description;

            Objects[this.KeyValue] = this;
        }




        public override string ToString()
        {
            return $"Class: {this.GetType().Name}\n----------------------\nGame ID: " +
                   $"{ID}\nKeyValue: {KeyValue}\nName: {Name}\nDescription: {Description}\nLocationKey: ";
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
