using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : MonoBehaviour
{
    private CharacterStats CharacterStatsProfile;
    [SerializeField] private EUnitType UnitType;
    private void Awake()
    {
        CharacterStatsProfile = gameObject.AddComponent<CharacterStats>();
    }

    public CharacterStats GetCharacterStats() { return CharacterStatsProfile; }
}
public enum EUnitType {Player, Partner, Enemy, NPC}
