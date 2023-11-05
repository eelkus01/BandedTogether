using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBlockMovement : MonoBehaviour
{
    public string noteName;
    public float moveSpeed = 1.0f;
    public bool isInTargetZone;
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
}
