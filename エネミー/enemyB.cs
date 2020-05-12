using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyB : MonoBehaviour
{
    //アニメーター
    Animator animator;
    //弾の格納
    public GameObject enemyBullet;
    //オーディオソース
    AudioSource audioSource;
    //ナビメッシュ
    NavMeshAgent navMesh;


    public float Life = 20;//ライフ

    public bool moveFlag;//移動用のフラグ
    public bool atackFlag;//攻撃時のフラグ
    public bool deathFlag;//死亡時のフラグ

    public GameObject target;//ターゲットオブジェクト格納箇所
    public GameObject range;//ターゲットとの距離での判定

    public float atackTime;//攻撃判定時の時間
    public float atackTimeMax = 5.0f;//攻撃判定時を初期化する時間

    public string playerBulletTag = "playerBullet";//プレイヤーの弾丸判別
    public string enemyBulletTag;//エネミーの弾丸判別

    public GameObject explosion;//爆発のオブジェクト
    public Transform explosionPos;//爆発の発生個所
    public AudioClip explosionSound;//爆発のサウンド

    public GameObject hitExplosion;//被弾爆発のオブジェクト
    public Transform hitExplosionPos;//被弾爆発の発生個所
    public AudioClip hitExplosionSound;//被弾爆発のサウンド


    void Start()
    {
        atackFlag = false;//攻撃判定初期化
        deathFlag = false;//死亡判定初期化
        range = transform.GetChild(2).gameObject;
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.destination = target.transform.position;
    }
}
