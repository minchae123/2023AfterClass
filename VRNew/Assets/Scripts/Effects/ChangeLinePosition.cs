using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{

    public int index=1;
    private LineRenderer target;
    
    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }
    
    public void Call(Vector3 worldPosition)
    {
        if (target.useWorldSpace)
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index, localPosition);
        }
    }
}
