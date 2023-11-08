using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleActions : MonoBehaviour, IBattleActions
{
    BattleManager battleManager;


    private void Start()
    {
        battleManager = GameManager.m_Instance.GetBattleManager();
    }

    public void Attack(UnitCharacter targetToAtack)
    {
        battleManager.endTurn();
    }
    public void Heal(UnitCharacter targetToHeal)
    {
        battleManager.endTurn();
    }
}
