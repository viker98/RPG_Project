using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObject/Equpiment")]

public class Equipment : Item
{
    [SerializeField] EEquipmentType EquipmentType;
    //Fix This
    public int MaxHealthMod, HealthMod, MaxManaMod, ManaMod, intelligenceMod, SpeedMod, PowerMod, DefenseMod;
    //
    public EEquipmentType GetEquipmentType() { return EquipmentType; }

    public override void Use()
    {
        base.Use();

        GameManager.m_Instance.GetPlayer().GetComponent<UnitCharacter>().GetEquipment().Equip(this);

        RemoveItem();
    }

    public override void RemoveItem()
    {
        base.RemoveItem();

        Debug.Log("Removing Equipment");

    }
}

public enum EEquipmentType {Head, MainHand_Weapon, OffHand_Weapon, Arms, Shield, Staff}
