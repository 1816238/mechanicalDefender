using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSetUp : MonoBehaviour
{

    AudioSource audioSource;

    //エネミーオブジェクト格納
    public GameObject[] enemys;
    public int enemyNumber;//格納したエネミーに番号判別
    public int enemyWave1Number = 2;//
    public int enemyWave2Number = 4;//
    public int enemyWave3Number = 5;//
    public int enemyWave3BossNumberA = 30;//
    public int enemyWave3BossNumberB = 50;//
    public int enemyWave3BossNumberC = 30;//
    public int enemyWave3BossNumberD = 90;//
    public int enemyWave3BossNumberE = 95;//
    public int enemyWave3BossNumberF = 100;//

    //BGM変更
    public AudioClip wave1BGM;
    public AudioClip wave2BGM;
    public AudioClip wave3BGM;
    public AudioClip intervaBGM;

    //UI用WAVE表示
    public GameObject intervalText;
    public GameObject wave1Text;
    public GameObject wave2Text;
    public GameObject wave3Text;
    public Text intervallTimeText;

    //エネミーの数
    public int enemyCount;
    public int enemyWave1CountMax;
    public int enemyWave2CountMax;
    public int enemyWave3CountMax;
    public int enemyDefeatCount;
    //クリア時のフラグ
    public bool clearFlag;

    //ランダムポイント
    public Vector3[] enemySetPos;
    public int number;

    //ウェーブの数
    public int waveCount;//ウェーブ数をカウント
    public bool waveEnd;//終了時のフラグ
    public bool intervalFlag;//インターバルのフラグ
    public float intervalTime = 30;//ウェーブ間の時間
    public float intervalTimeMax = 30;

    //エネミー生成カウント
    public float enemyGenerationTime;
    public float enemyGenerationWave1TimeMax = 2;
    public float enemyGenerationWave2TimeMax = 1.5f;
    public float enemyGenerationWave3TimeMax = 1;
    //合計制限時間
    private float totalTime;
    //秒の制限時間
    [SerializeField]
    private int minute;
    //分の制限時間
    [SerializeField]
    private float seconds;
    //前回のアップデート時の秒数
    private float oldSeconds;
    private Text timerText;


    //最大数表示
    //public TextAlignment 

    void Start()
    {
        intervalFlag = true;
        audioSource = GetComponent<AudioSource>();
        waveCount = 0;
        enemyGenerationTime = enemyGenerationWave1TimeMax;
        intervallTimeText.text = "" + intervalTime;
        waveEnd = false;
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        audioSource.clip = intervaBGM;
        audioSource.Play();
        intervalText.SetActive(true);
    }

    void Update()
    {
        if (intervalFlag == false)
        {
            //出現地点のランダム取得
            
            //ウェーブ1
            if (waveCount == 0)
            {
                number = Random.Range(0, enemySetPos.Length-6);
                Wave1();
            }
            //ウェーブ2
            if (waveCount == 1)
            {
                number = Random.Range(6, enemySetPos.Length);
                Wave2();
            }
            //ウェーブ3
            if (waveCount == 2)
            {
                number = Random.Range(0, enemySetPos.Length);
                Wave3();
            }
        }
        else//インターバル時の処理
        {
            //時間の更新
            TimeDown();

            if (intervalTime < 0)
            {
                if (waveCount == 0)
                {
                    intervalText.SetActive(false);
                    wave1Text.SetActive(true);
                    audioSource.clip = wave1BGM;
                }
                if (waveCount == 1)
                {
                    intervalText.SetActive(false);
                    wave2Text.SetActive(true);
                    audioSource.clip = wave2BGM;
                }
                if (waveCount == 2)
                {
                    intervalText.SetActive(false);
                    wave3Text.SetActive(true);
                    audioSource.clip = wave3BGM;
                }
                audioSource.Play();
                intervalFlag = false;
                seconds = intervalTimeMax;
            }
        }

    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------WAVE1処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Wave1()
    {
        enemyNumber = Random.Range(0, enemyWave1Number);
        if (enemyCount < enemyWave1CountMax)
        {
            enemyGenerationTime -= Time.deltaTime;
            if (enemyGenerationTime < 0)
            {
                Instantiate(enemys[enemyNumber], enemySetPos[number], Quaternion.identity);
                enemyCount++;
                enemyGenerationTime = enemyGenerationWave1TimeMax;
            }
        }
        if (enemyCount == enemyWave1CountMax)
        {
            enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyDefeatCount == 0)
            {
                wave1Text.SetActive(false);
                intervalText.SetActive(true);
                audioSource.clip = intervaBGM;
                audioSource.Play();
                waveCount++;
                intervalFlag = true;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------WAVE2処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Wave2()
    {
        enemyNumber = Random.Range(0, enemyWave2Number);
        if (enemyCount < enemyWave2CountMax)
        {
            enemyGenerationTime -= Time.deltaTime;
            if (enemyGenerationTime < 0)
            {
                Instantiate(enemys[enemyNumber], enemySetPos[number], Quaternion.identity);
                enemyCount++;
                enemyGenerationTime = enemyGenerationWave2TimeMax;
            }
        }
        if (enemyCount == enemyWave2CountMax)
        {
            enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyDefeatCount == 0)
            {
                wave2Text.SetActive(false);
                intervalText.SetActive(true);
                audioSource.clip = intervaBGM;
                audioSource.Play();
                waveCount++;
                intervalFlag = true;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------WAVE3処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Wave3()
    {
        enemyNumber = Random.Range(0, enemyWave2Number);
        if (enemyCount < enemyWave3CountMax)
        {
            enemyGenerationTime -= Time.deltaTime;
            if (enemyGenerationTime < 0)
            {
                if (enemyCount == enemyWave3BossNumberA ||
                   enemyCount == enemyWave3BossNumberB ||
                   enemyCount == enemyWave3BossNumberC ||
                   enemyCount == enemyWave3BossNumberD ||
                   enemyCount == enemyWave3BossNumberE ||
                   enemyCount == enemyWave3BossNumberE)
                {
                    enemyNumber = 4;
                }
                Instantiate(enemys[enemyNumber], enemySetPos[number], Quaternion.identity);
                enemyCount++;
                enemyGenerationTime = enemyGenerationWave3TimeMax;
            }
            if (enemyCount == enemyWave3CountMax)
            {
                enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                if (enemyDefeatCount == 0)
                {
                    wave3Text.SetActive(false);
                    intervalText.SetActive(true);
                    audioSource.clip = intervaBGM;
                    audioSource.Play();
                    waveCount++;
                    intervalFlag = true;
                    clearFlag = true;
                }
            }
        }
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///-----------------------------------------時間更新処理-----------------------------------------------------///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void TimeDown()
        {

            //トータル時間の計測
            totalTime = minute * 60 + seconds;
            totalTime -= Time.deltaTime;

            //設定
            minute = (int)totalTime / 60;
            seconds = totalTime - minute * 60;

            if ((int)seconds != (int)oldSeconds)
            {
                intervallTimeText.text = ((int)(seconds % 60)).ToString("00");
            }
            oldSeconds = seconds;
            intervalTime = totalTime;

        }
    }

