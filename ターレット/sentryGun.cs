using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sentryGun : MonoBehaviour
{
    public float life = 100;
    public bool deathFlag = false;

    public GameObject explosion;//爆発のオブジェクト
    public bool explosionFlag;//１回のみ爆発させるためのフラグ
    public Transform explosionPos;//爆発の発生個所
    public AudioClip explosionSound;//爆発のサウンド

    public string playerBulletTag = "playerBullet";//プレイヤーの弾丸判別
    public string enemyBulletTag = "EnemyBullet";//エネミーの弾丸判別
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (deathFlag == true)
        {
            if (explosionFlag == false)
            {
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                explosionFlag = true;
            }
            Invoke("Destroy", 3.0f);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == playerBulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);

            life -= 1;
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