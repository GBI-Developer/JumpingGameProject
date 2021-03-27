using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDelete : MonoBehaviour
{

    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;

    public string StartGroundTag = "StartGround";
    public string groundTag = "Ground";

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == StartGroundTag)
        {
            Debug.Log("collision.name::" + collision.name);
            Destroy(collision.gameObject);
        }
    }
}
