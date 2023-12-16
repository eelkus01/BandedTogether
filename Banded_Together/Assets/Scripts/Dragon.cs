using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{

    private Vector3 startSpot;
    public GameObject dragonBallPrefab;
    public string phase = "start";
    public string firePhase = "start";
    public int startHealth = 25;
    public int angryHealth = 12;
    public int weakHealth = 5;
    public int currentHealth = 25;
    
    private bool movingLeft;
    public float speed = 5.0f;
    private bool returning = true;
    public bool alive = true;

    float outOfRangeDistance = 15f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        startSpot = transform.position;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //check if on sreen before shooting fire balls
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < outOfRangeDistance) {
            if(phase == "start") {
                if (movingLeft)
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if(firePhase == "start") {
                    StartCoroutine(SpawnDragonBallsRoutine(false));
                    firePhase = "running"; // Update the phase to prevent the coroutine from being called multiple times
                }
                if(currentHealth <= angryHealth){
                    phase = "angry";
                }
            }
            if(phase == "angry") {
                if(returning) {
                    transform.position = Vector3.MoveTowards(transform.position, startSpot, speed * Time.deltaTime);

                    if(transform.position == startSpot){
                        returning = false;
                    }
                }
                else{
                    if(firePhase == "start") {
                        StartCoroutine(SpawnDragonBallsRoutine(true));
                    firePhase = "running"; // Update the phase to prevent the coroutine from being called multiple times
                    }
                }
            }
        } else {
            Debug.Log("Dragon not on screen");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Switch direction when a collision is detected
        movingLeft = !movingLeft;
    }

    IEnumerator SpawnDragonBallsRoutine(bool homing)
    {
        float startTime = Time.time;

        while (Time.time - startTime < 5)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0); // Z-coordinate set to 0
            GameObject newDragonBall = Instantiate(dragonBallPrefab, spawnPosition, Quaternion.identity);
            newDragonBall.GetComponent<DragonBall>().homing = homing;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(3);

        firePhase = "start"; // Reset the phase to allow the routine to run again
    }

    public void DamageEnemy(int damage) {

        currentHealth -= damage;
        if (currentHealth <= angryHealth) {
            phase = "angry";
        }
        if (currentHealth <= 0){
            DestroySelf();
        }
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spellblast"))
        {
            if(!returning){
                DamageEnemy(2);
            }
            // Vector2 forceDirection = (transform.position - other.transform.position).normalized;
            // int knockback = other.GetComponent<Spellblast>().knockback;
            // ApplyKnockback(forceDirection * knockback);

            //add particle effect before destroying
            other.GetComponent<ImpactParticles>().CreateParticles();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("DrumAttack"))
        {
            DamageEnemy(4);
        }
    }
}
