
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Beans
{
    public class GroundPosBean
    {
        // メンバ変数はprivateに設定
        // private List<float> xyPosList = null;
        private float x;
        private float y;

        public GroundPosBean()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }

        // 外部からの取得用（publicに設定）
        public float GetX()
        {
            return this.x;
        }
        public float GetY()
        {
            return this.y;
        }

        // 外部からの設定用（publicに設定）
        public void SetX(float x)
        {
            this.x = x;
        }
        public void SetY(float y)
        {
            this.y = y;
        }

    }
}


