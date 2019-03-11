using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    class VendingState : IGameState
    {
        public int TimesEntered { get; set; } = 0;
        public string UserInput { get; set; } = "";
        public GameController Controller;
        public Dictionary<string, Action<ParseTree>> Commands { get; set; }
 

        public VendingState(GameController controller)
        {
            Controller = controller;
        }

        public void GetInput()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(float elapsedTime)
        {
            throw new NotImplementedException();
        }
    }
}
