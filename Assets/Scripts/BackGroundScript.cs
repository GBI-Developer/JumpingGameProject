using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
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
    Camera _camera;

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
    }
    void Update()
    {

        // 座標変換
        Vector3 myViewport = _camera.WorldToViewportPoint(bgTfm.position);
        // Debug.Log("myViewport:" + myViewport);
        // Debug.Log("leftOffset:" + leftOffset);
        // Debug.Log("bgTfm.position:" + bgTfm.position);
        // 背景の回り込み(カメラがy軸プラス方向に移動時)
        if (myViewport.y < leftOffset) {
            Debug.Log("bgTfm.name:" + bgTfm.name);
            Debug.Log("bgTfm.position.y:" + bgTfm.position.y);
            Debug.Log("height:" + height);
            bgTfm.position += Vector3.up * (height * spriteCount);
        }
        // 背景の回り込み(カメラがy軸マイナス方向に移動時)
        else if (myViewport.y > rightOffset) {
            //bgTfm.position -= Vector3.up * (height * spriteCount);
        }
    }

}
