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

                        Name="Mug",
                        Description="A red ceramic mug that looks like the Marvel Villain Carnage",
                        Adjectives=new List<string>() {"Carnage"}

                    }
                },
                {
                    new GameObject()
                    {

                        Name="Mug",
                        Description="A faded yellow mug with a small chip in the side.",
                        Adjectives=new List<string>() {"Yellow"}

                    }
                }
            };
          
            foreach (var item in nouns)
            {
                Console.WriteLine($"{String.Join(" ",item.Adjectives)} {item.Name}: {item.Description}\n");
            }

            Console.ReadKey();
        }


        public static string FindObject(string input)
        {
            input = input.ToLower();
            string results = "";
            List<string> nouns = new List<string> { "carnage mug", "yellow mug", "yellow duck", "yellow dumbwaiter", "money", "crazy monkey", "crazy monk" };
            var filtered = nouns.Where(s1 => s1.Contains(input)).ToList(); ;

            foreach (var gameObject in filtered)
            {
                var search = input.Split(new char[] { ' ' });
                var words = gameObject.Split(new char[] { ' ' });
                //var foundObjects = search.Where(word => words.Any( w => w.Equals(word))).ToList();
                var foundObjects = words.Where(word => search.Any(sWord => sWord.Equals(word))).ToList();
                if (foundObjects.Count > 0)
                {
                    var foundObject = String.Join(" ", words);
                }

            }

            return results;
        }
    }


 


    public class GameObject
    {


        public string Name { get; set; }
        public List<string> Adjectives { get; set; }
        public string Description { get; set; }



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

//to make a match you must match 0 or more adjectives in any order plus the direct objectName
//-or-
//1 or more adjectives with an optional direct object name

