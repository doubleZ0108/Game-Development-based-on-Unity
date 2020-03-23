using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResource : MonoBehaviour
{
    private ResourceRequest request;
    private Transform cube;

    void Start()
    {
        StartCoroutine(LoadAsyncResourceCoroutine());
    }

    private IEnumerator LoadAsyncResourceCoroutine()
    {
        request = Resources.LoadAsync<Transform>("Cube");
        yield return request;

        cube = request.asset as Transform;
        if (cube == null)
        {
            Debug.LogError("LoadAsync failed!");
            request = null;
            yield break;
        }
        Instantiate(cube);
        request = null;
    }
}
