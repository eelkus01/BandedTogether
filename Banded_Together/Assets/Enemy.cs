using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;          // The target to follow (usually the player).
    public float moveSpeed = 3f;     // The speed at which the enemy follows the target.

    private Rigidbody2D rb2D;        // Reference to the enemy's Rigidbody2D component.

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Find the player GameObject by name and set its transform as the target.
        target = GameObject.Find("Player").transform;

        // Check if the player GameObject was found.
        if (target == null)
        {
            Debug.LogError("Player GameObject not found. Make sure it is named 'Player' in the Hierarchy.");
        }
    }

    private void Update()
    {
        // Check if the target exists.
        if (target != null)
        {
            // Calculate the direction from the enemy to the target.
            Vector2 moveDirection = (target.position - transform.position).normalized;

            // Update the enemy's Rigidbody2D velocity to move toward the target.
            rb2D.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // If the target is null (e.g., player is dead), stop moving.
            rb2D.velocity = Vector2.zero;
        }
    }
}
