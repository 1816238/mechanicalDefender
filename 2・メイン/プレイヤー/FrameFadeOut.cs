using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FrameFadeOut : MonoBehaviour
{


    //カラー情報の取得
    float r;
    float g;
    float b;
    public float brightA;//明るさ
    public float brightAAutoSpeed = 0.1F;//明るさ
    public float darkA = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
        brightA = 1.0F;
    }

    // Update is called once per frame
    void Update()
    {
        brightA -= brightAAutoSpeed;
        GetComponent<Image>().color = new Color(r, g, b, brightA);
    }
}
