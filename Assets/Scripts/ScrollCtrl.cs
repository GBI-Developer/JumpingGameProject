using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ScrollCtrl : MonoBehaviour
{

    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("デッドオブジェクト")] public GameObject deadPosObj;
    [Header("背景オブジェクトキャンバス")] public GameObject bkCanvasObj;
    [Header("カメラコライダー")] public GameObject cameraCollider;
    [Header("ゲームオブジェクト")] public SpriteRenderer gameObj;
    SpriteRenderer targetRenderer;
    Player player;
    Transform pTransform;
    private Transform _transform;
    GameObject cameraObj;
    Camera _camera;

    void Start()
    {
        // TODO: CameraColliderのポジション、スケールを固定化している。
        // transform inspector
        // Position x 0 y 53855 z 0
        // Rotation x 0 y 0     z 0
        // Scale    x 0 y 898   x 0

        cameraObj = GameObject.Find ("Main Camera");
        _camera = cameraObj.GetComponent<Camera>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        deadPosObj  = GameObject.FindGameObjectWithTag("DeadArea");
        player = playerObj.GetComponent<Player>();
        pTransform = playerObj.transform;

    }
    void LateUpdate()
    {
        Vector3 topLeft = _camera.ScreenToWorldPoint (Vector3.zero);
        deadPosObj.transform.position = new Vector3(0, topLeft.y, 0);
        cameraCollider.transform.position = new Vector3(0, topLeft.y, 0);
        // 背景画像のスプライトレンダラー
        // targetRenderer = GetComponent<SpriteRenderer>();
        // if (!GetComponent<SpriteRenderer>().isVisible) {
        //     Debug.Log("gameObject.name:" + this.gameObject.name);
        //     Destroy(this.gameObject);
        // }
        if (GManager.instance == null) {
            Debug.Log("GManager.instance NULL!!");
        }

    }

}
