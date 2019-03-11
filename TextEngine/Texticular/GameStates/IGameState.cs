using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    public interface IGameState
    {
        int TimesEntered { get; set; }
        void Update(float elapsedTime);
        void Render();
        void OnEnter();
        void OnExit();
        void GetInput();
        string UserInput { get; set; }
        Dictionary<string, Action<ParseTree>> Commands { get; set; }
    }
}
