using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    // MinHeap
    public List<T> heap = new List<T>();
    public int Count => heap.Count;

    public T Contains(T t)
    {
        int idx = heap.IndexOf(t);
        if(idx < 0) return default(T);
        return heap[idx];
    }

    public void Push(T data)
    {
        // 새로운 걸 넣을 때는 맨 밑에다가 넣고 올려가면서 자리 찾기
        heap.Add(data);
        int now = heap.Count - 1; // 마지막 원소 인덱스

        while(now > 0)
        {
            int next = (now - 1) / 2;
            if (heap[now].CompareTo(heap[next]) < 0)
            {
                break;
            }

            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }
    }

    public T Pop()
    {
        T ret = heap[0];

        int lastIdx = heap.Count - 1;
        heap[0] = heap[lastIdx];
        heap.RemoveAt(lastIdx);
        lastIdx--;

        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            if (left <= lastIdx && heap[next].CompareTo(heap[left]) < 0)
                next = left;
            if (right <= lastIdx && heap[next].CompareTo(heap[right]) < 0)
                next = right;
            if (next == now)
                break;

            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }

        return ret;
    }

    public T Peek()
    {
        return heap.Count == 0 ? default(T) : heap[0];
    }

    public void Clear()
    {
        heap.Clear();
    }
}
