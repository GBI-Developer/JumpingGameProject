using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;

    public string StartGroundTag = "StartGround";
    public string groundTag = "Ground";
    public string platformTag = "GroundPlatform";
    public string moveFloorTag = "MoveFloor";
    public string fallFloorTag = "FallFloor";
    public string deadAreaTag = "DeadArea";
    private string onFloorType = "";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    public bool gameStart = false;

    //床のタイプをセット？
    public void SetOnFloorType(string tagName)
    {
        if (tagName == StartGroundTag){
            onFloorType = tagName;
        } else if (tagName == groundTag) {
            onFloorType = tagName;
        } else if (tagName == moveFloorTag) {
            onFloorType = tagName;
        } else if (tagName == fallFloorTag) {
            onFloorType = tagName;
        } else if (tagName == groundTag) {
            onFloorType = "";
        }
    }

    //床のタイプを返す
    public string GetOnFloorType()
    {
        return onFloorType;
    }

    //接地判定を返すメソッド
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }
        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetOnFloorType(collision.tag);
        // ゲームスタート合図の床
        if (collision.tag == StartGroundTag)
        {
            gameStart = true;
        }

        if (collision.tag == groundTag || collision.tag == StartGroundTag)
        {
            isGroundEnter = true;
        }
        else if (checkPlatformGroud && (collision.tag == platformTag
             || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundEnter = true;
        }
        else if (collision.tag == deadAreaTag)
        {
            
            Debug.Log("type:" + deadAreaTag);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == StartGroundTag)
        {
            isGroundStay = true;
        }
        else if (checkPlatformGroud && (collision.tag == platformTag
             || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == StartGroundTag)
        {
            isGroundExit = true;
        }
        else if (checkPlatformGroud && (collision.tag == platformTag
             || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundExit = true;
        }
    }
}
