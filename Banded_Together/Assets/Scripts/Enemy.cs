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

    public int damage = 5;

    public int knockback = 10;

    public float knockbackDuration = .25f;
    private Rigidbody2D rb2D;


    // handle flashing red when damaged
    public Material redFlashMaterial; // Assign the material with the "RedFlashShader" here.
    public float flashDuration = 0.25f; // Duration of the red flash in seconds.

    private Material originalMaterial;
    private Renderer rend;
    private float flashTimer = 0f;
    private bool isFlashing = false;

    private bool isKnockedBack = false;
    

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
        if (!isKnockedBack)
    {
        Vector2 moveDirection = (target.position - transform.position).normalized;

            // Update the enemy's Rigidbody2D velocity to move toward the target.
        rb2D.velocity = moveDirection * moveSpeed;
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
    // for collisions with attacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spellblast"))
        {
            DamageEnemy(1);

            Vector2 forceDirection = (transform.position - other.transform.position).normalized;
            int knockback = other.GetComponent<Spellblast>().knockback;
            ApplyKnockback(forceDirection * knockback);

            Destroy(other.gameObject);
        }
        if (other.CompareTag("DrumAttack"))
        {
            DamageEnemy(2);
        }
    }

    private void ApplyKnockback(Vector2 force)
    {
        isKnockedBack = true;
        rb2D.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
        rb2D.velocity = Vector2.zero; // Optional: reset velocity after knockback
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assuming the Player object has a script with a GetDamaged method
            collision.gameObject.GetComponent<PlayerStateManager>().getDamaged(damage);

            // Knock back the player
            Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerMoveAround>().getKnockedBack(forceDirection, knockback);

            // Destroy this object
            Destroy(gameObject);
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
            GameObject.Find("Player").GetComponent<PlayerStateManager>().getHealed(startHealth);
            KillEnemy();
        }
    }

    public void KillEnemy(){
        if (alive){
            alive = false;
            Destroy(gameObject);
        }
    }
}
