using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    private BattleManager CurrentBattle;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private Animator TransitionPanelANIM;

    [SerializeField] private Slider P1Slider;
    [SerializeField] private Slider P2Slider;
    [SerializeField] private Slider E1Slider;
    [SerializeField] private Slider E2Slider;

    [SerializeField] List<GameObject> turn;

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
        turn = CurrentBattle.GetFirstList();
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

    public void AttackButton()
    {
        if (CurrentBattle) { CurrentBattle.SetActionType(EActionType.Attack); }
    }
    public void HealButton()
    {
        if (CurrentBattle) { CurrentBattle.SetActionType(EActionType.Heal); }
    }
  /*  public void SetHealthBars()
    {
        List<GameObject> turn = GameManager.m_Instance.GetPlayer().GetComponent<BringIntoBattle>().GetPartnerList();
        P1Slider.value = turn[0].gameObject.GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        P1Slider.value = turn[1].gameObject.GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        turn = CurrentBattle.GetEnemyList();
        E1Slider.value = turn[0].gameObject.GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        E2Slider.value = turn[1].gameObject.GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
    }
  */
    private void Update()
    {
        if (P1Slider.value <= 0)
        {
            P1Slider.value = 0;
        }
        else
        {
            P1Slider.value = turn[0].GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        }
        if (P2Slider.value <= 0)
        {
            P2Slider.value = 0;
        }
        else
        {
            P2Slider.value = turn[1].GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        }
        if (E1Slider.value <= 0)
        {
            E1Slider.value = 0;
        }
        else
        {
            E1Slider.value = turn[2].GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        }
        if (E2Slider.value <= 0)
        {
            E2Slider.value = 0;
        }
        else
        {
            E2Slider.value = turn[3].GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        }
    }
}
