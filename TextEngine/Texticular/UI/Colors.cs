using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.UI
{
    public class Colors
    {
        public static ConsoleColor FloorBackground = ConsoleColor.Black;
        public static ConsoleColor FloorBackgroundFov = ConsoleColor.Black;
        public static ConsoleColor WallBackgroundFov = ConsoleColor.Black;
        public static ConsoleColor WallBackground = ConsoleColor.Black;

        public static ConsoleColor Floor = ConsoleColor.DarkGray;
        public static ConsoleColor Player = ConsoleColor.Yellow;
        public static ConsoleColor Wall = ConsoleColor.DarkGray;
        public static ConsoleColor FloorFov = ConsoleColor.Gray;
        public static ConsoleColor WallFov = ConsoleColor.Gray;

        public static ConsoleColor Enemy = ConsoleColor.Red;
    }
    public enum Symbols
    {
        None = 0,
        Empty = 32
    }

    public enum FramePart
    {
        None = 0,
        Empty = 32,
        CornerTopRight = 191,
        CornerTopLeft = 218,
        CornerBottomRight = 217,
        CornerBottomLeft = 192,
        DividerTop = 194,
        DividerBottom = 193,
        DividerLeft = 195,
        DividerRight = 180,
        DividerCenter = 197,
        Horizontal = 196,
        Vertical = 179
    }
}
