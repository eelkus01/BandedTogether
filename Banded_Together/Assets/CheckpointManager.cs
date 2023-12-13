using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public int activeCheckpointId = -1;
    GameObject[] checkpoints;
    Transform[] checkpointTransforms;
    // Start is called before the first frame update
     void Start()
    {
        // Find all objects with the "Checkpoint" tag
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        // Create an array for their transforms
        checkpointTransforms = new Transform[checkpoints.Length];

        // Populate the transforms array and assign unique IDs
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpointTransforms[i] = checkpoints[i].transform;

            // Assuming each checkpoint object has a Checkpoint.cs script attached with a 'checkpointId' field
            Checkpoint checkpointScript = checkpoints[i].GetComponent<Checkpoint>();
            if (checkpointScript != null)
            {
                Debug.Log("Setting checkpointId to "+i);
                checkpointScript.checkpointId = i; // Assigning unique ID (0-indexed)
            }
            // Debug.Log("Id: "+i+" - Transform: "+ TransformToDebugString(checkpoints[i].transform));
        }

        // Optionally, you can now use the checkpointTransforms array for other purposes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActiveCheckpoint(int checkpointId) {
        if (checkpointId != activeCheckpointId) {
            // if some real checkpoint is active, deactivate it
            if(activeCheckpointId != -1){
                checkpoints[activeCheckpointId].GetComponent<Checkpoint>().activateCheckpoint(false);
            }

            // activate the new checkpoint
            activeCheckpointId = checkpointId;
            checkpoints[activeCheckpointId].GetComponent<Checkpoint>().activateCheckpoint(true);
        }
    }

    public Transform getRespawnPoint() {
        return checkpointTransforms[activeCheckpointId];
    }



    public static string TransformToDebugString(Transform transform)
    {
        return $"Position: {transform.position}, Rotation: {transform.rotation}, Scale: {transform.localScale}";
    }
}
