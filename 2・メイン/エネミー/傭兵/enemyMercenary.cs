using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMercenary : MonoBehaviour
{
    Rigidbody re;
    Vector3 velocity;

    public GameObject player;
    private Transform targetObj;

    //オーディオソース
    AudioSource audioSource;

    //生存中のフラグ
    public bool flag;
    public float life = 1000;

    //爆発
    public GameObject explosion;//爆発オブジェクト
    public Transform explosionPos;//爆発オブジェクト発生個所
    public AudioClip explosionSound;
    public GameObject hitExplosion;//被弾爆発オブジェクト
    public AudioClip hitExplosionSound;
    public float explosionWaitTime;//爆発発生の処理を増やしすぎないための時間
    public bool hitExplosionFlag;
    public float hitExplosionInterva;//被弾爆発を生成させる間隔
    public float hitExplosionIntervaMax=1;//被弾爆発を生成させる間隔最大値

    //死亡時のフラグ
    public bool explosionFlag;
    private bool oneExplosionFlag;
    //public float explosionWaitTime;


    //弾丸オブジェクト
    public GameObject rightBullet;
    public GameObject liftBullet;
    public GameObject shoulderBullet;

    //弾発生箇所
    public Transform bulletStartPosLift;
    public Transform bulletStartPosRight;
    public Transform bulletStartPosShoulderLift;
    public Transform bulletStartPosShoulderRight;

    //移動速度
    private float speedX;
    private float speedZ;
    public float speedDf = 40.0f;
    public float speedMax = 60.0f;
    private float speedUpDown = 0.0f;

    //移動状態のフラグ
    private bool moveFlag = false;
    private bool boustFlag = false;
    //移動方向を決める数値
    private float moveLeftRight;
    private float moveFrontBack;
    //移動方向変更までの時間
    private float moveTime;
    private float moveTimeMax;
    //攻撃方法変更までの時間
    public float atackTime;
    public float atackTimeMax=5;
    public float atackTimeAdjustment1=1;
    public float atackTimeAdjustment2= 1;
    public float atackTimeAdjustment3= 1;
    //攻撃時のサウンド
    public AudioClip atackType1;
    public GameObject atackType2;
    public AudioClip atackType3;
    //攻撃中のフラグ
    public bool atackFlag;
    //攻撃の種類
    public int atackType;
    //待機時間：待機時間＞最大値を超えた時攻撃発生
    public float waitTime;
    private float waitTimeMax=5;
    


    public string power1BulletTag = "BulletPower1";//弾丸判別
    public string power2BulletTag = "BulletPower2";//弾丸判別
    public string power3BulletTag = "BulletPower3";//弾丸判別
    public string power4BulletTag = "BulletPower4";//弾丸判別
    public string power5BulletTag = "BulletPower5";//弾丸判別



    //public Vector3 distance;
    [SerializeField]
    private Text distanceUI;
    void Start()
    {
        flag = true;
        explosionFlag = false;
        oneExplosionFlag = false;
        player = GameObject.FindGameObjectWithTag("Player");//ターゲットをTowerのタグに設定
        targetObj = player.transform;
        re = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        speedX = 0;
        atackType = 1;
    }
    void Update()
    {
        if (flag)
        {
            Move();
            Atack();
            HitExplosion();
            if(life<0)
            {
                flag = false;
            }
        }
        else
        {
            Explosion();
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------移動処理-----------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Move()
    {
     
        moveTime += Time.deltaTime;
        if (moveTime > moveTimeMax)
        {
            moveFrontBack = Random.Range(0, 2);
            moveLeftRight = Random.Range(0, 2);
            moveTimeMax = Random.Range(0, 3);
            moveTime = 0;
        }
        transform.LookAt(player.transform);
        //前移動
        if (moveFrontBack==1)
        {
            if (boustFlag == true)
            {
                speedZ = speedMax;
            }
            else
            {
                speedZ = speedDf;
            }
            //shoulderRightBurner.SetActive(true);
            //shoulderLiftBurner.SetActive(true);
            moveFlag = true;
        }

        //後ろ移動
        if (moveFrontBack == 2)
        {
            if (boustFlag == true)
            {
                speedZ = -speedMax;
            }
            else
            {
                speedZ = -speedDf;
            }

            moveFlag = true;
        }
        //前後移動停止
        if (moveFrontBack==0)
        {
            speedZ = 0;
            //shoulderRightBurner.SetActive(false);
            //shoulderLiftBurner.SetActive(false);
        }

        //左移動
        if (moveLeftRight == 1)
        {
            if (boustFlag == true)
            {
                speedX = speedMax;
            }
            else
            {
                speedX = speedDf;
            }
            //legLiftBurner.SetActive(true);
            moveFlag = true;
        }

        //右移動
        if (moveLeftRight==2)
        {
            if (boustFlag == true)
            {
                speedX = -speedMax;
            }
            else
            {
                speedX = -speedDf;
            }
            //legRightBurner.SetActive(true);
            moveFlag = true;
        }

        if (moveLeftRight==0)
        {
            speedX = 0;
            //legLiftBurner.SetActive(false);
            //legRightBurner.SetActive(false);
        }

        if (moveFrontBack==0&&moveLeftRight==0)
        {
            moveFlag = false;
        }

        if (moveFlag == true)
        {
            velocity = gameObject.transform.rotation * new Vector3(speedX, 0, speedZ);
            re.velocity = transform.forward + velocity;
        }
        //プレイヤーとの距離を図る
         var distance =Vector3.Distance(transform.position, targetObj.position);
        if(distance<8)
        {
            moveFrontBack = 2;
        }
        if (distanceUI != null)
        {
            distanceUI.text = distance.ToString("0.00m");
        }
        else
        {
            Debug.Log(distance.ToString("0.00m"));
        }


    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------攻撃処理-----------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Atack()
    {
        //攻撃変更時間を増加
        atackTime += Time.deltaTime;
        //攻撃方法を時間でランダム選択変更
        if (atackTime>atackTimeMax)
        {
            atackType2.SetActive(false);
            atackType = Random.Range(1,4);
            
           atackTime = 0;
        }

        if(atackFlag)
        {
            //atackType += Time.deltaTime;
            waitTime += Time.deltaTime;//攻撃の待機時間を増加

            switch (atackType)
            {
                case 1://待機時間＞再大待機時間/攻撃種類ごとの調整数値
                    if (waitTime > waitTimeMax/ atackTimeAdjustment1)
                    {
                        FireRight();
                        audioSource.PlayOneShot(atackType1);
                        waitTime = 0;
                    }
                    break;
                  
                case 2:
                    if (waitTime > waitTimeMax / atackTimeAdjustment2)
                    {
                        FireLift();
                        atackType2.SetActive(true);
                        waitTime = 0;
                    }
                    break;

                case 3:
                    if (waitTime > waitTimeMax/ atackTimeAdjustment3)
                    {
                        FireShoulder();
                        audioSource.PlayOneShot(atackType3);
                        waitTime = 0;
                    }
                    break;

            }


        }
        
    }
    //弾丸の右手生成処理//////////////////////////////////////////////////////////////////////////////////////////////
    void FireRight()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutRight());
    }
    //右手弾丸を作成//////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator CreateBlleutRight()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(rightBullet, bulletStartPosRight.position, bulletStartPosRight.rotation);
        yield return null;
    }
    //弾丸の右手生成処理//////////////////////////////////////////////////////////////////////////////////////////////
    void FireLift()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutLift());
    }
    //右手弾丸を作成//////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator CreateBlleutLift()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(liftBullet, bulletStartPosLift.position, bulletStartPosLift.rotation);
        yield return null;
    }
    //弾丸の肩生成処理//////////////////////////////////////////////////////////////////////////////////////////////
    void FireShoulder()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutShoulder());
    }
    //肩弾丸を作成//////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator CreateBlleutShoulder()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(shoulderBullet, bulletStartPosShoulderLift.position, bulletStartPosShoulderLift.rotation);
        GameObject.Instantiate(shoulderBullet, bulletStartPosShoulderRight.position, bulletStartPosShoulderRight.rotation);

        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------被弾処理-----------------------------------------------------------///
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
    ///-----------------------------------------被弾爆発生成処理---------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void HitExplosion()
    {
        if (hitExplosionFlag == true && hitExplosionInterva <= 0)
        {
            audioSource.PlayOneShot(hitExplosionSound);
            GameObject.Instantiate(hitExplosion, explosionPos.position, explosionPos.rotation);
            hitExplosionInterva = hitExplosionIntervaMax;
        }
        if (hitExplosionInterva > 0)
        {
            hitExplosionInterva -= Time.deltaTime;
            hitExplosionFlag = false;
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------死亡処理---------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Explosion()
    {
        if(oneExplosionFlag==false)
        {
            GameObject.Instantiate(explosion, explosionPos.position, explosionPos.rotation);
            audioSource.PlayOneShot(explosionSound);
            oneExplosionFlag = true;
        }
        Destroy(gameObject,3);
    }


}

