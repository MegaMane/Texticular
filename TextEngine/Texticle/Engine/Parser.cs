using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;
using Texticle.Actors;



namespace Texticle.Engine
{

    public class Parser
    {

        public Parser(Dictionary<string, GameObject> nouns)
        {
            Nouns = nouns;
        }

        public Dictionary<string, GameObject> Nouns { get; set; }

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
                                                        "hide",
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
            "on",
            "through",
            "inside",
            "up",
            "under",
            "over",
            "beside",
            "below",
            "down" //...{the apple}
        };


        public ParseTree Tokenize(string userInput)
        {
            ParseTree tokens = new ParseTree();
            tokens.UnparsedInput = userInput;

            char[] delimiters = { ' ', ',' };

            string[] commandParts = userInput.ToLower()
                                             .Trim(new char[]{'.','!','?' })
                                             .Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (commandParts.Length == 0) return null;


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

            //foreach (KeyValuePair<string, GameObject> obj in Nouns)
            //{
            //    offset = 0;
            //    string objectName = "";

            //    for (int i = 0; i < remainingInput.Count; i++)
            //    {
            //        objectName = String.Join(" ", remainingInput.ToArray(), 0, i + 1);
            //        offset = i + 1;

            //        if (objectName == obj.Value.Name.ToLower())
            //        {

            //            tokens.DirectObject = objectName;
            //            tokens.DirectObjectKeyValue = obj.Key;
            //            remainingInput.RemoveRange(0, offset);
            //            break;
            //        }
            //    }
            //}


            //check direction
            //if (tokens.DirectObject == null && remainingInput.Count > 0)
            //{



            //    var directionName = String.Join("", remainingInput.ToArray());
            //    directionName = directionName.First().ToString().ToUpper() + directionName.Substring(1);

            //    try
            //    {
            //        Direction desiredDirecton = (Direction)Enum.Parse(typeof(Direction), directionName);
            //        tokens.DirectObject = directionName;
            //    }

            //    catch (ArgumentException e)
            //    {
            //        ///GameController.InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", directionName);
            //        tokens.DirectObject = null;
            //    }


            //}
            
            if (remainingInput.Count > 0)
                tokens.DirectObject = String.Join(" ", remainingInput.ToArray());

            if (secondaryObject.Count > 0)
                tokens.IndirectObject = String.Join(" ", secondaryObject.ToArray());

            if (tokens.IndirectObject != "???" && tokens.Verb != "???" && tokens.DirectObject == "???")
                tokens.DirectObject = "player";


            //if (secondaryObject.Count > 0)
            //{
            //    foreach (KeyValuePair<string, GameObject> obj in Nouns)
            //    {
            //        offset = 0;
            //        string objectName = "";

                //        for (int i = 0; i < secondaryObject.Count; i++)
                //        {
                //            objectName = String.Join(" ", secondaryObject.ToArray(), 0, i + 1);
                //            offset = i + 1;

                //            if (objectName == obj.Value.Name.ToLower())
                //            {

                //                tokens.IndirectObject = objectName;
                //                tokens.IndirectObjectKeyValue = obj.Key;
                //                secondaryObject.RemoveRange(0, offset);
                //                break;
                //            }
                //        }
                //    }

                //}


            return tokens;

        }

        /*
        public void Parse()
        {

            Player Player = GameObject.GetComponent<Player>("player");
         

            string userInput = GameLog.UserInput;

            if (userInput.ToLower().Trim() == "exit")
            {
                Controller.SetGameState("PlayerQuit");
                return;
            }
            ParseTree tokens = Tokenize(userInput);

            if (tokens == null)
            {
                tokens = new ParseTree() { Verb = "look", DirectObject = Player.PlayerLocation.Name, DirectObjectKeyValue = Player.PlayerLocation.KeyValue };
            }


            if (tokens.Verb == null)
            {
                GameLog.InputResponse.Append($"I dont understand '{userInput}'! Type help for some examples of what I do understand.");
                return;
            }

            //look with no object after it means look at surroundings
            if (tokens.Verb == "look" && tokens.DirectObject == null)
            {
                tokens.DirectObject = Controller.Game.Player.PlayerLocation.Name.ToLower();
                tokens.DirectObjectKeyValue = Controller.Game.Player.PlayerLocation.KeyValue;
            }

            Action<ParseTree> parsedCommand;
            bool basicCommand = GameState .Commands.TryGetValue(tokens.Verb, out parsedCommand);

            //basic commands go, help, inventory
            if (basicCommand)
                parsedCommand(tokens);


            else if (tokens.Verb == "help")
            {
                Player.Commands["help"](tokens);
            }

            else if (tokens.Verb == "inventory" || tokens.Verb == "inv" || tokens.Verb == "backpack")
            {
                Player.Commands["inventory"](tokens);
            }


            //context sensitive commands
            else if (!basicCommand && tokens.DirectObject != null)
            {
                GameObject objectToFind = GameObject.Objects[tokens.DirectObjectKeyValue];


                Action<ParseTree> contextCommand;
                bool validcontextCommand = objectToFind.Commands.TryGetValue(tokens.Verb, out contextCommand);

                if (validcontextCommand)
                {
                    contextCommand(tokens);
                }

                else
                {
                    GameLog.InputResponse.Append($"You cant {tokens.Verb} {tokens.UnparsedInput.Replace(tokens.Verb, "")}.\n");
                }


            }

            else
            {
                //bogus command not understood
                GameLog.InputResponse.Append($"{tokens.Verb} what?");
            }




        }


    

    */

    }
}
