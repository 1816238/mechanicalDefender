using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCollder : MonoBehaviour
{

    public GameObject BossBHObject;
    //攻撃判定
    enemyBossBH enemyBossBH;
    public bool atackFlag;
   

    // Start is called before the first frame update
    void Start()
    {
        BossBHObject = transform.root.gameObject;
        atackFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyBossBH = BossBHObject.GetComponent<enemyBossBH>();
        atackFlag = enemyBossBH.atackFlag;

        if (atackFlag == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

