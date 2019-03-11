using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    class WestHallway : IGameState
    {
        enum SceneList
        {
            none,
            transitionRoom201
        }



        public int TimesEntered { get; set; } = 0;
        public string UserInput { get; set; } = "";
        public GameController Controller;
        public Player Player;
        public Room Room;
        Dictionary <SceneList, string> Scenes { get; set; }
        //Dictionary <ChoiceList, Func<GameController, String, SceneList>> Choices { get; set; }
        public Dictionary<string, Action<ParseTree>> Commands { get; set; }
        Lexer Tokenizer;
        SceneList CurrentScene;


        public WestHallway(GameController controller)
        {
            Controller = controller;
            Player = Controller.Game.Player;
            Room = GameObject.GetComponent<Room>("westHallway");
            UserInput = "";

            Scenes = new Dictionary<SceneList, string>();
            AddScenes();
            CurrentScene = SceneList.none;

            //Choices = new Dictionary<ChoiceList, Func<GameController, String, SceneList>>();
            //AddChoices();

            //Controller.Game.Player.PlayerLocationChanged += PlayerLocationChangedHandler;

            foreach (StoryItem item in Controller.Game.Items.Values)
            {
                item.LocationChanged += ItemLocationChangedHandler;
            }

            Commands = new Dictionary<string, Action<ParseTree>>();
            Tokenizer = new Lexer();

            //trigger commands
            Commands["go"] = go;
            Commands["walk"] = go;
            Commands["move"] = go;


        }

        public void OnEnter()
        {

            UserInput = "look";
            Update(Controller.ElapsedTime.ElapsedMilliseconds);
            

            
        }

        public void OnExit()
        {
            
        }

        public void Render()
        {
            Controller.UI.DrawGameUI();
            GameController.InputResponse.Clear();
            GetInput();
        }

        public void Update(float elapsedTime)
        {

            if (CurrentScene == SceneList.none)
            {
                Tokenizer.Parse(Controller);
                Controller.Game.Gamestats.updateStats(10);

                if (CurrentScene == SceneList.transitionRoom201)
                {
                    CurrentScene = SceneList.none;
                    Controller.SetGameState("Room201");
                    return;
                }

            }

        }

        public void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }



        #region Trigger Commands


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
                        if (direction.ToLower() == "west")
                        {
                            CurrentScene = SceneList.transitionRoom201;
                        }

                        player.PlayerLocation = Controller.Game.Rooms[Room.Exits[direction].DestinationKey];


                    }
                }

                catch (System.ArgumentException e)
                {
                    GameController.InputResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

            }



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

        void AddScenes()
        {
            // add scenes here
        }

        void AddChoices()
        {
            //add choices here

            
        }
    }
}
