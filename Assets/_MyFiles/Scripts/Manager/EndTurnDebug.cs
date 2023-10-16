using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnDebug : MonoBehaviour
{
    public void EndTurnDEBUG()
    {
        BattleManager currentBattle = GameManager.m_Instance.GetBattleManager();
        if (currentBattle)
        {
            currentBattle.endTurn();
        }
    }
}
