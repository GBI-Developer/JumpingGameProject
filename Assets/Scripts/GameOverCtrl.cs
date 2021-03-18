using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("ゲームオーバキャンバス")] public CanvasGroup cg;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("フェード")] public FadeImage fade;
    [Header("ゲームオーバー時に鳴らすSE")] public AudioClip gameOverSE;
    [Header("リトライ時に鳴らすSE")] public AudioClip retrySE;
    [Header("続行ボタン")] public GameObject continueButtonObj;
    [Header("終了ボタン")] public GameObject endButtonObj;

    private Player p;
    private bool startFade = false;
    private bool doGameOver = false;
    private bool retryGame = false;

    // Start is called before the first frame update
    void Start()
    {
        // アタッチされているか
        bool isAttach = playerObj != null && gameOverObj != null
            && fade != null;
        if (isAttach)
        {
            p = playerObj.GetComponent<Player>();
            if (p == null)
            {
                Debug.Log("プレイヤーじゃない物がアタッチされているよ！");
            }
        }
        else
        {
            Debug.Log("設定が足りてないよ！");
        }
    }

    //ゲームオーバーキャンバスを表示する
    public void ShowGameOverCanvas()
    {
            Debug.Log("GameOver!");
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
    }

    //続行ボタンを押されたら呼ばれる
    public void ContinueButton()
    {
        Debug.Log("continue Start!");
        //GManager.instance.PlaySE(buttonSE);
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        // GManager.instance.
        Retry();
    }

    //続行ボタンを押されたら呼ばれる
    public void EndButton()
    {
        Debug.Log("end Start!");
        SceneManager.LoadScene("Title"); //タイトル画面へ
    }

    /// <summary>
    /// 最初から始める
    /// </summary>
    public void Retry()
    {
        if (fade != null)
        {
            //nextStageNum = 1;
            fade.StartFadeIn();
            startFade = true;
        }
        GManager.instance.RetryGame();
        SceneManager.LoadScene("Stage"); //最初のステージに戻るので１
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
            //nextStageNum = num;
            fade.StartFadeOut();
            startFade = true;
        }
    }

}