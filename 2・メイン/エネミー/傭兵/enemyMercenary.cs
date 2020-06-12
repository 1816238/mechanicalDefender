using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyMercenary : MonoBehaviour
{
    Rigidbody re;
    Vector3 velocity;

    public GameObject player;
    private Transform targetObj;

    //弾丸オブジェクト
    public GameObject rightBullet;
    public GameObject liftBullet;

    //弾発生箇所
    public Transform bulletStartPosLift;
    public Transform bulletStartPosRight;
    public Transform bulletStartPosShoulderLift;
    public Transform bulletStartPosShoulderRight;

    //移動速度
    private float speedX;
    private float speedZ;
    private float speedDf = 10.0f;
    private float speedMax = 20.0f;
    private float speedUpDown = 0.0f;

    //移動状態のフラグ
    public bool moveFlag = false;
    public bool boustFlag = false;

    public float moveLeftRight;
    public float moveFrontBack;

    public float moveTime;
    public float moveTimeMax;

    public float atackTime;
    public float atackTimeMax;
    public bool atackFlag;

    public int atackTipe;
    public float waitTime;
    public float waitTimeMax;
   

    //public Vector3 distance;
    [SerializeField]
    private Text distanceUI;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//ターゲットをTowerのタグに設定
        targetObj = player.transform;
        re = GetComponent<Rigidbody>();
        speedX = 0;
        atackTipe = 1;
    }
    void Update()
    {
        move();
        atack();
       
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------移動処理-----------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void move()
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
    void atack()
    {
        //攻撃変更時間を増加
        atackTime += Time.deltaTime;
        //攻撃方法を時間でランダム選択変更
        if (atackTime>atackTimeMax)
        {
           atackTipe = Random.Range(1,4);
           atackTime = 0;
        }

        if(atackFlag)
        {
            //atackTipe += Time.deltaTime;
            waitTime += Time.deltaTime;

            switch (atackTipe)
            {
                case 1:
                    if (waitTime > waitTimeMax)
                    {
                        FireRight();
                        waitTime = 0;
                    }
                    break;
                  
                case 2:
                    if (waitTime > waitTimeMax / 10)
                    {
                        FireLift();
                        waitTime = 0;
                    }
                    break;

                case 3:
                    if (waitTime > waitTimeMax)
                    {
                        FireRight();
                        FireLift();
                        waitTime = 0;
                    }
                    break;

            }



            //if (atackMode.Length==1)
            //{
            //    if (atackTipe > atackTipeMax)
            //    {
            //        FireRight();
            //        atackTipe = 0;
            //    }
            //}
            //if (atackMode.Length == 2)
            //{
            //    if (atackTipe > atackTipeMax)
            //    {
            //        FireRight();
            //        atackTipe = 0;
            //    }
            //}

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
}

