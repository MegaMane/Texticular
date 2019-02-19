using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Texticular.Environment;
using Texticular.GameEngine;
using Texticular.UI;

namespace Texticular
{
    public class GameController
    {
        public Game Game;
        public string UserInput;
        public StringBuilder InputResponse;
        Dictionary<string, Action<string[]>> commands;
        public List<StoryItem> ItemsinInventory;
        public List<StoryItem> ItemsinRoom;
        Lexer Tokenizer;

        public Story story = new Story();

        //new UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;




        public GameController(Game game)
        {

            Game = game;
            Game.Player.OnPlayerLocationChanged += PlayerLocationChangedHandler;
            InputResponse = new StringBuilder();
            commands = new Dictionary<string, Action<string[]>>();
            ItemsinInventory = new List<StoryItem>();
            ItemsinRoom = new List<StoryItem>();
            Tokenizer = new Lexer();


            //basic commands not attached to an object
            commands["go"] = go;
            commands["walk"] = go;
            commands["move"] = go;

            commands["inventory"] = inventory;
            commands["backpack"] = inventory;
            commands["inv"] = inventory;

            commands["help"] = help;

            //new UI Stuff
            Terminal.Init(95, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
            Console.SetCursorPosition(0, 40);

            Gamestats testStats = new Gamestats();

            testStats.HP = testStats.MaxHP = 20;
            testStats.MP = testStats.MaxMP = 10;
            testStats.ST = testStats.MaxST = 10;
            testStats.GP = 0;
            testStats.Level = 1;
            testStats.Strength = 3;
            testStats.Intelligence = 2;
            testStats.Piety = 1;
            testStats.Vitality = 3;
            testStats.Dexterity = 1;
            testStats.Speed = 2;
            testStats.Personality = 1;
            testStats.Luck = 1;


            this.ui = new UserInterface(testStats);

            mainBuffer = Terminal.CreateBuffer(80, 41);
            Terminal.SetCurrentConsoleFontEx(10, 12);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
        }




  


        public void Start()
        {
            //Action<GameController> playScene = story.Scenes["intro"].SceneAction;
            //playScene(this);
            InputResponse.Append("Type Help for a list of commands...\n\n");
            //need to manually set the input to look after playing the intro scene 
            //to print the room description
            UserInput = "look";
            Parse("look");
            Render();
        }

        public void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public void Update()
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

        public void Render()
        {
            Console.Clear();
            ui.DrawGameUI(this);


           // mainBuffer = Terminal.CreateBuffer(80, 41);
            narrative = new Narrative(mainBuffer);
           // mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
            narrative.Write(InputResponse.ToString(), fg:ConsoleColor.DarkGreen);
            mainBuffer.Blit(0, 2);
            Console.SetCursorPosition(0, 45);
        }


        public void Parse(String userInput)
        {
            ItemsinInventory.Clear();
            foreach (StoryItem item in Game.Items)
            {
                if (item.LocationKey == "inventory")
                {
                    ItemsinInventory.Add(item);
                }
            }


            ItemsinRoom.Clear();
            foreach (StoryItem item in Game.Items)
            {
                if (item.LocationKey == Game.Player.PlayerLocation.KeyValue)
                {
                    ItemsinRoom.Add(item);
                }
            }

            char[] delimiters = { ' ', ',' };

            // look with no object after it means look at the players surroundings
            if(UserInput == "look")
            {
                UserInput += $" {Game.Player.PlayerLocation.Name.ToLower()}";
            }
            string[] commandParts = UserInput.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

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
                GameObject objectToFind;

                if (objectFound(parameters, out objectToFind))
                {

                    Action<GameController> contextCommand;
                    bool validcontextCommand = objectToFind.Commands.TryGetValue(name, out contextCommand);

                    if (validcontextCommand)
                    {
                        contextCommand(this);
                    }

                    else
                    {
                        InputResponse.Append($"You cant {name} {String.Join(" ", parameters)}.\n");
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


            Game.GameLog.Add(InputResponse.ToString() + "\n");

        }


        #region Basic Commands
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
            Game.Gamestats.Moves -= 1;
        }

        void go(string[] parameters)
        {

            Player player = Game.Player;
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
  
                        player.PlayerLocation = Game.Rooms[currentRoom.Exits[direction].DestinationKey];
                        currentRoom = player.PlayerLocation;
                        InputResponse.AppendFormat("\nMoving to {0}\n", currentRoom.Name);


                        //player.PlayerLocation.Commands["look"](this);

                        currentRoom.TimesVisited += 1;

                    }
                }

                catch (System.ArgumentException e)
                {
                    InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

            }



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

        #endregion

        #region event handlers

        void PlayerLocationChangedHandler(object sender, LocationChangedEventArgs args)
        {
            //look at the players surroundings automatically 
            //when they enter a new location
            args.NewLocation.Commands["look"](this);
        }


        #endregion

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
        bool objectFound(string[] parameters, out GameObject itemToActOn)
        {

            string noun = "";
            itemToActOn = null;

            //the target object is the current room

            for (int j = 0; j < parameters.Length; j++)
            {
                noun = String.Join(" ", parameters, 0, j + 1);

                if (noun == Game.Player.PlayerLocation.Name.ToLower())
                {

                    itemToActOn = Game.Player.PlayerLocation;
                    return true;

                }
            }

            

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
            foreach (Exit exit in  Game.Player.PlayerLocation.Exits.Values)
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


        #endregion



    }
}


    