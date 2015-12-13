#region using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

#endregion

namespace Galaxy.Environments
{
    /// <summary>
    ///   The level class for Open Mario.  This will be the first level that the player interacts with.
    /// </summary>
    /// 
    public class LevelOne : BaseLevel
    {
        private bool k = false;
        private int m_frameCount;
        private Stopwatch eBullet = new Stopwatch();
        private Stopwatch fon = new Stopwatch();

        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="LevelOne" /> class.
        /// </summary>
        public LevelOne()
        {
            // Backgrounds
            FileName = @"Assets\LevelOne.png";
            //FileName = @"Assets\LevelOne2.png";
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

        //public void timer()
        //  {
        //      System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        //      tmr.Interval = 1000;
        //      tmr.Tick += new EventHandler(t_Tick);
        //      tmr.Start();
        //  }

        //void t_Tick(object sender, EventArgs e)
        //{
        //    FileName = @"Assets\LevelOne.png";
        //    FileName = @"Assets\LevelOne2.png";
        //    (sender as Timer).Stop();
        //}

        public void Fon()
        {
            if (fon.ElapsedMilliseconds > 500)
            {
                if (k == false)
                {
                    FileName = @"Assets\LevelOne.png";
                    k = true;
                    fon.Restart();
                    h_load_image();
                }
                else
                {
                    FileName = @"Assets\LevelOne2.png";
                    k = false;
                    fon.Restart();
                    h_load_image();
                }
            }
        }

        #region Overrides

        private void h_dispatchKey()
        {
            if (!IsPressed(VirtualKeyStates.Space)) return;

            if (m_frameCount%10 != 0) return;

            Bullet bullet = new Bullet(this)
            {
                Position = Player.Position
            };

            bullet.Load();
            Actors.Add(bullet);
        }

        public override BaseLevel NextLevel()
        {
            return new StartScreen();
        }

        public void ShotEnemyBullet()
        {
            if (eBullet.ElapsedMilliseconds < 5)
                return;

            var enbull = new EnemyBullet(this);
            var elist = Actors.Where((actor) => actor is Ship_red || actor is Ship).ToList();
            if (elist.Count > 0)
            {
                Random ran = new Random();
                int r = ran.Next(elist.Count());

                var rrr = elist[r].Position;
                //int positionX = enbull.Position.X + 10;
                //int posititonY = enbull.Position.Y + 10;
                enbull.Position = new Point(rrr.X, rrr.Y + 10);
                enbull.Load();

                Actors.Add(enbull);
            }

            eBullet.Restart();
        }

        public override void Update()
        {
            m_frameCount++;
            h_dispatchKey();
            ShotEnemyBullet();
            Fon();

            base.Update();

            IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(Actors);

            //условие неубиваемости молнии
            foreach (BaseActor killedActor in killedActors)
            {
                if (killedActor.IsAlive) 
                killedActor.IsAlive = false;
            }

            List<BaseActor> toRemove = Actors.Where(actor => actor.CanDrop).ToList();
            BaseActor[] actors = new BaseActor[toRemove.Count()];
            toRemove.CopyTo(actors);

            foreach (BaseActor actor in actors.Where(actor => actor.CanDrop))
            {
                Actors.Remove(actor);
            }

            if (Player.CanDrop)
                Failed = true;

            //has no enemy
            if (Actors.All(actor => actor.ActorType != ActorType.Enemy))
                Success = true;
            //////
            if (Actors.Where((actor) => actor is Ship || actor is Ship_red).ToList().Count == 0)
                Success = true;
        }

        public override void Load()
        {
            base.Load();
            fon.Start();
            eBullet.Start();
        }

        #endregion
    }
}