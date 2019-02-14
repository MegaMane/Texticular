using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Texticular
{
    class Program
    {
        /*
         * Move Between Rooms with unlocked doors {complete}
         * Look at sourrondings {complete}
         * Examine Objects {complete}
         * Take and Drop Items {complete}
         * Unlock doors with correct key {complete}
         * Use healing items magic medicine
         * lock/unlock/open containers and place and retrieve items locker, chest
         * use special context sensitive items * use phone/keypad vending machine
         * talk to people
         * equip special items
         * fight
         * trigger game events or "cut scenes"
         * add a splash screen
         * add visuals
         * add sound
        */
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 43);
            Console.SetBufferSize(100, 43);
            Console.Title = "Busted Ass Text Adventure (Texticular)";

            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);
            bool gameRunning = true;

            Controller.Start();
            Console.Write(Controller.InputResponse.ToString());

            while (gameRunning)
            {


                Console.Write("\n>> ");
                string userInput=Console.ReadLine();


                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine("Thanks for Playing!");
                    gameRunning = false;
                    ActiveGame.Gamestats.stopWatch.Stop();
                    break;
                }

                Controller.Update(userInput);
                Console.Write(Controller.InputResponse.ToString());

            }
        }

        static string GetWordWrappedParagraph(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return string.Empty;
            }

            var approxLineCount = paragraph.Length / Console.WindowWidth;
            var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (var i = 0; i < paragraph.Length;)
            {
                var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
                var line = paragraph.Substring(i, grabLimit);

                var isLastChunk = grabLimit + i == paragraph.Length;

                if (isLastChunk)
                {
                    i = i + grabLimit;
                    lines.Append(line);
                }
                else
                {
                    var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
                    lines.AppendLine(line.Substring(0, lastSpace));

                    //Trailing spaces needn't be displayed as the first character on the new line
                    i = i + lastSpace + 1;
                }
            }
            return lines.ToString();
        }
    

    }
}
