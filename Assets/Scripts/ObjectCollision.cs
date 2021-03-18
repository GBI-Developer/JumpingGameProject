using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("これを踏んだ時のプレイヤーが跳ねる高さ")] public float boundHeight;

    /// <summary>
    /// このオブジェクトを踏んだかどうか
    /// </summay>
    [HideInInspector] public bool playerStepOn;

}
