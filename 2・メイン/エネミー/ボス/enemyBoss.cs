using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyBoss : MonoBehaviour
{
    //アニメーター
    Animator animator;

    //ターゲットオブジェクト格納箇所
    public GameObject target;
    public bool targetChangeFlag;

    //オーディオソース
    AudioSource audioSource;
    //移動用ナビメッシュ
    NavMeshAgent navMesh;

    //攻撃判定スクリプト・オブジェクト
    skillOwnJudgment skillOwn;
    public GameObject skiiOwnObject;
    skillTwoJudgment skillTwo;
    public GameObject skiiTwoObject;
    skillThreeJudgment skillThree;
    public GameObject skiiThreeObject;

    //待機時間
    public bool waitFlag;
    public float waitTime;
    public float waitTimeMax=2.0f;


    //生存フラグ
    public bool flag = true;
    //HP
    public float life = 100;
    public float lifeMax = 100;
    public Slider lifeSlider;

    //攻撃モーションの選択フラグ
    public GameObject distance;
    public bool atackFlag = false;
    public bool skillOneflag;
    public bool skillTwoflag;
    public bool skillThreeflag;
    public int runSkill;
    public int skill;

    //近距離攻撃範囲に入らなかった場合移動に変更
    public bool runFlag = false;
    public bool testFlag;

    //攻撃フラグのリセット
    public float atackTimeOut = 2.0f;
    public float atackTime;

    //攻撃B弾丸
    public GameObject shortSkillTwoBullet;//オブジェクト
    public AudioClip shortSkillTwoBulletSound;//サウンド
    public Transform shortSkillTwoBulletStartPos;//生成箇所

    //攻撃C弾丸
    public GameObject shortSkillThreeBullet;//オブジェクト
    public AudioClip shortSkillThreeBulletSound;//サウンド
    public Transform shortSkillThreeBulletRightStartPos;//生成箇所
    public Transform shortSkillThreeBulletLiftStartPos;//生成箇所
    public int shortSkillThreeCount;

    //Vector3 shotgun;
    public Quaternion shortSkillThreeQuaternion;
    public Quaternion shotgun;


    public float timeMax = 5.0f;//弾丸生成間隔最大値の設定
    public float time;//弾丸生成間隔の設定

    //タグ読み込み
    public string BulletTag = "Bullet";
    public string BUlletBGTag = "BulletBG";

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
    
    public string power1BulletTag = "BulletPower1";//弾丸判別
    public string power2BulletTag = "BulletPower2";//弾丸判別
    public string power3BulletTag = "BulletPower3";//弾丸判別
    public string power4BulletTag = "BulletPower4";//弾丸判別
    public string power5BulletTag = "BulletPower5";//弾丸判別




    void Start()
    {
        flag = true;
        atackFlag = false;
        waitFlag = true;
        targetChangeFlag = false;
        target = GameObject.FindGameObjectWithTag("Tower");//ターゲットをTowerのタグに設定
        //攻撃判定のオブジェクトを設定
        skiiOwnObject= transform.GetChild(0).gameObject;
        skiiTwoObject = transform.GetChild(1).gameObject;
        skiiThreeObject = transform.GetChild(2).gameObject;
        navMesh = GetComponent<NavMeshAgent>();
        //distance = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
            //近距離攻撃Aの判定を取得
            skillOwn = skiiOwnObject.GetComponent<skillOwnJudgment>();
            skillOneflag = skillOwn.skillOwnJudgmentFlag;


            //近距離攻撃Bの判定を取得
            skillTwo = skiiTwoObject.GetComponent<skillTwoJudgment>();
            skillTwoflag = skillTwo.skillTwoJudgmentFlag;

            //近距離攻撃Bの判定を取得
            skillThree = skiiThreeObject.GetComponent<skillThreeJudgment>();
            skillThreeflag = skillThree.skillThreeJudgmentFlag;

        

        //移動処理
        navMesh.destination = target.transform.position;
        /////////////////////////////////////////////////////
        //旧仕様
        /////////////////////////////////////////////////////
        //if (runFlag==false)
        //{

        //    animator.SetBool("走る", false);
        //    navMesh.speed = 0;
        //    if(skillOneflag==true&&waitFlag==false)
        //    {
        //        animator.SetTrigger("近接攻撃A");
        //        waitFlag = true ;
        //    }

        //    if (skillTwoflag == true && skillOneflag == false&& waitFlag == false)
        //    {
        //        animator.SetTrigger("近接攻撃B");
        //        Invoke("ShortSkillTwoBulletFire", 0.5F);
        //        waitFlag = true;
        //    }

        //    if (skillOneflag == false && skillTwoflag == false && skillThreeflag == true && waitFlag == false)
        //    {
        //        animator.SetTrigger("近接攻撃C");
        //        Invoke("ShortSkillThreeBulletFire", 1.0F);
        //        waitFlag = true;
        //    }
        //}
        //else
        //{
        //    animator.SetBool("走る", true);
        //    navMesh.speed = 10;

        //    if (skillOneflag == true && waitFlag == false)
        //    {
        //        navMesh.speed = 0;
        //        animator.SetTrigger("接近攻撃C");

        //        waitFlag = true;
        //        Invoke("RunFlagEnd", 2.0f);
        //    }

        //    if (skillTwoflag == true && waitFlag == false)
        //    {
        //        navMesh.speed = 0;
        //        animator.SetTrigger("接近攻撃B");
        //        Invoke("ShortSkillTwoBulletFire", 0.5F);
        //        waitFlag = true;
        //        Invoke("RunFlagEnd", 2.0f);
        //    }

        //    if (skillOneflag == false && skillTwoflag == false)
        //    {
        //        navMesh.speed = 10;

        //    }
        //}


        /////////////////////////////////////////////////////
        //ランダム選択仕様
        /////////////////////////////////////////////////////
        ///
        if (waitFlag == false&&life>0)
        {
            if (runFlag == false)
            {
                animator.SetBool("走る", false);
                navMesh.speed = 0;

                //if (skill == 0)
                //{
                //    animator.SetTrigger("近接攻撃A");
                //    waitFlag = true;
                //}

                if (skill == 0)
                {
                    animator.SetTrigger("近接攻撃B");
                    Invoke("ShortSkillTwoBulletFire", 0.5F);
                    waitFlag = true;
                }

                if (skill == 1)
                {
                    animator.SetTrigger("近接攻撃C");
                    Invoke("ShortSkillThreeBulletFire", 1.0F);
                    waitFlag = true;
                }
            }
            else
            {
                animator.SetBool("走る", true);
                navMesh.speed = 10;

                if (skillOneflag == true &&skill==0&& waitFlag == false)
                {
                    navMesh.speed = 0;
                    animator.SetTrigger("接近攻撃C");
                    animator.SetBool("走る", false);
                    waitFlag = true;
                    Invoke("RunFlagEnd", 2.0f);
                }

                if (skillTwoflag == true && skill == 1 && waitFlag == false)
                {
                    navMesh.speed = 0;
                    animator.SetTrigger("接近攻撃B");
                    animator.SetBool("走る", false);
                    Invoke("ShortSkillTwoBulletFire", 0.5F);
                    waitFlag = true;
                    Invoke("RunFlagEnd", 2.0f);
                }

                if (skillOneflag == false && skillTwoflag == false)
                {
                    navMesh.speed = 10;

                }
            }
        }

        /////////////////////////////////////////////////////
        //攻撃後の待機時間
        /////////////////////////////////////////////////////
        if (waitFlag==true)
        {
            waitTime += Time.deltaTime;
            //shortSkillThreeCount = 0;
            
            if (waitTime>waitTimeMax)
            {
                if(skillOneflag == false && skillTwoflag == false && skillThreeflag == false)
                { 
                    runFlag = true;
                    skill= Random.Range(0, 2);
                }
                else
                {
                    runFlag = false;
                    skill = Random.Range(0, 2);
                }
                    waitTime = 0;
                waitFlag = false;
            }
        }

        /////////////////////////////////////////////////////
        //Hp80％でターゲットをプレイヤーに変更
        /////////////////////////////////////////////////////
        if (life<lifeMax*0.8)
        {
            targetChangeFlag = true;
            target = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(target.transform);

        }
        /////////////////////////////////////////////////////
        //死亡処理
        /////////////////////////////////////////////////////
        if (life < 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            if (explosionflag == false)
            {
                //FindObjectOfType<EnemySetUp>().DefeatCount(1);
                //FindObjectOfType<Score>().AddPoint(100);
                audioSource.PlayOneShot(explosionSound);
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                explosionflag = true;
            }
            flag = false;
            navMesh.speed = 0;
            time = timeMax;
            animator.SetBool("死亡", true);
           
            Invoke("Destroy", 7.0f);
        }


        //ライフゲージの反映
        lifeSlider.value = life;
        //RayTest();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------近接攻撃B弾丸生成処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ShortSkillTwoBulletFire()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.ShortSkillTwoCreateBullet());
    }
    IEnumerator ShortSkillTwoCreateBullet()
    {
        GameObject.Instantiate(shortSkillTwoBullet, shortSkillTwoBulletStartPos.position, shortSkillTwoBulletStartPos.rotation); ;
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------近接攻撃C弾丸生成処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ShortSkillThreeBulletFire()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.ShortSkillThreeCreateBullet());
    }
    IEnumerator ShortSkillThreeCreateBullet()
    {
        shortSkillThreeQuaternion= shortSkillThreeBulletRightStartPos.rotation;

        //shotgun.x= Random.Range(-0, +1);
        
        
        for (shortSkillThreeCount=0; shortSkillThreeCount<10; shortSkillThreeCount++)
        {
            shotgun.y = Random.Range(-0.3f,0.3f);
            shortSkillThreeQuaternion.y *= shotgun.y;
            //shortSkillThreeBulletRightStartPos.transform.rotation = transform.Rotate(shotgun);
            GameObject.Instantiate(shortSkillThreeBullet, shortSkillThreeBulletRightStartPos.position, shortSkillThreeQuaternion);
            GameObject.Instantiate(shortSkillThreeBullet, shortSkillThreeBulletLiftStartPos.position, shortSkillThreeBulletLiftStartPos.rotation);
        }
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == power1BulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 1;
        }
        if (c.tag == power2BulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 2;
        }
        if (c.tag == power3BulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 3;
        }
        if (c.tag == power4BulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 4;
        }
        if (c.tag == power5BulletTag)
        {
            //FindObjectOfType<Score>().AddPoint(10);
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
            life -= 5;
        }

        
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------アニメーション中のフラグ変更用-------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void RunFlagEnd()
    {
        runFlag = false;
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}

