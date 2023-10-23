using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]

public class Item : ScriptableObject
{
    public  string m_itemName;
    public Sprite m_itemIcon = null;
    [TextArea][SerializeField] string EquipmentDiscription;

    public virtual void Use()
    {
        Debug.Log(m_itemName + " Was Used!");
    }
    public virtual void RemoveItem()
    {
        Debug.Log("Removing Item");

        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetInventory().RemoveItem(this);
    }
}
