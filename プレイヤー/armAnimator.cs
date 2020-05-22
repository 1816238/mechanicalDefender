using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armAnimator : MonoBehaviour
{
    //アニメーター
    Animator animator;

    //プレイヤー
    player playerCombatFlag;
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
        playerCombatFlag = player.GetComponent<player>();
        MoveFlag = playerCombatFlag.combatFlag;
        if (MoveFlag == true)
        {
            animator.SetBool("戦闘", true);
        }
        else
        {
            animator.SetBool("戦闘", false);
        }
    }
}
