using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour{
    // NOTE: This script depends on the GameHandler having a public int "thePieces"
    // that is updated with each pickup collected.
    public GameHandler gameHandler;
    public string NextLevel = "MainMenu";
    public GameObject exitClosed;
    public GameObject exitOpen;
    public int piecesCollected;
    public int piecesNeeded = 0;

    void Start(){
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        exitClosed.SetActive(true);
        exitOpen.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Update(){
        piecesCollected = gameHandler.partsGotten;
        piecesNeeded = gameHandler.partsNeeded;
        if (piecesCollected >= piecesNeeded){
            exitClosed.SetActive(false);
            exitOpen.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            SceneManager.LoadScene(NextLevel);
        }
    }
}