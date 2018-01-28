using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveGame.Objects.Circles
{
    class CircleMoveWithRoute : Circle
    {
        private Vector2 startPosition;
        private Vector2 velocity;

        private List<Vector2> routePoints;
        private int routeIndex;
        private int nextIndex;
        private float speed;

        public CircleMoveWithRoute(Vector2 position, List<Vector2> routePoints)
            :base(position)
        {
            this.routePoints = routePoints;
            startPosition = position;
        }

        public override void Initialize()
        {
            RotateAngle = 0;
            RotateSpeed = -2;

            routeIndex = 0;
            nextIndex = routeIndex + 1;
            speed = 3;

            Position = startPosition;
        }

        public override void Update() {
            base.Update();
            CheckVelo();
        }

        protected override void Move() { Position += velocity * speed; }

        private void Warp(ref int index) {
            index = index == routePoints.Count ? 0 : index;
         }

        private void CheckVelo() {
            if ((Position - routePoints[nextIndex]).LengthSquared() <= speed) {
                Position = routePoints[nextIndex];
                routeIndex++;
                nextIndex++;
            }
            Warp(ref routeIndex);
            Warp(ref nextIndex);

            velocity = routePoints[nextIndex] - routePoints[routeIndex];
            velocity.Normalize();
        }

    }
}
