using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsArriveDecisions : AIDecision
{
    public override bool MakeADecision()
    {
        brain.StateinfoCompo.isArrived = brain.NavAgentCompo.IsArrived;
        return brain.StateinfoCompo.isArrived;
    }
}
