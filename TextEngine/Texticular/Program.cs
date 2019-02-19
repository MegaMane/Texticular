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
                    ActiveGame.Gamestats.stopWatch.Stop();
                    break;
                }

                Controller.Update();
                Controller.Render();



            }
        }

 
    

    }
}
