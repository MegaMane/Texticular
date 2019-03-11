using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    class Room201 : IGameState
    {
        enum SceneList
        {
            none,
            intro,
            letter,
            transitionBathroom,
            transitionHallway
        }

        enum ChoiceList
        {
            PlayerName
        }

        public int TimesEntered { get; set; } = 0;
        public string UserInput { get; set; } = "";
        public GameController Controller;
        public Player Player;
        public Room Room;
        Dictionary <SceneList, string> Scenes { get; set; }
        Dictionary <ChoiceList, Func<GameController, String, SceneList>> Choices { get; set; }
        public Dictionary<string, Action<ParseTree>> Commands { get; set; }
        Lexer Tokenizer;
        SceneList CurrentScene;


        public Room201(GameController controller)
        {
            Controller = controller;
            Player = Controller.Game.Player;
            Room = GameObject.GetComponent<Room>("room201");
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
            if(TimesEntered > 1)
            {
                UserInput = "look";
                Update(Controller.ElapsedTime.ElapsedMilliseconds);
            }

            
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

            if (CurrentScene == SceneList.intro)
            {
                GameController.InputResponse.Clear();
                GameController.InputResponse.Append(Scenes[CurrentScene]);
                CurrentScene = Choices[ChoiceList.PlayerName](Controller, UserInput);
            }

            if (CurrentScene == SceneList.letter)
            {
                GameController.InputResponse.Clear();
                string SceneText = Scenes[CurrentScene];
                SceneText = SceneText.Replace("<firstName>", Player.FirstName);
                Scenes[CurrentScene] = SceneText;
                GameController.InputResponse.Append(Scenes[CurrentScene]);
                CurrentScene = SceneList.none;
                return;
            }

            if (CurrentScene == SceneList.none)
            {
                Tokenizer.Parse(Controller);
                Controller.Game.Gamestats.updateStats(10);

                if (CurrentScene == SceneList.transitionBathroom)
                {
                    CurrentScene = SceneList.none;
                    Controller.SetGameState("Room201Bathroom");
                    return;
                }

                if (CurrentScene == SceneList.transitionHallway)
                {
                    throw new NotImplementedException();
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
                            CurrentScene = SceneList.transitionBathroom;
                        }
                        
                        if (direction.ToLower() == "east")
                        {
                            CurrentScene = SceneList.transitionHallway;
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
           Scenes[SceneList.intro] = @"You wake up with a pounding headache in a shabby looking hotel room "
                    + "surrounded by a bunch of empty cans.You've got a taste in your mouth like a dirty old "
                    + "rat crawled in and died in there. Disoriented, you roll out of the bed you woke up in, barely "
                    + "avoiding some questionable stains on the sheets, as you stumble to your feet sending cans flying "
                    + "like bowling pins in your wake.You bend over to take a closer look at the pile of crushed aluminum. "
                    + "You read one of the labels: \"Fast Eddie's Colon Cleanse: When in doubt flush it out!\"\"Side effects "
                    + "may include: Dizzines, vomiting, diarrhea, cold sweats, hallucinations, intense panic, paranoia, permanent "
                    + "tongue discoloration, mild brain damage, amnesia, bowel implosion, and occasionally hiccups\". The can has "
                    + "a purple iridescent sludge oozing out of it that's really similar to the shade of purple that your hands "
                    + "are. Come to think of it, you vaguely remember signing up for a test group that was supposed to try out "
                    + "a new health drink.Looks like your part time job as a barrista just wasn't paying the bills, nothing "
                    + "like easy money! The thing is you don't remember anything about going to a hotel last night, and you "
                    + "definitely don't remember anything about drinking a 24 pack of Fast Eddie's Colon Cleanse. Your stomach "
                    + "starts to feel a little uneasy, but never mind that, it's time to spend some of that hard earned cash! "
                    + "You reach into your wallet and realize in that moment that you don't even remember your name. You look at "
                    + "your license and focus your still hazy eyes and barely make out that it says...\n\n ";

            Scenes[SceneList.letter] = "Dear <firstName>,\n\n Thank you so much for signing up to try out our exciting new drink! "
                                + "We hope you don't mind but we've taken the liberty of putting you up for the night "
                                + "in one of our sponsors hotels with a generous supply of Fast Eddie's to keep you company.\n\n ";

        }

        void AddChoices()
        {
            Choices[ChoiceList.PlayerName] = delegate (GameController controller, string userInput)
            {
                Player player = controller.Game.Player;

                if (userInput != "" || player.FirstName != "")
                {
                    player.FirstName = userInput;
                    return SceneList.letter;
                }

                GameController.InputResponse.Append("What is your name?");
                return SceneList.intro;

            };


            
        }
    }
}
