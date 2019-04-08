using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Actors;

namespace Texticle.Engine
{
    public class ObjectFinder
    {
        private ParseTree _ParseTree;
        private Dictionary<string, GameObject> _Nouns;

        public ObjectFinder(ParseTree parseTree, Dictionary<string, GameObject> nouns)
        {
            _ParseTree = parseTree;
            _Nouns = nouns;
        }

        
            
         /// <summary>
        /// to make a match you must match 0 or more adjectives in any order plus the direct objectName
        ///-or-
        //1 or more adjectives with an optional direct object name
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nouns"></param>
        /// <returns>List<GameObject></returns>
        public static List<GameObject> FindObject(string input, List<GameObject> nouns)
        {
            var results = new List<GameObject>();

            input = input.ToLower().Trim();
            var words = input.Split();
            List<GameObject> possibleMatches = new List<GameObject>();


            //compile a list of any objects that have at least one word in common with the input string (adjective or object name)
            foreach (var word in words)
            {
                var filtered = nouns.Where(n => n.Name.ToLower().Contains(word) || n.Adjectives.Any(a => a.ToLower().Contains(word)));
                possibleMatches.AddRange(filtered);
            }

            //remove any duplicates by checking the object ID property 
            possibleMatches = possibleMatches.Distinct(new GameObjectComparer()).ToList();

            if (possibleMatches.Count == 0)
            {
                return results;
            }

            //find the most likely match(s) removing matched words as they are found
            foreach (var gameObject in possibleMatches)
            {
                List<string> unmatchedWords = new List<string>(words);
                List<string> adjectives = new List<string>();

                bool directObjectFound = false;
                bool matchFound = false;

                //check for the name of the object
                for (int i = 0; i < unmatchedWords.Count; i++)
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
                    if (directObjectFound)
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


/*
        private void GetNouns()
        {

            Player player = GameObject.GetComponent<Player>("player");

            //items that are in the players inventory
            var inventoryItems = GameObject.Objects.Where(k => k.Value.LocationKey == player.BackPack.KeyValue)
                                                   .ToDictionary(G => G.Key, G => G.Value);
            //items that are located in the room
            var nouns = GameObject.Objects.Where(k => k.Value.LocationKey == player.LocationKey)
                                           .ToDictionary(G => G.Key, G => G.Value);

            nouns = nouns.Concat(inventoryItems).ToDictionary(G => G.Key, G => G.Value);

            //exits in the room
            foreach (var door in player.PlayerLocation.Exits.Values)
            {
                nouns[door.KeyValue] = (GameObject)door;
            }

            //the room itself
            nouns[player.LocationKey] = (GameObject)player.PlayerLocation;

            _Nouns = nouns;
        }
*/

/*
I need to find the object and determine if it is something that can be acted on
	step 1. See if the object exists in the global list of nouns
	Step 2. See if the object meets the following criteria

	Step 3. If the object meets the above criteria

	Step 4. Insert the resulting command object into a queue
	Step 5. execute all the commands
	Step 6. Render the results
		
 */


