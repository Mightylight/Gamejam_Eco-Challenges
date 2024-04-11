using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public string[] allowedTags; // Tags that this trashcan can destroy

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        foreach (string tag in allowedTags)
        {
            if (other.CompareTag(tag))
            {
                Debug.Log("Tag matched: " + tag);
                Destroy(other.gameObject);
                return; // Exit the loop once an item is destroyed
            }
        }
    }
}
