using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHPUI : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");//ターゲットをTowerのタグに設定
    }
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
