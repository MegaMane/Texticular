using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Texticle.Actors;

namespace Texticle.Engine
{
    public class GameStatistics
    {
        public int Moves { get; set; }
        public int Score { get; set; }
        public string ElapsedTime;
        public Stopwatch StopWatch;
        public Player Player;

        public int HP { get; set; }
        public int MaxHP { get; set; }

        public int Luck { get; set; }

        public GameStatistics()
        {
            Moves = 0;
            Score = 0;
            ElapsedTime = "0:00:00";
            StopWatch = new Stopwatch();
            StopWatch.Start();
            
        }

        public string updateStats(int score, int moves = 0)
        {
            this.Moves = moves;
            getTime();
            this.Score += score;
            return ("\n\n\t--------------------------------------------------------------------------\n" +
                    $"\t| Moves: {Moves}\t\t Score: {Score.ToString()} \t\t Time: {ElapsedTime}        |\n" +
                    $"\t| Health:  {Player.Health}                                                            |\n" +
                    "\t--------------------------------------------------------------------------\n\n");
        }

        public string updateStats(int score)
        {
            Moves += 1;
            getTime();
            this.Score += score;
            return ("\n\n\t--------------------------------------------------------------------------\n" +
                    $"\t| Moves: {Moves}\t\t Score: {Score.ToString()} \t\t Time: {ElapsedTime}        |\n" +
                    $"\t| Health:  {Player.Health}                                                            |\n" +
                    "\t--------------------------------------------------------------------------\n\n");
        }

        public void getTime ()
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = this.StopWatch.Elapsed;
            // Format and display the TimeSpan value.
           this.ElapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            //this.stopWatch.Stop();
            //this.stopWatch.Start();
        }
    }
}