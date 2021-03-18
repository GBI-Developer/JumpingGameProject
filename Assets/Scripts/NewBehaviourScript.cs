using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    //SpriteRenderer targetRenderer; // 判定したいオブジェクトのrendererへの参照
    [SerializeField]
    private GameObject ParentObject;
    [Header("接地判定")] public GroundCheck ground;

    private GameObject[] ChildObject;

    public string groundTag = "Ground";


    // Start is called before the first frame update
    void Start()
    {
        //targetRenderer = GetComponent<SpriteRenderer>();
        // atargetRenderer = GetComponentsInChildren<SpriteRenderer>();
        // go = GetComponents<SpriteRenderer>();
        GetAllChildObject();
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (SpriteRenderer sRenderer in atargetRenderer) {
        //     if(sRenderer.isVisible)
        //     {
        //         // 表示されている場合の処理
        //         Debug.Log("s.name" + sRenderer.name + "画面に表示されてるよ");
        //     }
        //     else
        //     {
        //         // 表示されていない場合の処理
        //         Debug.Log("s.name" + sRenderer.name + "画面から消えたよ");
        //         Destroy(sRenderer);
        //     }
        // }
        // 開始床でない場合は終了
        if (!ground.gameStart)
        {
            Debug.Log("まだ床に接地してないよ");
            return;
        }
        foreach (GameObject child in ChildObject) {
            try {
                if (child == null)
                {
                    continue;
                }
                if (child.tag == groundTag)
                {
                    SpriteRenderer srender = child.GetComponent<SpriteRenderer>();
                    if(srender.isVisible)
                    {
                        // 表示されている場合の処理
                        Debug.Log("s.name" + srender.name + "画面に表示されてるよ");
                    }
                    else
                    {
                        // 表示されていない場合の処理
                        Debug.Log("s.name" + srender.name + "画面から消えたよ");
                         Destroy(child);
                    }
                }
            } catch (MissingReferenceException e) {
                Debug.Log("catch:" + e.ToString());
            }
        }

    }

    private void GetAllChildObject()
    {
        ChildObject = new GameObject[ParentObject.transform.childCount];

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            ChildObject[i] = ParentObject.transform.GetChild(i).gameObject;
        }
    }

}
