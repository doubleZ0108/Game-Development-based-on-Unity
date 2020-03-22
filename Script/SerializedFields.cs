using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyClass
{
    public string name;
    public int id;
}

public class SerializedFields : MonoBehaviour
{
    public int intField;
    public bool boolField;
    public Vector3 vector3Field;
    public int[] arrayField;
    public List<char> listField;
    public Color colorField;
    public AnimationCurve animationCurveField;
    public Gradient gradientField;

    [SerializeField]
    private double doublePrivateField;

    public MyClass myClassField;
}
