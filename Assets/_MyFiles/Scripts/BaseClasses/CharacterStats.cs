using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private string CharacterName;
    [SerializeField] private string CharacterDescription;

    [SerializeField] private List<Stat> StatList = new List<Stat>();

    private void Awake()
    {
        for (int i = 0; i < (int)EStatType.COUNT; i++)
        {
            EStatType type = (EStatType)i;
            Stat newStat = new Stat();
            if (type == EStatType.Health ||  type == EStatType.MaxHealth 
                || type == EStatType.Mana || type == EStatType.MaxMana)
            {
                newStat.SetBaseValue(100);
            }
            else
            {
                newStat.SetBaseValue(3);
            }
            newStat.SetStatName(type.ToString());
            StatList.Add(newStat);
            
        }
    }
    public Stat GetStat(EStatType statToGet)
    {
        string nameToGet = statToGet.ToString();
        foreach (Stat stat in StatList)
        {
            if (stat.GetStatName() ==  nameToGet)
            {
                return stat;
            }
        }
        return null;
    }
}
public enum EStatType { MaxHealth, Health, MaxMana, Mana, intelligence, Speed, Power, Defense , COUNT }
