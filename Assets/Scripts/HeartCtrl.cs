using UnityEngine;
using UnityEngine.UI;

public class HeartCtrl : MonoBehaviour
{

    private Text heartText = null;
    private int oldHeart = 0;

    // Start is called before the first frame update
    void Start()
    {
        heartText = GetComponent<Text>();
        if (GManager.instance != null) {
            heartText.text = "Heart x " + GManager.instance.heart;
        } else {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldHeart != GManager.instance.heart) {
            heartText.text = "Heart x " + GManager.instance.heart;
            oldHeart = GManager.instance.heart;

        }
    }
}
