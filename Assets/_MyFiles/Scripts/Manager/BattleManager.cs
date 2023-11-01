using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Unity.Mathematics;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> TurnOrder = new List<GameObject>();
    [SerializeField] private bool BattleStarted = false;
    [SerializeField] private EBattleState BattleState = EBattleState.Wait;
    [SerializeField] private float TransitionTime = 3f;
    [SerializeField] private Transform PartyPos;
    [SerializeField] private Transform EnemyPos;
    private List<GameObject> EnemyList = new List<GameObject>();

    private BattleUIManager BattleUI;

    public EBattleState GetBattleState() { return BattleState; }
    public void SetBattleState(EBattleState stateToSet) 
    {
        BattleState = stateToSet;

        switch (stateToSet)
        {
            case EBattleState.Wait:
                //do nothing
                break;
            case EBattleState.StartBattle:
                StartCoroutine(BattleStart());
                break;
            case EBattleState.ChooseTurn:
                ChooseTurn();
                break;
            case EBattleState.PlayerTurn:
                PlayerTurn();
                break;
            case EBattleState.EnemyTurn:
                EnemyTurn();
                break;
            case EBattleState.BattleWon:

                break;
            case EBattleState.BattleLost:

                break;
            default:
                ChooseTurn();
                break;
        }
    }
    public void InitializeBattle(List<GameObject> enemyBattleList)
    {
        if(BattleStarted == false)
        {
            BattleStarted = true;
            Debug.Log("Intizling Battle...");

            GameObject BattleUIClone = Instantiate(GameManager.m_Instance.GetBattleUI(), this.gameObject.transform, false);
            BattleUI = BattleUIClone.GetComponent<BattleUIManager>();

            BattleUI.GetPlayerUIPanel().SetActive(false);

            EnemyList.Clear();
            EnemyList.AddRange(enemyBattleList);

            GatherUnits();
            OrderByDiceRoll();
            PlaceUnitsInBattle();

            //Spawn Enemies and players at locations

            SetBattleState(EBattleState.StartBattle);
        }
    }
    public IEnumerator BattleStart()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(.075f);

        //battle Transistion
        BattleUI.PlayTransition();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale;

        //move camera or load level
        //spawn good guys and bad guys
     //   BattleUI.SetActiveBattleStartPostP(false);

        yield return new WaitForSeconds(TransitionTime);
        BattleUI.EndTransition();

        SetBattleState(EBattleState.ChooseTurn);
    }
    public void GatherUnits()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(GameManager.m_Instance.GetPlayer().GetComponent<BringIntoBattle>().GetPartnerList());
        tempList.AddRange(EnemyList);

        foreach (GameObject unit in tempList)
        {
            GameObject unitClone = Instantiate(unit, new Vector3(0f, -1000f, 0f), this.transform.rotation, this.transform);
            TurnOrder.Add(unitClone);
        }
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
    public void PlaceUnitsInBattle()
    {
        PartyPos = GameObject.FindGameObjectWithTag("PartyPos").transform;
        EnemyPos = GameObject.FindGameObjectWithTag("EnemyPos").transform;

        int playerCount = 0;
        int enemyCount = 0;

        for (int iPlayer = 0; iPlayer < TurnOrder.Count; iPlayer++)
        {
            UnitCharacter unit = TurnOrder[iPlayer].GetComponent<UnitCharacter>();
            if (unit.GetUnitType() == EUnitType.Player || unit.GetUnitType() == EUnitType.Partner)
            {
                unit.gameObject.transform.position = new Vector3(PartyPos.transform.position.x, PartyPos.transform.position.y, PartyPos.transform.position.z + (playerCount * 5f));
                unit.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                playerCount++;
            }
            else
            {
                unit.gameObject.transform.position = new Vector3(EnemyPos.transform.position.x, EnemyPos.transform.position.y, EnemyPos.transform.position.z + (enemyCount * 5f));
                unit.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                enemyCount++;

            }
        }
    }
    public void ChooseTurn()
    {
        UnitCharacter currentTurn = TurnOrder[0].GetComponent<UnitCharacter>();
        if (currentTurn.GetUnitType() == EUnitType.Player || currentTurn.GetUnitType() == EUnitType.Partner)
        {
            SetBattleState(EBattleState.PlayerTurn);
        }
        else
        {
            SetBattleState(EBattleState.EnemyTurn);
        }
    }
    public void PlayerTurn()
    {
        BattleUI.GetPlayerUIPanel().SetActive(true);
    }
    public void EnemyTurn()
    {

    }
    public void endTurn()
    {
        BattleUI.GetPlayerUIPanel().SetActive(false);

        var old = TurnOrder[0];
        TurnOrder.RemoveAt(0);
        TurnOrder.Insert(TurnOrder.Count, old);

        SetBattleState(EBattleState.ChooseTurn);
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