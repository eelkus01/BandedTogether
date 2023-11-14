using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your Enemy prefab in the Inspector
    public float spawnInterval = 3f; // Time interval in seconds for spawning

    private void Start()
    {
        StartCoroutine(SpawnEnemyAtIntervals(spawnInterval));
    }

    private IEnumerator SpawnEnemyAtIntervals(float interval)
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        while (true) // Infinite loop to keep spawning enemies
        {
            yield return new WaitForSeconds(interval); // Wait for the specified interval
            Instantiate(enemyPrefab, transform.position, transform.rotation); // Spawn the enemy at the current position
        }
    }
}
