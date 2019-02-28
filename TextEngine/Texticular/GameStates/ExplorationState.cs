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
    class ExplorationState : IGameState
    {
        public string UserInput;
        public static StringBuilder InputResponse = new StringBuilder();

        Dictionary<string, Action<ParseTree>> commands;
        Lexer Tokenizer;

        //UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;

        public ExplorationState()
        {

            //UI Stuff
            Terminal.Init(110, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
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
