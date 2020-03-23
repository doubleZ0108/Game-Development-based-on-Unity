using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerInputHandler : MonoBehaviour
{
    /* Singleton */
    private static TankPlayerInputHandler _sInstance = null;

    public static TankPlayerInputHandler Instance()
    {
        _sInstance = FindObjectOfType<TankPlayerInputHandler>();        //保证整个场景只有一个
        if(_sInstance == null)
        {
            GameObject newObj = new GameObject(name: "TankPlayerInputHandler");
            _sInstance = newObj.AddComponent<TankPlayerInputHandler>();
        }
        return _sInstance;
    }

    public event Action<float> Axis1HorizontalInputEvent;
    public event Action<float> Axis1VerticalInputEvent;
    public event Action<float> Axis2HorizontalInputEvent;
    public event Action<float> Axis2VerticalInputEvent;
    public event Action FireInputEvent;

    public event Action<bool> SetControlActiveEvent;

    public void TakeTankControl(Tank tank)
    {
        if (SetControlActiveEvent != null)
        {
            SetControlActiveEvent(false);
        }
        tank.SetControlActive(true);
    }

    void Update()
    {
        /* 前进 后退 */
        if (Input.GetKey(KeyCode.W))
        {
            if(Axis1VerticalInputEvent != null)
            {
                Axis1VerticalInputEvent(1.0f);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Axis1VerticalInputEvent != null)
            {
                Axis1VerticalInputEvent(-1.0f);
            }
        }

        /* 原地旋转 (该脚本绑在Tank身上，所以全体都会跟着移动)*/
        if (Input.GetKey(KeyCode.A))
        {
            if (Axis1HorizontalInputEvent != null)
            {
                Axis1HorizontalInputEvent(1.0f);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Axis1HorizontalInputEvent != null)
            {
                Axis1HorizontalInputEvent(-1.0f);
            }
        }

        /* 炮塔旋转 */
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Axis2HorizontalInputEvent != null)
            {
                Axis2HorizontalInputEvent(1.0f);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Axis2HorizontalInputEvent != null)
            {
                Axis2HorizontalInputEvent(-1.0f);
            }
        }

        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Axis2VerticalInputEvent != null)
            {
                Axis2VerticalInputEvent(1.0f);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Axis2VerticalInputEvent != null)
            {
                Axis2VerticalInputEvent(-1.0f);
            }
        }

        /* 发射炮弹 */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (FireInputEvent != null)
            {
                FireInputEvent();
            }
        }
    }
}
