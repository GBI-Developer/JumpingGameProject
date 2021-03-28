using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    [Header("スコア")] public int score;
    [Header("現在のステージ")] public int stageNum;
    [Header("現在の復帰位置")] public int continueNum;
    [Header("現在の残機")] public int heart;
    [Header("デフォルトの残機")] public int defaultHeart;
    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public bool isStageClear = false;

    private AudioSource audioSource = null;
    private float maxPos = 0f;
    // ゲームモード
    private string mode = "easy";

    // TODO:以下プレイヤースクリプトで参照している。
    // シングルトンのスクリプトでわざわざ設定する必要があるか？
    private float bkMaxSizeX = 0.0f;
    private float bkMinSizeX = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 残機を１つ増やす
    /// </summary>
    public void AddHeartNum()
    {
        if (heart < 99)
        {
            ++heart;
        }
    }

    /// <summary>
    /// 残機を１つ減らす
    /// </summary>
    public void SubHeartNum()
    {
        if (heart > 0)
        {
            --heart;
        }
        else
        {
            isGameOver = true;
        }
    }

    /// <summary>
    /// 最初から始める時の処理
    /// </summary>
    public void RetryGame()
    {
        isGameOver = false;
        heart = defaultHeart;
        score = 0;
        stageNum = 1;
        continueNum = 0;
    }

    /// <summary>
    /// SEを鳴らす
    /// </summary>
    public void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);

        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    /// <summary>
    /// 現在の最高位置を返す
    /// </summary>
    public float GetMaxPos()
    {
        return maxPos;
    }

    /// <summary>
    /// 現在のゲームモードを返す
    /// </summary>
    public string GetMode()
    {
        return mode;
    }

    /// <summary>
    /// 現在の背景画像.bounds.max.xをセットする
    /// </summary>
    public void setBkMaxSizeX(float size)
    {
        this.bkMaxSizeX = size;
    }
    /// <summary>
    /// 現在の背景画像.bounds.max.xを返す
    /// </summary>
    public float getBkMaxSizeX()
    {
        return this.bkMaxSizeX;
    }

    /// <summary>
    /// 現在の背景画像.bounds.min.xをセットする
    /// </summary>
    public void setBkMinSizeX(float size)
    {
        this.bkMinSizeX = size;
    }
    /// <summary>
    /// 現在の背景画像.bounds.min.xを返す
    /// </summary>
    public float getBkMinSizeX()
    {
        return this.bkMinSizeX;
    }
}