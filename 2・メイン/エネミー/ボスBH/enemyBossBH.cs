using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class enemyBossBH : MonoBehaviour
{

    //アニメーター
    Animator animator;
    //ターゲットオブジェクト格納箇所
    public GameObject target;
    //オーディオソース
    AudioSource audioSource;
    //移動用ナビメッシュ
    NavMeshAgent navMesh;

    //待機時間
    public bool waitFlag;
    public float waitTime;
    public float waitTimeMax = 2.0f;

    //生存フラグ
    public bool flag = true;
    //HP
    public float life = 100;
    public float lifeMax = 100;
    public Slider lifeSlider;

    //攻撃フラグのリセット
    public bool atackFlag;
    public float atackSkill;
    public float atackTimeOut = 2.0f;
    public float atackTime;

    //攻撃判定
    atackJudgmentBH atackJudgmentBH;
    public GameObject atackObject;

    //ダメージ音
    public AudioClip damegeSound;

    //爆発時
    public bool explosionflag = false;
    public GameObject explosion;
    public Transform explosionPos;
    public AudioClip explosionSound;

    //hit時の小爆発
    public GameObject hitExplosion;
    public Transform hitExplosionPos;
    public AudioClip hitExplosionSound;//被弾爆発のサウンド
    public float hitExplosionInterva;//被弾爆発を生成させる間隔
    public float hitExplosionIntervaMax;//被弾爆発を生成させる間隔最大値
    public bool hitExplosionFlag;

    //弾丸判別
    public string power1BulletTag = "BulletPower1";//弾丸判別
    public string power2BulletTag = "BulletPower2";//弾丸判別
    public string power3BulletTag = "BulletPower3";//弾丸判別
    public string power4BulletTag = "BulletPower4";//弾丸判別
    public string power5BulletTag = "BulletPower5";//弾丸判別


    // Start is called before the first frame update
    void Start()
    {
        flag = true;
        atackFlag = false;
        waitFlag = true;
        target = GameObject.FindGameObjectWithTag("Tower");//ターゲットをTowerのタグに設定
        //攻撃判定のオブジェクトを設定
        atackObject = transform.GetChild(0).gameObject;
        navMesh = GetComponent<NavMeshAgent>();
        //distance = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        atackJudgmentBH = atackObject.GetComponent<atackJudgmentBH>();
        atackFlag = atackJudgmentBH.atackJudgmentFlag;

        //navMesh.destination = target.transform.position;
        //移動処理
        if (waitFlag==false&&life>0)
        {
            navMesh.destination = target.transform.position;
            if(atackFlag==true)
            {
                if(atackSkill==0)
                {
                    navMesh.speed = 3;
                    animator.SetTrigger("たたきつけ攻撃");
                    waitFlag =true;
                }
                if(atackSkill==1)
                {
                    navMesh.speed = 10;
                    animator.SetTrigger("回転攻撃");
                    waitFlag = true;
                }
            }
                


        }
        if (waitFlag == true)
        {
            waitTime += Time.deltaTime;
           

            if (waitTime > waitTimeMax)
            {
                atackSkill = Random.Range(0, 2);
                navMesh.speed = 10;
                waitTime = 0;
                waitFlag = false;
            }
        }




        if (life < 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            if (explosionflag == false)
            {
                audioSource.PlayOneShot(explosionSound);
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                explosionflag = true;
            }
            animator.SetBool("死亡", true);
            flag = false;
            navMesh.speed = 0;
            Invoke("Destroy", 7.0f);
        }


        lifeSlider.value = life;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == power1BulletTag)
        {
            hitExplosionFlag = true;
            life -= 1;
        }
        if (c.tag == power2BulletTag)
        {
            hitExplosionFlag = true;
            life -= 2;
        }
        if (c.tag == power3BulletTag)
        {
            hitExplosionFlag = true;
            life -= 3;
        }
        if (c.tag == power4BulletTag)
        {
            hitExplosionFlag = true;
            life -= 4;
        }
        if (c.tag == power5BulletTag)
        {
            hitExplosionFlag = true;
            life -= 5;
        }


    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------消滅処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Destroy()
    {
        Destroy(gameObject);
    }
}
