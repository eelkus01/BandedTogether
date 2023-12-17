using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrognome : MonoBehaviour
{
    public float interactiveDistance = 5f;
    private GameObject player;
    private SpriteRenderer interactArt;
    private bool playerInRange = false;
    private bool talkCanvasVisible = false;
    private GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        talkUI = GameObject.Find("TalkCanvas");
        talkUI.SetActive(false);

        // Assuming the sprite is the first child, adjust as needed
        interactArt = transform.GetChild(0).GetComponent<SpriteRenderer>();

        // Initially hide the sprite
        interactArt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is assigned
        if (player != null)
        {
            // Calculate the distance to the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Show or hide the sprite based on the distance
            if(distance < interactiveDistance){
                interactArt.enabled = true;
                playerInRange = true;
            }
            else{
                interactArt.enabled = false;
                playerInRange = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !talkCanvasVisible)
        {
            talkCanvasVisible = true;
            talkUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && talkCanvasVisible)
        {
            talkCanvasVisible = false;
            talkUI.SetActive(false);
        }


    }
}
