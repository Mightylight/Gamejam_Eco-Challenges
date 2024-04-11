using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovementtemp : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Move forward and backward
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);

        // Move sideways
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }
}
