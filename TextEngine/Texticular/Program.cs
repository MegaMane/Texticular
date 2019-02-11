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
         * Use healing items {need to flesh out}
         * lock/unlock/open containers and place and retrieve items
         * use special context sensitive items
         * talk to people
         * equip weapons
         * fight with weapons
         * use phone/keypad
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
                string userInput = Console.ReadLine();


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
    }
}
