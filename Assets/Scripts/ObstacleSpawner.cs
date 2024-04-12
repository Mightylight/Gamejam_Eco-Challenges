using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnInterval = 2f; // Adjust this to change the interval between spawns
    [SerializeField] private float cubeLifetime = 5f; // Adjust this to change the lifetime of each cube


    [SerializeField] private Renderer spawningArea;
    
    private void Start()
    {
        // Start the coroutine for spawning cubes
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            // Generate a random spawn position within a range
            // Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));
            Vector3 randomSpawnPosition = new Vector3(Random.Range(spawningArea.bounds.min.x, spawningArea.bounds.max.x), 5, Random.Range(spawningArea.bounds.min.z, spawningArea.bounds.max.z));
            // Instantiate a cube at the random position
            GameObject cube = Instantiate(obstacles[Random.Range(0,obstacles.Length)], randomSpawnPosition, Quaternion.identity);

            // Destroy the cube after its lifetime expires
            Destroy(cube, cubeLifetime);

            // Wait for the specified interval before spawning the next cube
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
