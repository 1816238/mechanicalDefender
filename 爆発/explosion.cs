using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float life;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life=life-Time.deltaTime;
        if(life<0)
        {
            Destroy(gameObject);
        }
    }
}
