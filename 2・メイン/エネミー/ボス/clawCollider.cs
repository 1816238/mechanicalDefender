using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clawCollider : MonoBehaviour
{
    enemyBoss enemyBossClaw;
    public bool colliderFlag;
    public GameObject enemyBossObject;
    
    void Start()
    {
        enemyBossObject = transform.root.gameObject;
        colliderFlag= false;
    }
    void Update()
    {
        enemyBossClaw = enemyBossObject.GetComponent<enemyBoss>();
        colliderFlag = enemyBossClaw.skillOneflag;
        //if ()
        if (colliderFlag == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
