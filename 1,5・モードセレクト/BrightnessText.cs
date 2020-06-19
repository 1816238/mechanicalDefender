using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessText: MonoBehaviour
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

    //下の文字のみの場合はフラグをtrueに設定
    public bool ExplanationMode;
    void Start()
    {
        //下の説明文の場合明るさを０に
        if (ExplanationMode)
        {
            darkA = 0;
        }
        modeSelectManager = GameObject.Find("ModeSelectManager");
        //画像の色を所得
        r = GetComponent<Text>().color.r;
        g = GetComponent<Text>().color.g;
        b = GetComponent<Text>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        MSMscript = modeSelectManager.GetComponent<ModeSelectManager>();
        scenesNumber = MSMscript.scenesNumber;

        //シーン番号と対応シーン番号が同じときのみ明るさを1.0に
        if (scenesNumber == selectScenesNumber)
        {
            GetComponent<Text>().color = new Color(r, g, b, brightA);
        }
        else//違う場合は0.5に
        {
            GetComponent<Text>().color = new Color(r, g, b, darkA);
        }
    }
}
