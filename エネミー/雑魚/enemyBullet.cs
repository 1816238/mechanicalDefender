using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    Rigidbody rb;
    public float lifeTime = 2.0f;
    public string playerTag = "Player";
    public string towerTag = "Tower";
    public string defenseweaponTag = "DefenseWeapon";
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //弾丸をForward方向に力をかける
        rb.AddRelativeForce(Vector3.forward * 1000.0f);
        //一定時間で削除
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == playerTag || c.tag == towerTag||c.tag== defenseweaponTag)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == playerTag|| coll.gameObject.tag == towerTag||coll.gameObject.tag==defenseweaponTag)
        {
            Destroy(gameObject);
        }
    }
}
