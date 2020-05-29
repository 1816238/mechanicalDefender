using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomCrystal : MonoBehaviour
{
    //オブジェクト格納
    public GameObject Cryctal;
    public GameObject[] CryctalNnver;
    //生成時間
    public float time;
    public float timeMax = 10;

    //数をカウント
    public int count;
    public int countMax = 50;


    //X座標の最小値
    public float xMinPos = -5f;
    //X座標の最大値
    public float xMaxPos = 5f;
    //Z座標の最小値
    public float zMinPos = -5f;
    //Z座標の最大値
    public float zMaxPos = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = GameObject.FindGameObjectsWithTag("Crystal").Length;




        time += Time.deltaTime;
        if(time>timeMax&&count<countMax)
        {
            GameObject CryctalObject = Instantiate(Cryctal);
            CryctalObject.transform.position = GetRandomPos();

            time = 0;
        }
    }

    private Vector3 GetRandomPos()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPos, xMaxPos);
        float z = Random.Range(zMinPos, zMaxPos);

        //Vector3型のPositionを返す
        return new Vector3(x, 0, z);
    }

}
