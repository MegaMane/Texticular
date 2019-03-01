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
    class CombatState: IGameState
    {

        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;

        public CombatState(GameController controller)
        {
            Controller = controller;
        }

        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            Console.WriteLine(GameController.InputResponse);
            Console.ReadKey();
        
        }

        public void Update(float elapsedTime)
        {
            Terminal.Init(110, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
            Console.Clear();
            GameController.InputResponse.Clear();
            GameController.InputResponse.Append("Thanks for Playing!\n Press any Key to exit...");
            Controller.Game.Gamestats.StopWatch.Stop();
            Controller.ElapsedTime.Stop();


        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
