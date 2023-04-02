using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(DelayCall(3));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aaaay");
        }
    }

    IEnumerator Start()
    {
        var t1 = this.RunCoroutine(CoA());
        var t2 = this.RunCoroutine(CoB());

        while(!t1.IsDone && !t2.IsDone)
        {
            yield return null;
        }
        Debug.Log("co");
    }

    IEnumerator CoA()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Co A Com");
    }

    IEnumerator CoB()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Co B Com");
    }

    IEnumerator DelayCall(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Äð!");
    }
}
