
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    Rigidbody re;
    Vector3 velocity;

    AudioSource audioSource;
    //アニメーター
    //Animator animator;
    //キャラクターコントローラー
    CharacterController controller;
    //移動速度
    public float speedX;
    public float speedZ;
    public float speedDf = 10.0f;
    public float speedMax = 20.0f;
    public float speedUpDown = 0.0f;
    public bool Boust = false;
    public GameObject ptcl;
    public GameObject ptclBoust;

    //プレイヤーHP/EN
    public float Life = 1000;
    //public Text LifeText;
    public Slider LifeSlider;
    public float energy = 1000;
    public float energyMax = 1000;
    public Slider energySlider;

    //public GameObject LifeText = null;

    //移動状態のフラグ
    public bool moveFlag = false;

    //ショット変数
    public GameObject LeftBullet;
    public GameObject RightBullet;
    public GameObject ShoulderBullet;

    //右手実弾変数
    public float bulletAmmo = 50;
    public float bulletAmmoCount = 0;
    public float bulletAmmoCountMax = 300;
    public float bulletAmmoMax = 50;
    public bool bulletReload = false;
    public Text bulletAmmoText;
    public GameObject Reload;
    public Slider ReloadSlider;
    public bool oneActionReloadFlag = false;
    public AudioClip bulletSound;

    //肩実弾変数
    public float bulletShoulderAmoo = 100;
    public float bulletShoulderAmmoCount = 0;
    public float bulletShoulderAmmoCountMax = 300;
    public float bulletShoulderAmmoMax = 100;
    public bool bulletShoulderReload = false;
    public Text bulletShoulderAmmoText;
    public GameObject ShoulderReload;
    public Slider ShoulderReloadSlider;
    public bool oneActionShoulderReloadFlag = false;
    public AudioClip bulleShouldertSound;

    //シールド変数
    public GameObject shiled;

    //エネルギー弾変数
    public float bulletEnerugy = 100;
    public float bulletEnerugyMax = 100;
    public AudioClip bulletEnerugySound;
    public bool bulletEnerugyBoust = false;

    //1回のみ読み込みフラグ

    public bool oneActionFlag = false;
    //Heat変数
    public GameObject HeatText;
    public AudioClip HeatSound;

    //弾発生箇所
    public Transform BulletStartPosLift;
    public Transform BulletStartPosRight;
    public Transform BulletStartPosShoulderLift;
    public Transform BulletStartPosShoulderRight;
   
    //　メインカメラ
    //public GameObject mainCamera;
    ////　切り替える他のカメラ
    //public GameObject sabuCamera;
    //ターゲットロックオンフラグ
    //public bool tagetoFlag = false;
    //public GameObject targetObject;
    //public int roteY;

    public static bool endFlag;
    public AudioClip DamegeSound;


    public string EnemyBulletTag = "enemyBallet";
    void Start()
    {
        controller = GetComponent<CharacterController>();
        re = GetComponent<Rigidbody>();
        speedX = 0;
        speedZ = 0;
        audioSource = GetComponent<AudioSource>();
        //energySlider = GameObject.Find("energySlider").GetComponent<Slider>();
        bulletAmmoText.text = "" + bulletAmmo;
        //LifeText.text = "" + Life;
        endFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ライフが0になったとき
        if (Life <= 0)
        {
            endFlag = true;
            Invoke("ChangeScene", 3.0F);
        }
        //移動関数
        move();
        
        /////////////////////////////////////////////////////
        //攻撃遠処理
        /////////////////////////////////////////////////////
        if (Input.GetAxis("RT") == 1)
        {
            if (bulletReload == false)
            {
                bulletAmmo--;
                audioSource.PlayOneShot(bulletSound);
                if (bulletAmmo < 1)
                {
                    bulletReload = true;
                }
                FireRight();
            }
        }

        if (bulletReload == true)
        {
            bulletAmmoCount++;
            if (bulletAmmoCount > bulletAmmoCountMax)
            {
                bulletAmmoCount = 0;
                bulletAmmo = bulletAmmoMax;
                bulletReload = false;
            }
        }

        if (Input.GetAxis("Y") == 1)
        {
            if (bulletShoulderReload == false)
            {
                bulletShoulderAmoo--;
                audioSource.PlayOneShot(bulleShouldertSound);
                if (bulletShoulderAmoo < 1)
                {
                    bulletShoulderReload = true;
                }
                FireShoulder();
            }
        }

        if (bulletShoulderReload == true)
        {
            bulletShoulderAmmoCount++;
            if (bulletShoulderAmmoCount > bulletShoulderAmmoCountMax)
            {
                bulletShoulderAmmoCount = 0;
                bulletShoulderAmoo = 100;
                bulletShoulderReload = false;
            }
        }

        if (Input.GetAxis("LT") == 1)
        {
            if (bulletEnerugyBoust == false)
            {
                bulletEnerugy--;
                audioSource.PlayOneShot(bulletEnerugySound);
                //animator.SetBool("連射ショット", true);
                if (bulletEnerugy < 0)
                {
                    bulletEnerugyBoust = true;
                }
                FireLift();
            }
        }
        else
        {
            if (Boust == false)
            {
                if (bulletEnerugy <= bulletEnerugyMax)
                {
                    bulletEnerugy++;
                }
                if (bulletEnerugy >= bulletEnerugyMax)
                {
                    bulletEnerugyBoust = false;
                }
            }
        }
        
        /////////////////////////////////////////////////////
        //シールド発生処理
        /////////////////////////////////////////////////////
        if (Input.GetAxis("X") == 1)
        {
            if (bulletEnerugyBoust == false)
            {
                bulletEnerugy -= 2;
                shiled.SetActive(true);
                if (bulletEnerugy < 0)
                {
                    shiled.SetActive(false);
                    bulletEnerugyBoust = true;
                }
            }
        }
        else
        {
            shiled.SetActive(false);
        }

        Heat();
        bulletReloadText();
        bulletShoulderReloadText();
        //残りHPの更新
        //LifeText.text = "" + Life;
        //エネルギー残量の更新
        energySlider.value = bulletEnerugy;
        //右手残弾表示更新
        bulletAmmoText.text = "" + bulletAmmo;
        //肩残弾表示更新
        bulletShoulderAmmoText.text = "" + bulletShoulderAmoo;
        ReloadSlider.value = bulletAmmoCount;
        ShoulderReloadSlider.value = bulletShoulderAmmoCount;
    }


   //旋回処理
    private void FixedUpdate()
    {
        if (Input.GetAxis("rotationX") == -1) TarnToLeft();
        if (Input.GetAxis("rotationX") == 1) TarnToRight();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------旋回処理-----------------------------------------------------------///
    //右旋回//////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void TarnToLeft()
    {
        transform.Rotate(0, Input.GetAxis("Vertical") - 3, 0);
    }
    //左旋回//////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void TarnToRight()
    {
        transform.Rotate(0, Input.GetAxis("Vertical") + 3, 0);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------ショット処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //弾丸の右手生成処理//////////////////////////////////////////////////////////////////////////////////////////////
    void FireRight()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutRight());
    }
    //右手弾丸を作成//////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator CreateBlleutRight()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(RightBullet, BulletStartPosRight.position, BulletStartPosRight.rotation);
        yield return null;
    }
    //弾丸の左手生成処理 /////////////////////////////////////////////////////////////////////////////////////////////
    void FireLift()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutLift());
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //左手弾丸を作成
    IEnumerator CreateBlleutLift()
    {
        GameObject.Instantiate(LeftBullet, BulletStartPosLift.position, BulletStartPosLift.rotation); ;
        yield return null;
    }
    //肩武器の右手生成処理//////////////////////////////////////////////////////////////////////////////////////////////
    void FireShoulder()
    {
        //弾丸の作成ルーチンはコルーチンを使用
        StartCoroutine(this.CreateBlleutShoulder());
    }
    //肩武器弾丸を作成//////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator CreateBlleutShoulder()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(ShoulderBullet, BulletStartPosShoulderRight.position, BulletStartPosShoulderRight.rotation);
        GameObject.Instantiate(ShoulderBullet, BulletStartPosShoulderLift.position, BulletStartPosShoulderLift.rotation);
        yield return null;
    }
    //右手Reload文字表示//////////////////////////////////////////////////////////////////////////////////////////////////
    void bulletReloadText()
    {
        //ヒート文字表示
        if (bulletReload == true)
        {
            Reload.SetActive(true);
        }
        else
        {
            Reload.SetActive(false);
        }
    }
    //肩Reload文字表示//////////////////////////////////////////////////////////////////////////////////////////////////
    void bulletShoulderReloadText()
    {
        //ヒート文字表示
        if (bulletShoulderReload == true)
        {
            if (oneActionShoulderReloadFlag == false)
            {
                ShoulderReload.SetActive(!ShoulderReload.activeSelf);
                oneActionShoulderReloadFlag = true;
            }
        }
        else
        {
            if (oneActionShoulderReloadFlag == true)
            {
                ShoulderReload.SetActive(!ShoulderReload.activeSelf);
                oneActionShoulderReloadFlag = false;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //ヒート表示
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Heat()
    {
        //ヒート文字表示
        if (bulletEnerugyBoust == true)
        {
            if (oneActionFlag == false)
            {

                HeatText.SetActive(!HeatText.activeSelf);
                oneActionFlag = true;
            }
        }
        else
        {
            if (oneActionFlag == true)
            {
                HeatText.SetActive(!HeatText.activeSelf);
                oneActionFlag = false;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == EnemyBulletTag)
        {
            Life -= 10;
            audioSource.PlayOneShot(DamegeSound);
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------移動処理-----------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void move()
    {
        //前移動
        if (Input.GetAxis("Vertical") <= 0)
        {
            speedZ = speedDf;
            moveFlag = true;
        }
        //後ろ移動
        if (Input.GetAxis("Vertical") >= 0)
        {
            speedZ = -speedDf;
            moveFlag = true;
        }
        //前後移動停止
        if (Input.GetAxis("Vertical") == 0)
        {
            speedZ = 0;
        }

        //左移動
        if (Input.GetAxis("Horizontal") >= 0)
        {
            speedX = speedDf;
            moveFlag = true;
        }
        //右移動
        if (Input.GetAxis("Horizontal") <= 0)
        {
            speedX = -speedDf;
            moveFlag = true;
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            speedX = 0;
        }

        if (Input.GetAxis("Horizontal") == 0&& Input.GetAxis("Vertical") == 0)
        {
            moveFlag = false;
        }

        

        if (moveFlag == true)
        {
            velocity = gameObject.transform.rotation * new Vector3(speedX, 0, speedZ);
            re.velocity = transform.forward + velocity;
        }

        //加速処理
        //if (Input.GetAxis("Boust") == 1)
        //{
        //    if (bulletEnerugyBoust == false)
        //    {
        //        Boust = true;
        //        ptcl.SetActive(false);
        //        ptclBoust.SetActive(true);
        //        bulletEnerugy -= 0.2f;
        //        if (bulletEnerugy < 0)
        //        {
        //            speedX = speedDf;
        //            speedZ = speedDf;
        //            bulletEnerugyBoust = true;
        //        }
        //        speed = speedMax;
        //    }
        //}
        //else
        //{
        //    Boust = false;
        //    ptcl.SetActive(true);
        //    ptclBoust.SetActive(false);
        //    speed = speedDf;
        //}
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}

