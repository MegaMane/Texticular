using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular;

namespace Texticular.GameEngine
{
    public class Lexer
    {
        public List<string> KnownCommands { get { return knownCommands; } }
        List<String> knownCommands = new List<string> {
                                                        "backpack",
                                                        "change channel",
                                                        "close",
                                                        "drop",
                                                        "examine",
                                                        "get",
                                                        "go" ,
                                                        "grab",
                                                        "help",
                                                        "inv",
                                                        "inventory",
                                                        "look",
                                                        "move",
                                                        "open",
                                                        "pick up",
                                                        "power off",
                                                        "power on",
                                                        "put",
                                                        "shut",
                                                        "take",
                                                        "unlock",
                                                        "use",
                                                        "turn off",
                                                        "turn on",
                                                        "walk"

                                                       };
        public List<string> Prepositions = new List<string>
        {
            "in",
            "on"
        };
        

        public string [] Tokenize(string userInput)
        {
            List<string> Tokens = new List<string>();

            char[] delimiters = { ' ', ',' };
            
            string[] commandParts = userInput.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (commandParts.Length == 0) return new string[] { } ;


            int offset = 0;
            string commandName = "";
            bool commandFound = false;

            
            // Search for the verb
            foreach (string command in KnownCommands)
            {
                commandName = "";

                for (int i = 0; i < commandParts.Length; i++)
                {
                    commandName = String.Join(" ", commandParts, 0, i + 1);
                    offset = i + 1;

                    if (commandName == command)
                    {

                        commandFound = true;
                        Tokens.Add(commandName);
                        break;
                    }
                }

                if (commandFound)
                {
                    break;
                }

            }


            //the rest of the input after the verb has been extracted
            List<string> remainingInput = new List<string>();

            //A list to hold any indirect objects if they exist in the command
            List<string> secondaryObject = new List<string>();



            for (int i = offset; i < commandParts.Length; i++)
            {
                remainingInput.Add(commandParts[i]);
            }

            //remove articles
            remainingInput = (from word in remainingInput
                              where word != "a"
                        && word != "an"
                        && word != "the"
                        select word).ToList();

            //Find a preposition if it exists

            var prepositionIndex = remainingInput.FindIndex(rItem => Prepositions.Any(pItem => pItem == rItem));

            if (prepositionIndex != -1) //preposition found
            {
                int startingIndex = prepositionIndex + 1;
                int count = remainingInput.Count - startingIndex;
                secondaryObject = remainingInput.GetRange(startingIndex, count);
                remainingInput = remainingInput.GetRange(0, prepositionIndex);
            }

            foreach (GameObject obj in GameObject.Objects.Values)
            {
                offset = 0;
                string objectName = "";

                for (int i = 0; i < remainingInput.Count; i++)
                {
                    objectName = String.Join(" ", remainingInput.ToArray(), 0, i + 1);
                    offset = i + 1;

                    if (objectName == obj.Name.ToLower())
                    {

                        Tokens.Add(objectName);
                        remainingInput.RemoveRange(0, offset);
                        break;
                    }
                }
            }

            if(secondaryObject.Count > 0)
            {
                foreach (GameObject obj in GameObject.Objects.Values)
                {
                    offset = 0;
                    string objectName = "";

                    for (int i = 0; i < secondaryObject.Count; i++)
                    {
                        objectName = String.Join(" ", secondaryObject.ToArray(), 0, i + 1);
                        offset = i + 1;

                        if (objectName == obj.Name.ToLower())
                        {

                            Tokens.Add(objectName);
                            secondaryObject.RemoveRange(0, offset);
                            break;
                        }
                    }
                }

            }


            return Tokens.ToArray();

        }

    }
}
