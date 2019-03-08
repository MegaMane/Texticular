using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Texticular.UI
{
    #region keys enum
    [System.Flags]
    [System.Runtime.InteropServices.ComVisible(true)]
    public enum Keys
    {
        Backspace = 0x08,
        Tab = 0x09,
        Clear = 0x0C,
        Enter = 0x0D,
        Shift = 0x10,
        Control = 0x11,
        Alt = 0x12,
        Pause = 0x13,
        CapsLock = 0x14,
        Escape = 0x1B,
        Space = 0x20,
        PageUp = 0x21,
        PageDown = 0x22,
        End = 0x23,
        Home = 0x24,
        Left = 0x25,
        Up = 0x26,
        Right = 0x27,
        Down = 0x28,
        Select = 0x29,
        Print = 0x2A,
        Execute = 0x2B,
        PrintScreen = 0x2C,
        Insert = 0x2D,
        Delete = 0x2E,
        Help = 0x2F,
        Zero = 0x30,
        One = 0x31,
        Two = 0x32,
        Three = 0x33,
        Four = 0x34,
        Five = 0x35,
        Six = 0x36,
        Seven = 0x37,
        Eight = 0x38,
        Nine = 0x39,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,
        LeftWindowsKey = 0x5B,
        RightWindowsKey = 0x5C,
        ApplicationsKey = 0x5D,
        Sleep = 0x5F,
        NumPad0 = 0x60,
        NumPad1 = 0x61,
        NumPad2 = 0x62,
        NumPad3 = 0x63,
        NumPad4 = 0x64,
        NumPad5 = 0x65,
        NumPad6 = 0x66,
        NumPad7 = 0x67,
        NumPad8 = 0x68,
        NumPad9 = 0x69,
        Multiply = 0x6A,
        Add = 0x6B,
        Seperator = 0x6C,
        Subtract = 0x6D,
        Decimal = 0x6E,
        Divide = 0x6F,
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        Numlock = 0x90,
        ScrollLock = 0x91,
        LeftShift = 0xA0,
        RightShift = 0xA1,
        LeftControl = 0xA2,
        RightContol = 0xA3,
        LeftMenu = 0xA4,
        RightMenu = 0xA5,
        BrowserBack = 0xA6,
        BrowserForward = 0xA7,
        BrowserRefresh = 0xA8,
        BrowserStop = 0xA9,
        BrowserSearch = 0xAA,
        BrowserFavorites = 0xAB,
        BrowserHome = 0xAC,
        VolumeMute = 0xAD,
        VolumeDown = 0xAE,
        VolumeUp = 0xAF,
        NextTrack = 0xB0,
        PreviousTrack = 0xB1,
        StopMedia = 0xB2,
        PlayPause = 0xB3,
        LaunchMail = 0xB4,
        SelectMedia = 0xB5,
        LaunchApp1 = 0xB6,
        LaunchApp2 = 0xB7,
        OEM1 = 0xBA,
        OEMPlus = 0xB8,
        OEMComma = 0xBC,
        OEMMinus = 0xBD,
        OEMPeriod = 0xBE,
        OEM2 = 0xBF,
        OEM3 = 0xC0,
        OEM4 = 0xDB,
        OEM5 = 0xDC,
        OEM6 = 0xDD,
        OEM7 = 0xDE,
        OEM8 = 0xDF,
        OEM102 = 0xE2,
        Process = 0xE5,
        Packet = 0xE7,
        Attn = 0xF6,
        CrSel = 0xF7,
        ExSel = 0xF8,
        EraseEOF = 0xF9,
        Play = 0xFA,
        Zoom = 0xFB,
        PA1 = 0xFD,
        OEMClear = 0xFE
    };
    #endregion


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConsoleFont
    {
        public uint Index;
        public short SizeX, SizeY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;

        public COORD(short x, short y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFOEX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public int FontFamily;
        public int FontWeight;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 42)]
        public string FaceName;
    }

    public class Terminal
    {
        [DllImport("kernel32")]
        private static extern bool SetConsoleIcon(IntPtr hIcon);
        //public static bool SetConsoleIcon(Icon icon){return SetConsoleIcon(icon.Handle);}

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetConsoleFont(IntPtr hOut, uint dwFontSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        extern static bool GetCurrentConsoleFontEx(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            ref CONSOLE_FONT_INFOEX lpConsoleCurrentFont);

        private enum StdHandle
        {
            OutputHandle = -11
        }

        [DllImport("kernel32")]
        private static extern IntPtr GetStdHandle(StdHandle index);

        public static bool SetConsoleFont(uint index)
        {
            var r = SetConsoleFont(GetStdHandle(StdHandle.OutputHandle), index);
            return r == 0;
        }

        [DllImport("kernel32")]
        private static extern bool GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)]bool bMaximize,
            uint count, [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

        [DllImport("kernel32")]
        private static extern uint GetNumberOfConsoleFonts();

        public static uint ConsoleFontsCount
        {
            get
            {
                return GetNumberOfConsoleFonts();
            }
        }

        public static ConsoleFont[] ConsoleFonts
        {
            get
            {
                var count = GetNumberOfConsoleFonts();
                ConsoleFont[] fonts = new ConsoleFont[count];
                if (fonts.Length > 0)
                    GetConsoleFontInfo(GetStdHandle(StdHandle.OutputHandle), false, (uint)fonts.Length, fonts);
                return fonts;
            }
        }

        public static CONSOLE_FONT_INFOEX GetCurrentConsoleFontEx()
        {
            // Instantiating CONSOLE_FONT_INFOEX and setting cbsize
            CONSOLE_FONT_INFOEX ConsoleFontInfo = new CONSOLE_FONT_INFOEX();
            ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);

            GetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);
            return ConsoleFontInfo;
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern Int32 SetCurrentConsoleFontEx(
        IntPtr ConsoleOutput,
        bool MaximumWindow,
        ref CONSOLE_FONT_INFOEX ConsoleCurrentFontEx);

        public static void SetCurrentConsoleFontEx(short sizeX = 6, short sizeY = 8, string faceName = "Lucida Console")
        {
            // Instantiating CONSOLE_FONT_INFO_EX and setting its size (the function will fail otherwise)
            CONSOLE_FONT_INFOEX ConsoleFontInfo = new CONSOLE_FONT_INFOEX();
            ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);

            // Optional, implementing this will keep the fontweight and fontsize from changing
            // See notes
            // GetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);

            ConsoleFontInfo.FaceName = faceName;
            ConsoleFontInfo.dwFontSize.X = sizeX;
            ConsoleFontInfo.dwFontSize.Y = sizeY;

            SetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);
        }

        //[DllImport("user32.dll")]
        //private static extern bool SetConsoleIcon(HICON hIcon);


        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public static bool IsKeyPressed(Keys keyData)
        {
            return 0 != (GetAsyncKeyState((int)keyData) & 0x8000);
        }

        public static ConsoleKeyInfo WaitForAnyKey()
        {
            // first clear the keyboard buffer of junk
            while (Console.KeyAvailable)
                Console.ReadKey(false);

            return Console.ReadKey(false); // read a full keypress
        }
        // from winuser.h
        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();


        // from winuser.h
        private const int GWL_STYLE = -16,
                          WS_MAXIMIZEBOX = 0x10000,
                          WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int value);


        /// <summary>
        /// Forces the width and height of the console window and buffer to the specified size and prevents any resizing by user
        /// </summary>
        /// <param name="width">width of terminal (in characters)</param>
        /// <param name="height">height of terminal (in characters)</param>
        public static void Init(int width, int height, string title = "", short fontSizeX = 6, short fontSizeY = 8, string fontFaceName = "Lucida Console")
        {
            if (width < 20 || width > Console.LargestWindowWidth)
                throw new ArgumentOutOfRangeException("width", width, "width must be >= 20 and <= Console.LargestWindowWidth");

            if (height < 10 || height > Console.LargestWindowHeight)
                throw new ArgumentOutOfRangeException("height", height, "height must be >= 10 and <= Console.LargestWindowHeight");

            _width = width;
            _height = height;
            _bufferLength = width * height;

            // set size (small hack here to prevent scroll bars or extra unused space)
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width - 1, height - 1);
            Console.SetWindowSize(width, height);

            // disable resize operations on console window
            var hWnd = GetConsoleWindow();
            DeleteMenu(GetSystemMenu(hWnd, false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(hWnd, false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(hWnd, false), SC_SIZE, MF_BYCOMMAND);

            //DrawMenuBar(GetSystemMenu(GetConsoleWindow(), false));

            // hide min and max buttons
            var currentStyle = GetWindowLong(hWnd, GWL_STYLE);
            SetWindowLong(hWnd, GWL_STYLE, (currentStyle & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX));

            Console.CursorVisible = true;
            Console.Title = title;

            SetCurrentConsoleFontEx(fontSizeX, fontSizeY, fontFaceName);
            //Terminal.SetConsoleIcon(SystemIcons.Information);

        }
        private static int _width;
        private static int _height;
        private static int _bufferLength;

        public static Buffer CreateBuffer(int width, int height)
        {
            Buffer buff = new Buffer(width, height, _width, _height);
            return buff;

        }
        public static Buffer CreateBuffer()
        {
            Buffer buff = new Buffer(_width, _height, _width, _height);
            return buff;
        }

        public static void Demo()
        {
            Terminal.Init(20, 10, "Demo");
            var buffer = Terminal.CreateBuffer();

            int i = 0, y = 9, ii = 0;
            bool up = true;
            while (!Console.KeyAvailable)
            {
                buffer.Clear();

                buffer.Draw("Hello World!", 0, (0 + ii) % 10);
                buffer.Draw("Hello World!", 1, (1 + ii) % 10, ConsoleColor.Green);
                buffer.Draw("Hello World!", 2, (2 + ii) % 10, ConsoleColor.Yellow);
                buffer.Draw("Hello World!", 3, (3 + ii) % 10, ConsoleColor.Black, ConsoleColor.White);
                buffer.Draw("Hello World!", 4, (4 + ii) % 10, ConsoleColor.Magenta, ConsoleColor.DarkGreen);
                buffer.Draw("Hello World!", 5, (5 + ii) % 10, ConsoleColor.DarkRed, ConsoleColor.Cyan);
                buffer.Draw("Hello World!", 4, (6 + ii) % 10, ConsoleColor.Green);
                buffer.Draw("Hello World!", 3, (7 + ii) % 10, ConsoleColor.Yellow);
                buffer.Draw("Hello World!", 2, (8 + ii) % 10, ConsoleColor.Black, ConsoleColor.White);
                buffer.Draw("Hello World!", 1, (9 + ii) % 10, ConsoleColor.Magenta, ConsoleColor.DarkGreen);

                buffer.Draw("O", 19, y, ConsoleColor.Magenta);
                buffer.Draw("X", 18, 9 - y, ConsoleColor.Magenta);

                if (up) { if (--y == 0) up = false; }
                else { if (++y == 9) up = true; }


                buffer.Blit();
                Thread.Sleep(60);
                i++;
                if (i % 2 == 0) ii++;
            }
        }
    }


    ///<summary> 
    ///This class allows for a double buffer in Visual C# cmd promt.  
    ///The buffer is persistent between frames. 
    ///</summary> 
    public class Buffer
    {
        public int Width => width;
        public int Height => height;

        private int width;
        private int height;
        private int windowWidth;
        private int windowHeight;
        private SafeFileHandle h;
        private CharInfo[] buf;
        private SmallRect rect;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            private short X;
            private short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        /// <summary>
        /// Helper class that simplifies working with foreground and background colors.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ConsoleCharAttribute
        {
            public static ConsoleCharAttribute Default = new ConsoleCharAttribute(1);

            private short attr;

            /// <summary>
            /// Creates a new instance of the ConsoleCharAttribute structure
            /// </summary>
            /// <param name="fg">The foreground color.</param>
            /// <param name="bg">The background color.</param>
            public ConsoleCharAttribute(ConsoleColor fg, ConsoleColor bg)
            {
                attr = (short)(((ushort)bg << 4) | (ushort)fg);
            }

            /// <summary>
            /// Creates a new instance of the ConsoleCharAttribute structure.
            /// </summary>
            /// <param name="wAttr">The combined foreground/background attribute.</param>
            public ConsoleCharAttribute(short wAttr)
            {
                attr = wAttr;
            }

            /// <summary>
            /// Gets or sets the foreground color attribute.
            /// </summary>
            public ConsoleColor Foreground
            {
                get { return (ConsoleColor)(attr & 0x0f); }
                set { attr = (short)((attr & 0xfff0) | (ushort)value); }
            }

            /// <summary>
            /// Gets or sets the background color attribute.
            /// </summary>
            public ConsoleColor Background
            {
                get { return (ConsoleColor)((attr >> 4) & 0x0f); }
                set { attr = (short)((attr & 0xff0f) | ((ushort)value << 4)); }
            }

            /// <summary>
            /// Gets or sets the attribute (combined foreground/background color).
            /// </summary>
            public short Attribute
            {
                get { return attr; }
                set { attr = value; }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            //public short Attributes;
            public ConsoleCharAttribute Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            private short Left;
            private short Top;
            private short Right;
            private short Bottom;
            public void setDrawCord(short l, short t)
            {
                Left = l;
                Top = t;
            }
            public void setWindowSize(short r, short b)
            {
                Right = r;
                Bottom = b;
            }
        }

        /// <summary> 
        /// Consctructor class for the buffer. Pass in the width and height you want the buffer to be. 
        /// </summary> 
        /// <param name="Width"></param> 
        /// <param name="Height"></param> 
        public Buffer(int Width, int Height, int wWidth, int wHeight) // Create and fill in a multideminsional list with blank spaces. 
        {
            if (Width > wWidth || Height > wHeight)
            {
                throw new System.ArgumentException("The buffer width and height can not be greater than the window width and height.");
            }
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            width = Width;
            height = Height;
            windowWidth = wWidth;
            windowHeight = wHeight;
            buf = new CharInfo[width * height];
            rect = new SmallRect();
            rect.setDrawCord(0, 0);
            rect.setWindowSize((short)windowWidth, (short)windowHeight);
            Clear();
        }

        public void Set(int x, int y, ConsoleColor fg, ConsoleColor bg, char c)
        {
            Draw(c.ToString(), x, y, fg, bg);
        }
        public void Set(char c, int x, int y, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Draw(c.ToString(), x, y, fg, bg);
        }
        public void Draw(String str, int x, int y, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black) //Draws the image to the buffer 
        {
            Draw(str, x, y, new ConsoleCharAttribute(fg, bg));
        }

        /// <summary> 
        /// This method draws any text to the buffer with given color. 
        /// To change the color, pass in a value above 0. (0 being black text, 15 being white text). 
        /// Put in the starting width and height you want the input string to be. 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        /// <param name="attribute"></param> 
        public void Draw(String str, int x, int y, ConsoleCharAttribute attribute) //Draws the image to the buffer 
        {
            if (x > windowWidth - 1 || y > windowHeight - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    buf[(x + tc) + (y * width)].Char.AsciiChar = (byte)(int)le; //Height * width is to get to the correct spot (since this array is not two dimensions). 
                    if (attribute.Attribute != 0)
                        buf[(x + tc) + (y * width)].Attributes = attribute;
                    tc++;
                }
            }
        }
        public void DrawVDiv(int x, int y, int height, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            height -= 1;
            Set((char)194, x, y, fg, bg);
            Set((char)180, x, y + height, fg, bg);
            for (var yy = y + 1; yy < y + height; yy++)
            {
                Set((char)179, x, yy, fg, bg);
            }
        }
        public void DrawHDiv(int x, int y, int width, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            width -= 1;
            Set((char)195, x, y, fg, bg);
            Set((char)180, x + width, y, fg, bg);
            for (var xx = x + 1; xx < x + width; xx++)
            {
                Set((char)196, xx, y, fg, bg);
            }
        }
        public void DrawFrame(
            ConsoleColor fg = ConsoleColor.White,
            ConsoleColor bg = ConsoleColor.Black,
            FramePart topLeft = FramePart.CornerTopLeft,
            FramePart topRight = FramePart.CornerTopRight,
            FramePart bottomRight = FramePart.CornerBottomRight,
            FramePart bottomLeft = FramePart.CornerBottomLeft,
            FramePart top = FramePart.Horizontal,
            FramePart bottom = FramePart.Horizontal,
            FramePart left = FramePart.Vertical,
            FramePart right = FramePart.Vertical,
            FramePart fill = FramePart.Empty)
        {
            int x = 0;
            int y = 0;
            int width = this.width;
            int height = this.height;
            width -= 1;
            height -= 1;

            if (topLeft != FramePart.None) Set((char)topLeft, x, y, fg, bg);
            if (topRight != FramePart.None) Set((char)topRight, x + width, y, fg, bg);
            if (bottomRight != FramePart.None) Set((char)bottomRight, x + width, y + height, fg, bg);
            if (bottomLeft != FramePart.None) Set((char)bottomLeft, x, y + height, fg, bg);

            if (left != FramePart.None && right != FramePart.None)
            {
                for (var yy = y + 1; yy < y + height; yy++)
                {
                    Set((char)left, x, yy, fg, bg);
                    Set((char)right, x + width, yy, fg, bg);
                }
            }
            else if (left == FramePart.None)
            {
                for (var yy = y + 1; yy < y + height; yy++)
                    Set((char)right, x + width, yy, fg, bg);
            }
            else if (right == FramePart.None)
            {
                for (var yy = y + 1; yy < y + height; yy++)
                    Set((char)left, x, yy, fg, bg);
            }

            if (top != FramePart.None && bottom != FramePart.None)
            {
                for (var xx = x + 1; xx < x + width; xx++)
                {
                    Set((char)top, xx, y, fg, bg);
                    Set((char)bottom, xx, y + height, fg, bg);
                }
            }
            else if (top == FramePart.None)
            {
                for (var xx = x + 1; xx < x + width; xx++)
                    Set((char)bottom, xx, y + height, fg, bg);
            }
            else if (bottom == FramePart.None)
            {
                for (var xx = x + 1; xx < x + width; xx++)
                    Set((char)top, xx, y, fg, bg);
            }

            if (fill != FramePart.None)
            {
                for (var yy = y + 1; yy < y + height; yy++)
                {
                    for (var xx = x + 1; xx < x + width; xx++)
                    {
                        Set((char)fill, xx, yy, fg, bg);
                    }
                }
            }
        }
        public void DrawFrame(int x, int y, int width, int height, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            width -= 1;
            height -= 1;

            Set((char)218, x, y, fg, bg);
            Set((char)191, x + width, y, fg, bg);
            Set((char)217, x + width, y + height, fg, bg);
            Set((char)192, x, y + height, fg, bg);

            for (var yy = y + 1; yy < y + height; yy++)
            {
                Set((char)179, x, yy, fg, bg);
                Set((char)179, x + width, yy, fg, bg);
            }
            for (var xx = x + 1; xx < x + width; xx++)
            {
                Set((char)196, xx, y, fg, bg);
                Set((char)196, xx, y + height, fg, bg);
            }

            for (var yy = y + 1; yy < y + height; yy++)
            {
                for (var xx = x + 1; xx < x + width; xx++)
                {
                    Set(' ', xx, yy, fg, bg);
                }
            }
        }
        public void DrawFrameLeft(int x, int y, int width, int height, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            height -= 1;

            Set((char)218, x, y, fg, bg);
            Set((char)192, x, y + height, fg, bg);

            for (var yy = y + 1; yy < y + height; yy++)
            {
                Set((char)179, x, yy, fg, bg);
            }
            for (var xx = x + 1; xx < x + width; xx++)
            {
                Set((char)196, xx, y, fg, bg);
                Set((char)196, xx, y + height, fg, bg);
            }

            for (var yy = y + 1; yy < y + height; yy++)
            {
                for (var xx = x + 1; xx < x + width; xx++)
                {
                    Set(' ', xx, yy, fg, bg);
                }
            }
        }
        /// <summary> 
        /// Prints the buffer to the screen. 
        /// </summary> 
        public void Blit(int x = 0, int y = 0) //Paint the image 
        {
            if (!h.IsInvalid)
            {
                rect.setDrawCord((short)x, (short)y);
                bool b = WriteConsoleOutput(h, buf, new Coord((short)width, (short)height), new Coord((short)0, (short)0), ref rect);
            }
        }

        /// <summary>
        /// Prints the buffer to another buffer
        /// </summary>        
        public void Blit(Buffer buffer, int x = 0, int y = 0)
        {
            for (var yy = 0; yy < height; yy++)
            {
                for (var xx = 0; xx < width; xx++)
                {
                    var c = getCharAt(xx, yy);
                    buffer.setCharAt(c, xx + x, yy + y);
                }
            }
        }

        /// <summary>
        /// Prints the buffer to another buffer
        /// </summary>        
        public void Blit(Buffer buffer, int x, int y, int srcX, int srcY, int srcWidth, int srcHeight)
        {

            Queue<KeyValuePair<byte, byte>> data = new Queue<KeyValuePair<byte, byte>>();

            for (var yy = srcY; yy < srcY + srcHeight; yy++)
            {
                for (var xx = srcX; xx < srcX + srcWidth; xx++)
                {
                    data.Enqueue(getCharAt(xx, yy));
                }
            }
            for (var yy = y; yy < y + srcHeight; yy++)
            {
                for (var xx = x; xx < x + srcWidth; xx++)
                {
                    var c = data.Dequeue();
                    buffer.setCharAt(c, xx, yy);
                }
            }

        }

        /// <summary> 
        /// Clears the buffer and resets all character values back to 32, and attribute values to 1. 
        /// </summary> 
        public void Clear()
        {

            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].Attributes = ConsoleCharAttribute.Default;
                buf[i].Char.AsciiChar = 32;
            }
        }
        /// <summary> 
        /// Pass in a buffer to change the current buffer. 
        /// </summary> 
        /// <param name="b"></param> 
        public void setBuf(CharInfo[] b)
        {
            if (b == null)
            {
                throw new System.ArgumentNullException();
            }

            buf = b;
        }

        /// <summary> 
        /// Set the x and y cordnants where you wish to draw your buffered image. 
        /// </summary> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        public void setDrawCord(short x, short y)
        {
            rect.setDrawCord(x, y);
        }

        /// <summary> 
        /// Clear the designated row and make all attribues = 1. 
        /// </summary> 
        /// <param name="row"></param> 
        public void clearRow(int row)
        {
            for (int i = (row * width); i < ((row * width + width)); i++)
            {
                if (row > windowHeight - 1)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                buf[i].Attributes = ConsoleCharAttribute.Default;
                buf[i].Char.AsciiChar = 32;
            }
        }

        /// <summary> 
        /// Clear the designated column and make all attribues = 1. 
        /// </summary> 
        /// <param name="col"></param> 
        public void clearColumn(int col)
        {
            if (col > windowWidth - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            for (int i = col; i < windowHeight * windowWidth; i += windowWidth)
            {
                buf[i].Attributes = ConsoleCharAttribute.Default;
                buf[i].Char.AsciiChar = 32;
            }
        }


        /// <summary> 
        /// This function return the character and attribute at given location. 
        /// </summary> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        /// <returns> 
        /// byte character 
        /// byte attribute 
        /// </returns> 
        public KeyValuePair<byte, byte> getCharAt(int x, int y)
        {
            if (x > windowWidth || y > windowHeight)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            return new KeyValuePair<byte, byte>((byte)buf[((y * width + x))].Char.AsciiChar, (byte)buf[((y * width + x))].Attributes.Attribute);
        }

        public void setCharAt(KeyValuePair<byte, byte> c, int x, int y)
        {
            if (x > windowWidth - 1 || y > windowHeight - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            int tc = 0;
            char le = (char)c.Key;
            buf[(x + tc) + (y * width)].Char.AsciiChar = (byte)(int)le; //Height * width is to get to the correct spot (since this array is not two dimensions). 
            buf[(x + tc) + (y * width)].Attributes.Attribute = c.Value;
        }

        public void clearAt(int x, int y)
        {
            var i = (x) + (y * width);
            buf[i].Attributes = ConsoleCharAttribute.Default;
            buf[i].Char.AsciiChar = 32;
        }

        public void Clear(int x, int y, int width, int height)
        {
            for (var yy = y; yy < y + height; yy++)
            {
                for (var xx = x; xx < x + width; xx++)
                {
                    clearAt(xx, yy);
                }
            }
        }
    }
}
