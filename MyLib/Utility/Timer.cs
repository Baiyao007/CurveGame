//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Timerクラス(カントダウンタイプｓ)
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

namespace MyLib.Utility
{
    public class Timer
    {
        private float currentTime;
        private float limitTime;
        private bool isTime;

        public delegate void timerDelegate();
        public timerDelegate Dt;

        public Timer(float second) {
            limitTime = second * 60;
            currentTime = second * 60;
            isTime = false;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            currentTime = limitTime;
            isTime = false;
        }

        public void SetIsTime() {
            isTime = true;
        }

        //時間がなくなる前にカウントダウンする
        public void Update() {
            if (isTime) { return; }
            currentTime--;
            if (currentTime <= 0) {
                isTime = true;
                currentTime = 0;
                if (Dt != null) {
                    Initialize();
                    Dt();
                }
            }
        }

        /// <summary>
        /// 時間になったかどうかをとる
        /// </summary>
        public bool IsTime{
            get { return isTime; }
        }

        /// <summary>
        /// 残った時間をとる
        /// </summary>
        public float NowTime {
            get { return currentTime; }
            set { currentTime = value * 60; }
        }

        /// <summary>
        /// 経過した時間の割合
        /// </summary>
        /// <returns>比</returns>
        public float Rate() {
            return currentTime / limitTime;
        }

        public float InterpoRate() {
            float rate = 0;
            if (currentTime > limitTime / 2) {
                rate = (limitTime - currentTime) / limitTime;
            }
            else {
                rate = currentTime / limitTime;
            }
            return rate;
        }


        public void SetTimer(float second) {
            limitTime = second * 60;
            currentTime = second * 60;
            isTime = false;
        }
    }
}
