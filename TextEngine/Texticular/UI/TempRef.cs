using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Texticular.UI
{
    class TempRef
    {
        static void OtherMain(string[] args)
        {


            Terminal.Init(95, 43, "caRL", 7, 9);
            //GameStateManager.SetCurrentGameState(new MainGameState());
            Console.SetCursorPosition(0, 4);

            Buffer buffer;
            Narrative dialogue;

            buffer = Terminal.CreateBuffer(80, 35);
            dialogue = new Narrative(buffer);

            buffer.DrawFrameLeft(0, 0, 80, 35, ConsoleColor.DarkGray);

            String textBlob = "Lorem ipsum dolor\n sit amet, consectetur adipiscing elit. Donec eget fermentum neque. Curabitur aliquam velit quis dapibus varius. Sed id dolor mattis, lacinia metus ac, varius leo. Nullam fringilla diam congue eros dictum lacinia. Donec ac suscipit turpis, a laoreet lectus. Nullam blandit tempus mi, a malesuada neque pellentesque ut. Maecenas nunc elit, dapibus et auctor vel, placerat sed lorem. Morbi rutrum eleifend volutpat."
                              + "Suspendisse a gravida leo, et volutpat diam.Proin efficitur tincidunt erat a varius.Nam in nulla vel nisl finibus commodo.Aliquam tempor sapien quis tellus interdum consequat.Donec aliquam dignissim egestas.Nullam non suscipit est, in interdum magna.Nunc volutpat enim hendrerit libero rhoncus, ac placerat nisl feugiat.Quisque condimentum tortor at enim laoreet, at rhoncus turpis lacinia.Aliquam lacus turpis, accumsan a viverra in, pretium ac tortor.Maecenas pharetra sollicitudin blandit.Ut in libero tincidunt elit fringilla rutrum.Mauris lacinia posuere diam a lacinia.Nulla volutpat, diam ac varius gravida, massa eros vehicula dui, in blandit elit augue sit amet orci.";

            var frameTimer = new Stopwatch();
            bool running = true;
            while (running)
            {
                // lock core game loop to 10 FPS
                var ms = (int)frameTimer.ElapsedMilliseconds;
                if (ms < 100) Thread.Sleep(100 - ms);
                frameTimer.Restart();

                //GameStateManager.CurrentGameState.Update();    // logic update
                //GameStateManager.CurrentGameState.Render();    // render update
                //Console.Write(CreateBorderedText(textBlob,80, prefix:"  ", suffix: " "));
                dialogue.Write(textBlob);
                buffer.Blit(0, 2);

                Console.ReadLine();

                //Console.SetCursorPosition(0, 5);
                //Console.Write("This is some sample text that I am going to keep typing so that I can test if it wraps. If it does then I will have achieved something cool. If not it's back to the drawing board" + Environment.NewLine + "here is a new line.");
                //Console.Read();
                if (Terminal.IsKeyPressed(Keys.Escape)) running = false;
            }
        }



        public static string CreateBorderedText(string input, int maxLength, bool canWrap = true, string prefix = "+ ", string suffix = " +")
        {
            const string wordSeparator = " ";

            var lines = new List<string>();
            var lineBuilder = new StringBuilder();
            var borderLength = prefix.Length + suffix.Length;
            var contentLength = maxLength - borderLength;

            var wrapRequired = input.Length > contentLength;
            if (wrapRequired && canWrap)
            {
                var words = input.Split(' ');
                foreach (var word in words)
                {
                    var projectedLength = lineBuilder.Length + word.Length + wordSeparator.Length + borderLength;
                    var wrap = projectedLength >= maxLength;
                    if (wrap)
                    {
                        PadRight(lineBuilder, contentLength);
                        AddBorder(lineBuilder, prefix, suffix);

                        lines.Add(lineBuilder.ToString());
                        lineBuilder.Clear();
                    }

                    lineBuilder.Append(word).Append(wordSeparator);
                }
            }
            else
            {
                lineBuilder.Append(input);
            }

            PadRight(lineBuilder, contentLength);
            AddBorder(lineBuilder, prefix, suffix);
            lines.Add(lineBuilder.ToString());

            return string.Join(System.Environment.NewLine, lines);
        }


        private static void AddBorder(StringBuilder lineBuilder, string prefix, string suffix)
        {
            lineBuilder
                .Insert(0, prefix)
                .Append(suffix);
        }

        private static void PadRight(StringBuilder lineBuilder, int targetLength)
        {
            lineBuilder.Append(' ', targetLength - lineBuilder.Length);
        }


    }

}
