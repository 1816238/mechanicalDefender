using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal : MonoBehaviour
{

    AudioSource audioSource;
    public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == playerTag)
        {

            Destroy(gameObject);
        }


    }
}
