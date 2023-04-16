using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttackDecisions : AIDecision
{
    public bool IsMelee;

    public override bool MakeADecision()
    {
        if (IsMelee)
        {
            float cool = brain.StateinfoCompo.meleeCool;
            return brain.StateinfoCompo.isAttack == false && cool < 0;
        }
        else
        {
            float cool = brain.StateinfoCompo.rangeCool;
            return brain.StateinfoCompo.isAttack == false && cool < 0;
        }
    }
}
