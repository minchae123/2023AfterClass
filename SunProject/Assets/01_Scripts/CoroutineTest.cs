using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    private List<string> list = new List<string>();
    IEnumerator t;

    private void Start()
    {
        t = Test();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
                t.MoveNext();
                Debug.Log(t.Current);
        }
    }

    IEnumerator Test()
    {
        Debug.Log("Someting!");
        yield return 1;
        Debug.Log("Someting!");
        yield return 2;
        Debug.Log("Someting!");
        yield return 3;
        Debug.Log("Someting!");
        yield return 4;

    }

    IEnumerator DelayCall(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Äð!");
    }
}
