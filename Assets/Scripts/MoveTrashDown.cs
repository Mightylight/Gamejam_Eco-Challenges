using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    public float descentSpeed = 2000f; // Speed at which the object descends

    private bool isDescending = false;
    [SerializeField] private Rigidbody rb;

    void Update()
    {
        if (isDescending)
        {

            // Move the object downwards
            //transform.Translate(-Vector3.forward * descentSpeed * Time.deltaTime);
            if(rb == null) return;
            rb.AddForce(-Vector3.forward * descentSpeed);
            Debug.Log("look ma no hands");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I Touch Water");
        // Check if the collider belongs to the water
        if (other.CompareTag("Water"))
        {
            // Start descending
            isDescending = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isDescending = false;
    }
}
