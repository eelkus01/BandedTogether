using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    public GameObject spellblastPrefab; // Reference to the Spellblast prefab.
    public float cooldownDuration = 0.5f; // Cooldown duration in seconds.

    private float lastSpawnTime; // Time when the last "Spellblast" was spawned.

    private void Start()
    {
        lastSpawnTime = -cooldownDuration; // Initialize lastSpawnTime to ensure the first spawn is allowed immediately.
    }

    private void Update()
    {
        // Check if the Space key is pressed and if enough time has passed since the last spawn.
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSpawnTime >= cooldownDuration)
        {
            // Spawn the Spellblast prefab at the current position of this object.
            Instantiate(spellblastPrefab, transform.position, Quaternion.identity);

            // Update the lastSpawnTime.
            lastSpawnTime = Time.time;
        }
    }
}
