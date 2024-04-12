using System;
using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab;
    public float spawnRate = 2f; // Adjust as needed
    public float fallSpeed = 5f; // Speed at which the trash falls
    public float floatDuration = 1f; // Duration for the trash to float up
    public LayerMask waterLayer; // Layer mask for the water plane

    [SerializeField] private GameObject[] spawnPoints;

    private float nextSpawnTime = 0;


    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + UnityEngine.Random.Range(0f, 2f) / spawnRate;
        }
    }

    void SpawnTrash()
    {
        // Calculate random x position within the screen width
        float randomX = UnityEngine.Random.Range(0.1f, 0.9f);
        Vector3 spawnPosition = new Vector3(
            randomX * Screen.width, 550f, 30f
        );

        // Convert screen coordinates to world coordinates
        spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

        // Instantiate trash at spawn position
        GameObject trash = Instantiate(trashPrefab, spawnPosition, Quaternion.identity);

        // Make the trash fall
        Rigidbody trashRb = trash.GetComponent<Rigidbody>();
        if (trashRb != null)
        {
            trashRb.velocity = Vector3.down * fallSpeed;

            // Start coroutine to float the trash up for a duration
            StartCoroutine(FloatTrashUp(trash));
        }
    }

    IEnumerator FloatTrashUp(GameObject trashObject)
    {
        Vector3 initialPosition = trashObject.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * 7.5f; // Move the object up by 10 units
        float elapsedTime = 0f;
        while (elapsedTime < floatDuration)
        {
            // Interpolate between initial and target positions over time
            trashObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / floatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }   
    }
}
