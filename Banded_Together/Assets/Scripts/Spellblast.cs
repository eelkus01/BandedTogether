using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellblast : MonoBehaviour
{
    public Transform target;          // The target to follow (usually the closest enemy).
    public float moveSpeed = 3f;     // The speed at which the enemy follows the target.

    private Rigidbody2D rb2D;        // Reference to the enemy's Rigidbody2D component.

    private void Start()
    {
        Destroy(gameObject, 2f);

        rb2D = GetComponent<Rigidbody2D>();

        // Find all GameObjects with the tag "Enemy."
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Initialize variables to keep track of the closest enemy and its distance.
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through each enemy to find the closest one.
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the enemy and the current GameObject.
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            // If this enemy is closer than the previously closest one, update the variables.
            if (distance < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distance;
            }
        }

        // Set the closest enemy's transform as the target.
        target = closestEnemy;
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
            // If the target is null (e.g., no enemies with the "Enemy" tag exist), stop moving.
            rb2D.velocity = Vector2.zero;
        }
    }
}
