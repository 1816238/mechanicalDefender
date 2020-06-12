using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Millise : MonoBehaviour
{

    //Rigidbodyを入れる変数
    Rigidbody rigid;
    //速度
    Vector3 velocity;
    //発射するときの初期位置
    Vector3 position;
    // 加速度
    public Vector3 acceleration;
    // ターゲットをセットする
    public Transform target;
    // 着弾時間
    float period = 2f;

    // 正面とする軸を指定する
    public string Axis = "z";
    // 正面とする軸
    Vector3 frontAxis;


    void Start()
    {
        //　初期位置をposionに格納
        position = transform.position;
        // rigidbody取得
        rigid = this.GetComponent<Rigidbody>();

    }

    void Update()
    {

        acceleration = Vector3.zero;

        //ターゲットと自分自身の差
        var diff = target.position - transform.position;
        //加速度を求めてるらしい
        acceleration += (diff - velocity * period) * 2f/ (period * period);

        //加速度が一定以上だと追尾を弱くする
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        //着弾時間を徐々に減らしていく
        period -= Time.deltaTime;
        Debug.Log(period);


        //移動処理
        velocity += acceleration * Time.deltaTime;

    }

    //void FixedUpdate()
    //{
    //    // 着弾時間内はターゲットを向くようにする
    //    if (period >= 0)
    //    {
    //        // 自分とターゲットとのベクトルを計算
    //        var diff = target.position - transform.position;

    //        //どの軸を正面として進むか決める処理
    //        switch (Axis)
    //        {
    //            case "x":
    //                frontAxis = transform.right;
    //                break;

    //            case "y":
    //                frontAxis = transform.up;
    //                break;

    //            case "z":
    //                frontAxis = transform.forward;
    //                break;

    //            default:
    //                Debug.Log("Axisに「x」、「y」、「z」のいずれかを指定してください");
    //                break;
    //        }
    //        // Axisで指定した軸を正面にしてターゲットに向きを変える
    //        transform.rotation = GetRotMat(frontAxis, diff) * transform.rotation;
    //    }

    //    // 移動処理
    //    rigid.MovePosition(transform.position + velocity * Time.deltaTime);

    //}

    //// aをbに向ける四元数を返す関数
    //static Quaternion GetRotMat(Vector3 a, Vector3 b)
    //{
    //    a.Normalize();
    //    b.Normalize();

    //    float dot = Vector3.Dot(a, b);
    //    float rotateRad = Mathf.Acos(dot);

    //    if (rotateRad > 0.01)
    //    {
    //        Vector3 n = Vector3.Cross(a, b).normalized;
    //        return Quaternion.AngleAxis(Mathf.Rad2Deg * rotateRad, n);
    //    }
    //    else
    //    {
    //        return Quaternion.identity;
    //    }
    //}


    ////void Start()
    ////{

    ////}
    ////void Update()
    ////{

    ////}
}

