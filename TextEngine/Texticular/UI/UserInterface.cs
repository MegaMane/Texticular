using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.UI
{
    public class UserInterface
    {
        public UserInterface(GameStatistics stats)
        {
            statsBuffer = Terminal.CreateBuffer(80, 3);
            attrBuffer = Terminal.CreateBuffer(30, 43);
            this.stats = stats;

        }

        private Buffer statsBuffer;
        private Buffer attrBuffer;
        private GameStatistics stats;

        public void DrawGameUI(GameController controller)
        {
            Player player = controller.Game.Player;

            statsBuffer.DrawFrameLeft(0, 0, 80, 3, ConsoleColor.DarkGray);
            //statsBuffer.Draw($"HP {stats.HP}/{stats.MaxHP}   MP {stats.MP}/{stats.MaxMP}   ST {stats.ST}/{stats.MaxST}   GP {stats.GP}", 2, 1, ConsoleColor.Cyan);
            statsBuffer.Draw($"Health: {player.Health.ToString()}", 2, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Moves: {controller.Game.Gamestats.Moves.ToString().PadLeft(4, '0')}", 18, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Score: {controller.Game.Gamestats.Score.ToString().PadLeft(6, '0')}", 32, 1, ConsoleColor.Gray);
            statsBuffer.Draw($"Time: {controller.Game.Gamestats.ElapsedTime}", 47, 1, ConsoleColor.Gray);
            //statsBuffer.Draw(controller.Game.Gamestats.ElapsedTime.PadRight(4, ' '), 40, 1, ConsoleColor.DarkGray);
            //statsBuffer.Draw(stats.GP.ToString().PadRight(4, ' '), 50, 1, ConsoleColor.Yellow);

            var hpColor = (stats.HP < stats.MaxHP / 2) ? ConsoleColor.Red : ConsoleColor.White;
            //statsBuffer.Draw(stats.HP.ToString().PadLeft(4), 5, 1, hpColor);
            var mpColor = (stats.MP < stats.MaxMP / 2) ? ConsoleColor.Magenta : ConsoleColor.White;
            //statsBuffer.Draw(stats.MP.ToString().PadLeft(4), 20, 1, mpColor);
            var stColor = (stats.ST < stats.MaxST / 2) ? ConsoleColor.DarkCyan : ConsoleColor.White;
            //statsBuffer.Draw(stats.ST.ToString().PadLeft(4), 35, 1, stColor);

            statsBuffer.Draw($"{controller.Game.Player.PlayerLocation.Name}".PadLeft(15), 64, 1, ConsoleColor.Yellow);
            statsBuffer.Blit(0, 0);

            attrBuffer.DrawFrame(0, 0, 30, 43, ConsoleColor.DarkGray);
            attrBuffer.DrawVDiv(0, 0, 3, ConsoleColor.DarkGray);
            attrBuffer.DrawHDiv(0, 15, 30, ConsoleColor.DarkGray);
            attrBuffer.DrawHDiv(0, 23, 30, ConsoleColor.DarkGray);
            //attrBuffer.Draw(" ATTR ", 2, 0, ConsoleColor.White);
            attrBuffer.Draw(player.FirstName == "" ?"Name:??????":$"Name:{player.FirstName}", 2, 1, ConsoleColor.White);
            attrBuffer.Draw("Disoriented Barista", 2, 3, ConsoleColor.DarkCyan);
            attrBuffer.Draw($"Status:", 2, 5, ConsoleColor.White);
            attrBuffer.Draw($"Has to take a shit", 2, 7, ConsoleColor.Yellow);

            //attrBuffer.Draw($"INT {stats.Intelligence.ToString()}", 2, 7, ConsoleColor.Yellow);
            //attrBuffer.Draw($"PIE {stats.Piety.ToString()}", 2, 8, ConsoleColor.Yellow);
            //attrBuffer.Draw($"VIT {stats.Vitality.ToString()}", 2, 9, ConsoleColor.Yellow);
            //attrBuffer.Draw($"DEX {stats.Dexterity.ToString()}", 2, 10, ConsoleColor.Yellow);
            //attrBuffer.Draw($"SPD {stats.Speed.ToString()}", 2, 11, ConsoleColor.Yellow);
            //attrBuffer.Draw($"PER {stats.Personality.ToString()}", 2, 12, ConsoleColor.Yellow);
            //attrBuffer.Draw($"LUK {stats.Luck.ToString()}", 2, 13, ConsoleColor.Yellow);

            attrBuffer.Draw("EQUIPPED", 2, 16, ConsoleColor.Gray);
            attrBuffer.Draw("Dirty Underwear", 2, 18, ConsoleColor.DarkGray);
            //attrBuffer.Draw("+7 War Axe", 2, 18, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Steel Plate", 2, 19, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Iron Shield", 2, 20, ConsoleColor.DarkGray);
            //attrBuffer.Draw("Heavy Boots", 2, 21, ConsoleColor.DarkGray);

            attrBuffer.Draw("INVENTORY", 2, 24, ConsoleColor.Gray);
            int currentY = 26;
            foreach(var item in controller.Game.Player.BackPack.RoomItems)
            {
                attrBuffer.Draw($"{item.Name}", 2, currentY, ConsoleColor.DarkGray);
                currentY += 1;
            }

            attrBuffer.Blit(80, 0);
        }
    }
}
