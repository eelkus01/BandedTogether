using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBlockSpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public float AudioTotalTime;
    public int AudioNumOfBeats;

    public GameObject rhythmBlockPrefab;

    private float beatInterval;
    private float distanceToTarget;
    public Transform spawnPoint; // Assign in Unity Inspector
    public Transform targetBlock; // Assign in Unity Inspector

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        AudioTotalTime = audioSource.clip.length;
        beatInterval = AudioTotalTime / AudioNumOfBeats;
        distanceToTarget = Vector3.Distance(spawnPoint.position, targetBlock.position);
        Debug.Log("Distance to target: "+distanceToTarget);
        speed = distanceToTarget / beatInterval;

        StartCoroutine(SpawnSpriteOnBeat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnSpriteOnBeat()
    {
        while (audioSource.isPlaying)
        {
            // Spawn a new sprite at the spawn point
            GameObject sprite = Instantiate(rhythmBlockPrefab, spawnPoint.position, Quaternion.identity);
            RhythmBlockMovement blockmover = sprite.GetComponent<RhythmBlockMovement>();
            blockmover.moveSpeed = speed;

            // Wait for one beat interval before spawning the next sprite
            yield return new WaitForSeconds(beatInterval);
        }
    }

    IEnumerator MoveSpriteToTarget(Transform spriteTransform)
    {
        // Calculate the time it takes to reach the target block (4 beats)
        float travelTime = beatInterval * 4;
        float startTime = Time.time;
        Vector3 startPosition = spriteTransform.position;
        Vector3 endPosition = targetBlock.position;

        // While there is still time left to travel, keep moving the sprite
        while (Time.time - startTime < travelTime)
        {
            // Calculate the interpolation factor
            float t = (Time.time - startTime) / travelTime;

            // Move the sprite towards the target block
            spriteTransform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Wait until the next frame
            yield return null;
        }

        // Ensure the sprite is exactly at the target's position after 4 beats
        spriteTransform.position = targetBlock.position;
    }
}
