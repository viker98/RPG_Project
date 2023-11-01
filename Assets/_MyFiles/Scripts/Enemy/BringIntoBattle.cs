using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringIntoBattle : MonoBehaviour
{
    [SerializeField] private List<GameObject> PartnerList = new List<GameObject>();
    public List<GameObject> GetPartnerList() { return PartnerList; }
}

