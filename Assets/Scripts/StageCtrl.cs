using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("最大位置")] public GameObject MaxPosObj;
    [Header("接地判定")] public GroundCheck ground;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("フェード")] public FadeImage fade;
    [Header("ゲームオーバー時に鳴らすSE")] public AudioClip gameOverSE;
    [Header("リトライ時に鳴らすSE")] public AudioClip retrySE;
    [Header("ステージクリアーSE")] public AudioClip stageClearSE;
    [Header("ステージクリア")] public GameObject stageClearObj;
    [Header("ステージクリア判定")] public PlayerTriggerCheck stageClearTrigger;
    [Header("続行ボタン")] public GameObject continueButtonObj;
    [Header("終了ボタン")] public GameObject endButtonObj;

    private Player p;
    float _maxPos = 0f;
    private int nextStageNum;
    private bool _isFirstStart = true;
    private bool startFade = false;
    private bool doGameOver = false;
    private bool retryGame = false;
    private bool doSceneChange = false;
    private bool doClear = false;
    public GameOverCtrl goc;
    public GameObject[] grounds;

    // Start is called befor the first frame update
    void Start()
    {
        // アタッチされているか
        bool isAttach = playerObj != null && continuePoint != null
            && continuePoint.Length > 0 && fade != null;

        if (!isAttach)
        {
            Debug.Log("設定が足りてないよ！");
        }
       //Transform _startPos = GameObject.FindGameObjectWithTag(ground.StartGroundTag).transform;
        //MaxPosObj.transform.position = _startPos.position;
        playerObj.transform.position = continuePoint[0].transform.position;
        p = playerObj.GetComponent<Player>();
        goc = gameOverObj.GetComponent<GameOverCtrl>();

        if (p == null)
        {
            Debug.Log("プレイヤーじゃない物がアタッチされているよ！");
        }
        if (goc == null)
        {
            Debug.Log("プレイヤーじゃない物がアタッチされているよ！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 最大位置更新
        if (ground.GetOnFloorType() != ground.StartGroundTag)
        {
            if (MaxPosObj.transform.position.y < playerObj.transform.position.y)
            {
                MaxPosObj.transform.position = new Vector3(transform.position
                    .x, playerObj.transform.position.y, transform.position.z);

                int _addScore = ((int)MaxPosObj.transform.position.y * 30)
                     - GManager.instance.score;
                if (_addScore > 0.0f)
                {
                    // スコアを非同期で加算する
                    StartCoroutine(ScoreAnimation(_addScore, 0.5f));
                }
            }
        }

        //ゲームオーバー時の処理
        if (GManager.instance.isGameOver && !doGameOver)
        {
            // ゲームオーバーキャンバスを呼び出す
            goc.ShowGameOverCanvas();
            GManager.instance.isGameOver = false;
            doGameOver = true;
        }
        //プレイヤーがやられた時の処理
        else if (p != null && p.IsContinueWaiting() && !doGameOver)
        {
            if (continuePoint.Length > GManager.instance.continueNum)
            {
                grounds = GameObject.FindGameObjectsWithTag("Ground");

                float min_target_distance = float.MaxValue;
                GameObject target = null;
                foreach (var ground in grounds) {
                    float target_distance = Vector3.Distance(MaxPosObj.transform.position, ground.transform.position);
                    if (target_distance < min_target_distance) {
                        min_target_distance = target_distance;
                        target = ground.transform.gameObject;
                        Debug.Log(ground.name);
                    }
                }
                playerObj.transform.position = target.transform.position;
                p.ContinuePlayer();
            }
            else
            {
                Debug.Log("コンティニューポイントの設定が足りてないよ！");
            }
        }
        else if (stageClearTrigger != null && stageClearTrigger.isOn && !doGameOver && !doClear)
        {
            StageClear();
            doClear = true;
        }

        //ステージを切り替える
        if (fade != null && startFade && !doSceneChange)
        {
            if (fade.IsFadeOutComplete())
            {
                //ゲームリトライ
                if (retryGame)
                {
                    GManager.instance.RetryGame();
                }
                //次のステージ
                else
                {
                    GManager.instance.stageNum = nextStageNum;
                }
                GManager.instance.isStageClear = false;
                SceneManager.LoadScene("stage" + nextStageNum);
                doSceneChange = true;
            }
        }
    }

    //続行ボタンを押されたら呼ばれる
    public void ContinueButton()
    {
        Debug.Log("continue Start!");
        Retry();
        // if (pausePush)
        // {
        //     GManager.instance.PlaySE(buttonSE);
        //     Debug.Log("ContinueButton!");
        //     cg.alpha = 0f;
        //     cg.blocksRaycasts = false;
        //     pausePush = false;
        // }

    }

    //続行ボタンを押されたら呼ばれる
    public void EndButton()
    {
        Debug.Log("end Start!");
        // if (pausePush)
        // {
        //     GManager.instance.PlaySE(buttonSE);
        //     Debug.Log("EndButton!");
        //     // pause.StartFadeOut();
        //     // pausePush = true;
        // }
 
    }

    /// <summary>
    /// 最初から始める
    /// </summary>
    public void Retry()
    {
        GManager.instance.PlaySE(retrySE);
        ChangeScene(1); //最初のステージに戻るので１
        retryGame = true;
    }

    /// <summary>
    /// ステージを切り替えます。
    /// </summary>
    /// <param name="num">ステージ番号</param>
    public void ChangeScene(int num)
    {
        if (fade != null)
        {
            nextStageNum = num;
            fade.StartFadeOut();
            startFade = true;
        }
    }

    /// <summary>
    /// ステージをクリアした
    /// </summary>
    public void StageClear()
    {
        GManager.instance.isStageClear = true;
        stageClearObj.SetActive(true);
        GManager.instance.PlaySE(stageClearSE);
    }

    /// /// <summary>
    /// 最大位置を更新する
    /// </summary>
    public void MaxPosUpdate()
    {

    }

    /// /// <summary>
    /// スコアをアニメーションさせる
    /// </summary>
    IEnumerator ScoreAnimation(int addScore, float time)
    {
        float befor = GManager.instance.score;
        float after = befor + addScore;
        float score = MaxPosObj.transform.position.y;
        //0fを経過時間にする
        float elapsedTime = 0.0f;
        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // スコアの更新
            GManager.instance.score = int.Parse((befor + (after - befor) * rate).ToString("f0"));
            Debug.Log((befor + (after - befor) * rate).ToString("f0"));
            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的なスコアをセット
        GManager.instance.score = (int)after;
    }
    
}