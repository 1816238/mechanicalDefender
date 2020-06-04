using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWeaponA : MonoBehaviour
{
    public float life = 100;
    public bool deathFlag = false;

    AudioSource audioSource;

    DefenseWeaponRangeA rangeA;
    public GameObject atackRange;//ターゲットとの距離での判定

    //弾丸・出現箇所の格納
    public GameObject sentryBullet;
    public Transform sentryBulletStartPos;
    public GameObject fireEffect;

    public bool atackFlag;//攻撃時のフラグ
    public bool angleFlag;//回転のフラグ

    //反転までの時間
    public float inversionTime;
    public float inversionTimeMax = 30;

    //攻撃時の時間
    public float atackTime;
    public float atackTimeOut = 100;

    //次弾丸生成までの時間
    public float bulletIntervalTime;
    public float bulletIntervalTimeMax = 100;

    public GameObject explosion;//爆発のオブジェクト
    public bool explosionFlag;//１回のみ爆発させるためのフラグ
    public Transform explosionPos;//爆発の発生個所
    public AudioClip explosionSound;//爆発のサウンド

    public string playerBulletTag = "playerBullet";//プレイヤーの弾丸判別
    public string enemyBulletTag = "EnemyBullet";//エネミーの弾丸判別
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        atackRange = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathFlag == false)
        {
            rangeA = atackRange.GetComponent<DefenseWeaponRangeA>();
            atackFlag = rangeA.atackFlag;


            if (atackFlag == false)
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
                if (bulletIntervalTime > bulletIntervalTimeMax)
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


        if (deathFlag == true)
        {
            if (explosionFlag == false)
            {
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                audioSource.PlayOneShot(explosionSound);
                explosionFlag = true;
            }
            Invoke("Destroy", 3.0f);
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
        GameObject.Instantiate(sentryBullet, sentryBulletStartPos.position, sentryBulletStartPos.rotation);
        GameObject.Instantiate(fireEffect, sentryBulletStartPos.position, sentryBulletStartPos.rotation);
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == enemyBulletTag)
        {
            
            life -= 20;
            if (life == 0)
            {
                deathFlag = true;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------消滅処理-----------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Destroy()
    {
        Destroy(gameObject);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///------------------------------------------------------------------------------------------------------------///
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
