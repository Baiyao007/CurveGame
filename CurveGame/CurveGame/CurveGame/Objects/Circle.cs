using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveGame.Objects
{
    class Circle
    {
        public Vector2 Position { get; set; }
        public float RotateAngle { get; set; }
        public float RotateSpeed { get; set; }

        protected Spot spot;

        private float MoveAngle;
        private float MoveSpeed;

        private Vector2 outCentre;
        private float radius;

        public Circle(Vector2 position) {
            Position = position;
        }

        public virtual void Initialize() {
            RotateAngle = 0;
            RotateSpeed = -3;
            radius = 60;


            outCentre = new Vector2(400, 300);
            MoveAngle = 0;
            MoveSpeed = 3;
        }

        public virtual void Update() {
            RotateAngle += RotateSpeed;
            if (RotateAngle > 360) { RotateAngle -= 360; }
            if (spot != null) { spot.Update(); }

            Move();
        }

        protected virtual void Move() {
            MoveAngle += MoveSpeed;
            if (MoveAngle > 360) { MoveAngle -= 360; }
            float radian = MathHelper.ToRadians(MoveAngle);
            Position = outCentre + new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian)) * radius;
        }

        public void SetSpot(Vector2 position) {
            if ((position - Position).Length() < ResouceManager.GetTextureSize("Wheel").X / 2) {
                spot = new Spot(position, this);
            }
        }

        public void Draw() {
            Renderer_2D.Begin();

            Vector2 imgSize = ResouceManager.GetTextureSize("Wheel");
            Rectangle rect = new Rectangle(0, 0, (int)imgSize.X, (int)imgSize.Y);
            Renderer_2D.DrawTexture("Wheel", Position, Color.White, 1, rect, Vector2.One, MathHelper.ToRadians(RotateAngle), imgSize / 2);

            Renderer_2D.End();

            if (spot != null) { spot.Draw(); }
        }
    }
}
