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
        public string UserInput;
        GameController Controller;


        Dictionary<string, Action<ParseTree>> commands;
        Lexer Tokenizer;

        //UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;

        public ExplorationState(GameController controller)
        {
            Controller = controller;
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

            //UI Stuff
            Terminal.Init(110, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
            GameStatistics testStats = new GameStatistics();
            this.ui = new UserInterface(testStats);

            mainBuffer = Terminal.CreateBuffer(80, 41);
            Terminal.SetCurrentConsoleFontEx(8, 10);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
        }

        public void OnEnter()
        {
            GameController.InputResponse.Clear();
            if (TimesEntered == 1)
            {
                GameController.InputResponse.Append("Type Help for a list of commands...\n\n ");
            }
            UserInput = "look";
            Parse("look");
            Render();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            Console.Clear();
            ui.DrawGameUI(Controller);


            mainBuffer = Terminal.CreateBuffer(80, 41);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
            narrative.Write(GameController.InputResponse.ToString(), fg: ConsoleColor.DarkGreen);
            mainBuffer.Blit(0, 2);
            Console.SetCursorPosition(0, 45);
        }

        public void Update(float elapsedTime)
        {
            GetInput();
            GameController.InputResponse.Clear();
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
            if (UserInput.ToLower().Trim() == "exit")
            {
                Controller.SetGameState("PlayerQuit");
                return;
            }
            ParseTree tokens = Tokenizer.Tokenize(UserInput);

            if (tokens == null) return; //the player just hit enter without typing anything


            if (tokens.Verb == null)
            {
                GameController.InputResponse.Append($"I dont understand '{UserInput}'! Type help for some examples of what I do understand.");
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



    }
}
