using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Texticular.UI
{
    public class Narrative
    {

        public Narrative(Buffer buffer, int padding = 4)
        {
            this.buffer = buffer;
            paddingH = padding;
            paddingV = padding;
            paddingBottom = padding / 2;
            paddingTop = padding / 2;
            paddingLeft = padding / 2;
            paddingRight = padding / 2;
            width = buffer.Width - paddingH;
            height = buffer.Height - paddingV;
            x = 0;
            y = 0;
        }
        private Buffer buffer;
        private int bottom;
        private int paddingH;
        private int paddingV;
        private int paddingLeft;
        private int paddingRight;
        private int paddingTop;
        private int paddingBottom;
        private int width;
        private int height;
        private int x;
        private int y;

        public void Write(string text = "", ConsoleColor fg = ConsoleColor.DarkGray, ConsoleColor bg = ConsoleColor.Black, ConsoleColor hilight = ConsoleColor.Red)
        {
            List<string> words = new List<string>(text.Split(' '));
            StringBuilder sb = new StringBuilder();
            int lineLength = 0;
            for (var i = 0; i < words.Count; i++)
            {
                var word = words[i];
                lineLength += word.Length + 1;
                if (lineLength < width)
                {
                    sb.Append(word);
                    sb.Append(' ');
                }

                ////forced newline
                //else if (lineLength < width && word.Contains("\n"))
                //{
                //    sb.Append(word.Replace("\n", ""));
                //    writeLine(sb.ToString(), fg, bg, hilight);
                //    //figure out how to use force additional line breaks
                //    // writeLine("", fg, bg, hilight);
                //    lineLength -= (word.Length + 1);
                //    sb.Clear();
                //}

                //word wrap
                else
                {
                    writeLine(sb.ToString(), fg, bg, hilight);
                    sb.Clear();
                    sb.Append(word);
                    sb.Append(' ');
                    lineLength = word.Length + 1;
                }
            }
            if (sb.Length > 0)
            {
                writeLine(sb.ToString(), fg, bg, hilight);
                sb.Clear();
            }
        }

        private void writeLine(string text, ConsoleColor fg = ConsoleColor.Gray, ConsoleColor bg = ConsoleColor.Black, ConsoleColor hilight = ConsoleColor.Red)
        {

            if (y == height)
            {
                buffer.Blit(buffer, paddingLeft, paddingTop, paddingLeft, paddingTop + 1, width, height - 1);
                buffer.Clear(paddingLeft, paddingTop + height - 1, width, 1);
                y--;
            }
            buffer.Draw(text, x + paddingLeft, y + paddingTop, fg, bg);


            //something related to character hp found in the text
            foreach (Match match in Regex.Matches(text, @"\dhp"))
            {
                buffer.Draw(match.Value, x + paddingLeft + match.Index, y + paddingTop, hilight, bg);
            }

            if (y < height) y++;
        }
    }
}
