using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] Equipment[] CurrentEqupment = new Equipment[System.Enum.GetNames(typeof(EEquipmentType)).Length];

    public void Equip(Equipment gear)
    {
        int equipmentIndex = (int)gear.GetEquipmentType();


        if (CurrentEqupment[equipmentIndex] != null)
        {
            Equipment oldItem = null;

            oldItem = CurrentEqupment[equipmentIndex];
            UnequipMods(oldItem);
        }
        CurrentEqupment[equipmentIndex] = gear;

        EquipMods(gear);
    }

    public void Unequip(int equipmentIndex)
    {
        if (CurrentEqupment[equipmentIndex])
        {
            Equipment oldItem = CurrentEqupment[equipmentIndex];
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetInventory().AddItem(oldItem);
            CurrentEqupment[equipmentIndex] = null;
        }
    }
    private void EquipMods(Equipment gear)
    {
        //Fix This
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).AddModifier(gear.HealthMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Mana).AddModifier(gear.ManaMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.MaxMana).AddModifier(gear.MaxManaMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.MaxHealth).AddModifier(gear.MaxHealthMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.intelligence).AddModifier(gear.intelligenceMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Speed).AddModifier(gear.SpeedMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Power).AddModifier(gear.PowerMod);
        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Defense).AddModifier(gear.DefenseMod);
        //
    }
    private void UnequipMods(Equipment gear)
    {
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Health).RemoveModifier(gear.HealthMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Mana).RemoveModifier(gear.ManaMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.MaxMana).RemoveModifier(gear.MaxManaMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.MaxHealth).RemoveModifier(gear.MaxHealthMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.intelligence).RemoveModifier(gear.intelligenceMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Speed).RemoveModifier(gear.SpeedMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Power).RemoveModifier(gear.PowerMod);
            GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Defense).RemoveModifier(gear.DefenseMod);
    }
}
