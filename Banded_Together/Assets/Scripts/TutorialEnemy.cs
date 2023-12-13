using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public Animator anim;
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
        if (!alive)
        {
            rb2D.velocity = Vector3.zero;
            return;
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

            //add particle effect before destroying spell blast
            other.GetComponent<ImpactParticles>().CreateParticles();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("DrumAttack"))
        {
            DamageEnemy(2);
        }
        if (other.CompareTag("IceSpike"))
        {
            DamageEnemy(3);
            Destroy(other.gameObject);
        }
    }


    public void DamageEnemy(int damage)
    {
        if (!isFlashing)
        {
            // Trigger the red flash effect.
            rend.material = redFlashMaterial;
            flashTimer = flashDuration;
            isFlashing = true;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameObject.Find("Player").GetComponent<PlayerStateManager>().getHealed(startHealth);
            StartCoroutine(KillEnemy());
        }
    }

    public IEnumerator KillEnemy()
    {
        if (alive)
        {
            anim.Play("Explode");
            alive = false;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
