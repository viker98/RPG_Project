using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.VFX;
using System.IO;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> TurnOrder = new List<GameObject>();
    [SerializeField] private bool BattleStarted = false;
    [SerializeField] private EBattleState BattleState = EBattleState.Wait;
    [SerializeField] private float TransitionTime = .5f;
    private List<GameObject> EnemyList = new List<GameObject>();

    private BattleUIManager BattleUI;

    public EBattleState GetBattleState() { return BattleState; }
    public void SetBattleState(EBattleState stateToSet) { BattleState = stateToSet; }
    public void InitializeBattle(List<GameObject> enemyBattleList)
    {
        if(BattleStarted == false)
        {
            Debug.Log("Intizling Battle...");
            BattleStarted = true;

            GameObject BattleUIClone = Instantiate(GameManager.m_Instance.GetBattleUI(), this.gameObject.transform, false);
            BattleUI = BattleUIClone.GetComponent<BattleUIManager>();

            EnemyList.Clear();
            EnemyList.AddRange(enemyBattleList);
            GatherUnits();
            OrderByDiceRoll();
            //Spawn Enemies and players at locations
            SetBattleState(EBattleState.StartBattle);
            StartCoroutine(BattleStart());
        }
    }
    public IEnumerator BattleStart()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(.1f);

        //battle Transistion
        BattleUI.PlayTransition();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale;

        //move camera or load level
        //spawn good guys and bad guys
        BattleUI.SetActiveBattleStartPostP(false);

        yield return new WaitForSeconds(TransitionTime);
        BattleUI.EndTransition();

        SetBattleState(EBattleState.ChooseTurn);
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
    public void ChooseTurn()
    {
        UnitCharacter currentTurn = TurnOrder[0].GetComponent<UnitCharacter>();
    }
    public void endTurn()
    {
        var old = TurnOrder[0];
        TurnOrder.RemoveAt(0);
        TurnOrder.Insert(TurnOrder.Count, old);
    }
    public void Attack(UnitCharacter target)
    {

    }
    public void Defend()
    {

    }
    public void Heal()
    {
        
    }

}

public enum EBattleState { Wait, StartBattle,ChooseTurn , PlayerTurn, EnemyTurn,BattleWon,BattleLost }