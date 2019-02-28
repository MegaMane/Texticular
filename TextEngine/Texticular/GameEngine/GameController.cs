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
        public static StringBuilder InputResponse = new StringBuilder();

        public Game Game;
        public Story story = new Story();
        public string UserInput;
        public List<StoryItem> ItemsinInventory;
        public List<StoryItem> ItemsinRoom;

        Dictionary<string, Action<ParseTree>> commands;
        Lexer Tokenizer;




        //new UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;




        public GameController(Game game)
        {

            Game = game;
            Game.Player.PlayerLocationChanged += PlayerLocationChangedHandler;

            foreach(StoryItem item in Game.Items.Values)
            {
                item.LocationChanged += ItemLocationChangedHandler;
            }

            commands = new Dictionary<string, Action<ParseTree>>(); 
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
            Terminal.Init(110, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
            //Console.SetCursorPosition(0, 40);

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
            Terminal.SetCurrentConsoleFontEx(8, 10);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
        }




  


        public void Start()
        {
            Action<GameController> playScene = story.Scenes["intro"].SceneAction;
            playScene(this);
            InputResponse.Append("Type Help for a list of commands...\n\n ");
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
            Game.Gamestats.updateStats(10);
            

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


            mainBuffer = Terminal.CreateBuffer(80, 41);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
            narrative.Write(InputResponse.ToString(), fg:ConsoleColor.DarkGreen);
            mainBuffer.Blit(0, 2);
            Console.SetCursorPosition(0, 45);
        }


        public void Parse(String userInput)
        {
            ParseTree tokens = Tokenizer.Tokenize(UserInput);

            if (tokens == null) return; //the player just hit enter without typing anything

            if(tokens.Verb == null)
            {
                InputResponse.Append($"I dont understand '{UserInput}'! Type help for some examples of what I do understand.");
                return;
            }

            //look with no object after it means look at surroundings
            if (tokens.Verb == "look" && tokens.DirectObject == null)
            {
                tokens.DirectObject = Game.Player.PlayerLocation.Name.ToLower();
                tokens.DirectObjectKeyValue = Game.Player.PlayerLocation.KeyValue;
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


                Action<GameController> contextCommand;
                bool validcontextCommand = objectToFind.Commands.TryGetValue(tokens.Verb, out contextCommand);

                if (validcontextCommand)
                {
                    contextCommand(this);
                }

                else
                {
                    InputResponse.Append($"You cant {tokens.Verb} {tokens.UnparsedInput.Replace(tokens.Verb, "")}.\n");
                }
                

            }

            else
            {
                //bogus command not understood
                InputResponse.Append($"{tokens.Verb} what?");
            }


            Game.GameLog.Add(InputResponse.ToString() + "\n");

        }


        #region Basic Commands
        void help(ParseTree tokens)
        {
            if (tokens.DirectObject == null && tokens.IndirectObject == null)
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

        void go(ParseTree tokens)
        {

            Player player = Game.Player;
            Room currentRoom = player.PlayerLocation;


            string direction = "";
            Direction desiredDirecton;


            if (tokens.DirectObject == null)
            {
                InputResponse.Append("Go Where?\n");
            }

            else
            {
                direction = tokens.DirectObject;

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
                    
                        
                    }
                }

                catch (System.ArgumentException e)
                {
                    InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

            }



        }

        void inventory(ParseTree tokens)
        {


            if (tokens.DirectObject != null && tokens.IndirectObject != null)
            {
                InputResponse.Append("The inventory command is not valid with any other combination of words. Try typing 'Inventory', 'Backpack', or 'Inv'\n ");
                return;
            }

            InputResponse.Append("\n Inventory\n ");
            InputResponse.Append("------------------------------------------------------\n\n ");

            foreach (var item in GameObject.Objects.Values)
            {
                if (item.LocationKey == "inventory")
                {
                    InputResponse.AppendFormat("{0} : {1}\n ", item.Name, item.Description);
                }



            }

            InputResponse.Append("\n ");
        }

        #endregion

        #region event handlers

        void PlayerLocationChangedHandler(object sender, PlayerLocationChangedEventArgs args)
        {
            //look at the players surroundings automatically 
            //when they enter a new location
            InputResponse.AppendFormat("Moving to {0}\n ", args.NewLocation.Name);
            args.NewLocation.TimesVisited += 1;
            args.NewLocation.Commands["look"](this);
        }

        void ItemLocationChangedHandler(object sender, ItemLocationChangedEventArgs args)
        {
            //the object was removed from a container
            StoryItem storyItem;
            bool itemFound = Game.Items.TryGetValue(args.CurrentLocation, out storyItem);

            if (itemFound && storyItem is Container)
            {
                Container container = (Container)storyItem;
                container.Items.Remove((StoryItem) sender);
            }

            //the object was placed in a container
            itemFound = Game.Items.TryGetValue(args.NewLocation, out storyItem);

            if (itemFound && storyItem is Container)
            {
                Container container = (Container)storyItem;
                container.Items.Add((StoryItem)sender);
            }
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

                if(ItemsinRoom[i] is Container && (ItemsinRoom[i] as Container).IsOpen)
                {
                    Container container = (Container)ItemsinRoom[i];

                    foreach (StoryItem loot in container.Items)
                    {
                        for (int j = 0; j < parameters.Length; j++)
                        {
                            noun = String.Join(" ", parameters, 0, j + 1);

                            if (noun == loot.Name.ToLower())
                            {

                                itemToActOn = loot;
                                return true;

                            }
                        }

                    }
                    
                }

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


    