using UnityEngine;
using System.Collections.Generic;

public class TriggerHandler : MonoBehaviour
{
    public List<GameObject> notes; // List of note game objects
    public float hitThreshold = 0.2f; // Adjust this threshold for timing accuracy

    private void Start()
    {
        // Find all game objects with the "Note" tag and add them to the notes list
        GameObject[] noteArray = GameObject.FindGameObjectsWithTag("Note");
        notes.AddRange(noteArray);
    }

    private void Update()
    {
        // Check for input corresponding to each note
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Debug.Log("Detected Keydown");
                GameObject lowestNoteAboveMinus3 = FindLowestNoteAboveY(-3.5f);
                lowestNoteAboveMinus3 = 
                NoteHandler noteScript = lowestNoteAboveMinus3.GetComponent<NoteHandler>();
                // check if input is correct for the note - 1 corresponds to g, 2 to a, 3 to b
                string targetNote = noteScript.noteName;
                if (targetNote == "G" && i == 0 || targetNote == "A" && i == 1 || targetNote == "B" && i == 2){
                    Debug.Log("Detected Good Combination");
                    if (noteScript.isCollidingWithHitBar) {
                        Debug.Log("Trying to set inactive!");
                        lowestNoteAboveMinus3.SetActive(false);
                        
                    }
                    else {
                        Debug.Log("Not Colliding Though!");
                    }
                }
            }
        }
    }

    private GameObject FindLowestNoteAboveY(float minY)
    {
        GameObject lowestNote = null;
        float lowestY = float.MaxValue;

        foreach (var note in notes)
        {
            if (note != null && note.activeSelf)
            {
                float noteY = note.transform.position.y;
                if (noteY < lowestY && noteY > minY)
                {
                    lowestY = noteY;
                    lowestNote = note;
                }
            }
        }

        return lowestNote;
    }
}