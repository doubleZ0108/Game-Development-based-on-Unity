using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Coroutine accelerateCoroutine = StartCoroutine(Accelerate(GetComponent<Rigidbody>(), new Vector3(1.0f, 0.0f, 0.0f), 3.0f));
        //...
        StopCoroutine(accelerateCoroutine);
    }


    private IEnumerator Accelerate(Rigidbody rigidbody, Vector3 acceleration, float time)
    {
        float elapsed = 0.0f;
        while (elapsed < 10.0f)
        {
            elapsed += time;
            rigidbody.velocity += acceleration * Time.fixedDeltaTime;
            yield return new WaitForSeconds(time);
        }
    }
}
