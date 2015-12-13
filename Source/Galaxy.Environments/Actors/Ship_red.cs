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
    internal class Ship_red : Ship
    {
        public Ship_red(ILevelInfo info) : base(info)
        {
            Width = 25;
            Height = 35;
            ActorType = ActorType.Enemy;
        }

        public override void Update()
        {
            base.Update();
            //EnemyBullet enemyBullet = new EnemyBullet(Info);
        }

        public override void Load()
        {
            base.Load();
            Load(@"Assets\ship_red.png");
        }
    }
}