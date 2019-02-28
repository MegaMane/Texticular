using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Texticular.UI;
using Texticular.Environment;

namespace Texticular
{
    class Program
    {

        static void Main(string[] args)
        {





            Game ActiveGame = new Game();
            GameController Controller = new GameController(ActiveGame);
            bool gameRunning = true;

            //GameLoadDiagnostic();

            Controller.Start();

            //while (true)
            //{
            //    processInput(); handles any user input that has happened since the last call. 
            //    update(); advances the game simulation one step It runs AI and physics (usually in that order). 
            //    render(); draws the game so the player can see what happened.
            //}


            while (gameRunning)
            {

                Controller.GetInput();


                if (Controller.UserInput.ToLower() == "exit")
                {
                    Console.WriteLine("Thanks for Playing!");
                    gameRunning = false;
                    ActiveGame.Gamestats.StopWatch.Stop();
                    break;
                }

                Controller.Update();
                Controller.Render();




            }
        }

        private static void GameLoadDiagnostic()
        {
            Console.WriteLine("Game Objects Created".PadLeft(45, ' '));
            Console.WriteLine();
            foreach (KeyValuePair<string, GameObject> obj in GameObject.Objects)
            {
                Console.WriteLine($"{obj.Key.PadLeft(30, ' ')} =>     {obj.Value.Name}");
            }

            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
        }



    }
}
