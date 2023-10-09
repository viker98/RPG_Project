using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform playerSpawn;

    private GameObject Player;
    [SerializeField] private PartyManager Party;

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
            Player = Instantiate(PlayerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
        }
        else
        {
            Debug.LogWarning("Player or Player Spawn Not Found");
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
}
