using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate int MyDelegate(int a, int b);

public class Student
{
    public int Plus(int a, int b)
    {
        return a + b;
    }

    public int Minus(int a, int b)
    {
        return a - b;
    }

}

public class Test : MonoBehaviour
{
    
    private void Start()
    {
        ABC();
    }

    public void ABC()
    {
        MyDelegate a = delegate(int a, int b)
        {
            return a + b;
        };
        int result = a.Invoke(3, 4);
        Debug.Log(result);
    }
}