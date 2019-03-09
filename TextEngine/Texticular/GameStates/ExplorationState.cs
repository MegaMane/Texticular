using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;
using Texticular.GameStates;
using Texticular.UI;

namespace Texticular.GameStates
{
    class ExplorationState : IGameState
    {
        public int TimesEntered { get; set; } = 0;
        public string UserInput="";
        GameController Controller;
        Player Player;


        Dictionary<string, Action<ParseTree>> commands;
        Lexer Tokenizer;


        public ExplorationState(GameController controller)
        {
            Controller = controller;

            Player = Controller.Game.Player;

            Controller.Game.Player.PlayerLocationChanged += PlayerLocationChangedHandler;

            foreach (StoryItem item in Controller.Game.Items.Values)
            {
                item.LocationChanged += ItemLocationChangedHandler;
            }

            commands = new Dictionary<string, Action<ParseTree>>();
            Tokenizer = new Lexer();

            //basic commands not attached to an object
            commands["go"] = go;
            commands["walk"] = go;
            commands["move"] = go;

            commands["inventory"] = inventory;
            commands["backpack"] = inventory;
            commands["inv"] = inventory;

            commands["help"] = help;

        }

        public void OnEnter()
        {
            GameController.InputResponse.Clear();
            if (TimesEntered == 1)
            {
                GameController.InputResponse.Append("Type Help for a list of commands...\n\n ");
            }

            //force the player location changed event so the player automatically 'looks' at their surroundings.
            //Controller.Game.Player.PlayerLocation = Controller.Game.Player.PlayerLocation;
            UserInput = "look";
            Update(Controller.ElapsedTime.ElapsedMilliseconds);
        }

        public void OnExit()
        {
            //code to cleanup state here
        }

        public void Render()
        {
            Controller.UI.DrawGameUI();
            GameController.InputResponse.Clear();
            GetInput();
        }

        public void Update(float elapsedTime)
        {

            Parse(UserInput);
            Controller.Game.Gamestats.updateStats(10);
        }

        void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        void Parse(String userInput)
        {

            if (userInput.ToLower().Trim() == "exit")
            {
                Controller.SetGameState("PlayerQuit");
                return;
            }
            ParseTree tokens = Tokenizer.Tokenize(userInput);

            if (tokens == null)
            {
                tokens = new ParseTree() { Verb = "look", DirectObject = Player.PlayerLocation.Name, DirectObjectKeyValue = Player.PlayerLocation.KeyValue };
            }


            if (tokens.Verb == null)
            {
                GameController.InputResponse.Append($"I dont understand '{userInput}'! Type help for some examples of what I do understand.");
                return;
            }

            //look with no object after it means look at surroundings
            if (tokens.Verb == "look" && tokens.DirectObject == null)
            {
                tokens.DirectObject = Controller.Game.Player.PlayerLocation.Name.ToLower();
                tokens.DirectObjectKeyValue = Controller.Game.Player.PlayerLocation.KeyValue;
            }

            Action<ParseTree> parsedCommand;
            bool basicCommand = commands.TryGetValue(tokens.Verb, out parsedCommand);

            //basic commands go, help, inventory
            if (basicCommand)
                parsedCommand(tokens);



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
                    GameController.InputResponse.Append($"You cant {tokens.Verb} {tokens.UnparsedInput.Replace(tokens.Verb, "")}.\n");
                }


            }

            else
            {
                //bogus command not understood
                GameController.InputResponse.Append($"{tokens.Verb} what?");
            }


            Controller.Game.GameLog.Add(GameController.InputResponse.ToString() + "\n");

        }

        #region Basic Commands
        void help(ParseTree tokens)
        {
            if (tokens.DirectObject == null && tokens.IndirectObject == null)
            {
                GameController.InputResponse.Append("\n\n----------------------\n" +
                                     "Command List\n" +
                                     "----------------------\n\n");
                GameController.InputResponse.Append(
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
                GameController.InputResponse.Append("I don't understand. If you wan't help just type help!\n");
            }

            //don't count this as an actual move in the game
            Controller.Game.Gamestats.Moves -= 1;
        }

        void go(ParseTree tokens)
        {

            Player player = Controller.Game.Player;
            Room currentRoom = player.PlayerLocation;


            string direction = "";
            Direction desiredDirecton;


            if (tokens.DirectObject == null)
            {
                GameController.InputResponse.Append("Go Where?\n");
            }

            else
            {
                direction = tokens.DirectObject;

                try
                {
                    desiredDirecton = (Direction)Enum.Parse(typeof(Direction), direction);

                    if (!currentRoom.Exits.ContainsKey(direction))
                    {
                        GameController.InputResponse.Append("You can't move in that direction\n");
                    }

                    else if (currentRoom.Exits[direction].IsLocked)
                    {
                        GameController.InputResponse.AppendFormat("{0} is locked, maybe if you had a key...\n", currentRoom.Exits[direction].Name);
                    }

                    else
                    {

                        player.PlayerLocation = Controller.Game.Rooms[currentRoom.Exits[direction].DestinationKey];


                    }
                }

                catch (System.ArgumentException e)
                {
                    GameController.InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

            }



        }

        void inventory(ParseTree tokens)
        {


            if (tokens.DirectObject != null && tokens.IndirectObject != null)
            {
                GameController.InputResponse.Append("The inventory command is not valid with any other combination of words. Try typing 'Inventory', 'Backpack', or 'Inv'\n ");
                return;
            }

            GameController.InputResponse.Append("\n Inventory\n ");
            GameController.InputResponse.Append("------------------------------------------------------\n\n ");

            foreach (var item in GameObject.Objects.Values)
            {
                if (item.LocationKey == "inventory")
                {
                    GameController.InputResponse.AppendFormat("{0} : {1}\n ", item.Name, item.Description);
                }



            }

            GameController.InputResponse.Append("\n ");
        }

        #endregion

        #region event handlers

        void PlayerLocationChangedHandler(object sender, PlayerLocationChangedEventArgs args)
        {
            //look at the players surroundings automatically 
            //when they enter a new location
            Player Player = (Player)sender;

            GameController.InputResponse.AppendFormat("Moving to {0}\n ", args.NewLocation.Name);
            args.NewLocation.TimesVisited += 1;
            args.NewLocation.Commands["look"](new ParseTree() { Verb = "look", DirectObject = args.NewLocation.Name, DirectObjectKeyValue = args.NewLocation.KeyValue });
            
            //see if entering the location should change the game state
            checkTriggers(Player, args);

        }

        void checkTriggers(Player player, PlayerLocationChangedEventArgs args)
        {
            if (args.NewLocation.KeyValue == "room201_bathroom")
            {
                if (args.NewLocation.TimesVisited == 1)
                {
                    Controller.ActiveStoryScene = Scene.Bathroom201FirstVisit;
                    Controller.SetGameState("StoryScene");

                }

                else
                {
                    Controller.ActiveStoryScene = Scene.GameOverBathroom201;
                    Controller.SetGameState("StoryScene");
                }
            }
        }

        void ItemLocationChangedHandler(object sender, ItemLocationChangedEventArgs args)
        {
            //Special events for specific item location changes
        }

        #endregion

    }
}
