using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBoss : MonoBehaviour
{
    //アニメーター
    Animator animator;
    //ターゲットオブジェクト格納箇所
    public GameObject target;
    //オーディオソース
    AudioSource audioSource;

    //エネミー射撃判定 script;

    NavMeshAgent navMesh;
    public bool Flag = true;
    public float Life = 20;

    public bool enemy_atack = false;
    public GameObject bullet;
    public AudioClip bulletSound;
    public Transform BulletStartPos;
    public GameObject distance;
    public float atackTimeOut = 2.0f;
    public float atackTime;

    public float timeMax = 5.0f;
    public float time;
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


    void Start()
    {
        enemy_atack = false;
        //target = GameObject.FindGameObjectWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        distance = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //script = distance.GetComponent<エネミー射撃判定>();
        //enemy_atack = script.enemy_distance;
        if (enemy_atack && Flag)
        {
            animator.SetBool("atack", true);
            time++;
            navMesh.speed = 0;
            if (time >= timeMax)
            {
                BuliteFire();
                time = 0;
            }
            atackTime += Time.deltaTime;
            if (atackTime > atackTimeOut)
            {
                enemy_atack = false;
                atackTime = 0;
            }

        }
        else
        {
            animator.SetBool("atack", false);
            navMesh.speed = 5;
        }
        navMesh.destination = target.transform.position;

        if (Life < 0)
        {
            if (explosionflag == false)
            {
                //FindObjectOfType<EnemySetUp>().DefeatCount(1);
                //FindObjectOfType<Score>().AddPoint(100);
                audioSource.PlayOneShot(explosionSound);
                GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
                explosionflag = true;
            }
            Flag = false;
            navMesh.speed = 0;
            time = timeMax;
            animator.SetTrigger("dead");
            Invoke("Destroy", 1.0f);
        }

        //RayTest();
    }
    void BuliteFire()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleut());
    }
    IEnumerator CreateBlleut()
    {
        GameObject.Instantiate(bullet, BulletStartPos.position, BulletStartPos.rotation); ;
        yield return null;
    }
    void OnTriggerEnter(Collider c)
    {
        //if (c.tag == BulletTag)
        //{
        //    FindObjectOfType<Score>().AddPoint(10);
        //    audioSource.PlayOneShot(damegeSound);
        //    GameObject.Instantiate(hitExplosion, hitExplosionPos.position, hitExplosionPos.rotation);
        //    Life -= 1;
        //}
        //if (c.tag == BUlletBGTag)
        //{
        //    FindObjectOfType<Score>().AddPoint(30);
        //    audioSource.PlayOneShot(damegeSound);
        //    Life -= 10;
        //}
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}

