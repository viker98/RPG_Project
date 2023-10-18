using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Item ItemProfile;

    private void OnTriggerEnter(Collider other)
    {
        UnitCharacter character = other.GetComponent<UnitCharacter>();
        if (character && character.GetUnitType() == EUnitType.Player)
        {
            Debug.Log("Item Get!");
            if (other.GetComponent<UnitCharacter>().GetInventory().AddItem(ItemProfile))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
