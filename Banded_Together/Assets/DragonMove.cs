using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : MonoBehaviour
{

    private Vector3 startSpot;
    public string phase = "start";
    public int startHealth = 25;
    public int angryHealth = 12;
    public int weakHealth = 5;
    public int currentHealth = 25;
    
    private bool movingLeft;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        startSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(phase == "start") {
            if (movingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if(currentHealth <= angryHealth){
            phase = "angry";
        }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Switch direction when a collision is detected
        movingLeft = !movingLeft;
    }
}
