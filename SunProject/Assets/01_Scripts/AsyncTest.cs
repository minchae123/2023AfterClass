using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTest : MonoBehaviour
{
    private ulong nu = 0;

    private void Awake()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("มกวม");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }

    private async void JobSequence()
    {
        Debug.Log("st");
        await Task.Run(() => MyJob());
        Debug.Log("dd");
    }

    private void MyJob()
    {
        for(int i = 0; i < 500000; i++)
        {
            nu--;
        }
        Debug.Log("J end");
    }
}
