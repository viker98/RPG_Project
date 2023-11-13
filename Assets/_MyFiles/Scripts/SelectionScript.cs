using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{

    BattleManager battleManager;
    [SerializeField] GameObject Marker;
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
            Marker.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (battleManager)
        {
            battleManager.SetCurrentSelection(null);
            Marker.SetActive(false);
        }
    }

    void Update()
    {

    }
}
