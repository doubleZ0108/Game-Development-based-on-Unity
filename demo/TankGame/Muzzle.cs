using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public float lifeTime = 5;

    void Start()
    {
        //子弹自销毁
        StartCoroutine(SelfDestroyCoroutine(lifeTime));
    }

    private IEnumerator SelfDestroyCoroutine(float time)
    {
        float elapsed = 0.0f;
        while(elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("destroy...");
        Destroy(gameObject);
    }

    
}
