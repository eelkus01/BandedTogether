using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your Enemy prefab in the Inspector
    public float spawnInterval = 3f; // Time interval in seconds for spawning
    public float activationDistance = 10f; // Distance from player under which will start spawning enemies.
    private bool isActive = false; // Will only spawn enemy instances when active.
    private GameObject inactiveArt;
    private GameObject activeArt;

    private void Start()
    {
        StartCoroutine(SpawnEnemyAtIntervals(spawnInterval));
    }

    void Update()
    {

        inactiveArt = transform.Find("InactiveArt").gameObject;
        activeArt   = transform.Find("ActiveArt").gameObject;
        if (inactiveArt == null || activeArt == null)
        {
            Debug.LogError("InactiveArt or ActiveArt not found!");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            isActive = distance <= activationDistance;
            if(isActive){
                activeArt.SetActive(true);
                inactiveArt.SetActive(false);
            }
            else{
                activeArt.SetActive(false);
                inactiveArt.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Player not found!");
            isActive = false;
        }
    }

    private IEnumerator SpawnEnemyAtIntervals(float interval)
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        while (true) // Infinite loop to keep spawning enemies
        {
            yield return new WaitForSeconds(interval); // Wait for the specified interval
            if(isActive){
                Instantiate(enemyPrefab, transform.position, transform.rotation); // Spawn the enemy at the current position
            }
        }
    }
}
