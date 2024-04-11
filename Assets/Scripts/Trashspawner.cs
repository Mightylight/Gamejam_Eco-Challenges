using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab;
    public float spawnRate = 2f; // Adjust as needed
    public float spawnDistance = 10f; // Distance above the camera
    public float fallSpeed = 5f; // Speed at which the trash falls

    [SerializeField] private GameObject[] spawnPoints;
    

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnTrash()
    {
        // Calculate random x position within the screen width
        //float randomX = Random.Range(0f, 1f);
        //Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 1f, spawnDistance));
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        // Instantiate trash at spawn position
        GameObject trash = Instantiate(trashPrefab, spawnPosition, Quaternion.identity);

        // Make the trash fall
        Rigidbody trashRb = trash.GetComponent<Rigidbody>();
        if (trashRb != null)
        {
            trashRb.velocity = Vector3.down * fallSpeed;
        }
    }
}