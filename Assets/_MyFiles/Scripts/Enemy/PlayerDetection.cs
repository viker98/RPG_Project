using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(BringIntoBattle))] 
public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BringIntoBattle bringIntoBattle = GetComponent<BringIntoBattle>();
        if (other.CompareTag(EUnitType.Player.ToString()) && bringIntoBattle)
        {
            Debug.Log("Detected Player...");
            GameManager.m_Instance.CreateBattleManager(bringIntoBattle.GetEnemyPartnerList());
        }
    }
    
}
