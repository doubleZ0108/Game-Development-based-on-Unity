using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Transform turrentHinge;
    public Transform gunHinge;

    private double gunZDegree;
    public double maxElevationGun = 10.0f;           //炮台最大仰角
    public double maxDepressionofGun = -20.0f;       //炮台最大俯角

    public Transform muzzle;
    public Rigidbody muzzlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world");
    }

    // Update is called once per frame
    void Update()
    {
        /* 前进 后退 */
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Time.deltaTime * 1.0f, 0.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Time.deltaTime * -1.0f, 0.0f, 0.0f, Space.Self);
        }

        /* 原地旋转 (该脚本绑在Tank身上，所以全体都会跟着移动)*/
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, Time.deltaTime * -20.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, Time.deltaTime * 20.0f, 0.0f);
        }

        /* 炮塔旋转 */
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turrentHinge.Rotate(0.0f, Time.deltaTime * -20.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            turrentHinge.Rotate(0.0f, Time.deltaTime * 20.0f, 0.0f, Space.Self);
        }

        /* 炮台俯角仰角 */
        this.gunZDegree = gunHinge.localEulerAngles.z;
        if (this.gunZDegree > 180.0f)
        {
            this.gunZDegree -= 360.0f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(this.gunZDegree < this.maxElevationGun)
            {
                gunHinge.Rotate(0.0f, 0.0f, Time.deltaTime * 20.0f, Space.Self);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(this.gunZDegree > this.maxDepressionofGun)
            {
                gunHinge.Rotate(0.0f, 0.0f, Time.deltaTime * -20.0f, Space.Self);
            }
        }

        /* 发射炮弹 */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rigidbody freshMuzzle = Instantiate<Rigidbody>(muzzlePrefab);   //复制一个prefab炮弹
            freshMuzzle.transform.position = muzzle.position;               //设置位置为标定的炮口位置
            freshMuzzle.velocity = muzzle.forward * 10.0f;                  //设置发射初速度
        }
    }
}
