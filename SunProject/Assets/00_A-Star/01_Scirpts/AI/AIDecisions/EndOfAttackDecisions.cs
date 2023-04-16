using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfAttackDecisions : AIDecision
{
    public bool IsMelee;

    public override bool MakeADecision()
    {
        if (IsMelee)
        {
            return brain.StateinfoCompo.isMelee == false;
        }
        else
        {
            return brain.StateinfoCompo.isRange == false;
        }
    }
}
