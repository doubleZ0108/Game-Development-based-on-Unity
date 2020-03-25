using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        Rect area = new Rect(10.0f, 10.0f, 300.0f, 40.0f);
        GUILayout.BeginArea(area);

        isEnabled = GUILayout.Toggle(isEnabled, "isEnabled");

        if (isEnabled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("FPS: ");
            GUILayout.Label(Mathf.RoundToInt(1.0f / Time.deltaTime).ToString());
            GUILayout.EndHorizontal();
        }

        GUILayout.EndArea();
    }
}
