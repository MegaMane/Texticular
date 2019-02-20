using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Texticular.UI;

namespace Texticular
{
    class Program
    {
        /*
         * Move Between Rooms with unlocked doors {complete}
         * Move using the location name or a cardinal direction
         * Look at sourrondings {complete}
         * Examine Objects {complete}
         * Take and Drop Items {complete}
         * Unlock doors with correct key {complete}
         * lock/unlock/open containers and place and retrieve items locker, chest
         * use special context sensitive items * use phone/keypad vending machine
         * trigger game events or "cut scenes"
         * talk to people
         * equip special items
         * fight
         * add a splash screen
         * Use healing items magic medicine
         * Unity
         * add visuals
         * add sound
        */
        static void Main(string[] args)
        {





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
                    ActiveGame.Gamestats.StopWatch.Stop();
                    break;
                }

                Controller.Update();
                Controller.Render();




            }
        }

 
    

    }
}
