using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item ItemProfile;
    [SerializeField] private Image Icon;
    [SerializeField] private Button RemoveButton;

    public void AddItem(Item itemToSet)
    {
        ItemProfile = itemToSet;
        Icon.sprite = ItemProfile.m_itemIcon;
        Icon.enabled = true;
        RemoveButton.interactable = true;
    }
    public void ClearItem()
    {
        ItemProfile = null;
        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.interactable = false;
    }
    public void RemoveButtonFUNC()
    {
        UnitCharacter unit = GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>();
        unit.GetInventory().RemoveItem(ItemProfile);
    }

    public void UseItem()
    {
        if (ItemProfile != null) { ItemProfile.Use(); }
    }
}
