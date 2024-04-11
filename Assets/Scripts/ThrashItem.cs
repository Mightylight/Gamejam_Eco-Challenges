using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrashItem : MonoBehaviour
{
    [SerializeField] public ItemType itemType;
}



public enum ItemType
{
    Plastic1,
    Plastic2,
    Plastic3,
}