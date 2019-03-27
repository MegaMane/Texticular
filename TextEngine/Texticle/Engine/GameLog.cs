using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    public class GameLog
    {
        private static GameLog _instance;
        public static string UserInput { get; set; } = "";
        public static StringBuilder InputResponse { get; set; } = new StringBuilder();
        private static List<string> _logText = new List<string>();

        private GameLog()
        {
            InputResponse = new StringBuilder();
            _logText = new List<string>();
        }

        public static string DisplayResponse()
        {
            var outputString = InputResponse.ToString();
            _logText.Add(UserInput);
            _logText.Add(outputString);
            InputResponse.Clear();
            return outputString;
        }

        public static void Append(string text)
        {
            InputResponse.Append(text);
        }

        public static IReadOnlyCollection<string> ViewLog()
        {
            return _logText;
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
