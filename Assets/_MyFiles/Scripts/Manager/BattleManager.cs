using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Unity.Mathematics;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> TurnOrder = new List<GameObject>();
    [SerializeField] private bool BattleStarted = false;
    [SerializeField] private EBattleState BattleState = EBattleState.Wait;
    [SerializeField] private float TransitionTime = 3f;
    [SerializeField] private Transform PartyPos;
    [SerializeField] private Transform EnemyPos;

    [SerializeField] private GameObject CurrentSelection;
    [SerializeField] private EActionType actionType = EActionType.None;
    
    private List<GameObject> EnemyList = new List<GameObject>();
    private BattleUIManager BattleUI;
    private GameObject BattleCamera;

    [SerializeField] GameObject currentTurnMarker;

    List<GameObject> firstList = new List<GameObject>();

    [SerializeField] private int PlayerCount = 0;
    [SerializeField] private int EnemyCount = 0;

    public List<GameObject> GetTurnOrder() { return TurnOrder; }
    
    public void SetCurrentSelection(GameObject unitToSet) { CurrentSelection = unitToSet; }
    public GameObject GetCurrentSelection() { return CurrentSelection; }

    public void SetActionType(EActionType actionTypeToSet) { actionType = actionTypeToSet; }
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
                StartCoroutine(EnemyTurn());
                break;
            case EBattleState.BattleWon:
                StartCoroutine(BattleWon());
                break;
            case EBattleState.BattleLost:
                StartCoroutine(BattleLost());
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

            BattleCamera = GameObject.FindGameObjectWithTag("BattleCamera");

            EnemyList.Clear();
            EnemyList.AddRange(enemyBattleList);

            GatherUnits();
            OrderByDiceRoll();
            PlaceUnitsInBattle();

            currentTurnMarker = GameObject.CreatePrimitive(PrimitiveType.Plane);
            currentTurnMarker.transform.localScale = new Vector3(.25f,.25f,.25f);
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

        yield return new WaitForSeconds(TransitionTime);


        BattleUI.GetPlayerUIPanel().SetActive(true);

        //move camera or load level
        GameManager.m_Instance.GetPlayer().transform.parent.gameObject.SetActive(false);
        BattleCamera.AddComponent<Camera>();
        BattleCamera.AddComponent<AudioListener>();

        BattleUI.EndTransition();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
            firstList.Add(unitClone);
            TurnOrder.Add(unitClone);
        }

    }
    public List<GameObject> GetFirstList()
    {
        return firstList;
    }
    public void OrderByDiceRoll()
    {
        foreach(GameObject unit in TurnOrder)
        {
            unit.GetComponent<UnitCharacter>().SetDiceNumber(GameManager.m_Instance.DiceRoll(1,20));
        }
        TurnOrder = TurnOrder.OrderBy(x => x.GetComponent<UnitCharacter>().GetDiceNumber()).ToList();
        TurnOrder.Reverse();
    }
    public void PlaceUnitsInBattle()
    {
        PartyPos = GameObject.FindGameObjectWithTag("PartyPos").transform;
        EnemyPos = GameObject.FindGameObjectWithTag("EnemyPos").transform;

        PlayerCount = 0;
        EnemyCount = 0;

        for (int iPlayer = 0; iPlayer < TurnOrder.Count; iPlayer++)
        {
            UnitCharacter unit = TurnOrder[iPlayer].GetComponent<UnitCharacter>();
            if (unit.GetUnitType() == EUnitType.Player || unit.GetUnitType() == EUnitType.Partner)
            {
                unit.gameObject.transform.position = new Vector3(PartyPos.transform.position.x, PartyPos.transform.position.y, PartyPos.transform.position.z + (PlayerCount * 5f));
                unit.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                PlayerCount++;
            }
            else
            {
                unit.gameObject.transform.position = new Vector3(EnemyPos.transform.position.x, EnemyPos.transform.position.y, EnemyPos.transform.position.z + (EnemyCount * 5f));
                unit.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                EnemyCount++;

            }
        }
    }
    public void SetPlayerCount() { PlayerCount--; }
    public void SetEnemyCount() {  EnemyCount--; }
    public int getPlayerCount() { return PlayerCount; }
    public int getEnemyCount() { return EnemyCount; }
    public void ChooseTurn()
    {
        UnitCharacter currentTurn = TurnOrder[0].GetComponent<UnitCharacter>();
        Debug.Log(BattleState.ToString());
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
    public IEnumerator EnemyTurn()
    {
        BattleUI.GetPlayerUIPanel().SetActive(false);

        yield return new WaitForSeconds(1.5f);
        actionType = EActionType.None;

        int ranRoll = GameManager.m_Instance.DiceRoll(1, 2);
        int ranRoll2 = GameManager.m_Instance.DiceRoll(1, 2);
        GameObject enemySelection = null;
        
        if (ranRoll == 1)
        {
            if (ranRoll2 == 1)
            {
                enemySelection = firstList[0];
            }
            if (ranRoll2 == 2)
            {
                enemySelection = firstList[1];
            }
            TurnOrder[0].GetComponent<IBattleActions>().Attack(enemySelection.GetComponent<UnitCharacter>());
        }
        if (ranRoll == 2)
        {
            if (ranRoll2 == 1)
            {
                enemySelection = firstList[2];
            }
            if (ranRoll2 == 2)
            {
                enemySelection = firstList[3];
            }
            TurnOrder[0].GetComponent<IBattleActions>().Heal(enemySelection.GetComponent<UnitCharacter>());
        }     
    }
    public void endTurn()
    {
       // BattleUI.GetPlayerUIPanel().SetActive(false);

        var old = TurnOrder[0];
        TurnOrder.RemoveAt(0);
        TurnOrder.Insert(TurnOrder.Count, old);

        SetBattleState(EBattleState.ChooseTurn);
    }

    public IEnumerator BattleWon()
    {
        Debug.Log("Battle Won");
        BattleUI.PlayTransition();
        BattleUI.GetPlayerUIPanel().SetActive(false);
        yield return new WaitForSeconds(1.5f);
        BattleUI.EndTransition();
        BattleUI.GetWinEndScreen().SetActive(true);
        


        yield return null;
    }

    public IEnumerator BattleLost()
    {
        Debug.Log("Battle Loss");
        BattleUI.PlayTransition();
        BattleUI.GetPlayerUIPanel().SetActive(false);
        yield return new WaitForSeconds(1.5f);
        BattleUI.EndTransition();
        BattleUI.GetLossEndScreen().SetActive(true);

        yield return null;
    }


    /*   public void Attack(UnitCharacter target)
       {

       }
       public void Defend()
       {

       }
       public void Heal()
       {

       }
    */
    public List<GameObject> GetEnemyList()
    {
        return EnemyList;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CurrentSelection)
        {
            switch (actionType)
            {
                case EActionType.None:
                    break;
                case EActionType.Attack:
                    TurnOrder[0].GetComponent<IBattleActions>().Attack(CurrentSelection.GetComponent<UnitCharacter>());
                    break;
                case EActionType.Heal:
                    TurnOrder[0].GetComponent<IBattleActions>().Heal(CurrentSelection.GetComponent<UnitCharacter>());
                    break;
                default:
                    break;
            }
        }
        currentTurnMarker.transform.position = TurnOrder[0].gameObject.transform.position;
    }

}

public enum EBattleState { Wait, StartBattle,ChooseTurn , PlayerTurn, EnemyTurn,BattleWon,BattleLost }

public enum EActionType {None, Attack, Heal}