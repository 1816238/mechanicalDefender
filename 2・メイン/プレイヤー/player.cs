
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
    Animator animator;
    //キャラクターコントローラー
    CharacterController controller;

    //開始終了時フラグ
    public bool combatFlag;


    //移動速度
    public float speedX;
    public float speedZ;
    public float speedDf = 10.0f;
    public float speedMax = 20.0f;
    public float speedUpDown = 0.0f;
    public bool boustFlag = false;
    public GameObject ptcl;
    public GameObject ptclBoust;

    //プレイヤーHP/EN
    public float Life = 1000;
    //public Text LifeText;
    public Slider LifeSlider;
    public float energy = 1000;
    public float energyMax = 1000;
    public Slider energySlider;
    public Image energyImage;

    //public GameObject LifeText = null;

    //移動状態のフラグ
    public bool moveFlag = false;

    //移動時のバーナー表示フラグ
    public GameObject shoulderRightBurner;
    public GameObject shoulderLiftBurner;
    public GameObject legRightBurner;
    public GameObject legLiftBurner;

    //ショットオブジェクト
    public GameObject LeftBullet;
    public GameObject RightBullet;
    public GameObject ShoulderBullet;

    //弾発生箇所
    public Transform bulletStartPosLift;
    public Transform bulletStartPosRight;
    public Transform bulletStartPosShoulderLift;
    public Transform bulletStartPosShoulderRight;

    //防衛兵器オブジェクト
    public GameObject upDefenseWeapon;
    public GameObject rightDefenseWeapon;
    public GameObject downDefenseWeapon;
    public GameObject liftDefenseWeapon;

    //防衛兵器ポイント
    public int defensePoint;
    public Text defensePointText;
    public AudioClip pointSound;


    //防衛兵器生成箇所
    public Transform defenseWeaponStartPos;
    
    //1回のみ生成するための変数
    public bool cooltimeDefenseWeaponFlag;
    public float cooltime;
    public float cooltimeMax=10;
    
    //テスト数値
    public float testUpDown;
    public float testRightLift;

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

    //終了時のフラグ
    public bool endFlag;

    //ダメージ音
    public AudioClip DamegeSound;

    //タグ格納
    public string enemyBulletTag = "EnemyBullet";
    public string enemyClawTag="Claw";
    public string crystalTag = "Crystal";
    void Start()
    {
        controller = GetComponent<CharacterController>();
        re = GetComponent<Rigidbody>();
        speedX = 0;
        speedZ = 0;
        combatFlag = true;
        cooltimeDefenseWeaponFlag = false;
        audioSource = GetComponent<AudioSource>();
        //energySlider = GameObject.Find("energySlider").GetComponent<Slider>();
        bulletAmmoText.text = "" + bulletAmmo;//残弾テキストの更新
        defensePoint = 1000;
        defensePointText.text = "" + defensePoint;//ポイントテキストの更新
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
            //Invoke("ChangeScene", 3.0F);
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
            if (boustFlag == false)
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
        //防衛兵器生成処理
        /////////////////////////////////////////////////////
        if (Input.GetAxis("UpDown") == 1&&testUpDown==0)
        {
            UpCreate();
            combatFlag = true;
        }
        if (Input.GetAxis("UpDown") == -1 && testUpDown == 0)
        {
            DownCreate();
            combatFlag = false;
        }
        if (Input.GetAxis("RigthLift") == -1 && testRightLift == 0)
        {
            RightCreate();
        }
        if (Input.GetAxis("RigthLift") == 1 && testRightLift == 0)
        {
            LiftCreate();
        }

        testUpDown = Input.GetAxis("UpDown");
        testRightLift = Input.GetAxis("RigthLift");

        Heat();
        bulletReloadText();
        bulletShoulderReloadText();
        //残りHPの更新
        LifeSlider.value = Life;
        //エネルギー残量の更新
        //energySlider.value = bulletEnerugy;
        energyImage.fillAmount = bulletEnerugy/100;
        //右手残弾表示更新
        bulletAmmoText.text = "" + bulletAmmo;
        //肩残弾表示更新
        bulletShoulderAmmoText.text = "" + bulletShoulderAmoo;
        ReloadSlider.value = bulletAmmoCount;
        ShoulderReloadSlider.value = bulletShoulderAmmoCount;
        //ポイントの更新
        defensePointText.text = "" + defensePoint;

        if(Life<0)
        {
            endFlag = true;
        }
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
        GameObject.Instantiate(RightBullet, bulletStartPosRight.position, bulletStartPosRight.rotation);
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
        GameObject.Instantiate(LeftBullet, bulletStartPosLift.position, bulletStartPosLift.rotation); ;
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
        GameObject.Instantiate(ShoulderBullet, bulletStartPosShoulderRight.position, bulletStartPosShoulderRight.rotation);
        GameObject.Instantiate(ShoulderBullet, bulletStartPosShoulderLift.position, bulletStartPosShoulderLift.rotation);
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
    ///-----------------------------------------防衛兵器生成処理-------------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///上ボタンでの生成
    void UpCreate()
    {
        StartCoroutine(this.CreateUpDefenseWeapon());
    }
    IEnumerator CreateUpDefenseWeapon()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(upDefenseWeapon, defenseWeaponStartPos.position, defenseWeaponStartPos.rotation);
        yield return null;
    }
    ///下ボタンでの生成
    void DownCreate()
    {
        StartCoroutine(this.CreateDownDefenseWeapon());
    }
    IEnumerator CreateDownDefenseWeapon()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(downDefenseWeapon, defenseWeaponStartPos.position, defenseWeaponStartPos.rotation);
        yield return null;
    }
    ///右ボタンでの生成
    void RightCreate()
    {
        StartCoroutine(this.CreateRightDefenseWeapon());
    }
    IEnumerator CreateRightDefenseWeapon()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(rightDefenseWeapon, defenseWeaponStartPos.position, defenseWeaponStartPos.rotation);
        yield return null;
    }
    ///左ボタンでの生成
    void LiftCreate()
    {
        StartCoroutine(this.CreateLiftDefenseWeapon());
    }
    IEnumerator CreateLiftDefenseWeapon()///////////////////////////////////////////////////////////////////////////////////
    {
        GameObject.Instantiate(liftDefenseWeapon, defenseWeaponStartPos.position, defenseWeaponStartPos.rotation);
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == enemyBulletTag||c.gameObject.tag==enemyClawTag)
        {
            Life -= 10;
            audioSource.PlayOneShot(DamegeSound);
        }
        if (c.gameObject.tag == crystalTag)
        {
            defensePoint +=10;
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == enemyBulletTag || c.gameObject.tag == enemyClawTag)
        {
            Life -= 10;
            audioSource.PlayOneShot(DamegeSound);
        }
        if (c.gameObject.tag == crystalTag)
        {
            defensePoint += 10;
            audioSource.PlayOneShot(pointSound);
        }

    }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///-----------------------------------------移動処理-----------------------------------------------------------///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void move()
    {
        //加速処理
        if (Input.GetAxis("A") == 1)
        {
            if (bulletEnerugyBoust == false)
            {
                boustFlag = true;
                ptcl.SetActive(false);
                ptclBoust.SetActive(true);
                bulletEnerugy -= 0.2f;
                
                if (bulletEnerugy < 0)
                {
                    boustFlag = false;
                    bulletEnerugyBoust = true;
                }
            }
        }
        else
        {
            boustFlag = false;
            ptcl.SetActive(true);
            ptclBoust.SetActive(false);
        }

        if(boustFlag==true)
        {
            speedZ = speedMax;
            speedX = speedMax;
        }
        else
        {
            speedZ = speedDf;
            speedX = speedDf;
        }

        //前移動
        if (Input.GetAxis("Vertical") <= 0)
        {
            if (boustFlag == true)
            {
                speedZ = speedMax;
            }
            else
            {
                speedZ = speedDf;
            }
            shoulderRightBurner.SetActive(true);
            shoulderLiftBurner.SetActive(true);
            moveFlag = true;
        }
       
        //後ろ移動
        if (Input.GetAxis("Vertical") >= 0)
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
        if (Input.GetAxis("Vertical") == 0)
        {
            speedZ = 0;
            shoulderRightBurner.SetActive(false);
            shoulderLiftBurner.SetActive(false);
        }

        //左移動
        if (Input.GetAxis("Horizontal") >= 0)
        {
            if (boustFlag == true)
            {
                speedX = speedMax;
            }
            else
            {
                speedX = speedDf;
            }
            legLiftBurner.SetActive(true);
            moveFlag = true;
        }
       
        //右移動
        if (Input.GetAxis("Horizontal") <= 0)
        {
            if (boustFlag == true)
            {
                speedX = -speedMax;
            }
            else
            {
                speedX =-speedDf;
            }
            legRightBurner.SetActive(true);
            moveFlag = true;
        }
       
        if (Input.GetAxis("Horizontal") == 0)
        {
            speedX = 0;
            legLiftBurner.SetActive(false);
            legRightBurner.SetActive(false);
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

        
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}

