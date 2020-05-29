using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFadeInOut : MonoBehaviour
{
    //モードセレクトマネージャーを取得
    GameObject gameOverManager;
    GameOverManager gameOver;


    //カラー情報の取得
    float r;
    float g;
    float b;
    public float brightA;//明るさ
    public float brightAAutoSpeed = 0.1F;//明るさ
    public float darkA = 0.5f;

    public bool brightFlag;


    //明るさ変更のフラグ
    public bool brightnessFlag;

    // Start is called before the first frame update
    void Start()
    {
        gameOverManager = GameObject.Find("GameOverManager");
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
        brightFlag = false;
        brightA = 1.0F;
    }

    // Update is called once per frame
    void Update()
    {
        //TitelMabgerからシーン遷移のフラグを受け取る
        gameOver = gameOverManager.GetComponent<GameOverManager>();
        brightFlag = gameOver.brightFlag;

        //だんだん明るく
        if (brightFlag == false)
        {
            if (brightA > 0)
            {
                brightA -= brightAAutoSpeed;
                GetComponent<Image>().color = new Color(r, g, b, brightA);
            }
            if (brightA < 0)
            {
                brightA = 0;
            }
        }
        else//だんだん暗く
        {
            if (brightA < 1.0)
            {
                brightA += brightAAutoSpeed;
                GetComponent<Image>().color = new Color(r, g, b, brightA);
            }
            if (brightA > 1.0)
            {
                brightA = 1.0F;
            }
        }

    }


}
