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
            //through, inside, up, under, over, beside, below, down ...{the apple}
        };
        

        public ParseTree Tokenize(string userInput)
        {
            ParseTree tokens = new ParseTree();
            tokens.UnparsedInput = userInput;

            char[] delimiters = { ' ', ',' };
            
            string[] commandParts = userInput.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (commandParts.Length == 0) return null ;


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
                        tokens.Verb = commandName;
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

            foreach (KeyValuePair<string, GameObject> obj in GameObject.Objects)
            {
                offset = 0;
                string objectName = "";

                for (int i = 0; i < remainingInput.Count; i++)
                {
                    objectName = String.Join(" ", remainingInput.ToArray(), 0, i + 1);
                    offset = i + 1;

                    if (objectName == obj.Value.Name.ToLower())
                    {

                        tokens.DirectObject = objectName;
                        tokens.DirectObjectKeyValue = obj.Key;
                        remainingInput.RemoveRange(0, offset);
                        break;
                    }
                }
            }


            //check direction
            if (tokens.DirectObject == null && remainingInput.Count > 0)
            {

                string directionName = "";

                directionName = GameController.FirstCharToUpper(String.Join("", remainingInput.ToArray()));

                try
                {
                    Direction desiredDirecton = (Direction)Enum.Parse(typeof(Direction), directionName);
                    tokens.DirectObject = directionName;
                }
                
                catch (ArgumentException e)
                {
                    ///GameController.InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", directionName);
                    tokens.DirectObject = null;
                }

                    
            }


            if(secondaryObject.Count > 0)
            {
                foreach (KeyValuePair<string, GameObject> obj in GameObject.Objects)
                {
                    offset = 0;
                    string objectName = "";

                    for (int i = 0; i < secondaryObject.Count; i++)
                    {
                        objectName = String.Join(" ", secondaryObject.ToArray(), 0, i + 1);
                        offset = i + 1;

                        if (objectName == obj.Value.Name.ToLower())
                        {

                            tokens.IndirectObject = objectName;
                            tokens.IndirectObjectKeyValue = obj.Key;
                            secondaryObject.RemoveRange(0, offset);
                            break;
                        }
                    }
                }

            }


            return tokens;

        }

    }


    public class ParseTree
    {
        public String Verb { get; set; } = null;
        public String DirectObject { get; set; } = null;
        public String DirectObjectKeyValue { get; set; } = null;
        public String IndirectObject { get; set; } = null;
        public String IndirectObjectKeyValue { get; set; } = null;
        public String UnparsedInput { get; set; } = null;

    }
}
