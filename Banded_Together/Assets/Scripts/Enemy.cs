using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;          // The target to follow (usually the player).
    public float moveSpeed = 3f;     // The speed at which the enemy follows the target.

    public int startHealth = 5;

    public bool alive = true;

    public int currentHealth;
    private Rigidbody2D rb2D;


    // handle flashing red when damaged
    public Material redFlashMaterial; // Assign the material with the "RedFlashShader" here.
    public float flashDuration = 0.25f; // Duration of the red flash in seconds.

    private Material originalMaterial;
    private Renderer rend;
    private float flashTimer = 0f;
    private bool isFlashing = false;
    

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalMaterial = rend.material;

        currentHealth = startHealth;

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

        // Check if the enemy is currently flashing.
        if (isFlashing)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0f)
            {
                // Stop flashing and restore the original material.
                rend.material = originalMaterial;
                isFlashing = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spellblast"))
        {
            DamageEnemy(1);
            Destroy(other.gameObject);
        }
    }

    public void DamageEnemy(int damage) {
        if (!isFlashing)
        {
            // Trigger the red flash effect.
            rend.material = redFlashMaterial;
            flashTimer = flashDuration;
            isFlashing = true;
        }
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillEnemy();
        }
    }

    public void KillEnemy(){
        if (alive){
            alive = false;
            gameObject.SetActive(false);
        }
    }
}
