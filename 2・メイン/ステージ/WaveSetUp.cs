using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSetUp : MonoBehaviour
{
    
    AudioSource audioSource;

    //エネミーオブジェクト格納
    public GameObject[] enemys;
    public int enemyNumber;//格納したエネミーに番号判別
    public int enemyWave1Number=2;//
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


    //エネミーの数
    public int enemyCount;
    public int enemyWave1CountMax;
    public int enemyWave2CountMax;
    public int enemyWave3CountMax;
    public int enemyDefeatCount;

    //ランダムポイント
    public Vector3[] enemySetPos;
    public int number;

    //ウェーブの数
    public int waveCount;//ウェーブ数をカウント
    public bool waveEnd;//終了時のフラグ
    public bool intervalFlag;//インターバルのフラグ
    public float intervalTime;//ウェーブ間の時間
    public float intervalTimeMax = 30;

    //エネミー生成カウント
    public float enemyGenerationTime;
    public float enemyGenerationTimeMax=2;
    



    //最大数表示
    //public TextAlignment 

    void Start()
    {
        intervalFlag = true;
        audioSource = GetComponent<AudioSource>();
        waveCount = 0;
        enemyGenerationTime = enemyGenerationTimeMax;
        waveEnd = false;
        audioSource.clip = intervaBGM;
        audioSource.Play();
        intervalText.SetActive(true);
    }

    void Update()
    {
        if (intervalFlag == false)
        {
            //出現地点のランダム取得
            number = Random.Range(0, enemySetPos.Length);
            //ウェーブ1
            if (waveCount == 0)
            {
                Wave1();
            }
            //ウェーブ2
            if (waveCount == 1)
            {
                Wave2();
            }
            //ウェーブ3
            if (waveCount == 2)
            {
                Wave3();
            }
        }
        else//インターバル時の処理
        {
            intervalTime += Time.deltaTime;
            if(intervalTime>intervalTimeMax)
            {
                if (waveCount==0)
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
                intervalTime = 0;
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
                enemyGenerationTime = enemyGenerationTimeMax;
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
                enemyGenerationTime = enemyGenerationTimeMax;
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
    ///-----------------------------------------WAVE2処理-----------------------------------------------------///
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
                enemyGenerationTime = enemyGenerationTimeMax;
            }
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
            }
        }
    }
}
