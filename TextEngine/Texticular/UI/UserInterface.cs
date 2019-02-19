using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.UI
{
    public class UserInterface
    {
        public UserInterface(Gamestats stats)
        {
            statsBuffer = Terminal.CreateBuffer(80, 3);
            attrBuffer = Terminal.CreateBuffer(15, 43);
            this.stats = stats;

        }

        private Buffer statsBuffer;
        private Buffer attrBuffer;
        private Gamestats stats;

        public void DrawGameUI()
        {

            statsBuffer.DrawFrameLeft(0, 0, 80, 3, ConsoleColor.DarkGray);
            //statsBuffer.Draw($"HP {stats.HP}/{stats.MaxHP}   MP {stats.MP}/{stats.MaxMP}   ST {stats.ST}/{stats.MaxST}   GP {stats.GP}", 2, 1, ConsoleColor.Cyan);
            statsBuffer.Draw($"HP     /       MP     /       ST     /       GP     ", 2, 1, ConsoleColor.Gray);
            statsBuffer.Draw(stats.MaxHP.ToString().PadRight(4, ' '), 10, 1, ConsoleColor.DarkGray);
            statsBuffer.Draw(stats.MaxMP.ToString().PadRight(4, ' '), 25, 1, ConsoleColor.DarkGray);
            statsBuffer.Draw(stats.MaxST.ToString().PadRight(4, ' '), 40, 1, ConsoleColor.DarkGray);
            statsBuffer.Draw(stats.GP.ToString().PadRight(4, ' '), 50, 1, ConsoleColor.Yellow);

            var hpColor = (stats.HP < stats.MaxHP / 2) ? ConsoleColor.Red : ConsoleColor.White;
            statsBuffer.Draw(stats.HP.ToString().PadLeft(4), 5, 1, hpColor);
            var mpColor = (stats.MP < stats.MaxMP / 2) ? ConsoleColor.Magenta : ConsoleColor.White;
            statsBuffer.Draw(stats.MP.ToString().PadLeft(4), 20, 1, mpColor);
            var stColor = (stats.ST < stats.MaxST / 2) ? ConsoleColor.DarkCyan : ConsoleColor.White;
            statsBuffer.Draw(stats.ST.ToString().PadLeft(4), 35, 1, stColor);

            statsBuffer.Draw($"DUNGEON 1-1".PadLeft(15), 64, 1, ConsoleColor.Gray);
            statsBuffer.Blit(0, 0);

            attrBuffer.DrawFrame(0, 0, 15, 43, ConsoleColor.DarkGray);
            attrBuffer.DrawVDiv(0, 0, 3, ConsoleColor.DarkGray);
            attrBuffer.DrawHDiv(0, 15, 15, ConsoleColor.DarkGray);
            attrBuffer.DrawHDiv(0, 23, 15, ConsoleColor.DarkGray);
            //attrBuffer.Draw(" ATTR ", 2, 0, ConsoleColor.White);
            attrBuffer.Draw("M-LIZARDMAN", 2, 1, ConsoleColor.White);
            attrBuffer.Draw("FIGHTER", 2, 3, ConsoleColor.DarkCyan);
            attrBuffer.Draw($"LVL {stats.Level.ToString().PadLeft(3)}", 2, 4, ConsoleColor.DarkCyan);
            attrBuffer.Draw($"STR {stats.Strength.ToString()}", 2, 6, ConsoleColor.Yellow);
            attrBuffer.Draw($"INT {stats.Intelligence.ToString()}", 2, 7, ConsoleColor.Yellow);
            attrBuffer.Draw($"PIE {stats.Piety.ToString()}", 2, 8, ConsoleColor.Yellow);
            attrBuffer.Draw($"VIT {stats.Vitality.ToString()}", 2, 9, ConsoleColor.Yellow);
            attrBuffer.Draw($"DEX {stats.Dexterity.ToString()}", 2, 10, ConsoleColor.Yellow);
            attrBuffer.Draw($"SPD {stats.Speed.ToString()}", 2, 11, ConsoleColor.Yellow);
            attrBuffer.Draw($"PER {stats.Personality.ToString()}", 2, 12, ConsoleColor.Yellow);
            attrBuffer.Draw($"LUK {stats.Luck.ToString()}", 2, 13, ConsoleColor.Yellow);

            attrBuffer.Draw("EQUIPPED", 2, 16, ConsoleColor.Gray);
            attrBuffer.Draw("+7 War Axe", 2, 18, ConsoleColor.DarkGray);
            attrBuffer.Draw("Steel Plate", 2, 19, ConsoleColor.DarkGray);
            attrBuffer.Draw("Iron Shield", 2, 20, ConsoleColor.DarkGray);
            attrBuffer.Draw("Heavy Boots", 2, 21, ConsoleColor.DarkGray);

            attrBuffer.Draw("INVENTORY", 2, 24, ConsoleColor.Gray);
            attrBuffer.Draw("Heal Pot  3", 2, 26, ConsoleColor.DarkGray);
            attrBuffer.Draw("Magic Pot 3", 2, 27, ConsoleColor.DarkGray);
            attrBuffer.Draw("Rations   1", 2, 28, ConsoleColor.DarkGray);
            attrBuffer.Draw("Keys      0", 2, 29, ConsoleColor.DarkGray);
            attrBuffer.Blit(80, 0);
        }
    }
}
