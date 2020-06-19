using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraObuject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Transform playerTransfrom;
    public GameObject camera2;
    public Transform camera2Transfrom;
    public Transform CameraPos;
    public float cameraMoveSpeed;
    Vector3 adjustment;
    public bool contactFlag;
    public string stageTag = "Stage";
    public Text distanceUI;
    public Text distanceUI2;
    public Text distanceUI3;
    public LayerMask obstacleLayer;
    public float distanceHit;
    public float distance2;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("nearCamera");
        playerTransfrom = player.transform;
        camera2Transfrom = camera2.transform;
        //CameraPos = player.transform;
        cameraMoveSpeed = 2;
        distanceHit = distance2;
    }

    // Update is called once per frame
    void Update()
    {
        //var cameraPos = CameraPos.position;
        var distance = Vector3.Distance(transform.position, playerTransfrom.position);
        var distance2 = Vector3.Distance(transform.position, camera2Transfrom.position);
        distanceUI.text = distance.ToString("0.00m");
        distanceUI2.text = distance2.ToString("0.00m");

        //transform.position = Vector3.Lerp(transform.position, cameraPos, cameraMoveSpeed * Time.deltaTime);
        Debug.DrawLine(playerTransfrom.position, transform.position, Color.red, 0f, false);
        Debug.DrawLine(camera2Transfrom.position, transform.position, Color.blue, 0f, false);

        //後ろ部分がHitを検知
        RaycastHit hit;
        if (Physics.Linecast(camera2Transfrom.position, transform.position, out hit, obstacleLayer))
        {
            //transform.position = hit.point;
            distanceHit = Vector3.Distance(transform.position,hit.point);
            distanceUI3.text = distanceHit.ToString("0.00m");
            
        }
        else
        {
            distanceHit = distance2;
            distanceUI3.text = distanceHit.ToString("0.00m");
        }
        //Vector3 pos= transform.localPosition;
        //pos.z = -7 + (distance - distanceHit);
        //transform.localPosition = pos;





        //Vector3 pos = this.gameObject.transform.position;
        //pos.z = -7-(distance-distanceHit);
        //transform.position =new Vector3(pos.z,pos.y,pos.z);


        //RaycastHit hit;
        //if (Physics.Linecast(playerTransfrom.position, transform.position, out hit, obstacleLayer))
        //{
        //    transform.position = hit.point;
        //}
    }
    void OnTriggerStay(Collider c)
    {
        contactFlag = true;
    }
    void OnTriggerExit(Collider c)
    {
        contactFlag = false;
    }
}
