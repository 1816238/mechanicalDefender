using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillOwnJudgment : MonoBehaviour
{


    //攻撃範囲に入ったかのフラグ
    public bool skillOwnJudgmentFlag;
    public bool skillOwnJudgmentActionFlag;

    //判定リセットまでの時間
    public float judgmentTime;
    public float judgmentTimeMax=2.0f;

    public bool targetChangeFlag;
    public GameObject enemyBossObject;
    enemyBoss boss;

    void Start()
    {
        enemyBossObject = transform.root.gameObject;
        skillOwnJudgmentFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        boss = enemyBossObject.GetComponent<enemyBoss>();
        targetChangeFlag = boss.targetChangeFlag;
        if (skillOwnJudgmentFlag==true)
        {
            judgmentTime +=Time.deltaTime;
            if(judgmentTime>judgmentTimeMax)
            {
                skillOwnJudgmentFlag = false;
                skillOwnJudgmentActionFlag= false;
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
            if (c.gameObject.tag == "Player" )
            {
                skillOwnJudgmentFlag = true;
                skillOwnJudgmentActionFlag = true;
            }
        }
        else
        {
            if ( c.gameObject.tag == "Tower")
            {
                skillOwnJudgmentFlag = true;
                skillOwnJudgmentActionFlag = true;
            }
        }

    }


}
