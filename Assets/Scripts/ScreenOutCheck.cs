using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 未使用
public class ScreenOutCheck : MonoBehaviour
{

    #region//インスペクターで設定する
    [Header("対象オブジェクト")] public Transform targetObj;

    #endregion

    Camera _camera;
    GameObject cameraObj;
	Rect rect = new Rect(0, 0, 1, 1); // 画面内か判定するためのRect

    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.Find ("Main Camera");
        _camera = cameraObj.GetComponent<Camera>();
        targetObj.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var viewportPos = _camera.WorldToViewportPoint(targetObj.position);

		if(rect.Contains(viewportPos))
			Debug.Log("画面内にいるよ");
		else
		{
			Debug.Log("画面外だよ");
		}
    }
}
