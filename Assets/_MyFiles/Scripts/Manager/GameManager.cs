using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] UnitCharacter playerUnit;
    [SerializeField] Transform playerSpawn;

    private GameObject Player;
    [SerializeField] private PartyManager Party;
    [SerializeField] private BattleManager CurrentBattle;

    bool bDebugToggle = false;
    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Debug.LogError("Multiple/not exsisting GameManager. Deleting Copy...");
            Destroy(this);
        }
        else
        {
            m_Instance = this;
        }
    }
    private void Start()
    {
        if (PlayerPrefab && playerSpawn)
        {
            //spawns player
            GameObject playerGRP = Instantiate(PlayerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
            Player = playerGRP.GetComponentInChildren<UnitCharacter>().gameObject;
            CreatePartyManager();
        }
        else
        {
            Debug.LogWarning("Player or Player Spawn Not Found");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            bDebugToggle = !bDebugToggle;

            if (bDebugToggle)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket) && Player)
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.RightBracket) && Player)
        {
            LoadData();
        }
    }
    public GameObject GetPlayer() { return Player; }

    public int DiceRoll()
    {
        int diceRoll = Random.Range(1, 20 + 1);
        return diceRoll;
    }

    public void CreatePartyManager()
    {
        Party = gameObject.AddComponent<PartyManager>();
    }
    public void DestroyPartyManager()
    {
        Destroy(Party);
        Party = null;
    }

    public PartyManager GetPartyManager() { return Party; }

    public void CreateBattleManager(List<GameObject> enemyBattleList)
    {
        if (CurrentBattle) { return; }

        Debug.Log("Created BattleManger...");
        CurrentBattle = gameObject.AddComponent<BattleManager>();
        CurrentBattle.InitializeBattle(enemyBattleList);
    }
    public void DestroyBattleManager()
    {
        Destroy(CurrentBattle); 
        CurrentBattle = null;
    }
    public BattleManager GetBattleManager() { return CurrentBattle; }

    public void SaveData()
    {
        Debug.Log("Saving Data...");
        string playerData = JsonUtility.ToJson(Player.GetComponent<UnitCharacter>().GetCharacterStats());
        string filepath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log(filepath);

        System.IO.File.WriteAllText(filepath, playerData);
        Debug.Log("Save Complete!");
    }
    public void LoadData()
    {
        Debug.Log("Loading Data...");
        string filepath = Application.persistentDataPath + "/PlayerData.json";
        string playerData = System.IO.File.ReadAllText(filepath);
        JsonUtility.FromJsonOverwrite(playerData, Player.GetComponent<UnitCharacter>().GetCharacterStats());
        Debug.Log("Load Complete!");
    }
}
