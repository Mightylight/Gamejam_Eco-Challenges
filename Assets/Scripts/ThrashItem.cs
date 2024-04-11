using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrashItem : MonoBehaviour
{
    [SerializeField] public ItemType itemType;

    [SerializeField] private Rigidbody rb;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ThrashBarrier"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}



public enum ItemType
{
    Plastic1,
    Plastic2,
    Plastic3,
}