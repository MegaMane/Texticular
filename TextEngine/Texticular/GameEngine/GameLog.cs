using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.GameEngine
{
    class GameLog
    {
        private static GameLog _instance;
        public static StringBuilder InputResponse { get; set; }
        private static List<string> _logText;

        private GameLog()
        {
            InputResponse = new StringBuilder();
            _logText = new List<string>();
        }

        public string DisplayResponse()
        {
            var outputString = InputResponse.ToString();
            _logText.Add(outputString);
            InputResponse.Clear();
            return outputString;
        }

        public static GameLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameLog();
                }

                return _instance;
            }
        }
    }
}
