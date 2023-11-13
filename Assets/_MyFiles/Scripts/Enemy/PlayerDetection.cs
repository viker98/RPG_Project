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
        UnitCharacter character = other.GetComponent<UnitCharacter>();
        if (character && bringIntoBattle && character.GetUnitType() == EUnitType.Player)
        {
            Debug.Log("Detected Player...");
            GameManager.m_Instance.CreateBattleManager(bringIntoBattle.GetPartnerList());

        }
    }
}
