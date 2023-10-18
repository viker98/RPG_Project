using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Range(1, 15)][SerializeField] private int InventorySpace;
    [SerializeField] private List<Item> Items = new List<Item>();

    public int GetInventorySpace()
    {
        return InventorySpace;
    }
    public void SetInventorySpace(int spaceToSet)
    {
        InventorySpace = spaceToSet;
    }
    public List<Item> GetItemList() { return Items; }
    public bool AddItem(Item itemToAdd)
    {
        if (itemToAdd != null && Items.Count < InventorySpace)
        {
            Items.Add(itemToAdd);
            GameManager.m_Instance.GetInventoryUIManager().UpdateUI(Items);
            return true;
        }
        else
        {
            Debug.Log("Inventory Full!"); 
            return false;
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (itemToRemove != null) 
        { 
            Items.Remove(itemToRemove);
            GameManager.m_Instance.GetInventoryUIManager().UpdateUI(Items);
        }
    }
}
