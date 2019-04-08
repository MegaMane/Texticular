using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;

namespace Texticle.Engine
{
    class CommandBuilder
    {
        private string _Verb;
        private GameObject _DirectObject;
        private GameObject _IndirectObject;

        private Dictionary<string, ICommand> _Commands;



        public CommandBuilder(string verb, GameObject directObject, GameObject indirectObject = null)
        {
            _Verb = verb;
            _DirectObject = directObject;
            _IndirectObject = indirectObject;




        }

        public ICommand GetCommand()
        {
            ICommand command = null;
            switch (_Verb)
            {
                case "take":
                    command = new TakeCommand(_DirectObject as ITakeable);
                    break;
                default:
                    break;
            }
            

            return command;

        }

    }
}

/*

public bool Parse()
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
    bool basicCommand = GameState.Commands.TryGetValue(tokens.Verb, out parsedCommand);

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