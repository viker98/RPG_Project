using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> PartyList = new List<GameObject>();

    private void Awake()
    {
        GatherParty();
    }
    public void GatherParty()
    {
        //gather party on creation
    }
    public List<GameObject> GetPartyList() { return PartyList; }
    public void AddPartyMember(UnitCharacter partyMember)
    {
        if (partyMember) { PartyList.Add(partyMember.gameObject); }
    }
    public void RemovePartyMember(UnitCharacter partyMember)
    {
        if (partyMember) { PartyList.Remove(partyMember.gameObject); }

    }
}
