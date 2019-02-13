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
                                                        "pick up",
                                                        "power off",
                                                        "power on",
                                                        "take",
                                                        "use",
                                                        "turn off",
                                                        "turn on",
                                                        "walk"

                                                       };



    }
}
