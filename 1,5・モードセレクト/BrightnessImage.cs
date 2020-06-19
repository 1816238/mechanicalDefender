using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessImage : MonoBehaviour
{
    // Start is called before the first frame update

    //モードセレクトマネージャーを取得
    GameObject modeSelectManager;
    ModeSelectManager MSMscript;

    //カラー情報の取得
    float r;
    float g;
    float b;
    float brightA = 1.0f;
    float darkA = 0.5f;

    //シーン番号と対応シーン番号
    public int scenesNumber;
    public int selectScenesNumber;

    void Start()
    {
       
        modeSelectManager = GameObject.Find("ModeSelectManager");
        //画像の色を所得
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        MSMscript = modeSelectManager.GetComponent<ModeSelectManager>();
        scenesNumber = MSMscript.scenesNumber;

        //シーン番号と対応シーン番号が同じときのみ明るさを1.0に
        if (scenesNumber == selectScenesNumber)
        {
            GetComponent<Image>().color = new Color(r, g, b, brightA);
        }
        else//違う場合は0.5に
        {
            GetComponent<Image>().color = new Color(r, g, b, darkA);
        }
    }
}
