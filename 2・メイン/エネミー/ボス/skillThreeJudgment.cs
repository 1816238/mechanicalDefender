using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillThreeJudgment : MonoBehaviour
{
    //攻撃範囲に入ったかのフラグ
    public bool skillThreeJudgmentFlag;

    //判定リセットまでの時間
    public float judgmentTime;
    public float judgmentTimeMax = 2.0f;

    public bool targetChangeFlag;
    public GameObject enemyBossObject;
    enemyBoss boss;

    void Start()
    {
        enemyBossObject = transform.root.gameObject;
        skillThreeJudgmentFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        boss = enemyBossObject.GetComponent<enemyBoss>();
        targetChangeFlag = boss.targetChangeFlag;
        if (skillThreeJudgmentFlag == true)
        {
            judgmentTime += Time.deltaTime;
            if (judgmentTime > judgmentTimeMax)
            {
                skillThreeJudgmentFlag = false;
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
                skillThreeJudgmentFlag = true;
            }
        }
        else
        {
            if (c.gameObject.tag == "Tower")
            {
                skillThreeJudgmentFlag = true;
            }
        }
    }

}
