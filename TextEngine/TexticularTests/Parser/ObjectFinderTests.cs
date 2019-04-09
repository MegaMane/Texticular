using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticular.ParserTests.Tests
{
    [TestClass()]
    public class ObjectFinderTests
    {
        Dictionary<string, GameObject> Nouns;

        [TestMethod()]
        public void FindMonkey()
        {
            Init();
            ObjectFinder finder = new ObjectFinder(Nouns);

            var input = "Monkey";

            List<GameObject> foundObjects = finder.FindObject(input);

            if (foundObjects.Count == 0)
            {
                Console.WriteLine($"{input} is not a sentence I understand.");
            }

            foreach (var item in foundObjects)
            {
                Console.WriteLine($"{String.Join(" ", item.Adjectives)} {item.Name}: {item.Description}".Trim() + "\n");
            }


        }

        void Init()
        {
            Nouns = new Dictionary<string, GameObject>()
            {
                {   "1",
                    new StoryItem()
                    {
                        Name="Mug",
                        Description="A red ceramic mug that looks like the Marvel Villain Carnage",
                        Adjectives=new List<string>() {"Carnage"}

                    }
                },
                {   "2",
                    new StoryItem()
                    {
                        Name="Mug",
                        Description="A faded yellow mug with a small chip in the side.",
                        Adjectives=new List<string>() {"Faded", "Yellow"}

                    }
                },
                {  "3",
                    new StoryItem()
                    {
                        Name="Duck",
                        Description="A rubber duck. It looks like a duck.",
                        Adjectives=new List<string>() {"Yellow"}

                    }
                },

                {   "4",
                    new Money()
                    {
                        Name="Money",
                        Description="50 green backs.",
                        Value = 50
        }
                },

                {   "5",
                    new StoryItem()
                    {
                        Name="Monkey",
                        Description="A monkey who cray.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },

                {   "6",
                    new StoryItem()
                    {
                        Name="Monk",
                        Description="A warrior monk who smelled one too many gonesh sticks.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },
                 {  "7",
                    new StoryItem()
                    {
                        Name="Duck",
                        Description="Just like the yellow duck, but uglier.",
                        Adjectives=new List<string>() {"Ugly","Yellow"}

                    }
                },
            };
        }

    }
}
