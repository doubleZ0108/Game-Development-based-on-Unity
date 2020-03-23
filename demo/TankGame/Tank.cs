using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tank : MonoBehaviour, IPointerClickHandler
{
    public Transform turrentHinge;
    public Transform gunHinge;

    private double gunZDegree;
    public double maxElevationGun = 10.0f;           //炮台最大仰角
    public double maxDepressionofGun = -20.0f;       //炮台最大俯角

    public Transform muzzle;
    public Rigidbody muzzlePrefab;

    private bool isControlActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetControlActive(bool isActive)
    {
        TankPlayerInputHandler handler = TankPlayerInputHandler.Instance();

        if (isActive != isControlActive)    //第一次点击 => 注册
        {
            isControlActive = isActive;

            handler.Axis1VerticalInputEvent += Move;
            handler.Axis1HorizontalInputEvent += Rotate;
            handler.Axis2VerticalInputEvent += RotateGun;
            handler.Axis2HorizontalInputEvent += RotateTurret;
            handler.FireInputEvent += Fire;
        }
        else
        {
            isControlActive = !isActive;    //再次点击 => 移除注册信息

            handler.Axis1VerticalInputEvent -= Move;
            handler.Axis1HorizontalInputEvent -= Rotate;
            handler.Axis2VerticalInputEvent -= RotateGun;
            handler.Axis2HorizontalInputEvent -= RotateTurret;
            handler.FireInputEvent -= Fire;
        }
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        TankPlayerInputHandler.Instance().TakeTankControl(this);
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