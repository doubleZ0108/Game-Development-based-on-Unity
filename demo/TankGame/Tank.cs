using System;
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
        TankPlayerInputHandler handler = TankPlayerInputHandler.Instance();
        handler.Axis1VerticalInputEvent += Move;
        handler.Axis1HorizontalInputEvent += Rotate;
        handler.Axis2VerticalInputEvent += RotateGun;
        handler.Axis2HorizontalInputEvent += RotateTurret;
        handler.FireInputEvent += Fire;
    }

    public void Move(float factor)
    {
        this.transform.Translate(Time.deltaTime * 1.0f * factor, 0.0f, 0.0f, Space.Self);
    }

    public void Rotate(float factor)
    {
        transform.Rotate(0.0f, Time.deltaTime * -20.0f * factor, 0.0f);
    }

    public void RotateTurret(float factor)
    {
        turrentHinge.Rotate(0.0f, Time.deltaTime * -20.0f * factor, 0.0f, Space.Self);
    }

    public void RotateGun(float factor)
    {
        /* 炮台俯角仰角 */
        this.gunZDegree = gunHinge.localEulerAngles.z;
        if (this.gunZDegree > 180.0f)
        {
            this.gunZDegree -= 360.0f;
        }

        if((factor > 0.0f && gunZDegree < maxElevationGun) || (factor < 0.0f && gunZDegree > maxDepressionofGun))
        {
            gunHinge.Rotate(0.0f, 0.0f, Time.deltaTime * 20.0f * factor, Space.Self);
        }
    }

    public void Fire()
    {
        Rigidbody freshMuzzle = Instantiate<Rigidbody>(muzzlePrefab);   //复制一个prefab炮弹
        freshMuzzle.transform.position = muzzle.position;               //设置位置为标定的炮口位置
        freshMuzzle.velocity = muzzle.forward * 10.0f;                  //设置发射初速度

        freshMuzzle.transform.gameObject.AddComponent<Muzzle>();
    }
}