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



        public PlayerChoiceState(GameController controller)
        {
            Controller = controller;

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

            Controller.UI.DrawGameUI();


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
