using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackAction : AIAction
{
    public override void TakeAction()
    {
        if (_brain.StateinfoCompo.isAttack == false && _brain.StateinfoCompo.rangeCool <= 0)
        {
            _brain.Attack(false);
        }
    }
}
