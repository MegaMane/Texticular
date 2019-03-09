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
    class PlayerQuitState : IGameState
    {

        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;


        public PlayerQuitState(GameController controller)
        {
            Controller = controller;
        }
        public void OnEnter()
        {
            Update(Controller.ElapsedTime.ElapsedMilliseconds);
        }

        public void OnExit()
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            Controller.UI.DrawGameUI();
            GetInput();

        }

        public void Update(float elapsedTime)
        {
            GameController.InputResponse.Clear();
            GameController.InputResponse.Append("Thanks for Playing!\n");
            Controller.Game.Gamestats.StopWatch.Stop();
            Controller.ElapsedTime.Stop();


        }

        void GetInput()
        {

            Console.Write("Press Enter to exit....\n>>");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
