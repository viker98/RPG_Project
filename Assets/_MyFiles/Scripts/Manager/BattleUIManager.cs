using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    private BattleManager CurrentBattle;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private Animator TransitionPanelANIM;

    private void Start()
    {
        CurrentBattle = GameManager.m_Instance.GetBattleManager();
        if (CurrentBattle)
        {
            Debug.Log("BattleUIManager: Connected to CurrentBattle!");
        }
        else
        {
            Debug.LogError("BattleUIManager: CurrentBattle Not Found!");
        }
    }
    public GameObject GetPlayerUIPanel() { return  PlayerUI; }
    public void SetActivePlayerUI(bool StatusToSet)
    {
        PlayerUI.SetActive(StatusToSet);
    }
    public void PlayTransition()
    {
        Debug.Log("playing tansistion");
        TransitionPanelANIM.SetTrigger("PlayTransition");
    }
    public void EndTransition()
    {
        TransitionPanelANIM.SetTrigger("EndTransition");
    }
    public void EndTurnButton()
    {
        if (CurrentBattle) { CurrentBattle.endTurn(); }
    }
}
