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
        public Stopwatch ElapsedTime;
        public static StringBuilder InputResponse = new StringBuilder();
        public Game Game;
        public Scene ActiveStoryScene;
        public string ActiveScene { get; set; }
        public Choice ActiveChoice;
        public UserInterface UI;


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
            }
        }



        public GameController(Game game)
        {

            UI = new UserInterface(this);

            ElapsedTime = new Stopwatch();
            ElapsedTime.Start();
            Game = game;



            GameStates = new Dictionary<string, IGameState>();

            GameStates["Room201"] = new Room201(this);
            GameStates["Room201Bathroom"] = new Room201Bathroom(this);
            GameStates["WestHallway"] = new WestHallway(this);
            GameStates["PlayerQuit"] = new PlayerQuitState(this);
            //GameStates["StoryScene"] = new StorySequenceState(this);
            //GameStates["PlayerChoice"] = new PlayerChoiceState(this);



        }


        public void Start()
        {
            //ActiveStoryScene = Scene.Intro;
            CurrentGameState = GameStates["Room201"];
            CurrentGameState.OnEnter();
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
            CurrentGameState.OnExit();
            CurrentGameState = GameStates[stateName];
            CurrentGameState.OnEnter();
        }



        #region helper methods




        static public string FirstCharToUpper(string input) => input.First().ToString().ToUpper() + input.Substring(1);


        #endregion



    }
}


    