using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sentryGunHed : MonoBehaviour
{

    //弾丸・出現箇所の格納
    public GameObject sentryBullet;
    public Transform sentryBulletStartPos;

    //回転のフラグ
    public bool angleFlag;
    //攻撃のフラグ
    public bool atackFlag;
    
   //反転までの時間
    public float inversionTime;
    public float inversionTimeMax=3;

    //攻撃時の時間
    public float atackTime;
    public float atackTimeOut=100;

    //次弾丸生成までの時間
    public float bulletIntervalTime;
    public float bulletIntervalTimeMax=100;

    // Start is called before the first frame update
    void Start()
    {
        angleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atackFlag==false)
        {
            if (angleFlag)
            {
                transform.Rotate(new Vector3(0, 1, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, -1, 0));
               
            }
            

            inversionTime++;

            if (inversionTime > inversionTimeMax)
            {
                if (angleFlag)
                {
                    angleFlag = false;
                    inversionTime = 0;
                }
                else
                {
                    angleFlag = true;
                    inversionTime = 0;
                }
            }
        }
        else
        {
            bulletIntervalTime++;
            if(bulletIntervalTime>bulletIntervalTimeMax)
            {
                BuliteFire();
                bulletIntervalTime = 0;
            }
            atackTime++;
            //攻撃フラグの初期化
            if (atackTime > atackTimeOut)
            {
                atackFlag = false;
                atackTime = 0;
            }
        }
       
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------弾丸生成処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void BuliteFire()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleut());
    }
    IEnumerator CreateBlleut()
    {
        GameObject.Instantiate(sentryBullet,sentryBulletStartPos.position, sentryBulletStartPos.rotation);
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            atackFlag = true;
        }

    }



}
