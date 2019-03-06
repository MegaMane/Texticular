using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Diagnostics;
using Texticular.Environment;
using Texticular.GameEngine;
using Texticular.GameStates;
using Texticular.UI;

namespace Texticular
{
    public class GameController
    {
        public Dictionary<string, IGameState> GameStates;
        public StateStack StateStack;

        private IGameState _gamestate;
        public IGameState CurrentGameState
        {
            get
            {
                return _gamestate;
            }
            private set
            {
                _gamestate = value;
                _gamestate.TimesEntered += 1;
                _gamestate.OnEnter();
            }
        }

        public Stopwatch ElapsedTime;
        public static StringBuilder InputResponse = new StringBuilder();

        public Game Game;
        public Scene ActiveStoryScene;
        public Choice ActiveChoice;




        public GameController(Game game)
        {
            ElapsedTime = new Stopwatch();
            ElapsedTime.Start();
            Game = game;
            Game.Player.PlayerLocationChanged += PlayerLocationChangedHandler;

            foreach(StoryItem item in Game.Items.Values)
            {
                item.LocationChanged += ItemLocationChangedHandler;
            }

            GameStates = new Dictionary<string, IGameState>();

            GameStates["Explore"] = new ExplorationState(this);
            GameStates["PlayerQuit"] = new PlayerQuitState(this);
            GameStates["StoryScene"] = new StorySequenceState(this);
            GameStates["PlayerChoice"] = new PlayerChoiceState(this);

            ActiveStoryScene = Scene.Intro;
            CurrentGameState = GameStates["StoryScene"];

        }


        public void Start()
        {
            //CurrentGameState.OnEnter();
        }

        
        public void Update()
        {

            CurrentGameState.Update(ElapsedTime.ElapsedMilliseconds);
        }

        public void Render()
        {
            CurrentGameState.Render();
        }

        public void SetGameState(string stateName)
        {
            CurrentGameState = GameStates[stateName];
        }

        #region event handlers

        void PlayerLocationChangedHandler(object sender, PlayerLocationChangedEventArgs args)
        {
            //look at the players surroundings automatically 
            //when they enter a new location
            Player Player = (Player)sender;

            InputResponse.AppendFormat("Moving to {0}\n ", args.NewLocation.Name);
            args.NewLocation.TimesVisited += 1;
            checkTriggers(Player, args);
            args.NewLocation.Commands["look"](new ParseTree() {Verb="look", DirectObject=args.NewLocation.Name, DirectObjectKeyValue=args.NewLocation.KeyValue });
            
        }

        void checkTriggers(Player player, PlayerLocationChangedEventArgs args)
        {
            if(args.NewLocation.KeyValue == "room201_bathroom" && args.NewLocation.TimesVisited == 1)
            {
                ActiveStoryScene = Scene.Bathroom201FirstVisit;
                CurrentGameState = GameStates["StoryScene"];
            }
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




        static public string FirstCharToUpper(string input) => input.First().ToString().ToUpper() + input.Substring(1);


        #endregion



    }
}


    