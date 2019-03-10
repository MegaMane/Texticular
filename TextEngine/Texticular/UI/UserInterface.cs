using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;

namespace Texticular.UI
{
    public class UserInterface
    {
        public UserInterface(GameController controller)
        {
            this.Controller = controller;
            //UI Stuff
            Terminal.Init(120, 50, "Busted Ass Text Adventure (Texticular)", 8, 12);

            mainBuffer = Terminal.CreateBuffer(80, 43);
            statsBuffer = Terminal.CreateBuffer(80, 3);
            attrBuffer = Terminal.CreateBuffer(50, 48);

            Terminal.SetCurrentConsoleFontEx(8, 12);
            narrative = new Narrative(mainBuffer);




        }

        //UI Stuff
        public Texticular.UI.Buffer mainBuffer;
        public Narrative narrative;
        private Buffer statsBuffer;
        private Buffer attrBuffer;
        private GameController Controller;
        

        public void DrawGameUI()
        {
            Player player = Controller.Game.Player;
            GameStatistics stats = Controller.Game.Gamestats;

            Console.Clear();


            //mainBuffer = Terminal.CreateBuffer(80, 43);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrame(0, 0, 80, 43, ConsoleColor.DarkGray);
            narrative.Write(GameController.InputResponse.ToString(), fg: ConsoleColor.DarkGreen);
            mainBuffer.Blit(0,3);
            

            statsBuffer.DrawFrame(0, 0, 80, 3, ConsoleColor.DarkGray);
            //statsBuffer.Draw($"HP {stats.HP}/{stats.MaxHP}   MP {stats.MP}/{stats.MaxMP}   ST {stats.ST}/{stats.MaxST}   GP {stats.GP}", 2, 1, ConsoleColor.Cyan);
            statsBuffer.Draw($"Health: {player.Health.ToString()}", 2, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Moves: {stats.Moves.ToString().PadLeft(4, '0')}", 18, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Score: {stats.Score.ToString().PadLeft(6, '0')}", 32, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Time: {stats.ElapsedTime}", 47, 1, ConsoleColor.Gray);

            var hpColor = (stats.HP < stats.MaxHP / 2) ? ConsoleColor.Red : ConsoleColor.White;

            statsBuffer.Draw($"{player.PlayerLocation.Name}".PadLeft(15), 64, 1, ConsoleColor.Yellow);
            statsBuffer.Blit(0, 0);

            attrBuffer.DrawFrame(0, 0,38, 46, ConsoleColor.DarkGray);
            //attrBuffer.DrawVDiv(0, 0, 3, ConsoleColor.DarkGray);
            //attrBuffer.DrawHDiv(0, 15, 45, ConsoleColor.DarkGray);
            //attrBuffer.DrawHDiv(0, 23, 45, ConsoleColor.DarkGray);
            //attrBuffer.DrawHDiv(0, 34, 45, ConsoleColor.DarkGray);
            //attrBuffer.Draw(" ATTR ", 2, 0, ConsoleColor.White);
            attrBuffer.Draw(player.FirstName == "" ?"Name:??????":$"Name:{player.FirstName}", 2, 1, ConsoleColor.White);
            attrBuffer.Draw("Disoriented Barista", 2, 3, ConsoleColor.DarkCyan);
            attrBuffer.Draw($"Status:", 2, 5, ConsoleColor.White);
            attrBuffer.Draw($"Has to take a shit", 2, 7, ConsoleColor.Yellow);


            attrBuffer.Draw("EQUIPPED", 2, 16, ConsoleColor.Gray);
            attrBuffer.Draw("Dirty Underwear", 2, 18, ConsoleColor.DarkGray);
            //attrBuffer.Draw("+7 War Axe", 2, 18, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Steel Plate", 2, 19, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Iron Shield", 2, 20, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Heavy Boots", 2, 21, ConsoleColor.DarkGray);

            attrBuffer.Draw("Exits", 2, 24, ConsoleColor.Gray);
            int currentY = 26;
            foreach(KeyValuePair<string,Exit> exit in player.PlayerLocation.Exits)
            {
                Room destination = GameObject.GetComponent<Room>(exit.Value.DestinationKey);
                //display the name of the door to the player if the path is locked else just tell them where the exit leads
                string nameDisplay = exit.Value.IsLocked ? exit.Value.Name : destination.Name;
                attrBuffer.Draw($"{exit.Key}: {nameDisplay}", 2, currentY, ConsoleColor.DarkGray);
                currentY += 1;
            }

            attrBuffer.Draw("Interesting Things", 2, 36, ConsoleColor.Gray);

            currentY = 38;
            foreach (StoryItem item in player.PlayerLocation.RoomItems)
            {
                string itemString="";
                attrBuffer.Draw($"{item.Name }", 2, currentY, ConsoleColor.DarkGray);
                currentY += 1;

            }

            attrBuffer.Blit(81, 0);

            Console.SetCursorPosition(0, 45);
        }
    }
}
