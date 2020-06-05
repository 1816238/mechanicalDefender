using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLock : MonoBehaviour
{

    Vector3 shotgun;
    enemyBoss boss;

    public GameObject target;
    public GameObject root;
    // Start is called before the first frame update
    void Start()
    {
        root = transform.root.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        boss = root.GetComponent<enemyBoss>();
        target = boss.target;
        //shotgun.y = Random.Range(-50, +50);
        // shotgun.y = Random.Range(-50, +50);

        transform.LookAt(target.transform);
        //transform.Rotate(shotgun);
    }
}
