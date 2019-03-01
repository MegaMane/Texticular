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
    class GameLaunchState : IGameState
    {

        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;

        public GameLaunchState(GameController controller)
        {
            Controller = controller;
        }


        public override string ToString()
        {
            return this.GetType().Name;
        }

        public void Update(float elapsedTime)
        {
            throw new NotImplementedException();
        }

        public void Render()
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
    }
}
