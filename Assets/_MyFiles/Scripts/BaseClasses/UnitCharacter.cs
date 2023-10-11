using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : MonoBehaviour
{
    private CharacterStats CharacterStatsProfile;
    [SerializeField] private EUnitType UnitType;
    [SerializeField] private int DiceNumber;
    private void Awake()
    {
        CharacterStatsProfile = gameObject.AddComponent<CharacterStats>();
    }

    public CharacterStats GetCharacterStats() { return CharacterStatsProfile; }
    public int GetDiceNumber() { return DiceNumber; }
    public void SetDiceNumber(int valueToSet) { DiceNumber = valueToSet; }
}
public enum EUnitType {Player, Partner, Enemy, NPC}
