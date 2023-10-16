using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]

public class Item : ScriptableObject
{
    public string m_itemName;
    public Sprite m_itemIcon = null;
}
