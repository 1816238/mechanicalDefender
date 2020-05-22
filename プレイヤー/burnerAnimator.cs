using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnerAnimator : MonoBehaviour
{
    //アニメーター
    Animator animator;

    //プレイヤー
    player playerBoustFlag;
    public bool boustFlag;
    public GameObject player;
    void Start()
    {
        //親を読み込む
        player = transform.root.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerBoustFlag= player.GetComponent<player>();
        boustFlag = playerBoustFlag.boustFlag;
    if(boustFlag==true)
        {
            animator.SetBool("加速状態", true);
        }
        else
        {
            animator.SetBool("加速状態", false);
        }
    }
}
