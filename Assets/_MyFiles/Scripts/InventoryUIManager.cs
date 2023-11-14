using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] Transform InventoryGRP;
    ItemSlot[] ItemSlots;
    bool InventoryClosed = false;
    private void Start()
    {
        InventoryGRP = GameObject.FindGameObjectWithTag("InventoryGRP").transform;
        ItemSlots = InventoryGRP.GetComponentsInChildren<ItemSlot>();
    }
    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.I) && !InventoryClosed)
        {
            InventoryGRP.gameObject.SetActive(false);
            InventoryClosed = true;
        } 
        else if (Input.GetKeyDown(KeyCode.I) && InventoryClosed)
        {
            InventoryGRP.gameObject.SetActive(true);
            InventoryClosed = false;
        }
        
    }
    public void UpdateUI(List<Item> itemsToUpdate)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (i < itemsToUpdate.Count)
            {
                ItemSlots[i].AddItem(itemsToUpdate[i]);
            }
            else
            {
                ItemSlots[i].ClearItem();
            }
        }
    }
}
