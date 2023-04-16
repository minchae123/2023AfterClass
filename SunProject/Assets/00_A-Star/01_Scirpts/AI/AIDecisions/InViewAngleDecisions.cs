using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InViewAngleDecisions : AIDecision
{
    public override bool MakeADecision()
    {
        Vector3 right = brain.transform.right;
        Vector3 direction = (brain.PlayerTrm.position - brain.transform.position).normalized;

        float degreeAngle = Vector3.Angle(right, direction);
        if(degreeAngle < brain.ViewAngle * 0.5f)
        {
            return true;
        }
        return false;
    }
}
