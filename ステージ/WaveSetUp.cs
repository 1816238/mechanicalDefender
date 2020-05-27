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




    //public GameObject enemyA2;
    //public GameObject enemyB1;
    //public GameObject enemyB2;
    //public GameObject enemyBoss;

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
    }

    void Update()
    {
        if (intervalFlag == false)
        {
            number = Random.Range(0, enemySetPos.Length);

            if (waveCount == 0)
            {
                enemyNumber= Random.Range(0, enemyWave1Number);
                if (enemyCount<enemyWave1CountMax)
                {
                    enemyGenerationTime -= Time.deltaTime;
                    if (enemyGenerationTime < 0)
                    {
                        Instantiate(enemys[enemyNumber], enemySetPos[enemyNumber], Quaternion.identity);
                        enemyCount++;
                        enemyGenerationTime = enemyGenerationTimeMax;
                    }
                }
                if (enemyCount == enemyWave1CountMax)
                {
                    enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                    if(enemyDefeatCount==0)
                    {

                        audioSource.clip = intervaBGM;
                        audioSource.Play();
                        waveCount++;
                        intervalFlag = true;
                    }
                }
            }

            if (waveCount == 1)
            {
                enemyNumber = Random.Range(0, enemyWave2Number);
                if (enemyCount < enemyWave2CountMax)
                {
                    enemyGenerationTime -= Time.deltaTime;
                    if (enemyGenerationTime < 0)
                    {
                        Instantiate(enemys[enemyNumber], enemySetPos[enemyNumber], Quaternion.identity);
                        enemyCount++;
                        enemyGenerationTime = enemyGenerationTimeMax;
                    }
                }
                if (enemyCount == enemyWave2CountMax)
                {
                    enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                    if (enemyDefeatCount == 0)
                    {
                        audioSource.clip = intervaBGM;
                        audioSource.Play();
                        waveCount++;
                        intervalFlag = true;
                    }
                }
            }

            if (waveCount == 2)
            {
                enemyNumber = Random.Range(0, enemyWave2Number);
                if (enemyCount < enemyWave3CountMax)
                {
                    enemyGenerationTime -= Time.deltaTime;
                    if (enemyGenerationTime < 0)
                    {
                        if(enemyCount==enemyWave3BossNumberA||
                           enemyCount==enemyWave3BossNumberB||
                           enemyCount==enemyWave3BossNumberC||
                           enemyCount==enemyWave3BossNumberD||
                           enemyCount==enemyWave3BossNumberE||
                           enemyCount==enemyWave3BossNumberE)
                        {
                            enemyNumber = 4;
                        }
                        Instantiate(enemys[enemyNumber], enemySetPos[enemyNumber], Quaternion.identity);
                        enemyCount++;
                        enemyGenerationTime = enemyGenerationTimeMax;
                    }
                }
                if (enemyCount == enemyWave3CountMax)
                {
                    enemyDefeatCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                    if (enemyDefeatCount == 0)
                    {
                        audioSource.clip = intervaBGM;
                        audioSource.Play();
                        waveCount++;
                        intervalFlag = true;
                    }
                }
            }



        }
        else
        {
            intervalTime += Time.deltaTime;
            if(intervalTime>intervalTimeMax)
            {
                if (waveCount==0)
                {
                    audioSource.clip = wave1BGM;
                }
                if (waveCount == 1)
                {
                    audioSource.clip = wave2BGM;
                }
                if (waveCount == 2)
                {
                    audioSource.clip = wave3BGM;
                }



                audioSource.Play();

                intervalFlag = false;
                intervalTime = 0;
            }
        }



    }
}
