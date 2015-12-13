#region using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

#endregion

namespace Galaxy.Environments
{
    public class LevelTwo : LevelOne
    {
        private bool k = false;
        private Stopwatch fon = new Stopwatch();
        #region Constructors

        public LevelTwo()
        {
            // Backgrounds
            FileName = @"Assets\LevelTwo.png";
            Fon();

            // Enemies
            for (int i = 0; i < 5; i++)
            {
                var ship = new Ship(this);
                int positionY = ship.Height + 10;
                int positionX = 150 + i*(ship.Width + 50);

                ship.Position = new Point(positionX, positionY);

                Actors.Add(ship);
            }

            for (int i = 0; i < 6; i++)
            {
                var ship_red = new Ship_red(this);
                int positionY = ship_red.Height + 50;
                int positionX = 100 + i*(ship_red.Width + 50);

                ship_red.Position = new Point(positionX, positionY);

                Actors.Add(ship_red);
            }

            for (int i = 0; i < 1; i++)
            {
                var ball = new Ball_lightning(this);
                int positionY = ball.Height + 10;
                int positionX = 150 + i*(ball.Width + 50);

                ball.Position = new Point(positionX, positionY);

                Actors.Add(ball);
            }

            // Player
            Player = new PlayerShip(this);
            int playerPositionX = Size.Width/2 - Player.Width/2;
            int playerPositionY = Size.Height - Player.Height - 50;
            Player.Position = new Point(playerPositionX, playerPositionY);
            Actors.Add(Player);
        }

        #endregion

        public void Fon()
        {
            if (fon.ElapsedMilliseconds > 500)
            {
                if (k == false)
                {
                    FileName = @"Assets\LevelTwo.png";
                    k = true;
                    fon.Restart();
                    h_load_image();
                }
                else
                {
                    FileName = @"Assets\LevelTwo2.png";
                    k = false;
                    fon.Restart();
                    h_load_image();
                }
            }
        }

        public override BaseLevel NextLevel()
        {
            return new StartScreen();
        }

        public override void Update()
        {
            base.Update();
            Fon();
        }

        public override void Load()
        {
            base.Load();
            fon.Start();
        }
    }
}