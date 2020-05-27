using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWeaponRangeA : MonoBehaviour
{
    //攻撃するかの判定
    public bool atackFlag = false;
    //public GameObject enemyA;
    //判定の時間
    public float atackTime;
    public float atackTimeOut = 2.0f;

    void Start()
    {

    }

    void Update()
    {
        /////////////////////////////////////////////////////
        //攻撃判定時処理
        /////////////////////////////////////////////////////
        if (atackFlag == true)
        {
            atackTime += Time.deltaTime;
            if (atackTime > atackTimeOut)
            {
                atackFlag = false;
                atackTime = 0;
            }
        }

    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            atackFlag = true;
        }

    }
}
