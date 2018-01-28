using Microsoft.Xna.Framework;
using MyLib.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurveGame.Objects
{
    class Spot
    {
        private Vector2 position;
        private Circle circle;
        private List<Vector2> route;

        private float radius;

        public Spot(Vector2 position, Circle circle) {
            this.position = position;
            this.circle = circle;
            route = new List<Vector2>();

            radius = (circle.Position - position).Length();
        }

        public void Update() {
            float radian = MathHelper.ToRadians(circle.RotateAngle);
            position = circle.Position + new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian)) * radius;
            if (route.Count > 2000) { return; }
            if (route.Count > 0) {
                if ((position - route[route.Count - 1]).LengthSquared() < 4) { return; }
            }
            route.Add(position);
        }

        public void Draw() {
            Renderer_2D.Begin();

            Vector2 imgSize = ResouceManager.GetTextureSize("Point");
            Rectangle rect = new Rectangle(0, 0, (int)imgSize.X, (int)imgSize.Y);
            Renderer_2D.DrawTexture("Point", position, Color.Red, 1, rect, Vector2.One, MathHelper.ToRadians(circle.RotateAngle), imgSize / 2);

            for (int i = 0; i < route.Count; i++) {
                Renderer_2D.DrawTexture("Point", route[i], Color.Blue, 1, rect, Vector2.One * 0.3f, MathHelper.ToRadians(circle.RotateAngle), imgSize / 2);
            }

            Renderer_2D.End();
        }
    }
}
