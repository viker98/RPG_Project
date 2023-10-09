using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stat
{
    [SerializeField] private string StatName;
    [SerializeField] private int BaseValue;
    [SerializeField] private List<int> modifiers = new List<int>();

    
    public int GetBaseValue() { return BaseValue; }
    public void SetBaseValue(int value) { BaseValue = value; }
    public string GetStatName() { return StatName; }
    public void SetStatName(string nameToSet) { StatName = nameToSet; }
    public int GetValue()
    {
        int totalValue = BaseValue;
        modifiers.ForEach(x  => totalValue += x);
        return totalValue;
    }
    
    public void AddModifier(int modifier)
    {
        if(modifier != 0)
            modifiers.Add(modifier);
    }
    public void RemoveModifier(int modifier)
    {
        if(modifier != 0)
            modifiers.Remove(modifier);
    }
}
