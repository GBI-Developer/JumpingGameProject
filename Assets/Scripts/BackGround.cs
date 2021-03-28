using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Beans;

public class BackGround : MonoBehaviour
{
    // 背景の枚数
    int spriteCount = 3;
    // 背景が回り込み
    float rightOffset = 1.45f;
    float leftOffset = -0.8f;
    float height;

    Transform bgTfm;
    SpriteRenderer mySpriteRndr;
    GameObject cameraObj;
    public GameObject groundPrefab;
    Camera _camera;
    public int mathSizeX = 0;
    public int mathSizeY = 0;

    void Start()
    {
        cameraObj = GameObject.Find ("Main Camera");
        _camera = cameraObj.GetComponent<Camera>();
        // 背景画像のトランスフォーム
        bgTfm = transform;
        // 背景画像のスプライトレンダラー
        mySpriteRndr = GetComponent<SpriteRenderer>();
        // 背景画像の縦サイズ
        height = mySpriteRndr.bounds.size.y;
        // マスサイズを決める
        List<int> mathSize = this.GetMathSize();
        this.mathSizeX = mathSize[0];
        this.mathSizeY = mathSize[1];
    }
    void Update()
    {
        // 座標変換
        Vector3 myViewport = _camera.WorldToViewportPoint(bgTfm.position);
        Vector3 localPos   = bgTfm.localPosition;
        GManager.instance.setBkMaxSizeX(mySpriteRndr.bounds.max.x);
        GManager.instance.setBkMinSizeX(mySpriteRndr.bounds.min.x);

        // //Debug.Log("leftOffset:" + leftOffset);
        // //Debug.Log("bgTfm.position:" + bgTfm.position);
        // 背景の回り込み(カメラがy軸プラス方向に移動時)
        //Debug.Log("bg.name:" + bgTfm.name + " bg.loPos.y:" + localPos.y + " bg.loPos.x:" + localPos.x);
        //Debug.Log("bg.name:" + bgTfm.name + " bg.pos.y:" + bgTfm.position.y + " bg.pos.x:" + bgTfm.position.x);
        if (myViewport.y < leftOffset) {
            var math = createMath();
            //Debug.Log("bgTfm.name:" + bgTfm.name);
            //Debug.Log("bgTfm.position.y:" + bgTfm.position.y);
            //Debug.Log("height:" + height);
            bgTfm.position += Vector3.up * (height * spriteCount);
            float _xSpan = mySpriteRndr.bounds.size.x / mathSizeX;
            float _ySpan = mySpriteRndr.bounds.size.y / mathSizeY;
            float _ypos  = 0;
            for (var i = 0; i < math.Count; i++)
            {
                _ypos = _ySpan * i;
                for (var j = 0; j < math[i].Count; j++)
                {
                    float chosei = Random.Range(0.0f, 2.0f);
                    float _calcX = (mySpriteRndr.bounds.min.x + (_xSpan * j));
                    float _calcY = (mySpriteRndr.bounds.min.y + (_ypos));
                    float _xPos = Random.Range(_calcX, _calcX);
                    float _yPos = Random.Range(_calcY - chosei, _calcY);
                    math[i][j].SetX(_xPos);
                    math[i][j].SetY(_yPos);
                }
            }
            // 座標デバッグ出力
            // var fileName = @"D:\work\output.txt";
            // var encoding = System.Text.Encoding.GetEncoding("SHIFT_JIS");
            // var writer = new System.IO.StreamWriter(fileName, false, encoding);
            // for (var i = 0; i < math.Count; i++)
            // {
            //     for (var j = 0; j < math[i].Count; j++)
            //     {
            //         writer.WriteLine("math i:"+i+" j:"+j+" getX:" + math[i][j].GetX());
            //         writer.WriteLine("math i:"+i+" j:"+j+" getY:" + math[i][j].GetY());
            //     }
            // }
            // writer.Close();

            for (var i = 0; i < math.Count; i++)
            {
                for (var j = 0; j < math[i].Count; j++)
                {
                    if (Random.Range(0, 10) > 2)
                    {
                        continue;
                    }
                    Instantiate(groundPrefab, new Vector3(
                        math[i][j].GetX(), math[i][j].GetY(), bgTfm.position.z -5.0f
                    ), Quaternion.identity, bgTfm);
                }
            }

        }
        // 背景の回り込み(カメラがy軸マイナス方向に移動時)
        else if (myViewport.y > rightOffset) {
            //bgTfm.position -= Vector3.up * (height * spriteCount);
        }
    }

    private List<List<GroundPosBean>> createMath()
    {
        var math = new List<List<GroundPosBean>>();
        for (var i = 0; i < this.mathSizeY; i++)
        {
            List<GroundPosBean> xLine = new List<GroundPosBean>();
            for (var j = 0; j < this.mathSizeX; j++)
            {
                xLine.Add(new GroundPosBean());
            }
            math.Add(xLine);
        }
        return math;
    }

    /// <summary>
    /// 現在のゲームモードによってマスのサイズを返す
    /// </summary>
    public List<int> GetMathSize()
    {
        // マネージャからゲームモードを取得
        string mode = GManager.instance.GetMode();
        List<int> mathSize = new List<int>();

        if (mode.Equals("easy"))
        {
            mathSize.Add(7);
            mathSize.Add(15);
        }
        else if (mode.Equals("nomal"))
        {
            mathSize.Add(6);
            mathSize.Add(13);
        }
        else if (mode.Equals("hard"))
        {
            mathSize.Add(5);
            mathSize.Add(11);
        }
        return mathSize;
    }

}
