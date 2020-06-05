using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillTwoJudgment : MonoBehaviour
{
    //攻撃範囲に入ったかのフラグ
    public bool skillTwoJudgmentFlag;
   
    //判定リセットまでの時間
    public float judgmentTime;
    public float judgmentTimeMax = 2.0f;

    public bool targetChangeFlag;
    public GameObject enemyBossObject;
    enemyBoss boss;
    void Start()
    {
        enemyBossObject = transform.root.gameObject;
        skillTwoJudgmentFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        boss = enemyBossObject.GetComponent<enemyBoss>();
        targetChangeFlag = boss.targetChangeFlag;

        if (skillTwoJudgmentFlag == true)
        {
            judgmentTime += Time.deltaTime;
            if (judgmentTime > judgmentTimeMax)
            {
                skillTwoJudgmentFlag = false;
                judgmentTime = 0;
            }

        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnTriggerStay(Collider c)
    {
        if (targetChangeFlag)
        {
            if (c.gameObject.tag == "Player")
            {
                skillTwoJudgmentFlag = true;
            }
        }
        else
        {
            if( c.gameObject.tag == "Tower")
            {
                skillTwoJudgmentFlag = true;
            }
        }
    }
}



