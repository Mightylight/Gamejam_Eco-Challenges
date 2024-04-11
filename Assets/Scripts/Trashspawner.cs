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
        
        //pick a random spawn point between 0 and 1 of the spawnpoints array
        Vector3 spawnpoint1 = spawnPoints[0].transform.position;
        Vector3 spawnpoint2 = spawnPoints[1].transform.position;
        
        Vector3 spawnPosition = Vector3.Lerp(spawnpoint1, spawnpoint2, Random.Range(0f, 1f));
        
        
        //Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

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