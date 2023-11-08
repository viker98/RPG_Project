using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{

    BattleManager battleManager;

    void Start()
    {
        battleManager = GameManager.m_Instance.GetBattleManager();
        if(battleManager == null)
        {
            Debug.LogError("BattleManger broken fix it");
        }
    }
    private void OnMouseOver()
    {
        if (battleManager)
        {
            battleManager.SetCurrentSelection(this.gameObject);
        }
    }

    private void OnMouseExit()
    {
        if (battleManager)
        {
            battleManager.SetCurrentSelection(null);
        }
    }

    void Update()
    {
        
    }
}
