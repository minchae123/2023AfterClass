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
    public void ABC()
    {
        Student s = new Student();
        MyDelegate a = s.Minus;

        Debug.Log(a?.Invoke(3, 4));
    }
}