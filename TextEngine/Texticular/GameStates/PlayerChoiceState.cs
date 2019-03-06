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
    class PlayerChoiceState: IGameState
    {

        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;
        PlayerChoice ActiveChoice;

        //UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;

        public PlayerChoiceState(GameController controller)
        {
            Controller = controller;

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
            ActiveChoice = Controller.Game.Choices[Controller.ActiveChoice];
            ActiveChoice.ChoicePrompt(Controller,"");
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
            bool ChoiceMade = ActiveChoice.ChoicePrompt(Controller, UserInput);
            
            //the criteria for the choice have been met so handle any work and change the game state
            if (ChoiceMade)
            {
                ActiveChoice.ChoiceResult(Controller);
            }

            



        }

        void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
