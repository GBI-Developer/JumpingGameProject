using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;
    [Header("ボタン押下時に鳴らすSE")] public AudioClip buttonSE;

     private bool firstPush = false;
     private bool goNextScene = false;

    /// <summary>
    /// スタートボタン押下
    /// </summary>
    /// <returns></returns>
     public void StartButton()
     {
          Debug.Log("StartButton!");
          if (!fade)
          {
            Debug.Log("設定が足りてないよ！");
            return;
          }
          if (!firstPush)
          {
              GManager.instance.PlaySE(buttonSE);
              Debug.Log("Go Stage Scene!");
              fade.StartFadeOut();
              firstPush = true; // ２重押下防止
          }
     }

    private void Update()
    {
        if (!fade)
        {
            Debug.Log("設定が足りてないよ！");
            return;
        }
        if (fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Stage");
            goNextScene = true;
        }
    }
}
