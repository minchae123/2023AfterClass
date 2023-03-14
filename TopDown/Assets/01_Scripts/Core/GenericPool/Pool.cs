using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();
    private T prefab; // 모자랄 때 찍어낼 용도
    private Transform parent; // 생성시킬 부모 

    public Pool(T prefab, Transform parent, int count)
    {
        this.prefab = prefab;
        this.parent = parent;

        for(int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", ""); // 클론이라는 이름 지우기
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;

        if(pool.Count <= 0) // 스택에 풀링해둔게 다 떨어지면 새로 만들어
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Pop(); // 스택에 남은거 중에 가장 위에 꺼
            obj.gameObject.SetActive(true); 
        }
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
