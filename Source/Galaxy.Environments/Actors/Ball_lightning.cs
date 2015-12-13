#region using

using System;
using System.Diagnostics;
using System.Windows;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

#endregion

namespace Galaxy.Environments.Actors
{
    //шаровая молния Ctrl+E+F
    public class Ball_lightning : BaseActor
    {
        #region Constant

        private bool m_isAlive;
        
        #endregion

        #region Private fields

        private bool m_flying;
        private Stopwatch m_flyTimer;

        #endregion

        public Ball_lightning(ILevelInfo info) : base(info)
        {
            Width = 35;
            Height = 35;
            ActorType = ActorType.Ball;
        }

        //условие неубиваемости
        public override bool IsAlive
        {
            get { return m_isAlive; }
            set
            {
                m_isAlive = true; //всегда живет
                CanDrop = false; //умереть не может
            }
        }

        #region Overrides

        public override void Update()
        {
            base.Update();

            h_changePosition();
        }

        #endregion

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\ball.png");
        }

        #endregion

        #region Private methods

        //траектория движения
        private void h_changePosition()
        {
            Point playerPosition = Info.GetPlayerPosition();

            Position = new Point((Position.X + 2), (Position.Y + 2));
        }

        #endregion
    }
}