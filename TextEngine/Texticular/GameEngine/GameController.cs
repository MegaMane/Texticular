using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular
{
    public class GameController
    {
        public Game game;
        public StringBuilder InputResponse;
        Dictionary<string, Action<string[]>> commands;
        public List<StoryItem> ItemsinInventory;
        public List<StoryItem> ItemsinRoom;
        Lexer Tokenizer;
        public string UserInput;
        Story story = new Story();


        public GameController(Game game)
        {

            this.game = game;
            this.InputResponse = new StringBuilder();
            this.commands = new Dictionary<string, Action<string[]>>();
            this.ItemsinInventory = new List<StoryItem>();
            this.ItemsinRoom = new List<StoryItem>();
            Tokenizer = new Lexer();


            commands["go"] = go;
            commands["walk"] = go;
            commands["move"] = go;

            commands["look"] = look;

            commands["examine"] = examine;

            //commands["take"] = take;
            //commands["get"] = take;
            //commands["grab"] = take;
           //commands["pick up"] = take;

            commands["drop"] = drop;

            commands["inventory"] = inventory;
            commands["backpack"] = inventory;
            commands["inv"] = inventory;

            commands["help"] = help;



        }




  


        public void Start()
        {
            InputResponse.Append("Type Help for a list of commands...\n\n");
            Parse("look");
            Render();
            //story.CurrentScene = "intro";
            //story.PlayScene(this);

        }

        public void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput;
        }   
        
        public void Render()
        {
            Console.Clear();
            Console.WriteLine("Insert Score Here\n");
            Console.WriteLine(InputResponse.ToString());
        }

        public void Update ()
        {

            InputResponse.Clear();
            Parse(UserInput);
           

            //else
            //{
            //    Action<GameController> storyScene;
            //    storyScene = story.Scenes[story.CurrentScene].SceneAction;
            //    storyScene(this);
            //}

        }

        public void Parse(String userInput)
        {
            this.UserInput = userInput;
            ItemsinInventory.Clear();
            foreach (StoryItem item in game.Items)
            {
                if (item.LocationKey == "inventory")
                {
                    ItemsinInventory.Add(item);
                }
            }


            ItemsinRoom.Clear();
            foreach (StoryItem item in game.Items)
            {
                if (item.LocationKey == game.Player.PlayerLocation.KeyValue)
                {
                    ItemsinRoom.Add(item);
                }
            }

            char[] delimiters = { ' ', ',' };
            string[] commandParts = userInput.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (commandParts.Length == 0) return ; //the player just hit enter without typing anything


            int offset = 0;
            string name="";
            bool commandFound = false;

            // Name of the method we want to call.
            foreach (string command in Tokenizer.KnownCommands)
            {
                name = "";

                for (int i = 0; i < commandParts.Length; i++)
                {
                    name = String.Join(" ", commandParts, 0, i + 1);
                    offset = i + 1;

                    if (name == command)
                    {

                        commandFound = true;
                        break;
                    }
                }

                if (commandFound)
                {
                    break;
                }

            }
            

            //need to remove articles and parse adjectives
            string[] parameters = new string[commandParts.Length - offset];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = commandParts[i + offset];
            }

            //object[] args = new object[2];
            //args[0] = gamestate;
            //args[1] = parameters;

            Action<string[]> parsedCommand;
            bool basicCommand = commands.TryGetValue(name, out parsedCommand);

            //basic commands
            if (basicCommand)
                parsedCommand(parameters);

            //context sensitive commands
            else if (!basicCommand && Tokenizer.KnownCommands.Contains(name))
            {
                StoryItem objectToFind;

                if (objectFound(parameters, out objectToFind))
                {

                    Action<GameController> contextCommand;
                    bool validcontextCommand = objectToFind.Commands.TryGetValue(name, out contextCommand);

                    if (validcontextCommand)
                    {
                        contextCommand(this);
                    }
                }

                else
                {
                    InputResponse.Append($"There is no {String.Join(" ", parameters)} here.\n");
                }

            }

            else
            {
                //bogus command not understood
                InputResponse.Append("I dont understand\n");
            }


            game.GameLog.Add(InputResponse.ToString() + "\n");

        }

        void help(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                InputResponse.Append("\n\n----------------------\n"+
                                     "Command List\n" +
                                     "----------------------\n\n");
                InputResponse.Append(
                "Go, Walk, Move: Typing any of these will move the character in the direction specified.\n"
                + "Look: take a look at your sorroundings and list any obvious exits and visible items.\n"
                + "Examine: Take a closer look at an object.\n"
                + "Get, Take, Grab, Pick Up: Typing any of these will attempt to pick up the \n\t\t\t  specified object and add it to your invnetory.\n"
                + "Drop: Drop the specified object at the players current position. \n      Some objects my persist in the location they were dropped.\n"
                + "Inventory: Open the player Inventory.\n\n"
                );
            }

            else
            {
                InputResponse.Append("I don't understand. If you wan't help just type help!\n");
            }

            //don't count this as an actual move in the game
            game.Gamestats.Moves -= 1;
        }

        void go(string[] parameters)
        {

            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            string direction = "";
            Direction desiredDirecton;


            if (parameters.Length == 0)
            {
                InputResponse.Append("Go Where?\n");
            }

            else
            {
                direction = parameters[0].ToLower().Trim();
                direction = FirstCharToUpper(direction);
                try
                {
                    desiredDirecton = (Direction) Enum.Parse(typeof(Direction), direction);

                    if (!currentRoom.Exits.ContainsKey(direction))
                    {
                        InputResponse.Append("You can't move in that direction\n");
                    }

                    else if (currentRoom.Exits[direction].IsLocked)
                    {
                        InputResponse.AppendFormat("{0} is locked, maybe if you had a key...\n", currentRoom.Exits[direction].Name);
                    }

                    else
                    {
  
                        player.PlayerLocation = game.Rooms[currentRoom.Exits[direction].DestinationKey];
                        currentRoom = player.PlayerLocation;
                        InputResponse.AppendFormat("\nMoving to {0}\n", currentRoom.Name);

                        if (currentRoom.TimesVisited == 0)
                        {
                            look(new string[] { });
                        }
                        currentRoom.TimesVisited += 1;

                    }
                }

                catch (System.ArgumentException e)
                {
                    InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

            }



        }

        void look(string[] parameters)
        {

            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            //location description
            InputResponse.AppendFormat("\nYou are in {0}: {1}\n\n", currentRoom.Name, currentRoom.Description);


            //list items
            InputResponse.Append("You see: \n");
            string itemString = "";
            foreach (GameObject item in game.Items)
            {
                if (item.LocationKey == player.PlayerLocation.KeyValue)
                {
                    itemString += item.Name + " : " + item.Description + "\n";
                }

            }


            InputResponse.Append(itemString != "" ? itemString + "\n" : "Nothing\n\n");

            //exits
            InputResponse.Append("Obvious Exits : \n");

            var exits = from pair in currentRoom.Exits
                        orderby (Direction)Enum.Parse(typeof(Direction), pair.Key) ascending
                        select pair;

            foreach (KeyValuePair<string, Exit> exit in exits)
            {
                

                if (exit.Value.IsLocked)
                {
                    InputResponse.AppendFormat("To the {0} you see: {1}\n", exit.Key, exit.Value.Name);
                }

                else
                {
                    InputResponse.AppendFormat("To the {0} you see: {1}\n", exit.Key, game.Rooms[exit.Value.DestinationKey].Name);
                }

                
            }

        }

        
        void examine(string[] parameters)
        {
            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            //join the parameters with a space and loop through the rooms to see if there is a match
            string searchString = String.Join(" ", parameters);

            //if the player is examining a room call the look function instead
            foreach (KeyValuePair<string, Room> room in game.Rooms)
            {
                if (room.Key.ToLower() == searchString.ToLower())
                {
                    if (room.Value.KeyValue == currentRoom.KeyValue)
                    {
                        look(new[] { "" });
                    }

                    else
                    {
                        InputResponse.AppendFormat($"You are not in the {searchString}...\n\n");
                        look(new[] { "" });
                    }
                }
            }

            StoryItem objectToExamine;

            objectToExamine = checkInventory(parameters);

            if (objectToExamine != null) {
                InputResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n", objectToExamine.ExamineResponse);
                return;
            }

            objectToExamine = checkRoom(parameters);

            if (objectToExamine != null)
            {
                InputResponse.AppendFormat(objectToExamine.ExamineResponse + "\n\n");
                return;
            }

            InputResponse.AppendFormat($"There is no {String.Join(" ", parameters)} here.\n");





        }


        void inventory(string[] parameters)
        {


            if (parameters.Length > 0)
            {
                InputResponse.Append("The inventory command is not valid with any other combination of words. Try typing 'Inventory', 'Backpack', or 'Inv' \n");
                return;
            }

            InputResponse.Append("\nInventory\n");
            InputResponse.Append("--------------------------------------------------------------------------------------------\n\n");

            foreach (StoryItem item in ItemsinInventory)
            {

                InputResponse.AppendFormat("{0} : {1} \n", item.Name, item.Description);


            }

            InputResponse.Append("\n");
        }

        //todo refactor to use internal inventory add method
        /*
        void take(string[] parameters)
        {
            Player player = game.Player;

            StoryItem itemToTake;
            itemToTake = checkInventory(parameters);

            if (itemToTake != null)
            {
                InputResponse.AppendFormat("You already have the item {0}.\n", itemToTake.Name);
                return;
            }

            itemToTake = checkRoom(parameters);

            if (itemToTake != null)
            {

                if (itemToTake.IsPortable)
                {
                    if (player.BackPack.ItemCount < player.BackPack.Slots)
                    {
                        itemToTake.LocationKey = "inventory";
                        InputResponse.AppendFormat("{0} taken.\n", itemToTake.Name);
                        player.BackPack.ItemCount += 1;
                    }

                    else
                    {
                        InputResponse.AppendFormat("You don't have any space for the {0} in your inventory! Try dropping something you don't need.\n", itemToTake.Name);
                    }


                }

                else
                {
                    InputResponse.AppendFormat("You try to take the {0} but it won't budge!\n", itemToTake.Name);
                }
                
            }



            else
            {
                InputResponse.AppendFormat($"There is no {String.Join(" ", parameters)} here to take.\n");
            }
            

        }
        */
        //todo refactor to use internal inventory drop method
        void drop(string[] parameters)
        {
            Player player = game.Player;
            StoryItem itemToDrop;

            itemToDrop = checkInventory(parameters);


            if (itemToDrop != null)
            {

                itemToDrop.LocationKey = game.Player.PlayerLocation.KeyValue;
                InputResponse.AppendFormat($"You dropped the {itemToDrop.Name} like it's hot.\n");
                player.BackPack.ItemCount -= 1;
                
            }

            else
            {
                InputResponse.AppendFormat($"You don't have a {String.Join(" ", parameters)} to drop.\n");
            }
            

        }


        #region helper methods

        /// <summary>
        ///Check if a there is an object that matches 
        ///the tokens left in the parameters array
        ///either in the players inventory or the current room
        ///and return the result and a reference to the object
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="itemToActOn"></param>
        /// <returns>bool</returns>
        bool objectFound(string[] parameters, out StoryItem itemToActOn)
        {

            string noun = "";
            itemToActOn = null;

            //items in player inventory
            for (int i = 0; i < ItemsinInventory.Count; i++)
            {
                noun = "";

                for (int j = 0; j < parameters.Length; j++)
                {
                    noun = String.Join(" ", parameters, 0, j + 1);

                    if (noun == ItemsinInventory[i].Name.ToLower())
                    {

                        itemToActOn = ItemsinInventory[i];
                        return true;

                    }
                }

            }

            //items in current room
            for (int i = 0; i < ItemsinRoom.Count; i++)
            {
                noun = "";

                for (int j = 0; j < parameters.Length; j++)
                {
                    noun = String.Join(" ", parameters, 0, j + 1);

                    if (noun == ItemsinRoom[i].Name.ToLower())
                    {

                        itemToActOn = ItemsinRoom[i];
                        return true;

                    }
                }

            }

            //Exits in current room
            foreach (Exit exit in  game.Player.PlayerLocation.Exits.Values)
            {
                noun = "";

                for (int j = 0; j < parameters.Length; j++)
                {
                    noun = String.Join(" ", parameters, 0, j + 1);

                    if (noun == exit.Name.ToLower())
                    {

                        itemToActOn = exit;
                        return true;

                    }
                }

            }

            return false;

        }

        public StoryItem checkInventory(string[] parameters)
        {
            StoryItem itemToActOn = null;
            
            //player inventory
            for (int i = 0; i < ItemsinInventory.Count; i++)
            {
                string noun = "";

                for (int j = 0; j < parameters.Length; j++)
                {
                    noun = String.Join(" ", parameters, 0, j + 1);

                    if (noun.ToLower() == ItemsinInventory[i].Name.ToLower())
                    {

                        return itemToActOn = ItemsinInventory[i];

                    }
                }

            }

            return itemToActOn;
        }

        public StoryItem checkRoom(string[] parameters)
        {
            StoryItem itemToActOn = null;

            for (int i = 0; i < ItemsinRoom.Count; i++)
            {
                string noun = "";

                for (int j = 0; j < parameters.Length; j++)
                {
                    noun = String.Join(" ", parameters, 0, j + 1);

                    if (noun.ToLower() == ItemsinRoom[i].Name.ToLower())
                    {

                        return itemToActOn = ItemsinRoom[i];

                    }
                }

            }

            return itemToActOn;
        }



        static public string FirstCharToUpper(string input) => input.First().ToString().ToUpper() + input.Substring(1);


        static string GetWordWrappedParagraph(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return string.Empty;
            }

            var approxLineCount = paragraph.Length / Console.WindowWidth;
            var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (var i = 0; i < paragraph.Length;)
            {
                var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
                var line = paragraph.Substring(i, grabLimit);

                var isLastChunk = grabLimit + i == paragraph.Length;

                if (isLastChunk)
                {
                    i = i + grabLimit;
                    lines.Append(line);
                }
                else
                {
                    var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
                    lines.AppendLine(line.Substring(0, lastSpace));

                    //Trailing spaces needn't be displayed as the first character on the new line
                    i = i + lastSpace + 1;
                }
            }
            return lines.ToString();
        }
        #endregion



    }
}


    