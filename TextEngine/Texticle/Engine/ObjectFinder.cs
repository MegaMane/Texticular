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
        public List<GameObject> FindDirectObject ()
        {

            _Nouns.Values.Where(v => v.Name.Contains(_ParseTree.DirectObject));
;            return _Nouns.Values.Where(v => v.Name.Contains(_ParseTree.DirectObject)).ToList();
        }
    }
}


/*
I need to find the object and determine if it is something that can be acted on
	step 1. See if the object exists in the global list of nouns
	Step 2. See if the object meets the following criteria
			The object is the player
            The object is the players current location
			The object is in the players current location and visible
			the object is a child of an object in the current location that is currently accessible
			the object is in the players inventory
	Step 3. If the object meets the above criteria

	Step 4. Insert the resulting command object into a queue
	Step 5. execute all the commands
	Step 6. Render the results
		
 */


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

/*
public bool FindObjects()
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
