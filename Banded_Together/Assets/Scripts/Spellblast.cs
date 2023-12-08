using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spellblast : MonoBehaviour
{
    public Transform target;          // The target to follow (usually the closest enemy).
    public float moveSpeed = 3f;     // The speed at which the enemy follows the target.

    public int knockback = 15;

    private Rigidbody2D rb2D;        // Reference to the enemy's Rigidbody2D component.

    //sound for spell blast
    private AudioSource source;

    private Animator anim;

    private void Start()
    {
        Destroy(gameObject, 2f);

        anim = GetComponent<Animator>();
        anim.SetTrigger("StartFireAnimation");

        rb2D = GetComponent<Rigidbody2D>();
        source = GetComponentInChildren<AudioSource>();

        // Find all GameObjects with the tag "Enemy or Dragon"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("Dragon")).ToArray();

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

        //play sound of blast
        source.GetComponent<RandomSound>().PlaySound();
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

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            float orientationAdjustment = -90f; // Adjust this value as needed for your specific sprite orientation
            angle -= orientationAdjustment;

            // Set the rotation of the spellblast
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
        {
            // If the target is null (e.g., no enemies with the "Enemy" tag exist), stop moving.
            rb2D.velocity = Vector2.zero;
        }
        
    }
}
