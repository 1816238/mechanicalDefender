using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 1000;
    public string enemyBulletTag = "EnemyBullet";
    public string enemyClawTag = "Claw";
    public bool endFlag;
    void Start()
    {
        
        life = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if(life<0)
        {
            endFlag = true;
        }
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == enemyBulletTag || c.gameObject.tag == enemyClawTag)
        {
            life -= 10;
            
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == enemyBulletTag || c.gameObject.tag == enemyClawTag)
        {
            life -= 10;
           
        }
        
    }
}
