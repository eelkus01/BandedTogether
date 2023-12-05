using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : MonoBehaviour
{
    public bool homing = false;
    public float moveSpeed = 10f;
    public int damage = 4;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliders = FindObjectsOfType<Collider2D>(); // Find all colliders in the scene
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Dragon") || collider.gameObject.CompareTag("DragonBall"))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true); // Ignore collision with "Dragon"
            }
        }

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // move straight down if not homing
        if(!homing) {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else {
            //move towards player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            //rotate towards player
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerStateManager>().getDamaged(damage);
        }
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


}
