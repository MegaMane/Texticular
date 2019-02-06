using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace TextEngine
{
    public partial class GameController
    {
        //add the rest of the exits for the map
        //add simple interactable object like use key
        Game game;
        public StringBuilder InputResponse;
        Dictionary<string, Action<string[]>> commands;
        List<StoryItem> ItemsinInventory;
        List<StoryItem> ItemsinRoom;

        public GameController(Game game)
        {
            this.game = game;
            this.InputResponse = new StringBuilder();
            this.commands = new Dictionary<string, Action<string[]>>();
            this.ItemsinInventory = new List<StoryItem>();
            this.ItemsinRoom = new List<StoryItem>();

            commands["go"] = go;
            commands["walk"] = go;
            commands["move"] = go;

            commands["look"] = look;

            commands["examine"] = examine;

            commands["take"] = take;
            commands["get"] = take;
            commands["grab"] = take;
            commands["pick up"] = take;

            commands["drop"] = drop;

            commands["inventory"] = inventory;

            commands["use"] = use;

            commands["help"] = help;

            //commands["unlock"] = unlock;
            //commands["open"]
            // commands["talk"]
        }

        public void Start()
        {
            InputResponse.Append("Type Help for a list of commands...\n\n");
            Parse("look");
            int startingScore = 0;
            int startingMoves = 0;
            InputResponse.Append(game.gamestats.updateStats(startingScore, startingMoves));
        }

        public void Update (string userInput)
        {
            InputResponse.Clear();
            Parse(userInput);
            int testScore = 100;
            InputResponse.Append(game.gamestats.updateStats(testScore));

        }

        public void Parse(String userInput)
        {

            ItemsinInventory.Clear();
            foreach (StoryItem item in game.Items)
            {
                if (item.LocationID == game.Rooms["inventory"].ID)
                {
                    ItemsinInventory.Add(item);
                }
            }


            ItemsinRoom.Clear();
            foreach (StoryItem item in game.Items)
            {
                if (item.LocationID == game.Player.PlayerLocation.ID)
                {
                    ItemsinRoom.Add(item);
                }
            }

            char[] delimiters = { ' ', ',' };
            string[] commandParts = userInput.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (commandParts.Length == 0) return ; //the player just hit enter without typing anything

            int offset = 1;
            
            // Name of the method we want to call.
            string name = commandParts[0];

            //two word verbs
            if (name == "pick")//pick up
            {
                name += " " + commandParts[1];
                offset = 2;
            }

            // Get the nouns after the verb (the objects to act on)
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
            bool validCommand = commands.TryGetValue(name, out parsedCommand);

            if (validCommand)
                parsedCommand(parameters);

            else
               InputResponse.Append("I dont understand\n");

            game.gameLog.Add(InputResponse.ToString() + "\n");
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
            game.gamestats.Moves -= 1;
        }

        void go(string[] parameters)
        {

            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            string direction = "";
            int desiredDirecton;


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
                    desiredDirecton = (int)Enum.Parse(typeof(Direction), direction);

                    if (currentRoom.Exits[desiredDirecton].Destination == null)
                    {
                        InputResponse.Append("You can't move in that direction\n");
                    }

                    else if (currentRoom.Exits[desiredDirecton].IsLocked == true)
                    {
                        InputResponse.AppendFormat("The {0} is locked, maybe if you had a key...\n", currentRoom.Exits[desiredDirecton].Name);
                    }

                    else
                    {
  
                        player.PlayerLocation = currentRoom.Exits[desiredDirecton].Destination;
                        currentRoom = player.PlayerLocation;
                        InputResponse.AppendFormat("Moving to {0}\n", currentRoom.Name);

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
            InputResponse.AppendFormat("\nYou are in the {0}: {1}\n\n", currentRoom.Name, currentRoom.Description);


            //list items
            InputResponse.Append("You see: \n");
            string itemString = "";
            foreach (StoryItem item in game.Items)
            {
                if (item.LocationID == player.PlayerLocation.ID)
                {
                    itemString += item.Name + " : " + item.Description + "\n";
                }

            }

            InputResponse.Append(itemString != "" ? itemString + "\n" : "Nothing\n\n");

            //exits
            InputResponse.Append("Obvious Exits : \n");

            for (int i = 0; i < (currentRoom.Exits.Length); i++)
            {
                Exit exit = currentRoom.Exits[i];
                if (exit.Destination != null)
                {
                    if (exit.IsLocked)
                    {
                        InputResponse.AppendFormat("To the {0} you see the {1}\n", (Direction)i, exit.Name);
                    }

                    else
                    {
                        InputResponse.AppendFormat("To the {0} you see the {1}\n", (Direction)i, exit.Destination.Name);
                    }

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
            foreach(Room room in game.Rooms.Values)
            {
                if (room.Name.ToLower() == searchString.ToLower())
                {
                    if (room.ID == currentRoom.ID)
                    {
                        look(new []{ ""});
                    }

                    else
                    {
                        InputResponse.AppendFormat($"You are not in the {searchString}...\n\n");
                        look(new[] { "" });
                    }
                }
            }

            foreach (string item in parameters)
            {
                var examineInventory = ItemsinInventory.Where(p => p.Name.ToLower() == item).ToList();
                var examineRoom = ItemsinRoom.Where(p => p.Name.ToLower() == item).ToList();

                if (examineInventory.Count > 0)
                {
                    foreach(StoryItem inventoryItem in examineInventory)
                    {
                        InputResponse.AppendFormat("You look in your trusty backpack and you see {0}.\n\n",inventoryItem.ExamineResponse);
                    }
                }

                else if (examineRoom.Count > 0)
                {
                    foreach (StoryItem roomItem in examineRoom)
                    {
                        InputResponse.Append(roomItem.ExamineResponse + "\n\n");
                    }
                }

                else
                {
                    InputResponse.AppendFormat("There is no {0} here.\n", item);
                }
            }


        }





            void inventory(string[] parameters)
        {


            if (parameters.Length > 0)
            {
                InputResponse.Append("The inventory command is not valid with any other combination of words. Try typing 'Inventory' \n");
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


        void take(string[] parameters)
        {
            Player player = game.Player;

            foreach (string item in parameters)
            {
                var examineInventory = ItemsinInventory.Where(p => p.Name.ToLower() == item).ToList();
                var examineRoom = ItemsinRoom.Where(p => p.Name.ToLower() == item).ToList();

                if (examineRoom.Count > 0)
                {
                    foreach (StoryItem roomItem in examineRoom)
                    {
                        if (roomItem.IsPortable)
                        {
                            if (player.BackPack.ItemCount < player.BackPack.Slots)
                            {
                                roomItem.LocationID = game.Rooms["inventory"].ID;
                                InputResponse.AppendFormat("{0} taken.\n", roomItem.Name);
                                player.BackPack.ItemCount += 1;
                            }

                            else
                            {
                                InputResponse.AppendFormat("You don't have any space for the {0} in your inventory! Try dropping something you don't need.\n", roomItem.Name);
                            }


                        }

                        else
                        {
                            InputResponse.AppendFormat("You try to take the {0} but it won't budge!\n", roomItem.Name);
                        }
                    }
                }

                else if (examineInventory.Count > 0)
                {
                    foreach (StoryItem inventoryItem in examineInventory)
                    {
                        InputResponse.AppendFormat("You already have the item {0}.\n",inventoryItem.Name);
                    }
                }

                else
                {
                    InputResponse.AppendFormat("There is no {0} here to take.\n", item);
                }
            }

        }


        void drop(string[] parameters)
        {
            Player player = game.Player;

            foreach (string item in parameters)
            {
                var examineInventory = ItemsinInventory.Where(p => p.Name.ToLower() == item).ToList();

                if (examineInventory.Count > 0)
                {
                    foreach (StoryItem inventoryItem in examineInventory)
                    {
                        inventoryItem.LocationID = game.Player.PlayerLocation.ID;
                        InputResponse.AppendFormat("You dropped the {0} like it's hot.\n", inventoryItem.Name);
                        player.BackPack.ItemCount -= 1;
                    }
                }

                else
                {
                    InputResponse.AppendFormat("You don't have a {0} to drop.\n", item);
                }
            }

        }


  
        //helper methods
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}