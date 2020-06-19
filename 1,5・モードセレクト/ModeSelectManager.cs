using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ModeSelectManager : MonoBehaviour
{
    // Start is called before the first frame update

    //オーディオソース
    AudioSource audioSource;
    public AudioClip CursorMoveButtion;
    public AudioClip ChangeScenesButtion;

    //シーン選択番号
    public int scenesNumber;//現在の選択番号
    public int scenesNumberMin=0;//選択番号最大値
    public int scenesNumberMax=2;//選択番号最小値

    //上下ボタンの入力数値を格納させる変数
    public float axisUpDown;
    public float axisVertical;

    public bool ChangeScenesFlag;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scenesNumber = 0;//番号初期化
        ChangeScenesFlag=false;
    }

    // Update is called once per frame
    void Update()
    {
        CursorMove();
        ChangeScenes();

    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------カーソル移動処理---------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void CursorMove()
    {
        //シーン遷移が始まってない時のみカーソル移動可
        if (ChangeScenesFlag==false)
        {
            //上ボタンorスティックを上にでカーソルを上に
            if (Input.GetAxis("UpDown") == 1 || Input.GetAxis("Vertical") < 0)
            {
                if (axisUpDown == 0 && axisVertical == 0)
                {
                    audioSource.PlayOneShot(CursorMoveButtion);
                    scenesNumber -= 1;
                }
            }
            //最小値を超えた場合最小値に変更
            if (scenesNumber == scenesNumberMin - 1)
            {
                scenesNumber = scenesNumberMin;
            }

            //下ボタンorスティックを下にでカーソルを下に
            if (Input.GetAxis("UpDown") == -1 || Input.GetAxis("Vertical") > 0)
            {
                // scenesNumber += 1;
                if (axisUpDown == 0 && axisVertical == 0)
                {
                    audioSource.PlayOneShot(CursorMoveButtion);
                    scenesNumber += 1;
                }
            }
            if (scenesNumber == scenesNumberMax + 1)
            {
                scenesNumber = scenesNumberMax;
            }
            axisUpDown = Input.GetAxis("UpDown");
            axisVertical = Input.GetAxis("Vertical");
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-------------------------------------------シーン移動処理---------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ChangeScenes()
    {
        //シーン遷移状態になってないときのみフラグ変更
        if (ChangeScenesFlag == false)
        {
            if (Input.GetAxis("B") == 1)
            {
                audioSource.PlayOneShot(ChangeScenesButtion);
                ChangeScenesFlag = true;
                Invoke("SerectScene", 2.0f);
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-------------------------------------------シーン移動処理---------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void SerectScene()
    {
        if (scenesNumber == 0)
        {
            SceneManager.LoadScene("MainDefender");
        }
        if (scenesNumber == 1)
        {
            SceneManager.LoadScene("PvE");
        }
        if (scenesNumber == 2)
        {
            SceneManager.LoadScene("PvP");
        }
    }
}
