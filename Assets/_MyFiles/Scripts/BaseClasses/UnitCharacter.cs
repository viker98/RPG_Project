using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : MonoBehaviour
{
    private CharacterStats CharacterStatsProfile;
    [SerializeField] private EUnitType UnitType;
    [SerializeField] private int DiceNumber;
    [SerializeField] private Inventory CharacterInventory;
    private void Awake()
    {
        CharacterStatsProfile = gameObject.AddComponent<CharacterStats>();
        CharacterInventory = gameObject.AddComponent<Inventory>();
        CharacterInventory.SetInventorySpace(15);
    }

    public EUnitType GetUnitType() { return UnitType; }
    public CharacterStats GetCharacterStats() { return CharacterStatsProfile; }
    public int GetDiceNumber() { return DiceNumber; }
    public void SetDiceNumber(int valueToSet) { DiceNumber = valueToSet; }
    public Inventory GetInventory() { return CharacterInventory; }
    public void SetInventoryItems(List<Item> itemsToSet)
    {
        CharacterInventory.GetItemList().Clear();
        CharacterInventory.GetItemList().AddRange(itemsToSet);
    }
}
public enum EUnitType {Player, Partner, Enemy, NPC}
