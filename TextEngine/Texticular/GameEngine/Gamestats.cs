using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Texticular
{
    public class Gamestats
    {
        public int Moves { get; set; }
        public int Score { get; set; }
        public string elapsedTime;
        public Stopwatch stopWatch;
        public Player player;

        public Gamestats()
        {
            Moves = 0;
            Score = 0;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            
        }

        public string updateStats(int score, int moves = 0)
        {
            this.Moves = moves;
            getTime();
            this.Score += score;
            return ("\n\n\t--------------------------------------------------------------------------\n" +
                    $"\t| Moves: {Moves}\t\t Score: {Score.ToString()} \t\t Time: {elapsedTime}        |\n" +
                    $"\t| Health:  {player.Health}                                                            |\n" +
                    "\t--------------------------------------------------------------------------\n\n");
        }

        public string updateStats(int score)
        {
            Moves += 1;
            getTime();
            this.Score += score;
            return ("\n\n\t--------------------------------------------------------------------------\n" +
                    $"\t| Moves: {Moves}\t\t Score: {Score.ToString()} \t\t Time: {elapsedTime}        |\n" +
                    $"\t| Health:  {player.Health}                                                            |\n" +
                    "\t--------------------------------------------------------------------------\n\n");
        }

        public void getTime ()
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = this.stopWatch.Elapsed;
            // Format and display the TimeSpan value.
           this.elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            //this.stopWatch.Stop();
            //this.stopWatch.Start();
        }
    }
}