using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleActions
{
    void Attack(UnitCharacter targetToAttack);

    void Heal(UnitCharacter targetToHeal);
}
