using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAction : AIAction
{
    public override void TakeAction()
    {
        if(_brain.StateinfoCompo.isAttack == false && _brain.StateinfoCompo.meleeCool <= 0)
        {
            _brain.Attack(true);
        }
    }
}
