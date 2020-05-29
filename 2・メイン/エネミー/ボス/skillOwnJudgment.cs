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


    void Start()
    {
        skillOwnJudgmentFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(skillOwnJudgmentFlag==true)
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
        if (c.gameObject.tag == "Player")
        {
            skillOwnJudgmentFlag = true;
            skillOwnJudgmentActionFlag= true;
        }

    }


}
