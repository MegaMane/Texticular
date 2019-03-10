using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.GameEngine
{
    public class PlayerChoice
    {
        public string ChoiceName { get; set; }
        public Func<GameController,String, bool> ChoicePrompt { get; set; }
        public Action<GameController> ChoiceResult { get; set; }

        public PlayerChoice(string choiceName)
        {
            this.ChoiceName = choiceName;
        }
    }
}
