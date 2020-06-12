using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atackJudgmentBH : MonoBehaviour
{
    //攻撃範囲に入ったかのフラグ
    public bool atackJudgmentFlag;

    //判定リセットまでの時間
    public float judgmentTime;
    public float judgmentTimeMax = 5.0f;

    public GameObject enemyBossBHObject;
    enemyBossBH bossBH;

    void Start()
    {
        enemyBossBHObject = transform.root.gameObject;
        atackJudgmentFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        bossBH = enemyBossBHObject.GetComponent<enemyBossBH>();
        
        if (atackJudgmentFlag == true)
        {
            judgmentTime += Time.deltaTime;
            if (judgmentTime > judgmentTimeMax)
            {
                atackJudgmentFlag = false;
                judgmentTime = 0;
            }

        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///-----------------------------------------当たり判定処理-----------------------------------------------------///
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnTriggerStay(Collider c)
    {
            if (c.gameObject.tag == "Tower" || c.gameObject.tag == "DefenseWeapon")
            {
                atackJudgmentFlag = true;
                
            }
    }
}
