using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regAnimator : MonoBehaviour
{
    //アニメーター
    Animator animator;

    //プレイヤー
    player playerMoveFlag;
    public bool MoveFlag;
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
        playerMoveFlag = player.GetComponent<player>();
        MoveFlag = playerMoveFlag.moveFlag;
        if (MoveFlag == true)
        {
            animator.SetBool("移動", true);
        }
        else
        {
            animator.SetBool("移動", false);
        }
    }
}

