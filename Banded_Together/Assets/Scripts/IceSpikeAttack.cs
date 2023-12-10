using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeAttack : MonoBehaviour
{
    public GameObject spikePrefab; // Assign your prefab in the Inspector
    public float speed = 5f; // Speed of the prefab instances

    public float baseAngle = 0f; // face right by default, but this should be edited by PlayerCast when instantiated to match the player's direction

    void Start()
    {
        // Current direction
        LaunchPrefab(baseAngle, speed);

        // 30 degrees clockwise
        LaunchPrefab(baseAngle-15f, speed);

        // 30 degrees counterclockwise
        LaunchPrefab(baseAngle+15f, speed);
    }

    public void LaunchPrefab(float degrees, float speed)
{
    // Convert degrees to radians
    float radians = degrees * Mathf.Deg2Rad;

    // Calculate direction vector
    Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

    // Create a rotation Quaternion from the Z-axis rotation
    Quaternion rotation = Quaternion.Euler(0f, 0f, degrees);

    // Instantiate the prefab with the specified rotation
    GameObject instance = Instantiate(spikePrefab, transform.position, rotation);

    // Calculate velocity
    Vector2 velocity = direction * speed;

    // Apply the velocity to the instantiated object's Rigidbody2D
    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.velocity = velocity;
    }
    else
    {
        Debug.LogError("Rigidbody2D not found on the instantiated prefab!");
    }
}
}
