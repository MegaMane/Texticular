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
        List<string> Tokens;

        public string [] Tokenize(GameController controller)
        {
            char[] delimiters = { ' ', ',' };
            
            string[] commandParts = controller.UserInput.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

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


            //need to remove articles and parse adjectives
            List<string> parameters = new List<string>();

            for (int i = offset; i < commandParts.Length; i++)
            {
                parameters.Add(commandParts[i]);
            }

            foreach (GameObject obj in GameObject.Objects.Values)
            {
                Console.WriteLine(obj.Name);
            }

            return Tokens.ToArray();

        }

    }
}
