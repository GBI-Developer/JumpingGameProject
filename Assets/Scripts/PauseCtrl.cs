﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCtrl : MonoBehaviour
{
    [Header("ポーズキャンバス")] public CanvasGroup cg;
    [Header("ポーズ")] public PauseImage pause;
    [Header("ボタン押下時に鳴らすSE")] public AudioClip buttonSE;
    [Header("ポーズボタン")] public GameObject pauseButtonObj;
    [Header("続行ボタン")] public GameObject continueButtonObj;
    [Header("終了ボタン")] public GameObject endButtonObj;
     private bool pausePush = false;
     private CanvasGroup thisCanvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        // アタッチされているか
        bool isAttach = pauseButtonObj != null
            && continueButtonObj != null && endButtonObj != null;

        if (isAttach)
        {
            // continueButtonObj.SetActive(false);
            // endButtonObj.SetActive(false);
        }
    }
    //ポーズボタンを押されたら呼ばれる
    public void PauseButton()
    {
        Debug.Log("pause Start!");
        if (!pausePush)
        {
            GManager.instance.PlaySE(buttonSE);
            Debug.Log("PauseButton!");
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            //ShowWithFade();
            pausePush = true;
        }
        else
        {
            GManager.instance.PlaySE(buttonSE);
            Debug.Log("UnPauseButton!");
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
            pausePush = false;
        }
    }

    //続行ボタンを押されたら呼ばれる
    public void ContinueButton()
    {
        Debug.Log("continue Start!");
        if (pausePush)
        {
            GManager.instance.PlaySE(buttonSE);
            Debug.Log("ContinueButton!");
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
            pausePush = false;
        }

    }

    //続行ボタンを押されたら呼ばれる
    public void EndButton()
    {
        Debug.Log("end Start!");
        if (pausePush)
        {
            GManager.instance.PlaySE(buttonSE);
            Debug.Log("EndButton!");
            SceneManager.LoadScene("Title"); //タイトル画面へ

        }
 
    }

    //表示する
    public void ShowWithFade (bool isFade = true, float FadeSpeed = 5f)
    {

    }

    //非表示にする
    public void HideWithFade (bool isFade = true, float FadeSpeed = 5f)
    {

    }

}
