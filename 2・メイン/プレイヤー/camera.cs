using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Transform playerTransfrom;
    Vector3 adjustment;
    public bool contactFlag;
    public string stageTag = "Stage";
    public Text distanceUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransfrom = player.transform;
        adjustment.y = 5;
        adjustment.z = -10;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

        //if (contactFlag)
        //{

        //}
        //else
        //{
        //    transform.position = player.transform.position + adjustment;
        //}
        //var distance = Vector3.Distance(transform.position, playerTransfrom.position);
        //distanceUI.text = distance.ToString("0.00m");
    }
    //void OnTriggerStay(Collider c)
    //{
    //    contactFlag = true;
    //}
    //void OnTriggerExit(Collider c)
    //{
    //    contactFlag = false;
    //}
}
