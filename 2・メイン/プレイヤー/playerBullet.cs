using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    Rigidbody rb;
    public float lifeTime = 2.0f;
    public string enemyTag = "Enemy";
    public string stagyTag = "Stage";
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
        if (c.tag == enemyTag || c.tag == stagyTag)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == stagyTag)
        {
            Destroy(gameObject);
        }
    }
}
