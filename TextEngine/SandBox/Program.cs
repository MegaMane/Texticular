using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GameObject> nouns = new List<GameObject>()
            {
                {
                    new GameObject()
                    {
                        ID=1,
                        Name="Mug",
                        Description="A red ceramic mug that looks like the Marvel Villain Carnage",
                        Adjectives=new List<string>() {"Carnage"}

                    }
                },
                {
                    new GameObject()
                    {
                        ID=2,
                        Name="Mug",
                        Description="A faded yellow mug with a small chip in the side.",
                        Adjectives=new List<string>() {"Faded", "Yellow"}

                    }
                },
                {
                    new GameObject()
                    {
                        ID=3,
                        Name="Duck",
                        Description="A rubber duck. It looks like a duck.",
                        Adjectives=new List<string>() {"Yellow"}

                    }
                },

                {
                    new GameObject()
                    {
                        ID=4,
                        Name="Money",
                        Description="50 green backs.",

                    }
                },

                {
                    new GameObject()
                    {
                        ID=5,
                        Name="Monkey",
                        Description="A monkey who cray.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },

                {
                    new GameObject()
                    {
                        ID=6,
                        Name="Monk",
                        Description="A warrior monk who smelled one too many gonesh sticks.",
                        Adjectives=new List<string>() {"Crazy"}
                    }
                },
                 {
                    new GameObject()
                    {
                        ID=7,
                        Name="Duck",
                        Description="Just like the yellow duck, but uglier.",
                        Adjectives=new List<string>() {"Ugly","Yellow"}

                    }
                },
            };

            var input = "Monkey";

            List<GameObject> foundObjects = FindObject(input, nouns);

            if(foundObjects.Count == 0)
            {
                Console.WriteLine($"{input} is not a sentence I understand.");
            }

            foreach (var item in foundObjects)
            {
                Console.WriteLine($"{String.Join(" ", item.Adjectives)} {item.Name}: {item.Description}".Trim() + "\n");
            }


            Console.ReadKey();
        }


        //to make a match you must match 0 or more adjectives in any order plus the direct objectName
        //-or-
        //1 or more adjectives with an optional direct object name
        public static List<GameObject> FindObject(string input, List<GameObject> nouns)
        {
            var results = new List<GameObject>();
            
            input = input.ToLower().Trim();
            var words = input.Split();
            List<GameObject> possibleMatches = new List<GameObject>();
            

            //compile a list of any objects that have at least one matching word with the input string
            foreach (var word in words)
            {
             var filtered = nouns.Where(n => n.Name.ToLower().Contains(word) || n.Adjectives.Any(a => a.ToLower().Contains(word)));
             possibleMatches.AddRange(filtered);
            }

            //remove any duplicates by checking the object ID property 
            possibleMatches = possibleMatches.Distinct(new GameObjectComparer()).ToList();

            if(possibleMatches.Count == 0)
            {
                return results;
            }

            foreach (var gameObject in possibleMatches)
            {
                List<string> adjectives = new List<string>();
                List<string> unmatchedWords = new List<string>(words);
                bool directObjectFound = false;
                bool matchFound = false;

                //check for the name of the object
                for (int i =0; i< unmatchedWords.Count; i++)
                {

                    if (unmatchedWords[i] == gameObject.Name.ToLower())
                    {
                        unmatchedWords.RemoveAt(i);
                        i--;
                        directObjectFound = true;
                    }

                }

                //check if any adjectives match
                for (int i = 0; i < unmatchedWords.Count; i++)
                {
                    for (int j = 0; j < gameObject.Adjectives.Count; j++)
                    {
                        if (unmatchedWords[i] == gameObject.Adjectives[j].ToLower())
                        {
                            adjectives.Add(unmatchedWords[i]);
                            unmatchedWords.RemoveAt(i);
                            i--;
                            break;
                        }

                    }

                }


                if (unmatchedWords.Count == 0)
                {
                    if(directObjectFound)
                    {
                        //make sure that it's the last word
                        matchFound = words[words.Length - 1] == gameObject.Name.ToLower();
                    }

                    else
                    {
                        matchFound = adjectives.Count > 0;
                    }
                }

                else
                {
                    matchFound = false;
                }

                if (matchFound)
                {
                    results.Add(gameObject);
                }

            }

            return results;
        }
    }


 


    public class GameObject
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> Adjectives { get; set; }
        public string Description { get; set; }

        public GameObject()
        {
            Adjectives = new List<string>();
        }

    }

    internal class GameObjectComparer : IEqualityComparer<GameObject>
    {
        public bool Equals(GameObject x, GameObject y)
        {
            if (x.ID == y.ID)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GameObject obj)
        {
            return obj.Name.GetHashCode();
        }
    }

}




//check to make sure the list of nouns contains the input and return a sub list of filtered input


//split the input string
//Loop over the filtered nouns collection
//    split each noun into an array of strings called words

//    check to see if the first word in the input string matches any of the words in the words array
//		if (matchFound):
//			If(Input array length > 1)

//                check the first two words
//                against the first two words of each noun
		
//			else
				
//				if Matches.Count > 1
//					return List ambiguos objects
//				else
//					return the match
		
		
//		return Invalid Object



//char[] delimiters = { ' ', ',' };

//string[] objectParts = userInput.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

//if (commandParts.Length == 0) return null ;

		
//int offset = 0;
//string objectName = "";
//bool commandFound = false;


//// Search for the verb
//foreach (string word in nouns)
//{
//	objectName = "";

//	for (int i = 0; i<objectParts.Length; i++)
//	{
//		objectName = String.Join(" ", objectParts, 0, i + 1);
//		offset = i + 1;

//		if (commandName == command)
//		{

//			commandFound = true;
//			tokens.Verb = commandName;
//			break;
//		}
//	}

//	if (commandFound)
//	{
//		break;
//	}

//}

//for a potential match


