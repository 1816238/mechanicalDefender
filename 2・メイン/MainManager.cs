using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    AudioSource audioSource;
    MainFadeInOut bright;
    player playerScript;
    tower towerScript;
    WaveSetUp waveSetUpScripy;

    public GameObject playerObject;
    public GameObject towerObject;
    public GameObject waveSetUpObject;


    public AudioClip startSound;

    
    public bool brightFlag = false;

    //終了のフラグ受け取り変数
    public bool towerEndFlag;
    public bool playerEndFlag;
    public bool waveEndFlag;

    //ボイス格納
    private bool gameClearVoiceFlag;
    private bool gameOverVoiceFlag;
    private bool towerHp70VoiceFlag;
    private bool towerHp50VoiceFlag;
    private bool towerHp30VoiceFlag;

    public AudioClip gameClear;
    public AudioClip gameOver;
    public AudioClip towerHp70;
    public AudioClip towerHp50;
    public AudioClip towerHp30;



    void Start()
    {
        brightFlag = false;
        gameClearVoiceFlag = false;
        gameOverVoiceFlag = false;
        towerHp30VoiceFlag = false;
        towerHp50VoiceFlag = false;
        towerHp70VoiceFlag = false;
        audioSource = GetComponent<AudioSource>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        towerObject = GameObject.Find("タワー");
        waveSetUpObject= GameObject.Find("WaveSetUp");
        
    }

    // Update is called once per frame
    void Update()
    {
        playerScript = playerObject.GetComponent<player>();
        towerScript = towerObject.GetComponent<tower>();
        waveSetUpScripy=waveSetUpObject.GetComponent<WaveSetUp>();
        playerEndFlag = playerScript.endFlag;
        towerEndFlag = towerScript.endFlag;
        waveEndFlag = waveSetUpScripy.clearFlag;

        ////////////HP低下でのボイス//////////
        if(towerScript.life<700&&towerHp70VoiceFlag==false)
        {
            audioSource.PlayOneShot(towerHp70);
            towerHp70VoiceFlag = true;
        }

        if (towerScript.life < 500 && towerHp50VoiceFlag == false)
        {
            audioSource.PlayOneShot(towerHp50);
            towerHp50VoiceFlag = true;
        }

        if (towerScript.life < 300 && towerHp30VoiceFlag == false)
        {
            audioSource.PlayOneShot(towerHp30);
            towerHp30VoiceFlag = true;
        }

        //////////ゲームクリアにシーン遷移//////////
        if(waveEndFlag)
        {
            if(gameClearVoiceFlag==false)
            {
                audioSource.PlayOneShot(gameClear);
                gameClearVoiceFlag = true;
            }
            brightFlag = true;
            Invoke("GameClearScene", 4.0f);

        }






        //////////ゲームオーバーにシーン遷移/////////
        if (towerEndFlag)
        {
            if(gameOverVoiceFlag == false)
            {
                audioSource.PlayOneShot(gameOver);
                gameOverVoiceFlag = true;
            }
            brightFlag = true;
            Invoke("GameOverScene", 2.0f);
        }
        if(playerEndFlag)
        {
            if (gameOverVoiceFlag == false)
            {
                audioSource.PlayOneShot(gameOver);
                gameOverVoiceFlag = true;
            }
            brightFlag = true;
            Invoke("GameOverScene", 2.0f);
        }


    }

    void GameClearScene()
    {
        SceneManager.LoadScene("GameClear");
    }

    void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

}
