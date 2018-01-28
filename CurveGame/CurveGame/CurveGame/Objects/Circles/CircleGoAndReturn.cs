using Microsoft.Xna.Framework;
using MyLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveGame.Objects.Circles
{
    class CircleGoAndReturn : Circle
    {
        private Timer timer;
        private Vector2 startPosition;
        private Vector2 velocity;

        public CircleGoAndReturn(Vector2 position, Vector2 velocity)
            :base(position)
        {
            this.velocity = velocity;
            startPosition = position;
            timer = new Timer(2);
            timer.Dt = new Timer.timerDelegate(Trun);
        }

        public override void Initialize() {
            RotateAngle = 0;
            RotateSpeed = 5;

             Position = startPosition;
            timer.Initialize();
        }

        public override void Update() {
            base.Update();
            timer.Update();
        }

        protected override void Move() {
            Position += velocity * timer.InterpoRate();
        }

        private void Trun() {
            velocity *= -1;
        }

    }
}
