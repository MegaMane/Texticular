using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    class Room201Bathroom : IGameState
    {
        enum SceneList
        {
            none,
            intro,
            movePlayer,
            gameOver,
            transitionHotelRoom,

        }

        enum ChoiceList
        {
            PlayerName
        }

        public int TimesEntered { get; set; } = 0;
        public string UserInput { get; set; }
        public GameController Controller;
        public Player Player;
        public Room Room;
        Dictionary <SceneList, string> Scenes { get; set; }
        Dictionary <ChoiceList, Func<GameController, String, SceneList>> Choices { get; set; }
        public Dictionary<string, Action<ParseTree>> Commands { get; set; }
        Lexer Tokenizer;
        SceneList CurrentScene;


        public Room201Bathroom(GameController controller)
        {
            Controller = controller;
            Player = Controller.Game.Player;
            Room = GameObject.GetComponent<Room>("room201_bathroom");
            UserInput = "";

            Scenes = new Dictionary<SceneList, string>();
            AddScenes();
            CurrentScene = SceneList.intro;

            Choices = new Dictionary<ChoiceList, Func<GameController, String, SceneList>>();
            AddChoices();

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

            if (CurrentScene == SceneList.intro)
            {
                //push the player back to the room if it's the first time they have tried to enter
               
                if (TimesEntered == 1)
                {
                    GameController.InputResponse.Clear();
                    GameController.InputResponse.Append(Scenes[CurrentScene]);
                    CurrentScene = SceneList.movePlayer;
                    return;
                }

                else
                {
                    //check if the dog is still in here eventually
                    GameController.InputResponse.Clear();
                    CurrentScene = SceneList.gameOver;
                    GameController.InputResponse.Append(Scenes[CurrentScene]);
                    return;
                }

            }

            if (CurrentScene == SceneList.movePlayer)
            {
                UserInput = "go east";
                Tokenizer.Parse(Controller);
                CurrentScene = SceneList.intro;
                Controller.SetGameState("Room201");
            }

            if (CurrentScene == SceneList.gameOver)
            {
                Controller.SetGameState("PlayerQuit");
            }


            if (CurrentScene == SceneList.none)
            {
                Tokenizer.Parse(Controller);
                Controller.Game.Gamestats.updateStats(10);

                if (CurrentScene == SceneList.transitionHotelRoom)
                {
                    Controller.SetGameState("Room201");
                }

                if (CurrentScene == SceneList.gameOver)
                {
                    Controller.SetGameState("PlayerQuit");
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
                        if (direction.ToLower() == "east")
                        {
                            CurrentScene = SceneList.transitionHotelRoom;
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
            Scenes[SceneList.intro] = "You crack open the door to the bathroom and it looks like it's seen better days. From the smell of it, it looks like "
                      + "someone beat you to it and narrowly escaped a hard fought battle with an eight pound burrito. The {sink} is old and yellowed. "
                      + "and caked with brown muck in the corners. The {mirror} is cracked and something is written on it red. You can't quite "
                      + "make it out. But you don't care...you've gotta take a shit! You rush to be the first in line to make a deposit in the "
                      + "porcelain bank {toilet}. But just as you are about to Drop it like it's hot you notice there is an an angry {Great Dane} "
                      + "guarding the toilet and he looks hungry! You quickly shut the door and somehow manage to not lose your shit (literally). "
                      + "Looks like you have to find somewhere else to go if you value your junk...and your life.";

            Scenes[SceneList.gameOver] = @"You blatantly ignore the fact that their is a vicious Great Dane in the bathroom. "
                     + "This brown baby is coming now! Unfortunately the dog does not ignore you and decides now would be a good time to rip your throat out.";

        }

        void AddChoices()
        {
            //Choices[ChoiceList.PlayerName] = delegate (GameController controller, string userInput)
            //{
            //    Player player = controller.Game.Player;

            //    if (userInput != "" || player.FirstName != "")
            //    {
            //        player.FirstName = userInput;
            //        return SceneList.letter;
            //    }

            //    GameController.InputResponse.Append("What is your name?");
            //    return SceneList.intro;

            //};


            
        }
    }
}
