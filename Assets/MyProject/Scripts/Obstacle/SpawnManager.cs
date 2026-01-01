using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnPosX = 3.0f;
    public float minSpawnPosZ = 1.0f;
    public float maxSpawnPosZ = 3.5f;

    public float startDelay = 1.0f;
    public float spawnInterval = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // Test function
        //if(Input.GetKeyDown(KeyCode.S))
        //    SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), 
                                        0, 
                                        Random.Range(minSpawnPosZ, maxSpawnPosZ));
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }    
}
