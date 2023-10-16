using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Item m_itemProfile;

    private void OnTriggerEnter(Collider other)
    {
        UnitCharacter character = other.GetComponent<UnitCharacter>();
        if (character && character.GetUnitType() == EUnitType.Player)
        {
            Debug.Log("Item Get!");
            //add item to inventory

            Destroy(this.gameObject);
        }
    }
}
