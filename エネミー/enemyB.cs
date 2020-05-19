using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyB : MonoBehaviour
{
    //アニメーター
    Animator animator;
    //弾の格納
    public GameObject enemyBullet;
    public Transform enemyBulletStartPos;
    //オーディオソース
    AudioSource audioSource;
    //ナビメッシュ
    NavMeshAgent navMesh;
    enemyRange range;

    public float life = 20;//ライフ
    public float lifeMax = 20;

    public bool moveFlag;//移動用のフラグ
    public bool atackFlag;//攻撃時のフラグ
    public bool deathFlag;//死亡時のフラグ

    public GameObject target;//ターゲットオブジェクト格納箇所
    public bool targetChangeFlag;
    public GameObject atackRange;//ターゲットとの距離での判定

    public float atackTime;//攻撃判定時の時間
    public float atackTimeMax = 5.0f;//攻撃判定時を初期化する時間

    public string playerBulletTag = "playerBullet";//プレイヤーの弾丸判別
    public string enemyBulletTag;//エネミーの弾丸判別

    public GameObject explosion;//爆発のオブジェクト
    public bool explosionFlag;//１回のみ爆発させるためのフラグ
    public Transform explosionPos;//爆発の発生個所
    public AudioClip explosionSound;//爆発のサウンド

    public GameObject hitExplosion;//被弾爆発のオブジェクト
    public Transform hitExplosionPos;//被弾爆発の発生個所
    public AudioClip hitExplosionSound;//被弾爆発のサウンド


    void Start()
    {
        atackFlag = false;//攻撃判定初期化
        deathFlag = false;//死亡判定初期化
        targetChangeFlag=false;
        target = GameObject.FindGameObjectWithTag("Tower");//ターゲットをTowerのタグに設定
        atackRange = transform.GetChild(2).gameObject;
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.destination = target.transform.position;

        range = atackRange.GetComponent<enemyRange>();
        atackFlag = range.atackFlag;



        //ナビメッシュでの移動
        navMesh.destination = target.transform.position;
        /////////////////////////////////////////////////////
        //攻撃処理
        /////////////////////////////////////////////////////
        if (atackFlag == true && deathFlag == false)
        {
            navMesh.speed = 0;
            atackTime++;
            if (atackTime > atackTimeMax)
            {
                BuliteFire();
                atackTime = 0;
            }
            animator.SetBool("攻撃", true);
        }
        /////////////////////////////////////////////////////
        //攻撃しない状態の処理
        /////////////////////////////////////////////////////
        if (atackFlag == false)
        {
            animator.SetBool("攻撃", false);
            if (targetChangeFlag == false)
            {
                navMesh.speed = 5;
            }
            else
            {
                navMesh.speed = 10;
            }
        }
        /////////////////////////////////////////////////////
        //ターゲット変更の処理
        /////////////////////////////////////////////////////
        if (life<lifeMax)
        {
            if (targetChangeFlag == false)
            {
                target = GameObject.FindGameObjectWithTag("Player");
                navMesh.speed = 10;
                targetChangeFlag = true;
            }

        }






        /////////////////////////////////////////////////////
        //死亡処理
        /////////////////////////////////////////////////////
        if (life <= 0)
        {
            deathFlag = true;
        }
        if (deathFlag == true)
        {
            GetComponent<BoxCollider>().enabled = false;
            if (explosionFlag == false)
            {
                audioSource.PlayOneShot(explosionSound);
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                explosionFlag = true;
            }
            atackTime = atackTimeMax;
            navMesh.speed = 0;
            animator.SetBool("死亡", true);
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
        GameObject.Instantiate(enemyBullet, enemyBulletStartPos.position, enemyBulletStartPos.rotation);
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == playerBulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 1;
            if (life == 0)
            {
                deathFlag = true;
            }
        }
        //if (c.tag == BUlletBGTag)
        //{
        //    FindObjectOfType<Score>().AddPoint(30);
        //    audioSource.PlayOneShot(damegeSound);
        //    Life -= 10;
        //}
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

