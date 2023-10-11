using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> TurnOrder = new List<GameObject>();
    [SerializeField] private bool BattleStarted = false;
    private List<GameObject> EnemyList = new List<GameObject>();
    public void InitializeBattle(List<GameObject> enemyBattleList)
    {
        if(BattleStarted == false)
        {
            Debug.Log("Intizling Battle...");
            BattleStarted = true;
            EnemyList.Clear();
            EnemyList.AddRange(enemyBattleList);
            GatherUnits();
            OrderByDiceRoll();
            //Start First state of battle
        }
    }
    public void GatherUnits()
    {
        TurnOrder.Add(GameManager.m_Instance.GetPlayer());
        TurnOrder.AddRange(GameManager.m_Instance.GetPartyManager().GetPartyList());

        TurnOrder.AddRange(EnemyList);
    }

    public void OrderByDiceRoll()
    {
        foreach(GameObject unit in TurnOrder)
        {
            unit.GetComponent<UnitCharacter>().SetDiceNumber(GameManager.m_Instance.DiceRoll());
        }
        TurnOrder = TurnOrder.OrderBy(x => x.GetComponent<UnitCharacter>().GetDiceNumber()).ToList();
        TurnOrder.Reverse();
    }

    public void endTurn()
    {
        var old = TurnOrder[0];
        TurnOrder.RemoveAt(0);
        TurnOrder.Insert(TurnOrder.Count, old);
    }
}
