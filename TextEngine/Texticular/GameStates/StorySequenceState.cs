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
    class StorySequenceState: IGameState
    {
        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;
        GameScene ActiveScene;

        //UI Stuff
        //private Texticular.UI.Buffer mainBuffer;
        //private UserInterface ui;
        //private Narrative narrative;

        public StorySequenceState(GameController controller)
        {
            Controller = controller;

            //UI Stuff
            //Terminal.Init(110, 75, "Busted Ass Text Adventure (Texticular)", 7, 9);
            //GameStatistics testStats = new GameStatistics();
            //this.ui = new UserInterface(testStats);

            //mainBuffer = Terminal.CreateBuffer(80, 50);
            //Terminal.SetCurrentConsoleFontEx(8, 10);
            //narrative = new Narrative(mainBuffer);
            //mainBuffer.DrawFrameLeft(0, 0, 80, 50, ConsoleColor.DarkGray);
        }

        public void OnEnter()
        {
            ActiveScene = Controller.Game.Scenes[Controller.ActiveStoryScene];
            GameController.InputResponse.Append(ActiveScene.SceneText.Dequeue());
            Render();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {

            Console.Clear();
            Controller.ui.DrawGameUI(Controller);
            Controller.mainBuffer = Terminal.CreateBuffer(80, 43);
            Controller.narrative = new Narrative(Controller.mainBuffer);
            Controller.mainBuffer.DrawFrameLeft(0, 0, 80, 43, ConsoleColor.DarkGray);
            Controller.narrative.Write(GameController.InputResponse.ToString(), fg: ConsoleColor.DarkGreen);
            Controller.mainBuffer.Blit(0, 2);
            Controller.SetCursorPosition(0, 45);

        }

        public void Update(float elapsedTime)
        {
            GetInput();
            GameController.InputResponse.Clear();
            
            //get the next page of the story
            if(ActiveScene.SceneText.Count > 0)
            {
               GameController.InputResponse.Append(ActiveScene.SceneText.Dequeue());
            }
            else
            {
                //the scene has come to an end so handle any work and change the game state
                ActiveScene.SceneAction(Controller);
            }
                



        }

        void GetInput()
        {

            Console.Write("Press Enter to continue...\n>>");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

    }
}
