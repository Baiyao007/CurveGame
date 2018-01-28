//作成日：　2017.10.04
//作成者：　柏
//クラス内容：　自分用メソッド
//修正内容リスト：
//名前：柏　　　日付：20171020　　　内容：線形補間追加
//名前：　　　日付：　　　内容：


using Microsoft.Xna.Framework;
using System;

namespace MyLib.Utility
{
    public static class Methord
    {
        //for内forのまとめ
        public static void MyForeach(Action<int, int> action, Vector2 xy)
        {
            for (int y = 0; y < xy.Y; y++) {
                for (int x = 0; x < xy.X; x++) {
                    action(x, y);
                }
            }
        }

        //点の四角い内判定（外積法）
        public static bool IsInScale(Vector2 position, Vector2 leftTop, Vector2 scaleXY)
        {
            bool isIn1 = Vector2Cross(new Vector2(-scaleXY.X, 0), position - new Vector2(leftTop.X + scaleXY.X, leftTop.Y)) < 0;
            bool isIn2 = Vector2Cross(new Vector2(scaleXY.X, 0), position - new Vector2(leftTop.X, leftTop.Y + scaleXY.Y)) < 0;
            bool isIn3 = Vector2Cross(new Vector2(0, -scaleXY.Y), position - new Vector2(leftTop.X + scaleXY.X, leftTop.Y + scaleXY.Y)) < 0;
            bool isIn4 = Vector2Cross(new Vector2(0, scaleXY.Y), position - leftTop) < 0;
            return isIn1 && isIn2 && isIn3 && isIn4;
        }

        //二次元外積
        public static float Vector2Cross(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        //二次元ベクトル回す
        public static Vector2 RotateVector2(Vector2 vec, float angle) {
            float radian = MathHelper.ToRadians(angle);
            Vector2 newVec = new Vector2(
                vec.X * (float)Math.Cos(-radian) - vec.Y * (float)Math.Sin(-radian),
                vec.X * (float)Math.Sin(-radian) + vec.Y * (float)Math.Cos(-radian)
            );
            return newVec;
        }

        public static Vector3 RotateVector3(Vector3 vec, float angle) {
            float radian = MathHelper.ToRadians(angle);
            Vector3 newVec = new Vector3(
                vec.X * (float)Math.Cos(-radian) - vec.Y * (float)Math.Sin(-radian),
                vec.X * (float)Math.Sin(-radian) + vec.Y * (float)Math.Cos(-radian),
                vec.Z
            );
            return newVec;
        }


        //二次線形補間
        public static float GetQuadraticInterpolateValue(float timeRate)
        {
            float interpolateValue = timeRate * (2 - timeRate);
            return interpolateValue;
        }

        //三次線形補間
        public static float GetCubicInterpolateValue(float timeRate)
        {
            float interpolateValue = timeRate * timeRate * (3 - 2 * timeRate);
            return interpolateValue;
        }

        public static float GetSinInterpolateValue(float timeRate) {
            float interpolateValue =  (float)Math.Sin(Math.PI * timeRate);
            return interpolateValue;
        }

        public static float GetCosInterpolateValue(float timeRate)
        {
            float interpolateValue = (float)Math.Cos(Math.PI * timeRate);
            return interpolateValue;
        }


        //直角方向移動
        public static Vector2 RightAngleMove(Vector2 direction, float distance) {
            bool isRight = direction.X > 0;
            if (isRight) {
                direction = RotateVector2(direction, 90);
            }
            else {
                direction = RotateVector2(direction, -90);
            }
            direction.Normalize();
            direction *= distance;
            return direction;
        }

        public static int GetQuadrant(float angle) {
            int quadrant = (int)(angle / 90) % 4;
            return quadrant;
        }

        public static float AngleClamp(float angle) {
            if (angle >= 360) { angle -= 360; }
            if (angle <= 0) { angle += 360; }
            return angle;
        }

        public static float ToDegree(float radian) {
            float angle = MathHelper.ToDegrees(radian);
            angle = AngleClamp(angle);
            return angle;
        }




        public static bool CircleSegment(ref Vector2 center, float radius, Vector2 p1, Vector2 p2, ref Vector2 normal)
        {
            Vector2 v = p2 - p1;
            Vector2 nv = Vector2.Normalize(v);
            Vector2 v1 = center - p1;
            float r2 = radius * radius;
            float t = Vector2.Dot(v, v1) / Vector2.Dot(v, v);
            normal = Vector2.Zero;

            float l = Vector2Cross(nv, v1);
            Vector2 vn = v1 - v * t;    //法線方向のベクトル
            if ((0 <= t && t <= 1) && l * l <= r2)
            {
                center = p1 + v * t + radius * Vector2.Normalize(vn);
                normal = Vector2.Normalize(new Vector2(v.Y, -v.X));
                return true;
            }

            if (t < 0 && v1.LengthSquared() <= r2)
            {
                center = p1 + radius * Vector2.Normalize(v1);
                normal = Vector2.Normalize(v1);
                return true;
            }

            Vector2 v2 = center - p2;
            if (t > 1 && v2.LengthSquared() <= r2)
            {
                center = p2 + radius * Vector2.Normalize(v2);
                normal = Vector2.Normalize(v2);
                return true;
            }
            return false;
        }


        public static float MaxClamp(float target, float max ) {
            return target > max ? max : target;
        }

        public static float MinClamp(float target, float min) {
            return target < min ? min : target;
        }

    }
}
