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

    public GameObject playerObject;
    public GameObject towerObject;


    public AudioClip startSound;

    
    public bool brightFlag = false;

    //終了のフラグ受け取り変数
    public bool towerEndFlag;
    public bool playerEndFlag;
    public bool waveEndFlag;



   
    void Start()
    {
        brightFlag = false;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        towerObject = GameObject.Find("タワー");
    }

    // Update is called once per frame
    void Update()
    {
        playerScript = playerObject.GetComponent<player>();
        towerScript = towerObject.GetComponent<tower>();
        playerEndFlag = playerScript.endFlag;
        towerEndFlag = towerScript.endFlag;


        //ゲームオーバーにシーン遷移
        if(towerEndFlag)
        {
            brightFlag = true;
            Invoke("GameOverScene", 2.0f);
        }
        if(playerEndFlag)
        {
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
