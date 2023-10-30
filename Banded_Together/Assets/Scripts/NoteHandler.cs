using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHandler : MonoBehaviour
{
    public string noteName;
    public float moveSpeed = 1.0f;
    public bool isCollidingWithHitBar = false;
    public Vector3 moveDirection = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // This function is called when a collider enters the trigger collider of this object.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider has the "HitBar" tag.
        if (other.CompareTag("HitBar")){
            // The object is colliding with an object tagged as "HitBar."
            isCollidingWithHitBar = true;
            Debug.Log("DETECTED COLLIDING WITH HITBAR");
        }
    }
    // This function is called when a collider exits the trigger collider of this object.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting collider has specific characteristics (e.g., tag, component) if needed.
        if (other.CompareTag("HitBar"))
        {
            // Perform actions when the "Player" object exits the trigger zone.
            // You can put your custom logic here.
            isCollidingWithHitBar = false;
            Debug.Log("EXiTING HITBAR COLLISION");
        }
    }
}
