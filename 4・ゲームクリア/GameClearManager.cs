using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    AudioSource audioSource;
    GameClearFadeInOut bright;
    public AudioClip startSound;
    public bool RogInflag = false;
    public bool audioflag = false;
    public bool brightFlag = false;


    void Start()
    {
        brightFlag = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("B") == 1)
        {
            if (audioflag == false)
            {
                brightFlag = true;
                audioSource.PlayOneShot(startSound);
                audioflag = true;
            }
            Invoke("TitleScene", 2.0f);
        }


    }
    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
