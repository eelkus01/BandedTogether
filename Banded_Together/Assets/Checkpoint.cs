using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointManager;
    public int checkpointId;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")) {
            checkpointManager.GetComponent<CheckpointManager>().setActiveCheckpoint(checkpointId);
        }
    }
    public void activateCheckpoint(bool targetState){
        // TODO - trigger animation
        
        isActive = targetState;
    }
}
