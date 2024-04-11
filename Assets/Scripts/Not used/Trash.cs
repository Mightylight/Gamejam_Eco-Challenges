using UnityEngine;

public class Trash : MonoBehaviour
{
    public float shorePosition = 0f;
    public float movementSpeed = 1f; // Adjust this value to control the speed of movement towards shore

    private bool movingTowardsShore = false;

    void Update()
    {
        if (movingTowardsShore)
        {
            MoveTowardsShore();
        }
    }

    public void StartMovingTowardsShore()
    {
        movingTowardsShore = true;
    }

    private void MoveTowardsShore()
    {
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);

        // Check if trash has reached shore
        if (transform.position.y <= shorePosition)
        {
            Destroy(gameObject);
            // You can add any other action here, like decreasing score or triggering an effect.
        }
    }

    public void ApplyOceanFlow(Vector3 oceanFlow)
    {
        transform.position += oceanFlow;
    }
}