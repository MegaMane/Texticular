using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.UI;
using Texticular.GameEngine;

namespace Texticular.GameStates
{
    public class MainGameState : GameState
    {
        public MainGameState()
        {
            //this.player = Game.Player;
            //this.enemies = Game.Enemies;
            Gamestats testStats = new Gamestats();

            testStats.HP = testStats.MaxHP = 20;
            testStats.MP = testStats.MaxMP = 10;
            testStats.ST = testStats.MaxST = 10;
            testStats.GP = 0;
            testStats.Level = 1;
            testStats.Strength = 3;
            testStats.Intelligence = 2;
            testStats.Piety = 1;
            testStats.Vitality = 3;
            testStats.Dexterity = 1;
            testStats.Speed = 2;
            testStats.Personality = 1;
            testStats.Luck = 1;

            this.ui = new UserInterface(testStats);

            buffer = Terminal.CreateBuffer(81, 20);
            dialogue = new Narrative(buffer);

            //var mapGenerator = new MapGenerator(80, 40);

        }

        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Texticular.UI.Buffer buffer;
        private Narrative dialogue;



        public override bool Update()
        {
            // logic
            //if (Terminal.IsKeyPressed(Keys.Up)) player.MovePlayer(Actor.Direction.Up);
            //if (Terminal.IsKeyPressed(Keys.Right)) player.MovePlayer(Actor.Direction.Right);
            //if (Terminal.IsKeyPressed(Keys.Down)) player.MovePlayer(Actor.Direction.Down);
            //if (Terminal.IsKeyPressed(Keys.Left)) player.MovePlayer(Actor.Direction.Left);
            //if (Terminal.IsKeyPressed(Keys.Escape)) return false;
            //foreach (var e in enemies)
            //{
            //    if (e.Update(player, dungeonMap))
            //    {
            //        GameStateManager.SetCurrentGameState(new BattleGameState(e));
            //        return true;
            //    }
            //}
            return true;
        }

        public override void Render()
        {
            // render

            //dungeonMap.Draw(mapBuffer);
            //foreach (var e in enemies)
            //{
            //    e.Draw(mapBuffer, dungeonMap);
            //}
            //player.Draw(mapBuffer, dungeonMap);
            //mapBuffer.Blit(0, 3);
            //ui.DrawGameUI();
            //mainBuffer = Terminal.CreateBuffer(70, 40);
            //mainBuffer.Clear();
            //mainBuffer.Blit(0, 5);
        }
    }

    public abstract class GameState
    {
        public abstract bool Update();
        public abstract void Render();
    }
}
