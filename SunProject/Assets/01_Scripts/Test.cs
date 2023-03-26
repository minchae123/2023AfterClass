using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Human
{
    public bool PassCertificate();
}

public class Student
{
    public string name;
    public int code;
    public string phone;
    public Sprite picture;

    public virtual void Introduce()
    {
        Debug.Log("안녕하세요 학생입니다");
    }
}

public class Student2 : Student, Human
{
    public override void Introduce()
    {
        base.Introduce();
        Debug.Log("안녕하세요 2학년학생입니다");
    }

    public bool PassCertificate()
    {
        return true;
    }
}

public class Test : MonoBehaviour
{
    private Collision2D co;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        co = collision;
        Debug.Log("충돌");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(co.gameObject.name);
        }
    }

    public void PrintMsg()
    {
        Student s = new Student();
        s.Introduce();

        Human s2 = new Student2();
        if (s2.PassCertificate())
        {
            Debug.Log("ㅊㅋ");
        }
        else
        {
            Debug.Log("우끼끼");
        }
    }    
}